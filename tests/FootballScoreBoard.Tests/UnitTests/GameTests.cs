namespace FootballScoreBoard.Tests.UnitTests;

public class GameTests
{
    [Fact]
    public void Game_ShouldInitialize_WithCorrectDefaultValues()
    {
        // Act
        var game = new Game("Mexico", "Canada");

        // Assert
        Assert.Equal("Mexico", game.HomeTeam);
        Assert.Equal("Canada", game.AwayTeam);
        Assert.Equal("Mexico|Canada", game.Id);
        Assert.Equal(0, game.HomeScore);
        Assert.Equal(0, game.AwayScore);
        Assert.Null(game.StartTime);
    }

    [Theory]
    [MemberData(nameof(InvalidTeamNames))]
    public void Game_ShouldThrowArgumentException_WhenHomeTeamOrAwayTeamIsInvalid(string homeTeam, string awayTeam)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Game(homeTeam, awayTeam));
        Assert.Equal("Team names cannot be empty", exception.Message);
    }

    public static IEnumerable<object[]> InvalidTeamNames()
    {
        string[] invalidTeamNames = [null, string.Empty, " ", "  ", "\n", "\t"];

        foreach (var invalidTeamName in invalidTeamNames)
        {
            yield return new object[] { "Mexico", invalidTeamName };
            yield return new object[] { invalidTeamName, "Canada" };
        }
    }
}
