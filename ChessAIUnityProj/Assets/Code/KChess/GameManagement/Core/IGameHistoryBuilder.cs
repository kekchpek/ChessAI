using KChess.Domain;
using KChess.GameManagement.Domain;

namespace KChess.GameManagement.Core
{
    public interface IGameHistoryBuilder
    {
        IGameHistoryBuilder SetPosition(Position position);
        IGameHistoryBuilder Move(IMove move);
        IGameHistoryBuilder SetResult(GameResult result);
        IGameHistory Build();
    }
}