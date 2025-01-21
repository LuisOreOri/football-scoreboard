using FootballScoreBoard.Core;
using FootballScoreBoard.Tests.Mocks;

namespace FootballScoreBoard.Tests.UnitTests;

public class DefaultGameSortingStrategyTests
{
    [Fact]
    public void Sort_ShouldThrowArgumentNullException_WhenGamesIsNull()
    {
        // Arrange
        var sortingStrategy = new DefaultGameSortingStrategy();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => sortingStrategy.Sort(null!));
        Assert.Equal("games", exception.ParamName);
    }

    [Fact]
    public void Sort_ShouldReturnGamesSortedByTotalScoreAndStartTime()
    {
        // Arrange
        var strategy = new DefaultGameSortingStrategy();

        var mockGame1 = MockGame.Create("Mexico", "Canada", 3, 1, DateTime.UtcNow.AddMinutes(-10));
        var mockGame2 = MockGame.Create("Argentina", "Australia", 5, 5, DateTime.UtcNow.AddMinutes(-5));
        var mockGame3 = MockGame.Create("Brazil", "France", 3, 1, DateTime.UtcNow.AddMinutes(-15));

        var games = new[] { mockGame1.Object, mockGame2.Object, mockGame3.Object };

        // Act
        var sortedGames = strategy.Sort(games).ToList();

        // Assert
        Assert.Equal(mockGame2.Object, sortedGames[0]); // Highest total score
        Assert.Equal(mockGame1.Object, sortedGames[1]); // Same score as Game3 but later start time
        Assert.Equal(mockGame3.Object, sortedGames[2]); // Earlier start time
    }

    [Fact]
    public void Sort_ShouldReturnEmpty_WhenInputIsEmpty()
    {
        // Arrange
        var strategy = new DefaultGameSortingStrategy();

        var games = Enumerable.Empty<IGame>();

        // Act
        var sortedGames = strategy.Sort(games);

        // Assert
        Assert.Empty(sortedGames);
    }

    [Fact]
    public void Sort_ShouldHandleSingleGame()
    {
        // Arrange
        var strategy = new DefaultGameSortingStrategy();

        var mockGame = MockGame.Create("Mexico", "Canada", 3, 1, DateTime.UtcNow.AddMinutes(-10));
        var games = new[] { mockGame.Object };

        // Act
        var sortedGames = strategy.Sort(games);

        // Assert
        Assert.Single(sortedGames);
        Assert.Equal(mockGame.Object, sortedGames.First());
    }
}
