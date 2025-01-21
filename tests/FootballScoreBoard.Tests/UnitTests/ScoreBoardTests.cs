namespace FootballScoreBoard.Tests.UnitTests;

public class ScoreBoardTests
{
    [Fact]
    public void StartGame_ShouldAddGame_WhenGameNotExists()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var game = new Game("Mexico", "Canada");

        // Act
        scoreboard.StartGame(game);

        // Assert
        var games = scoreboard.GetSummary();
        Assert.Single(games);
        Assert.Equal("Mexico|Canada", games[0].Id);
    }

    [Fact]
    public void StartGame_ShouldThrowInvalidOperationException_WhenGameAlreadyExists()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var game1 = new Game("Mexico", "Canada");
        scoreboard.StartGame(game1);

        // Act 
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.StartGame(game1));

        // Assert
        Assert.Equal("Game already exists.", exception.Message);
    }

    [Fact]
    public void FinishGame_ShouldRemoveGame_WhenGameExists()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var game = new Game("Mexico", "Canada");
        scoreboard.StartGame(game);

        // Act
        scoreboard.FinishGame(game);

        // Assert
        var games = scoreboard.GetSummary();
        Assert.Empty(games);
    }

    [Fact]
    public void FinishGame_ShouldThrowInvalidOperationException_WhenGameNotExists()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var game = new Game("Mexico", "Canada");

        // Act - Finish a game not added to the board
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.FinishGame(game));

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
        var scoreboard = new ScoreBoard();
        var game = new Game("Mexico", "Canada");

        // Act - Update a game not added to the board
        var exception = Assert.Throws<InvalidOperationException>(() => scoreboard.UpdateScore(game, 3, 1));

        // Assert
        Assert.Equal("Game not found.", exception.Message);
    }

    [Fact]
    public void GetSummary_ShouldReturnEmpty_WhenNoGamesAdded()
    {
        // Arrange
        var scoreBoard = new ScoreBoard();

        // Act
        var result = scoreBoard.GetSummary();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetSummary_ShouldSortByScores_WhenGamesHaveDifferentScores()
    {
        // Arrange
        var scoreBoard = new ScoreBoard();
        var game1 = new Game("Argentina", "Australia");
        var game2 = new Game("Mexico", "Canada");
        var game3 = new Game("Brazil", "France");

        scoreBoard.StartGame(game1);
        scoreBoard.StartGame(game2);
        scoreBoard.StartGame(game3);

        game1.UpdateScore(3, 1);
        game2.UpdateScore(2, 3);
        game3.UpdateScore(5, 5);

        // Act
        var result = scoreBoard.GetSummary();

        // Assert
        Assert.Equal(game3, result[0]);
        Assert.Equal(game2, result[1]);
        Assert.Equal(game1, result[2]);
    }

    [Fact]
    public void GetSummary_ShouldSortByStartTime_WhenScoresAreEqual()
    {
        // Arrange
        var scoreBoard = new ScoreBoard();
        var game1 = new Game("Argentina", "Australia");
        var game2 = new Game("Mexico", "Canada");

        scoreBoard.StartGame(game1);
        scoreBoard.StartGame(game2);

        game1.UpdateScore(3, 3);
        game2.UpdateScore(3, 3);

        // Act
        var result = scoreBoard.GetSummary();

        // Assert
        Assert.Equal(game2, result[0]);
        Assert.Equal(game1, result[1]);
    }

    [Fact]
    public void GetSummary_ShouldSortByScoresAndStartTime()
    {
        // Arrange
        var scoreBoard = new ScoreBoard();

        var game1 = new Game("Mexico", "Canada");
        var game2 = new Game("Spain", "Brazil");
        var game3 = new Game("Germany", "France");
        var game4 = new Game("Uruguay", "Italy");
        var game5 = new Game("Argentina", "Australia");

        scoreBoard.StartGame(game1);
        scoreBoard.StartGame(game2);
        scoreBoard.StartGame(game3);
        scoreBoard.StartGame(game4);
        scoreBoard.StartGame(game5);

        game1.UpdateScore(0, 5);
        game2.UpdateScore(10, 2);
        game3.UpdateScore(2, 2);
        game4.UpdateScore(6, 6);
        game5.UpdateScore(3, 1);

        // Act
        var result = scoreBoard.GetSummary();

        // Assert
        Assert.Equal(game4, result[0]);
        Assert.Equal(game2, result[1]);
        Assert.Equal(game1, result[2]);
        Assert.Equal(game5, result[3]);
        Assert.Equal(game3, result[4]);
    }
}
