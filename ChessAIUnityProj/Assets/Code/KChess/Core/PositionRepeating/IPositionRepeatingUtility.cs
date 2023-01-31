using KChess.Domain;

namespace KChess.Core.PositionRepeating
{
    internal interface IPositionRepeatingUtility
    {
        void IndexPosition(IBoard board);

        int GetPositionRepeatingTimes(IBoard board);
    }
}