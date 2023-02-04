using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.GameManagement.Domain
{
    public interface IMove
    {
        PieceType Piece { get; }
        PieceColor Color { get; }
        BoardCoordinates PositionFrom { get; }
        BoardCoordinates PositionTo { get; }
        PieceType? TakenPiece { get; }
        PawnTransformationVariant? PawnTransformationVariant { get; }
    }
}