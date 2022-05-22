using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    public interface IBoardStateGetter
    {
        BoardState Get();
    }
}