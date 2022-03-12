using KekChessCore.Domain;

namespace KekChessCore.TurnUtility
{
    public interface ITurnSetter
    {
        void SetTurn(PieceColor pieceColor);
    }
}