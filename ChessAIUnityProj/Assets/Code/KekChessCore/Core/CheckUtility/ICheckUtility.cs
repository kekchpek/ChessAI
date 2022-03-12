using System.Collections.Generic;
using ChessAI.Domain;

namespace ChessAI.Core.CheckUtility
{
    public interface ICheckUtility
    {
        bool IsPositionWithCheck(out PieceColor checkedColor);

        IReadOnlyList<IPiece> GetCheckingPieces();
    }
}