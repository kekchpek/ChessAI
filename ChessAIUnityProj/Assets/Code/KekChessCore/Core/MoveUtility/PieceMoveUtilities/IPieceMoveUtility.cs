using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.MoveUtility.PieceMoveUtilities
{
    public interface IPieceMoveUtility
    {
        BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color);
    }
}