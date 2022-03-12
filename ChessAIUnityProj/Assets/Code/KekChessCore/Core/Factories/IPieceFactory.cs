using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.MoveUtility;

namespace KekChessCore.Factories
{
    public interface IPieceFactory
    {
        IPiece Create(IMoveUtility moveUtility, PieceType pieceType,
            PieceColor pieceColor, BoardCoordinates position, IBoard boardToPlace);

        IPiece Copy(IPiece piece, IBoard baordToPlace);

    }
}