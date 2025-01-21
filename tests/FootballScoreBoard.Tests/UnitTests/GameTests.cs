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

    [Fact]
    public void Game_ShouldStart_WhenNotStartedAlready()
    {
        // Arrange
        var game = new Game("Mexico", "Canada");

        // Act
        game.Start();

        // Assert
        Assert.NotNull(game.StartTime);
    }

    [Fact]
    public void Game_ShouldThrowInvalidOperationException_WhenAlreadyStarted()
    {
        // Arrange
        var game = new Game("Mexico", "Canada");

        // Act
        game.Start();
        
        var exception = Assert.Throws<InvalidOperationException>(() => game.Start());
        Assert.Equal("The game has already started", exception.Message);
    }

    [Fact]
    public void Game_ShouldUpdateScore_WhenGameIsStarted()
    {
        // Arrange
        var game = new Game("Mexico", "Canada");
        game.Start();

        // Act
        game.UpdateScore(2, 1);

        // Assert
        Assert.Equal(2, game.HomeScore);
        Assert.Equal(1, game.AwayScore);
    }

    [Fact]
    public void Game_ShouldThrowInvalidOperationException_WhenGameIsNotStarted()
    {
        // Arrange
        var game = new Game("Mexico", "Canada");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => game.UpdateScore(2, 1));
        Assert.Equal("The game must have started to update the result", exception.Message);
    }

    [Fact]
    public void Game_ShouldThrowArgumentException_WhenScoresAreNegative()
    {
        // Arrange
        var game = new Game("Mexico", "Canada");
        game.Start();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => game.UpdateScore(-1, 2));
        Assert.Equal("Scores cannot be negative", exception.Message);
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
