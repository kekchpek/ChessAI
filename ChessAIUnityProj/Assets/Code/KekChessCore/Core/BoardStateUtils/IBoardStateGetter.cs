using KekChessCore.Domain;

namespace KekChessCore.BoardStateUtils
{
    public interface IBoardStateGetter
    {
        BoardState Get();
    }
}