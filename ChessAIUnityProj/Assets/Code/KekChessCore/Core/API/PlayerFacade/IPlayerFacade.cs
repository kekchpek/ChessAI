using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.API.PlayerFacade
{
    public interface IPlayerFacade
    {
        bool TryMovePiece();

        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}