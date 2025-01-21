namespace FootballScoreBoard;

public class Game
{
    public string Id { get; }
    public string HomeTeam { get; }
    public string AwayTeam { get; }
    public int HomeScore { get; private set; }
    public int AwayScore { get; private set; }
    public DateTime? StartTime { get; private set; }

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

    private static string GetGameId(string homeTeam, string awayTeam)
    {
        return $"{homeTeam}|{awayTeam}";
    }
}
