namespace FootballScoreBoard;

/// <summary>
/// Represents a football game between two teams, tracking their scores and game status.
/// </summary>
public class Game : IGame
{
    public string Id { get; }
    public string HomeTeam { get; }
    public string AwayTeam { get; }
    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }
    public DateTime? StartTime { get; private set; }

    private static string GetGameId(string homeTeam, string awayTeam) => $"{homeTeam}|{awayTeam}";

    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class.
    /// </summary>
    /// <param name="homeTeam">The home team name.</param>
    /// <param name="awayTeam">The away team name.</param>
    /// <exception cref="ArgumentException">Thrown when either team name is null or empty.</exception>
    public Game(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
        {
            throw new ArgumentException("Team names cannot be empty");
        }

        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Id = GetGameId(homeTeam, awayTeam);
        HomeScore = 0;
        AwayScore = 0;
    }

    /// <summary>
    /// Starts the game by setting the start time.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the game has already started.</exception>
    public void Start()
    {
        if (StartTime is not null)
        {
            throw new InvalidOperationException("The game has already started");
        }

        StartTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the scores of the game.
    /// </summary>
    /// <param name="homeScore">The new score for the home team.</param>
    /// <param name="awayScore">The new score for the away team.</param>
    /// <exception cref="InvalidOperationException">Thrown if the game has not started.</exception>
    /// <exception cref="ArgumentException">Thrown if either score is negative.</exception>
    public void UpdateScore(int homeScore, int awayScore)
    {
        if (StartTime is null)
        {
            throw new InvalidOperationException("The game must have started to update the result");
        }

        if (homeScore < 0 || awayScore < 0)
        {
            throw new ArgumentException("Scores cannot be negative");
        }

        HomeScore = homeScore;
        AwayScore = awayScore;
    }
}
