using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.CheckBlockingUtility
{
    public interface ICheckBlockingUtility
    {
        BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer);
    }
}