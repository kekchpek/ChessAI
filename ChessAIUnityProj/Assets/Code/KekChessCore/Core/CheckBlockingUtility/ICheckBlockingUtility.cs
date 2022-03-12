using ChessAI.Domain;
using ChessAI.Domain.Impl;
using JetBrains.Annotations;

namespace ChessAI.Core.CheckBlockingUtility
{
    public interface ICheckBlockingUtility
    {
        BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer);
    }
}