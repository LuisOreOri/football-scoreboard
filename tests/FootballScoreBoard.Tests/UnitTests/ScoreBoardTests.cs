using Moq;

namespace FootballScoreBoard.Tests.UnitTests;

public class ScoreBoardTests
{
    private static Mock<IGame> CreateMockGame(string homeTeam, string awayTeam, int homeScore = 0, int awayScore = 0, DateTime? startTime = null)
    {
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns($"{homeTeam}|{awayTeam}");
        mockGame.Setup(g => g.HomeTeam).Returns(homeTeam);
        mockGame.Setup(g => g.AwayTeam).Returns(awayTeam);
        mockGame.Setup(g => g.HomeScore).Returns(homeScore);
        mockGame.Setup(g => g.AwayScore).Returns(awayScore);
        mockGame.Setup(g => g.StartTime).Returns(startTime ?? DateTime.UtcNow);

        return mockGame;
    }

    [Fact]
    public void StartGame_ShouldAddGame_WhenGameNotExists()
    {
        // Arrange
        var mockGame = CreateMockGame("Mexico", "Canada");
        var scoreboard = new ScoreBoard();

        // Act
        scoreboard.StartGame(mockGame.Object);

        // Assert
        var games = scoreboard.GetSummary();
        Assert.Single(games);
        Assert.Equal("Mexico|Canada", games[0].Id);
    }

    [Fact]
    public void StartGame_ShouldThrowInvalidOperationException_WhenGameAlreadyExists()
    {
        // Arrange
        var mockGame = CreateMockGame("Mexico", "Canada");
        var scoreboard = new ScoreBoard();
        scoreboard.StartGame(mockGame.Object);

        // Act 
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.StartGame(mockGame.Object));

        // Assert
        Assert.Equal("Game already exists.", exception.Message);
    }

    [Fact]
    public void FinishGame_ShouldRemoveGame_WhenGameExists()
    {
        // Arrange
        var mockGame = CreateMockGame("Mexico", "Canada");
        var scoreboard = new ScoreBoard();
        scoreboard.StartGame(mockGame.Object);

        // Act
        scoreboard.FinishGame(mockGame.Object);

        // Assert
        var games = scoreboard.GetSummary();
        Assert.Empty(games);
    }

    [Fact]
    public void FinishGame_ShouldThrowInvalidOperationException_WhenGameNotExists()
    {
        // Arrange
        var mockGame = CreateMockGame("Mexico", "Canada");
        var scoreboard = new ScoreBoard();

        // Act - Finish a game not added to the board
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.FinishGame(mockGame.Object));

        // Assert
        Assert.Equal("Game not found.", exception.Message);
    }

    [Fact]
    public void UpdateScore_ShouldChangeScore_WhenGameExists()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var game = new Game("Mexico", "Canada");
        scoreboard.StartGame(game);

        // Act
        scoreboard.UpdateScore(game, 3, 1);

        // Assert
        var games = scoreboard.GetSummary();
        Assert.Equal(3, games[0].HomeScore);
        Assert.Equal(1, games[0].AwayScore);
    }

    [Fact]
    public void UpdateScore_ShouldThrowInvalidOperationException_WhenGameNotExists()
    {
        // Arrange
        var mockGame = CreateMockGame("Mexico", "Canada");
        var scoreboard = new ScoreBoard();

        // Act - Update a game not added to the board
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.UpdateScore(mockGame.Object, 3, 1));

        // Assert
        Assert.Equal("Game not found.", exception.Message);
    }

    [Fact]
    public void GetSummary_ShouldReturnEmpty_WhenNoGamesAdded()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Act
        var result = scoreboard.GetSummary();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetSummary_ShouldSortByScores_WhenGamesHaveDifferentScores()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var mockGame1 = CreateMockGame("Argentina", "Australia", 3, 1, DateTime.UtcNow.AddMinutes(-10));
        var mockGame2 = CreateMockGame("Mexico", "Canada", 2, 3, DateTime.UtcNow.AddMinutes(-5));
        var mockGame3 = CreateMockGame("Brazil", "France", 5, 5, DateTime.UtcNow);

        scoreboard.StartGame(mockGame1.Object);
        scoreboard.StartGame(mockGame2.Object);
        scoreboard.StartGame(mockGame3.Object);

        // Act
        var result = scoreboard.GetSummary();

        // Assert
        Assert.Equal(mockGame3.Object, result[0]);
        Assert.Equal(mockGame2.Object, result[1]);
        Assert.Equal(mockGame1.Object, result[2]);
    }

    [Fact]
    public void GetSummary_ShouldSortByStartTime_WhenScoresAreEqual()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Create mock games with equal scores but different start times
        var mockGame1 = CreateMockGame("Argentina", "Australia", 3, 3, DateTime.UtcNow.AddMinutes(-10));
        var mockGame2 = CreateMockGame("Mexico", "Canada", 3, 3, DateTime.UtcNow.AddMinutes(-5));

        scoreboard.StartGame(mockGame1.Object);
        scoreboard.StartGame(mockGame2.Object);

        // Act
        var result = scoreboard.GetSummary();

        // Assert: The game with the earlier start time should come first
        Assert.Equal(mockGame2.Object, result[0]);
        Assert.Equal(mockGame1.Object, result[1]);
    }

    [Fact]
    public void GetSummary_ShouldSortByScoresAndStartTime()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var mockGame1 = CreateMockGame("Mexico", "Canada", 0, 5, DateTime.UtcNow.AddMinutes(-5));
        var mockGame2 = CreateMockGame("Spain", "Brazil", 10, 2, DateTime.UtcNow.AddMinutes(-4));
        var mockGame3 = CreateMockGame("Germany", "France", 2, 2, DateTime.UtcNow.AddMinutes(-3));
        var mockGame4 = CreateMockGame("Uruguay", "Italy", 6, 6, DateTime.UtcNow.AddMinutes(-2));
        var mockGame5 = CreateMockGame("Argentina", "Australia", 3, 1, DateTime.UtcNow.AddMinutes(-1));

        scoreboard.StartGame(mockGame1.Object);
        scoreboard.StartGame(mockGame2.Object);
        scoreboard.StartGame(mockGame3.Object);
        scoreboard.StartGame(mockGame4.Object);
        scoreboard.StartGame(mockGame5.Object);

        // Act
        var result = scoreboard.GetSummary();

        // Assert
        Assert.Equal(mockGame4.Object, result[0]);
        Assert.Equal(mockGame2.Object, result[1]);
        Assert.Equal(mockGame1.Object, result[2]);
        Assert.Equal(mockGame5.Object, result[3]);
        Assert.Equal(mockGame3.Object, result[4]);
    }

    [Fact]
    public void StartGame_ShouldCallStartGame_WhenUsingInterface()
    {
        // Arrange
        var mockScoreBoard = new Mock<IScoreBoard>();
        var mockGame = CreateMockGame("Argentina", "Brazil");

        // Act
        mockScoreBoard.Object.StartGame(mockGame.Object);

        // Assert
        mockScoreBoard.Verify(s => s.StartGame(mockGame.Object), Times.Once());
    }

    [Fact]
    public void FinishGame_ShouldCallFinishGame_WhenUsingInterface()
    {
        // Arrange
        var mockScoreBoard = new Mock<IScoreBoard>();
        var mockGame = CreateMockGame("Argentina", "Brazil");

        // Act
        mockScoreBoard.Object.FinishGame(mockGame.Object);

        // Assert
        mockScoreBoard.Verify(s => s.FinishGame(mockGame.Object), Times.Once());
    }

    [Fact]
    public void UpdateScore_ShouldCallUpdateScore_WhenUsingInterface()
    {
        // Arrange
        var mockScoreBoard = new Mock<IScoreBoard>();
        var mockGame = CreateMockGame("Argentina", "Brazil");

        // Act
        mockScoreBoard.Object.UpdateScore(mockGame.Object, 3, 2);

        // Assert
        mockScoreBoard.Verify(s => s.UpdateScore(mockGame.Object, 3, 2), Times.Once());
    }
}
