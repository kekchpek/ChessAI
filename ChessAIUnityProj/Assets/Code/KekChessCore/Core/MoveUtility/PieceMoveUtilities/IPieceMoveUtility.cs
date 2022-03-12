using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.MoveUtility.PieceMoveUtilities
{
    public interface IPieceMoveUtility
    {
        BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color);
    }
}