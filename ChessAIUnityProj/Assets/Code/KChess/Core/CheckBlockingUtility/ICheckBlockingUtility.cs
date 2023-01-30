using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.CheckBlockingUtility
{
    internal interface ICheckBlockingUtility
    {
        BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer);
    }
}