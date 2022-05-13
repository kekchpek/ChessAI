using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.CastleMoveUtility
{
    public interface ICastleMoveUtility
    {
        BoardCoordinates[] GetCastleMoves(PieceColor color);
    }
}