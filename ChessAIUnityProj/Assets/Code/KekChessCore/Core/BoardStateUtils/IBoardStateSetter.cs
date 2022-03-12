using ChessAI.Domain;

namespace ChessAI.Core.BoardStateUtils
{
    public interface IBoardStateSetter
    {
        void Set(BoardState boardState);
    }
}