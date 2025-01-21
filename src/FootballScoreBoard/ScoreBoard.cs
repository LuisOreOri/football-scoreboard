namespace FootballScoreBoard;

public class ScoreBoard
{
    private readonly Dictionary<string, Game> _games = [];

    public void StartGame(Game game)
    {
        if (_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game already exists.");
        }

        game.Start();
        _games[game.Id] = game;
    }

    public void FinishGame(Game game)
    {
        if (!_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game not found.");
        }

        _games.Remove(game.Id);
    }

    public void UpdateScore(Game game, int homeScore, int awayScore)
    {
        if (!_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game not found.");
        }

        _games[game.Id].UpdateScore(homeScore, awayScore);
    }

    public List<Game> GetSummary()
    {
        return _games.Values
            .OrderByDescending(g => g.HomeScore + g.AwayScore) 
            .ThenByDescending(g => g.StartTime) 
            .ToList(); 
    }
}
