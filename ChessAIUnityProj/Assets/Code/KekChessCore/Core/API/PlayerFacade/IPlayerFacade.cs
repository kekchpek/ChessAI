using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.API.PlayerFacade
{
    public interface IPlayerFacade
    {
        bool TryMovePiece();

        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}