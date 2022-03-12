using KekChessCore.Domain;

namespace KekChessCore.LastMovedPieceUtils
{
    public interface ILastMovedPieceGetter
    {
        IPiece GetLastMovedPiece();
    }
}