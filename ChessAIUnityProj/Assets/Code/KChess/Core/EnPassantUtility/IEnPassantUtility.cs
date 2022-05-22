using KChess.Domain;

namespace KChess.Core.EnPassantUtility
{
    public interface IEnPassantUtility
    {
        bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece);
    }
}