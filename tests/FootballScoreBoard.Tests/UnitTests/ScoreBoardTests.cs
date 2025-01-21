using FootballScoreBoard.Core;
using FootballScoreBoard.Infrastructure;
using FootballScoreBoard.Tests.Mocks;
using Moq;

namespace FootballScoreBoard.Tests.UnitTests;

public class ScoreBoardTests
{
    [Fact]
    public void ScoreBoard_DefaultConstructor_ShouldUseInMemoryRepository()
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
    public void ScoreBoard_CustomConstructor_ShouldUseInjectedRepository()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();
        var scoreboard = new ScoreBoard(mockRepository.Object);
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");

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
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");

        var scoreboard = new ScoreBoard(mockRepository.Object);

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
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");

        var scoreboard = new ScoreBoard(mockRepository.Object);

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
        var mockGame = new Mock<IGame>();
        mockGame.Setup(g => g.Id).Returns("Team1|Team2");
        mockRepository.Setup(r => r.GetById("Team1|Team2")).Returns(mockGame.Object);

        var scoreboard = new ScoreBoard(mockRepository.Object);

        // Act
        scoreboard.UpdateScore(mockGame.Object, 3, 1);

        // Assert
        mockRepository.Verify(r => r.GetById("Team1|Team2"), Times.Once());
        mockGame.Verify(g => g.UpdateScore(3, 1), Times.Once());
    }

    [Fact]
    public void GetSummary_ShouldReturnGamesSortedByTotalScoreAndStartTime()
    {
        // Arrange
        var mockRepository = new Mock<IGameRepository>();

        // Mock games with specific scores and start times
        var mockGame1 = new Mock<IGame>();
        mockGame1.Setup(g => g.Id).Returns("Team1|Team2");
        mockGame1.Setup(g => g.HomeScore).Returns(3);
        mockGame1.Setup(g => g.AwayScore).Returns(2);
        mockGame1.Setup(g => g.StartTime).Returns(DateTime.UtcNow.AddMinutes(-10));

        var mockGame2 = new Mock<IGame>();
        mockGame2.Setup(g => g.Id).Returns("Team3|Team4");
        mockGame2.Setup(g => g.HomeScore).Returns(5);
        mockGame2.Setup(g => g.AwayScore).Returns(5);
        mockGame2.Setup(g => g.StartTime).Returns(DateTime.UtcNow.AddMinutes(-5));

        var mockGame3 = new Mock<IGame>();
        mockGame3.Setup(g => g.Id).Returns("Team5|Team6");
        mockGame3.Setup(g => g.HomeScore).Returns(3);
        mockGame3.Setup(g => g.AwayScore).Returns(2);
        mockGame3.Setup(g => g.StartTime).Returns(DateTime.UtcNow.AddMinutes(-15));

        // Return the mocked games from the repository
        mockRepository.Setup(r => r.GetAll()).Returns([mockGame1.Object, mockGame2.Object, mockGame3.Object]);

        var scoreboard = new ScoreBoard(mockRepository.Object);

        // Act
        var result = scoreboard.GetSummary();

        // Assert
        Assert.Equal(mockGame2.Object, result[0]); // Highest total score: 5 + 5 = 10
        Assert.Equal(mockGame1.Object, result[1]); // Same total score as Game3, but later start time
        Assert.Equal(mockGame3.Object, result[2]); // Same total score as Game1, but earlier start time
    }
}
