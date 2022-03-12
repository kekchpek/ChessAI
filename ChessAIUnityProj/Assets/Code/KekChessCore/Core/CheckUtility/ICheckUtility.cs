using System.Collections.Generic;
using KekChessCore.Domain;

namespace KekChessCore.CheckUtility
{
    public interface ICheckUtility
    {
        bool IsPositionWithCheck(out PieceColor checkedColor);

        IReadOnlyList<IPiece> GetCheckingPieces();
    }
}