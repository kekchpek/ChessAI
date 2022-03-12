using ChessAI.Core.MoveUtility;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.Factories
{
    public interface IPieceFactory
    {
        IPiece Create(IMoveUtility moveUtility, PieceType pieceType,
            PieceColor pieceColor, BoardCoordinates position, IBoard boardToPlace);

        IPiece Copy(IPiece piece, IBoard baordToPlace);

    }
}