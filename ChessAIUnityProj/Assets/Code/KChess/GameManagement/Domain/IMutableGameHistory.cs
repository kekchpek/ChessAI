using KChess.Domain;

namespace KChess.GameManagement.Domain
{
    internal interface IMutableGameHistory : IGameHistory
    {
        void SetInitialPosition(Position position);
        void AddMove(IMove move);
        void SetResult(GameResult result);
    }
}