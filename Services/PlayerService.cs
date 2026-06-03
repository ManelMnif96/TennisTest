using System.Text.Json;
using AtelierTest.Models;
using AtelierTest.DTOs;
using AtelierTest.DTOs;

namespace AtelierTest.Services;

public class PlayerService
{
    private readonly List<Player> _players;

    public PlayerService()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "headtohead.json");

        var json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<PlayersRoot>(json, options);

        _players = root?.Players ?? new List<Player>();
    }

    public List<Player> GetAll()
    {
        return _players.OrderBy(p => p.Data.Rank).ToList();
    }

    public Player? GetById(int id)
    {
        return _players.FirstOrDefault(p => p.Id == id);
    }

    public StatisticsDto GetStatistics()
    {
        // 1. Best country (ratio wins)
        var bestCountry = _players
            .GroupBy(p => p.Country.Code)
            .Select(g =>
            {
                var wins = g.Sum(x => x.Data.Last.Count(v => v == 1));
                var total = g.Sum(x => x.Data.Last.Count);

                return new
                {
                    Country = g.Key,
                    Ratio = (double)wins / total
                };
            })
            .OrderByDescending(x => x.Ratio)
            .First().Country;

        // 2. BMI average
        var bmis = _players.Select(p =>
        {
            var weightKg = p.Data.Weight / 1000.0;
            var heightM = p.Data.Height / 100.0;
            return weightKg / (heightM * heightM);
        });

        var avgBmi = bmis.Average();

        // 3. Median height
        var heights = _players
            .Select(p => p.Data.Height)
            .OrderBy(x => x)
            .ToList();

        double median;
        int count = heights.Count;

        if (count % 2 == 0)
        {
            median = (heights[count / 2 - 1] + heights[count / 2]) / 2.0;
        }
        else
        {
            median = heights[count / 2];
        }

        return new StatisticsDto
        {
            BestCountry = bestCountry,
            AverageBMI = avgBmi,
            MedianHeight = median
        };
    }

    public Player AddPlayer(CreatePlayerDto dto)
    {
        var newPlayer = new Player
        {
            Id = _players.Max(p => p.Id) + 1,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Shortname = dto.Shortname,
            Sex = dto.Sex,
            Picture = dto.Picture,

            Country = new Country
            {
                Code = dto.CountryCode,
                Picture = dto.CountryPicture
            },

            Data = new PlayerData
            {
                Rank = dto.Rank,
                Points = dto.Points,
                Weight = dto.Weight,
                Height = dto.Height,
                Age = dto.Age,
                Last = new List<int>()
            }
        };

        _players.Add(newPlayer);

        return newPlayer;
    }
}