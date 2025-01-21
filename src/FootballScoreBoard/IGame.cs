namespace FootballScoreBoard;

public interface IGame
{
    int AwayScore { get; }
    string AwayTeam { get; }
    int HomeScore { get; }
    string HomeTeam { get; }
    string Id { get; }
    DateTime? StartTime { get; }

    void Start();
    void UpdateScore(int homeScore, int awayScore);
}