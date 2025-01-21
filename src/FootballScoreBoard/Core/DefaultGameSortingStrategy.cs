namespace FootballScoreBoard.Core;

/// <summary>
/// Default sorting strategy: sorts games by total score and start time.
/// </summary>
internal class DefaultGameSortingStrategy : IGameSortingStrategy
{
    public IEnumerable<IGame> Sort(IEnumerable<IGame> games)
    {
        if (games == null)
        {
            throw new ArgumentNullException(nameof(games), "The collection of games cannot be null.");
        }

        return games
            .OrderByDescending(g => g.HomeScore + g.AwayScore)
            .ThenByDescending(g => g.StartTime);
    }
}
