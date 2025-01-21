namespace FootballScoreBoard.Core;

public interface IScoreBoard
{
    void FinishGame(IGame game);
    List<IGame> GetSummary();
    void StartGame(IGame game);
    void UpdateScore(IGame game, int homeScore, int awayScore);
}