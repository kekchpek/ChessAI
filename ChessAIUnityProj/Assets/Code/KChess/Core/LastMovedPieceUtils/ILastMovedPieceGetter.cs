using KChess.Domain;

namespace KChess.Core.LastMovedPieceUtils
{
    public interface ILastMovedPieceGetter
    {
        IPiece GetLastMovedPiece();
    }
}