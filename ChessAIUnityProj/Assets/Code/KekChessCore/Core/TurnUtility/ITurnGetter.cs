using KekChessCore.Domain;

namespace KekChessCore.TurnUtility
{
    public interface ITurnGetter
    {
        PieceColor GetTurn();
    }
}