namespace FootballScoreBoard.Core;

/// <summary>
/// Defines the contract for managing games in a scoreboard, including starting, finishing, updating, and retrieving games.
/// </summary>
public interface IScoreBoard
{
    /// <summary>
    /// Starts a game and adds it to the scoreboard.
    /// </summary>
    /// <param name="game">The game to start and add to the scoreboard.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game is already present in the scoreboard.</exception>
    void StartGame(IGame game);

    /// <summary>
    /// Updates the score of an existing game on the scoreboard.
    /// </summary>
    /// <param name="game">The game to update.</param>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game is not found on the scoreboard.</exception>
    /// <exception cref="ArgumentException">Thrown if any of the scores are negative.</exception>
    void UpdateScore(IGame game, int homeScore, int awayScore);

    /// <summary>
    /// Removes a game from the scoreboard.
    /// </summary>
    /// <param name="game">The game to remove.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game is not found on the scoreboard.</exception>
    void FinishGame(IGame game);

    /// <summary>
    /// Retrieves a summary of all games on the scoreboard, sorted by total score in descending order
    /// and then by start time in descending order for games with the same score.
    /// </summary>
    /// <returns>A sorted list of games on the scoreboard.</returns>
    List<IGame> GetSummary();
}
