using KekChessCore.Domain;

namespace KekChessCore.EnPassantUtility
{
    public interface IEnPassantUtility
    {
        bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece);
    }
}