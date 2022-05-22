using KChess.Domain;

namespace KChess.Core.TurnUtility
{
    public interface ITurnSetter
    {
        void SetTurn(PieceColor pieceColor);
    }
}