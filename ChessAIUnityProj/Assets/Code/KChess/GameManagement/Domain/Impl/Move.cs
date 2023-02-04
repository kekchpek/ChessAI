using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.GameManagement.Domain.Impl
{
    public class Move : IMove
    {
        public PieceType Piece { get; }
        public PieceColor Color { get; }
        public BoardCoordinates PositionFrom { get; }
        public BoardCoordinates PositionTo { get; }
        public PieceType? TakenPiece { get; }
        public PawnTransformationVariant? PawnTransformationVariant { get; }

        public Move(PieceType piece, BoardCoordinates from, BoardCoordinates to, PieceColor color,
            PieceType? takenPiece = null, PawnTransformationVariant? pawnTransformationVariant = null)
        {
            Piece = piece;
            PositionFrom = from;
            PositionTo = to;
            Color = color;
            TakenPiece = takenPiece;
            PawnTransformationVariant = pawnTransformationVariant;
        }
    }
}