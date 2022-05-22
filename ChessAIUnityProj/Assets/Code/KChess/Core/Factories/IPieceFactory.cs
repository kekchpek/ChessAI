using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.Factories
{
    public interface IPieceFactory
    {
        IPiece Create(PieceType pieceType, PieceColor pieceColor,
            BoardCoordinates position, IBoard boardToPlace);

        IPiece Copy(IPiece piece, IBoard boardToPlace);

    }
}