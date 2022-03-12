using ChessAI.Domain;

namespace ChessAI.Core.EnPassantUtility
{
    public interface IEnPassantUtility
    {
        bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece);
    }
}