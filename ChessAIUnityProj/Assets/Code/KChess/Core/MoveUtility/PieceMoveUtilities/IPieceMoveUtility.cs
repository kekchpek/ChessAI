using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities
{
    internal interface IPieceMoveUtility
    {
        BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color);
        BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor);
    }
}