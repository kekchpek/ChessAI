using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.MoveUtility
{
    public interface IPieceMoveUtilityFacade
    {
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}