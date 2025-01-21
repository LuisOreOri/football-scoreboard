namespace FootballScoreBoard.Core;

/// <summary>
/// Represents a strategy for sorting games.
/// </summary>
public interface IGameSortingStrategy
{
    /// <summary>
    /// Sorts a collection of games.
    /// </summary>
    /// <param name="games">The games to sort.</param>
    /// <returns>A sorted enumerable of games.</returns>
    IEnumerable<IGame> Sort(IEnumerable<IGame> games);
}
