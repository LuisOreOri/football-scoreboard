namespace FootballScoreBoard;

/// <summary>
/// Manages a collection of football games, allowing tracking of game status and scores.
/// </summary>
public class ScoreBoard
{
    private readonly Dictionary<string, Game> _games = [];

    /// <summary>
    /// Starts a new game and adds it to the scoreboard.
    /// </summary>
    /// <param name="game">The game to start.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game already exists on the scoreboard.</exception>

    public void StartGame(Game game)
    {
        if (_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game already exists.");
        }

        game.Start();
        _games[game.Id] = game;
    }

    /// <summary>
    /// Removes a finished game from the scoreboard.
    /// </summary>
    /// <param name="game">The game to finish and remove.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game is not found on the scoreboard.</exception>
    public void FinishGame(Game game)
    {
        if (!_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game not found.");
        }

        _games.Remove(game.Id);
    }

    /// <summary>
    /// Updates the score of an ongoing game.
    /// </summary>
    /// <param name="game">The game to update.</param>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game is not found on the scoreboard.</exception>
    public void UpdateScore(Game game, int homeScore, int awayScore)
    {
        if (!_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game not found.");
        }

        _games[game.Id].UpdateScore(homeScore, awayScore);
    }

    /// <summary>
    /// Retrieves a summary of all ongoing games, sorted by total score and start time.
    /// </summary>
    /// <returns>A list of games ordered by total score (descending) and start time (most recent first).</returns>
    public List<Game> GetSummary()
    {
        return _games.Values
            .OrderByDescending(g => g.HomeScore + g.AwayScore) 
            .ThenByDescending(g => g.StartTime) 
            .ToList(); 
    }
}
