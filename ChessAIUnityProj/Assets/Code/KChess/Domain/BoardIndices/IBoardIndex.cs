using KChess.Domain.Impl;

namespace KChess.Domain.BoardIndices
{
    public interface IBoardIndex
    {
        IPiece GetPieceOn(BoardCoordinates boardCoordinates);
    }
}