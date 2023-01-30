using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.AttackedCellsUtility
{
    internal interface IAttackedCellsUtility
    {
        bool IsCellAttacked(BoardCoordinates coordinates, PieceColor attackingColor);
    }
}