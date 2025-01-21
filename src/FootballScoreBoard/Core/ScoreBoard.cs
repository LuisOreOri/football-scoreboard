using FootballScoreBoard.Infrastructure;

namespace FootballScoreBoard.Core;

/// <summary>
/// Represents a scoreboard for managing games and their scores.
/// </summary>
public class ScoreBoard : IScoreBoard
{
    private readonly IGameRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreBoard"/> class using an in-memory repository.
    /// </summary>
    public ScoreBoard() : this(new InMemoryGameRepository()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScoreBoard"/> class with the specified game repository.
    /// </summary>
    /// <param name="repository">The repository to store and manage games.</param>
    public ScoreBoard(IGameRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Starts a new game by adding it to the scoreboard and setting its start time.
    /// </summary>
    /// <param name="game">The game to start.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game already exists in the repository.</exception>
    public void StartGame(IGame game)
    {
        game.Start();
        _repository.Add(game);
    }

    /// <summary>
    /// Removes a game from the scoreboard, indicating that the game has finished.
    /// </summary>
    /// <param name="game">The game to remove.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game does not exist in the repository.</exception>
    public void FinishGame(IGame game)
    {
        _repository.Remove(game.Id);
    }

    /// <summary>
    /// Updates the score of an existing game in the scoreboard.
    /// </summary>
    /// <param name="game">The game whose score needs to be updated.</param>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game does not exist in the repository.</exception>
    public void UpdateScore(IGame game, int homeScore, int awayScore)
    {
        var existingGame = _repository.GetById(game.Id);
        if (existingGame is null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        existingGame.UpdateScore(homeScore, awayScore);
    }

    /// <summary>
    /// Retrieves a summary of all active games, sorted by total score (descending) and start time (descending).
    /// </summary>
    /// <returns>A list of games sorted by their total score and start time.</returns>
    public List<IGame> GetSummary()
    {
        return _repository.GetAll()
            .OrderByDescending(g => g.HomeScore + g.AwayScore)
            .ThenByDescending(g => g.StartTime)
            .ToList();
    }
}
