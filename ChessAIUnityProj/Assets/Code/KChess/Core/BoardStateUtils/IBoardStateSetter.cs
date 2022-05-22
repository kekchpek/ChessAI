using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    public interface IBoardStateSetter
    {
        void Set(BoardState boardState);
    }
}