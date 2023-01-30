using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    internal interface IBoardStateGetter
    {
        BoardState Get();
    }
}