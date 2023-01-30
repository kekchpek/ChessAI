using System;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.Factories
{
    internal class PieceFactory : IPieceFactory
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
            if (piece.Position.HasValue)
            {
                var createdPiece = new Piece(piece.Type, piece.Position.Value, piece.Color);
                boardToPlace.PlacePiece(createdPiece);
                return createdPiece;
            }
            else
            {
                throw new InvalidOperationException("Can not copy piece removed from a board.");
            }
        }
    }
}