using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.AttackedCellsUtility
{
    public interface IAttackedCellsUtility
    {
        bool IsCellAttacked(BoardCoordinates coordinates, PieceColor attackingColor);
    }
}