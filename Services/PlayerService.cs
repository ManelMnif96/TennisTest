using System.Text.Json;
using AtelierTest.Models;
using AtelierTest.DTOs;
using AtelierTest.Exceptions;

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
        if (_players == null || !_players.Any())
            throw new EmptyPlayersException();

        return _players.OrderBy(p => p.Data.Rank).ToList();
    }

    public Player? GetById(int id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);

        if (player == null)
            throw new PlayerNotFoundException(id);

        return player;
    }

    public StatisticsDto GetStatistics()
    {
        if (_players == null || !_players.Any())
            throw new EmptyPlayersException();

        // Best country
        var bestCountry = _players
            .GroupBy(p => p.Country.Code)
            .Select(g =>
            {
                var wins = g.Sum(x => x.Data.Last.Count(v => v == 1));
                var total = g.Sum(x => x.Data.Last.Count);

                return new
                {
                    Country = g.Key,
                    Ratio = total == 0 ? 0 : (double)wins / total
                };
            })
            .OrderByDescending(x => x.Ratio)
            .First().Country;

        // BMI
        var bmis = _players.Select(p =>
        {
            var weightKg = p.Data.Weight / 1000.0;
            var heightM = p.Data.Height / 100.0;
            return weightKg / (heightM * heightM);
        });

        var avgBmi = bmis.Average();

        // Median
        var heights = _players.Select(p => p.Data.Height).OrderBy(x => x).ToList();

        double median;
        int count = heights.Count;

        if (count % 2 == 0)
            median = (heights[count / 2 - 1] + heights[count / 2]) / 2.0;
        else
            median = heights[count / 2];

        return new StatisticsDto
        {
            BestCountry = bestCountry,
            AverageBMI = avgBmi,
            MedianHeight = median
        };
    }

    public Player AddPlayer(CreatePlayerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Firstname))
            throw new InvalidPlayerDataException("Firstname is required.");

        if (string.IsNullOrWhiteSpace(dto.Lastname))
            throw new InvalidPlayerDataException("Lastname is required.");

        if (dto.Age <= 0)
            throw new InvalidPlayerDataException("Age must be greater than 0.");

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