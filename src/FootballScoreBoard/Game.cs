namespace FootballScoreBoard;

public class Game
{
    public string Id { get; }
    public string HomeTeam { get; }
    public string AwayTeam { get; }
    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }
    public DateTime? StartTime { get; private set; }

    private static string GetGameId(string homeTeam, string awayTeam) => $"{homeTeam}|{awayTeam}";

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

    public void Start()
    {
        if (StartTime is not null)
        {
            throw new InvalidOperationException("The game has already started");
        }

        StartTime = DateTime.UtcNow;
    }

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
