using ChessAI.Domain;

namespace ChessAI.Core.BoardStateUtils
{
    public interface IBoardStateGetter
    {
        BoardState Get();
    }
}