using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.MoveUtility
{
    public interface IPieceMoveUtilityFacade
    {
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}