using KChess.Core.BoardEnvironment;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility
{
    public interface IPieceMoveUtilityFacade : IBoardEnvironmentComponent
    {
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}