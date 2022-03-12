using KekChessCore.Domain;

namespace KekChessCore.BoardStateUtils
{
    public interface IBoardStateSetter
    {
        void Set(BoardState boardState);
    }
}