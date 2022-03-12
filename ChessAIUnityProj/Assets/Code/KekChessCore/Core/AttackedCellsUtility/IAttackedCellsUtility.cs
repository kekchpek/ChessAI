using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.AttackedCellsUtility
{
    public interface IAttackedCellsUtility
    {
        bool IsCellAttacked(BoardCoordinates coordinates, PieceColor attackingColor);
    }
}