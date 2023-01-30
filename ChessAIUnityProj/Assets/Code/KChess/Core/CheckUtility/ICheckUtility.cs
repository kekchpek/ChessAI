using System.Collections.Generic;
using KChess.Domain;

namespace KChess.Core.CheckUtility
{
    internal interface ICheckUtility
    {
        bool IsPositionWithCheck(out PieceColor checkedColor);

        IReadOnlyList<IPiece> GetCheckingPieces();
    }
}