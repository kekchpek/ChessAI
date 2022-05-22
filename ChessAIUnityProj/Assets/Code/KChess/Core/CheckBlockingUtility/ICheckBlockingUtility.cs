using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.CheckBlockingUtility
{
    public interface ICheckBlockingUtility
    {
        BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer);
    }
}