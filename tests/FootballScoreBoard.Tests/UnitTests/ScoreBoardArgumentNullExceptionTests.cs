using FootballScoreBoard.Core;
using FootballScoreBoard.Infrastructure;

namespace FootballScoreBoard.Tests.UnitTests;

public class ScoreBoardArgumentNullExceptionTests
{
    [Fact]
    public void Constructor_WithNullRepository_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new ScoreBoard((IGameRepository)null!));
        Assert.Equal("repository", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullSortingStrategy_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new ScoreBoard((IGameSortingStrategy)null!));
        Assert.Equal("sortingStrategy", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullRepositoryAndSortingStrategy_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new ScoreBoard(null!, new DefaultGameSortingStrategy()));
        Assert.Equal("repository", exception.ParamName);

        exception = Assert.Throws<ArgumentNullException>(() => new ScoreBoard(new InMemoryGameRepository(), null!));
        Assert.Equal("sortingStrategy", exception.ParamName);
    }

    [Fact]
    public void StartGame_WithNullGame_ShouldThrowArgumentNullException()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => scoreboard.StartGame(null!));
        Assert.Equal("game", exception.ParamName);
    }

    [Fact]
    public void FinishGame_WithNullGame_ShouldThrowArgumentNullException()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => scoreboard.FinishGame(null!));
        Assert.Equal("game", exception.ParamName);
    }

    [Fact]
    public void UpdateScore_WithNullGame_ShouldThrowArgumentNullException()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => scoreboard.UpdateScore(null!, 3, 2));
        Assert.Equal("game", exception.ParamName);
    }
}
