namespace FootballScoreBoard.Core;

/// <summary>
/// Provides an abstraction for storing and managing games in a repository.
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Adds a game to the repository.
    /// </summary>
    /// <param name="game">The game to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="game"/> is null.</exception>
    void Add(IGame game);

    /// <summary>
    /// Removes a game from the repository by its unique identifier.
    /// </summary>
    /// <param name="gameId">The unique identifier of the game to remove.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="gameId"/> is null or empty.</exception>
    void Remove(string gameId);

    /// <summary>
    /// Retrieves a game from the repository by its unique identifier.
    /// </summary>
    /// <param name="gameId">The unique identifier of the game to retrieve.</param>
    /// <returns>
    /// The <see cref="IGame"/> instance if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="gameId"/> is null or empty.</exception>
    IGame? GetById(string gameId);

    /// <summary>
    /// Retrieves all games currently stored in the repository.
    /// </summary>
    /// <returns>An enumerable collection of all <see cref="IGame"/> instances in the repository.</returns>
    IEnumerable<IGame> GetAll();
}
