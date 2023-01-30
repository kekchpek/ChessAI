using KChess.Domain;

namespace KChess.Core.LastMovedPieceUtils
{
    internal interface ILastMovedPieceSetter
    {
        void SetLastMovedPiece(IPiece piece);
    }
}