using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.AttackedCellsUtility
{
    public interface IAttackedCellsUtility
    {
        bool IsCellAttacked(BoardCoordinates coordinates, PieceColor attackingColor);
    }
}