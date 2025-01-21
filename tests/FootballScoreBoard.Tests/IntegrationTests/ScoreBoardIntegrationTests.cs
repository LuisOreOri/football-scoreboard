using FootballScoreBoard.Core;

namespace FootballScoreBoard.Tests.IntegrationTests;

public class ScoreBoardIntegrationTests
{
    [Fact]
    public void ScoreBoard_ShouldHandleGameLifecycle_EndToEnd()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        var game1 = new Game("Argentina", "Brazil");
        var game2 = new Game("Mexico", "Canada");
        var game3 = new Game("Germany", "France");

        // Act
        scoreboard.StartGame(game1);
        scoreboard.StartGame(game2);
        scoreboard.StartGame(game3);

        game1.UpdateScore(3, 1);  // Argentina 3 - Brazil 1
        game2.UpdateScore(2, 3); // Mexico 2 - Canada 3
        game3.UpdateScore(5, 5); // Germany 5 - France 5

        var summary = scoreboard.GetSummary();

        // Assert
        Assert.Equal(3, summary.Count);
        Assert.Equal(game3, summary[0]); // Highest score (10)
        Assert.Equal(game2, summary[1]); // Next highest score (5)
        Assert.Equal(game1, summary[2]); // Lowest score (4)
    }

    [Fact]
    public void ScoreBoard_ShouldRemoveGameCorrectly()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        var game1 = new Game("Argentina", "Brazil");
        var game2 = new Game("Mexico", "Canada");

        scoreboard.StartGame(game1);
        scoreboard.StartGame(game2);

        // Act
        scoreboard.FinishGame(game1);
        var summary = scoreboard.GetSummary();

        // Assert
        Assert.Single(summary);
        Assert.Equal(game2, summary[0]);
    }
}
