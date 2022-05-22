using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.CastleMoveUtility
{
    public interface ICastleMoveUtility
    {
        BoardCoordinates[] GetCastleMoves(PieceColor color);
    }
}