using ChessAI.Domain;

namespace ChessAI.Core.LastMovedPieceUtils
{
    public interface ILastMovedPieceGetter
    {
        IPiece GetLastMovedPiece();
    }
}