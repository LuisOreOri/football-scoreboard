using FootballScoreBoard.Core;
using Moq;

namespace FootballScoreBoard.Tests.Mocks;
internal class MockGame
{
    public static Mock<IGame> Create(string homeTeam, string awayTeam, int homeScore = 0, int awayScore = 0, DateTime? startTime = null)
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
}
