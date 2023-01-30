using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.CastleMoveUtility
{
    internal interface ICastleMoveUtility
    {
        BoardCoordinates[] GetCastleMoves(PieceColor color);
    }
}