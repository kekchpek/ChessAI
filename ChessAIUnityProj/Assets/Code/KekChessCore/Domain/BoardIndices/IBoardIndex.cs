using KekChessCore.Domain.Impl;

namespace KekChessCore.Domain.BoardIndices
{
    public interface IBoardIndex
    {
        IPiece GetPieceOn(BoardCoordinates boardCoordinates);
    }
}