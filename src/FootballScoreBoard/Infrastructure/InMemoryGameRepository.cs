using FootballScoreBoard.Core;

namespace FootballScoreBoard.Infrastructure;

internal class InMemoryGameRepository : IGameRepository
{
    private readonly Dictionary<string, IGame> _games = [];

    public void Add(IGame game)
    {
        if (_games.ContainsKey(game.Id))
        {
            throw new InvalidOperationException("Game already exists.");
        }
        _games[game.Id] = game;
    }

    public void Remove(string gameId)
    {
        if (!_games.ContainsKey(gameId))
        {
            throw new InvalidOperationException("Game not found.");
        }
        _games.Remove(gameId);
    }

    public IGame? GetById(string gameId)
    {
        _games.TryGetValue(gameId, out var game);
        return game;
    }

    public IEnumerable<IGame> GetAll()
    {
        return _games.Values;
    }
}
