using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.Factories
{
    public class PieceFactory : IPieceFactory
    {
        public IPiece Create(PieceType pieceType, PieceColor pieceColor,
            BoardCoordinates position, IBoard boardToPlace)
        {
            var createdPiece = new Piece(pieceType, position, pieceColor);
            boardToPlace.PlacePiece(createdPiece);
            return createdPiece;
        }

        public IPiece Copy(IPiece piece, IBoard boardToPlace)
        {
            var createdPiece = new Piece(piece.Type, piece.Position, piece.Color);
            boardToPlace.PlacePiece(createdPiece);
            return createdPiece;
        }
    }
}