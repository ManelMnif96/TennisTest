using AtelierTest.Services;
using AtelierTest.DTOs;

namespace AtelierTest.Tests;

public class PlayerServiceTests
{
    [Fact]
    public void GetAll_Should_ReturnPlayersSortedByRank()
    {
        // Arrange
        var service = new PlayerService();

        // Act
        var players = service.GetAll();

        // Assert
        for (int i = 0; i < players.Count - 1; i++)
        {
            Assert.True(
                players[i].Data.Rank <= players[i + 1].Data.Rank
            );
        }
    }

    [Fact]
    public void GetById_Should_ReturnPlayer_WhenPlayerExists()
    {
        // Arrange
        var service = new PlayerService();

        var existingPlayer = service.GetAll().First();

        // Act
        var result = service.GetById(existingPlayer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingPlayer.Id, result!.Id);
    }

    [Fact]
    public void GetById_Should_ReturnNull_WhenPlayerDoesNotExist()
    {
        // Arrange
        var service = new PlayerService();

        // Act
        var result = service.GetById(-999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AddPlayer_Should_AddPlayerSuccessfully()
    {
        // Arrange
        var service = new PlayerService();

        var dto = new CreatePlayerDto
        {
            Firstname = "Ons",
            Lastname = "Jabeur",
            Shortname = "O.JAB",
            Sex = "F",
            CountryCode = "TUN",
            CountryPicture = "tun.png",
            Picture = "ons.png",
            Rank = 5,
            Points = 5000,
            Weight = 66000,
            Height = 167,
            Age = 30
        };

        var countBefore = service.GetAll().Count;

        // Act
        var created = service.AddPlayer(dto);

        var countAfter = service.GetAll().Count;

        // Assert
        Assert.Equal(countBefore + 1, countAfter);

        Assert.Equal("Ons", created.Firstname);
        Assert.Equal("Jabeur", created.Lastname);
    }

    [Fact]
    public void GetStatistics_Should_ReturnValidStatistics()
    {
        // Arrange
        var service = new PlayerService();

        // Act
        var stats = service.GetStatistics();

        // Assert
        Assert.NotNull(stats);

        Assert.False(string.IsNullOrWhiteSpace(stats.BestCountry));

        Assert.True(stats.AverageBMI > 0);

        Assert.True(stats.MedianHeight > 0);
    }
}