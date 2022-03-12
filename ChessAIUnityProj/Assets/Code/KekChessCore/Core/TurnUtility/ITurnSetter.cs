using ChessAI.Domain;

namespace ChessAI.Core.TurnUtility
{
    public interface ITurnSetter
    {
        void SetTurn(PieceColor pieceColor);
    }
}