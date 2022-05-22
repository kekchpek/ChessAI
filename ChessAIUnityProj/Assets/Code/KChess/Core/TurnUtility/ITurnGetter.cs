using KChess.Domain;

namespace KChess.Core.TurnUtility
{
    public interface ITurnGetter
    {
        PieceColor GetTurn();
    }
}