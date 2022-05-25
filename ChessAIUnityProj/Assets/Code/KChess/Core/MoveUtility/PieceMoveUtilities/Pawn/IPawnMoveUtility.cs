using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Pawn
{
    public interface IPawnMoveUtility : IPieceMoveUtility
    {
        BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor);
    }
}