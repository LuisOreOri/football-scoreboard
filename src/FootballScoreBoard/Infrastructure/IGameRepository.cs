using FootballScoreBoard.Core;

namespace FootballScoreBoard.Infrastructure;

public interface IGameRepository
{
    void Add(IGame game);
    void Remove(string gameId);
    IGame? GetById(string gameId);
    IEnumerable<IGame> GetAll();
}
