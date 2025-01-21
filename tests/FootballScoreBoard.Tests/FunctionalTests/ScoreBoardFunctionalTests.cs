using FootballScoreBoard.Core;

namespace FootballScoreBoard.Tests.FunctionalTests;

public class ScoreBoardFunctionalTests
{
    [Fact]
    public void GetSummary_ShouldProvideCorrectOrder_WhenSimulatingPdfExample()
    {
        // Arrange
        var scoreboard = new ScoreBoard();

        // Create games and add them to the scoreboard
        var game1 = new Game("Mexico", "Canada");
        var game2 = new Game("Spain", "Brazil");
        var game3 = new Game("Germany", "France");
        var game4 = new Game("Uruguay", "Italy");
        var game5 = new Game("Argentina", "Australia");

        scoreboard.StartGame(game1);
        scoreboard.StartGame(game2);
        scoreboard.StartGame(game3);
        scoreboard.StartGame(game4);
        scoreboard.StartGame(game5);

        // Update scores 
        game1.UpdateScore(0, 5);  // Mexico 0 - Canada 5
        game2.UpdateScore(10, 2); // Spain 10 - Brazil 2
        game3.UpdateScore(2, 2);  // Germany 2 - France 2
        game4.UpdateScore(6, 6);  // Uruguay 6 - Italy 6
        game5.UpdateScore(3, 1);  // Argentina 3 - Australia 1

        // Act
        var summary = scoreboard.GetSummary();

        // Assert
        Assert.Equal(5, summary.Count);
        Assert.Equal("Uruguay|Italy", summary[0].Id);       // Uruguay 6 - Italy 6
        Assert.Equal("Spain|Brazil", summary[1].Id);        // Spain 10 - Brazil 2
        Assert.Equal("Mexico|Canada", summary[2].Id);       // Mexico 0 - Canada 5
        Assert.Equal("Argentina|Australia", summary[3].Id); // Argentina 3 - Australia 1
        Assert.Equal("Germany|France", summary[4].Id);      // Germany 2 - France 2
    }
}
