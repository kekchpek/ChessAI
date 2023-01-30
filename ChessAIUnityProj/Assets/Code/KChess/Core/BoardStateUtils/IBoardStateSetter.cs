using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    internal interface IBoardStateSetter
    {
        void Set(BoardState boardState);
    }
}