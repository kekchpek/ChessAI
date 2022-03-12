using ChessAI.Domain;

namespace ChessAI.Core.TurnUtility
{
    public interface ITurnGetter
    {
        PieceColor GetTurn();
    }
}