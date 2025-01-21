using FootballScoreBoard.Infrastructure;
using FootballScoreBoard.Tests.Mocks;

namespace FootballScoreBoard.Tests.UnitTests;

public class InMemoryGameRepositoryTests
{
    [Fact]
    public void Add_ShouldThrowArgumentNullException_WhenGameIsNull()
    {
        // Arrange
        var repository = new InMemoryGameRepository();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => repository.Add(null!));
        Assert.Equal("game", exception.ParamName);
    }

    [Fact]
    public void Add_ShouldAddGameToRepository()
    {
        // Arrange
        var repository = new InMemoryGameRepository();
        var mockGame = MockGame.Create("Mexico", "Canada");

        // Act
        repository.Add(mockGame.Object);

        // Assert
        var result = repository.GetById(mockGame.Object.Id);
        Assert.NotNull(result);
        Assert.Equal(mockGame.Object, result);
    }

    [Fact]
    public void Add_ShouldThrowInvalidOperationException_WhenGameAlreadyExists()
    {
        // Arrange
        var repository = new InMemoryGameRepository();
        var mockGame = MockGame.Create("Mexico", "Canada");
        repository.Add(mockGame.Object);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => repository.Add(mockGame.Object));
        Assert.Equal("Game already exists.", exception.Message);
    }

    [Fact]
    public void Remove_ShouldRemoveGameFromRepository()
    {
        // Arrange
        var repository = new InMemoryGameRepository();
        var mockGame = MockGame.Create("Mexico", "Canada");
        repository.Add(mockGame.Object);

        // Act
        repository.Remove(mockGame.Object.Id);

        // Assert
        var result = repository.GetById(mockGame.Object.Id);
        Assert.Null(result);
    }

    [Fact]
    public void Remove_ShouldThrowInvalidOperationException_WhenGameNotFound()
    {
        // Arrange
        var repository = new InMemoryGameRepository();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => repository.Remove("NonExistingGameId"));
        Assert.Equal("Game not found.", exception.Message);
    }

    [Fact]
    public void GetById_ShouldReturnGame_WhenGameExists()
    {
        // Arrange
        var repository = new InMemoryGameRepository();
        var mockGame = MockGame.Create("Mexico", "Canada");
        repository.Add(mockGame.Object);

        // Act
        var result = repository.GetById(mockGame.Object.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockGame.Object, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenGameNotFound()
    {
        // Arrange
        var repository = new InMemoryGameRepository();

        // Act
        var result = repository.GetById("NonExistingGameId");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAllGames()
    {
        // Arrange
        var repository = new InMemoryGameRepository();
        var mockGame1 = MockGame.Create("Mexico", "Canada");
        var mockGame2 = MockGame.Create("Argentina", "Australia");
        repository.Add(mockGame1.Object);
        repository.Add(mockGame2.Object);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(mockGame1.Object, result);
        Assert.Contains(mockGame2.Object, result);
    }
}
