using FootballScoreBoard.Core;
using FootballScoreBoard.Infrastructure;
using FootballScoreBoard.Tests.Mocks;
using Moq;

namespace FootballScoreBoard.Tests.UnitTests;

public class ScoreBoardTests
{
    [Fact]
    public void ScoreBoard_DefaultConstructor_ShouldUseDefaultSortingStrategyAndUseInMemoryRepository()
    {
        // Arrange
        var scoreboard = new ScoreBoard();
        var mockGame = MockGame.Create("Mexico", "Canada");

        // Act
        scoreboard.StartGame(mockGame.Object);
        var summary = scoreboard.GetSummary();

        // Assert
        Assert.Single(summary);
        Assert.Equal(mockGame.Object, summary[0]);
    }

    [Fact]
    public void Constructor_WithCustomRepository_ShouldUseDefaultSortingStrategy()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();

        // Act
        var scoreboard = new ScoreBoard(mockRepository.Object);
        var mockGame = MockGame.Create("Team1", "Team2");

        scoreboard.StartGame(mockGame.Object);

        // Assert
        mockRepository.Verify(r => r.Add(mockGame.Object), Times.Once());
    }

    [Fact]
    public void Constructor_WithCustomSortingStrategy_ShouldUseDefaultRepository()
    {
        // Arrange
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();

        // Act
        var scoreboard = new ScoreBoard(mockSortingStrategy.Object);
        var mockGames = new List<IGame>
        {
            MockGame.Create("Team1", "Team2", 3, 2, DateTime.UtcNow.AddMinutes(-10)).Object,        
        };
        
        mockSortingStrategy.Setup(s => s.Sort(It.IsAny<IEnumerable<IGame>>()))
                           .Returns(mockGames.OrderByDescending(g => g.HomeScore + g.AwayScore));

        scoreboard.StartGame(mockGames[0]);
        var summary = scoreboard.GetSummary();

        // Assert
        Assert.Single(summary);
        Assert.Equal(mockGames[0], summary[0]);
        mockSortingStrategy.Verify(s => s.Sort(It.IsAny<IEnumerable<IGame>>()), Times.Once());
    }

    [Fact]
    public void ScoreBoard_CustomConstructor_ShouldUseInjectedRepositoryAndSortingStrategy()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();
        var scoreboard = new ScoreBoard(mockRepository.Object, mockSortingStrategy.Object);
        var mockGame = MockGame.Create("Mexico", "Canada");

        // Act
        scoreboard.StartGame(mockGame.Object);

        // Assert
        mockRepository.Verify(r => r.Add(mockGame.Object), Times.Once());
    }

    [Fact]
    public void StartGame_ShouldAddGameToRepository()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();
        var mockGame = MockGame.Create("Team1", "Team2");

        var scoreboard = new ScoreBoard(mockRepository.Object, mockSortingStrategy.Object);

        // Act
        scoreboard.StartGame(mockGame.Object);

        // Assert
        mockGame.Verify(g => g.Start(), Times.Once());
        mockRepository.Verify(r => r.Add(mockGame.Object), Times.Once());
    }

    [Fact]
    public void FinishGame_ShouldRemoveGameFromRepository()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");

        var scoreboard = new ScoreBoard(mockRepository.Object, mockSortingStrategy.Object);

        // Act
        scoreboard.FinishGame(mockGame.Object);

        // Assert
        mockRepository.Verify(r => r.Remove("Team1|Team2"), Times.Once());
    }

    [Fact]
    public void UpdateScore_ShouldUpdateGameScoreInRepository()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");
        mockRepository.Setup(r => r.GetById("Team1|Team2")).Returns(mockGame.Object);

        var scoreboard = new ScoreBoard(mockRepository.Object, mockSortingStrategy.Object);

        // Act
        scoreboard.UpdateScore(mockGame.Object, 3, 1);

        // Assert
        mockRepository.Verify(r => r.GetById("Team1|Team2"), Times.Once());
        mockGame.Verify(g => g.UpdateScore(3, 1), Times.Once());
    }

    [Fact]
    public void GetSummary_ShouldUseInjectedSortingStrategy()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var mockSortingStrategy = new Mock<IGameSortingStrategy>();

        var mockGames = new List<IGame>
        {
            MockGame.Create("Team1", "Team2", 3, 2, DateTime.UtcNow.AddMinutes(-10)).Object,
            MockGame.Create("Team3", "Team4", 5, 5, DateTime.UtcNow.AddMinutes(-5)).Object
        };

        mockRepository.Setup(r => r.GetAll()).Returns(mockGames);
        mockSortingStrategy.Setup(s => s.Sort(It.IsAny<IEnumerable<IGame>>()))
                           .Returns(mockGames.OrderByDescending(g => g.HomeScore + g.AwayScore));

        var scoreboard = new ScoreBoard(mockRepository.Object, mockSortingStrategy.Object);

        // Act
        var summary = scoreboard.GetSummary();

        // Assert
        mockSortingStrategy.Verify(s => s.Sort(mockGames), Times.Once());
        Assert.Equal(mockGames.OrderByDescending(g => g.HomeScore + g.AwayScore), summary);
    }
}
