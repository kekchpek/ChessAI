using KChess.Domain;

namespace KChess.Core.EnPassantUtility
{
    internal interface IEnPassantUtility
    {
        bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece);
    }
}