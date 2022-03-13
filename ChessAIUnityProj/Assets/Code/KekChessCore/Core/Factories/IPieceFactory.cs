using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.Factories
{
    public interface IPieceFactory
    {
        IPiece Create(PieceType pieceType, PieceColor pieceColor,
            BoardCoordinates position, IBoard boardToPlace);

        IPiece Copy(IPiece piece, IBoard boardToPlace);

    }
}