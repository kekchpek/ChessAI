using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility
{
    public interface IPieceMoveUtilityFacade
    {
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
        BoardCoordinates[] GetAttackedCells(IPiece piece);
    }
}