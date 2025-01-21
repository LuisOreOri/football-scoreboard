using FootballScoreBoard.Infrastructure;

namespace FootballScoreBoard.Core;

/// <summary>
/// Manages games in a scoreboard, allowing starting, finishing, updating scores, and retrieving game summaries.
/// </summary>
public class ScoreBoard : IScoreBoard
{
    private readonly IGameRepository _repository;
    private readonly IGameSortingStrategy _sortingStrategy;

    /// <summary>
    /// Default constructor: uses InMemoryGameRepository and DefaultGameSortingStrategy.
    /// </summary>
    public ScoreBoard()
        : this(new InMemoryGameRepository(), new DefaultGameSortingStrategy()) { }

    /// <summary>
    /// Single-parameter constructor: uses a custom repository and DefaultGameSortingStrategy.
    /// </summary>
    /// <param name="repository">Custom game repository.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="repository"/> is null.</exception>
    public ScoreBoard(IGameRepository repository)
        : this(repository, new DefaultGameSortingStrategy())
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Game repository cannot be null.");
    }

    /// <summary>
    /// Single-parameter constructor: uses a custom sorting strategy and InMemoryGameRepository.
    /// </summary>
    /// <param name="sortingStrategy">Custom game sorting strategy.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="sortingStrategy"/> is null.</exception>
    public ScoreBoard(IGameSortingStrategy sortingStrategy)
        : this(new InMemoryGameRepository(), sortingStrategy)
    {
        _sortingStrategy = sortingStrategy ?? throw new ArgumentNullException(nameof(sortingStrategy), "Sorting strategy cannot be null.");
    }

    /// <summary>
    /// Constructor with custom repository and sorting strategy.
    /// </summary>
    /// <param name="repository">Custom game repository.</param>
    /// <param name="sortingStrategy">Custom game sorting strategy.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="repository"/> or <paramref name="sortingStrategy"/> is null.</exception>
    public ScoreBoard(IGameRepository repository, IGameSortingStrategy sortingStrategy)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository), "Game repository cannot be null.");
        _sortingStrategy = sortingStrategy ?? throw new ArgumentNullException(nameof(sortingStrategy), "Sorting strategy cannot be null.");
    }

    /// <summary>
    /// Starts a game and adds it to the scoreboard.
    /// </summary>
    /// <param name="game">The game to start and add to the scoreboard.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="game"/> is null.</exception>
    public void StartGame(IGame game)
    {
        if (game == null)
        {
            throw new ArgumentNullException(nameof(game), "Game cannot be null.");
        }

        game.Start();
        _repository.Add(game);
    }

    /// <summary>
    /// Removes a game from the scoreboard.
    /// </summary>
    /// <param name="game">The game to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="game"/> is null.</exception>
    public void FinishGame(IGame game)
    {
        if (game == null)
        {
            throw new ArgumentNullException(nameof(game), "Game cannot be null.");
        }

        _repository.Remove(game.Id);
    }

    /// <summary>
    /// Updates the score of an existing game on the scoreboard.
    /// </summary>
    /// <param name="game">The game to update.</param>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="game"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the game is not found on the scoreboard.</exception>
    public void UpdateScore(IGame game, int homeScore, int awayScore)
    {
        if (game == null)
        {
            throw new ArgumentNullException(nameof(game), "Game cannot be null.");
        }

        var existingGame = _repository.GetById(game.Id);
        if (existingGame == null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        existingGame.UpdateScore(homeScore, awayScore);
    }

    /// <summary>
    /// Retrieves a summary of all games on the scoreboard, sorted by total score and start time.
    /// </summary>
    /// <returns>A sorted list of games on the scoreboard.</returns>
    public List<IGame> GetSummary()
    {
        var games = _repository.GetAll();
        return _sortingStrategy.Sort(games).ToList();
    }
}
