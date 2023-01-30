using System;
using KChess.Core.Factories;
using KChess.Domain;

namespace KChess.Core.PawnTransformation
{
    internal class PawnTransformationUtility : IPawnTransformationUtility
    {
        public event Action<IPiece> TransformationBecomesRequired;
        
        private readonly IBoard _board;
        private readonly IPieceFactory _pieceFactory;

        private IPiece _transformingPiece;

        public PawnTransformationUtility(IBoard board, IPieceFactory pieceFactory)
        {
            _board = board;
            _pieceFactory = pieceFactory;
        }

        public IPiece GetTransformingPiece()
        {
            return _transformingPiece;
        }

        public bool TryTransform(PawnTransformationVariant transformationVariant)
        {
            if (_transformingPiece is not {Position: { }})
                return false;
            _pieceFactory.Create(MapVariantToPieceType(transformationVariant), _transformingPiece.Color,
                _transformingPiece.Position.Value, _board);
            _board.RemovePiece(_transformingPiece);
            _transformingPiece = null;
            return true;
        }
        
        public void UpdateTransformingPiece(IPiece piece)
        {
            if (piece.Position.HasValue && piece.Type == PieceType.Pawn)
            {
                var position = piece.Position.Value.ToNumeric();
                if (piece.Color == PieceColor.White && position.Item2 == 7 ||
                    piece.Color == PieceColor.Black && position.Item2 == 0)
                {
                    SetTransformingPiece(piece);
                }
            }
        }
        
        private void SetTransformingPiece(IPiece piece)
        {
            _transformingPiece = piece;
            TransformationBecomesRequired?.Invoke(piece);
        }
        
        private static PieceType MapVariantToPieceType(PawnTransformationVariant variant)
        {
            return variant switch
            {
                PawnTransformationVariant.Knight => PieceType.Knight,
                PawnTransformationVariant.Bishop => PieceType.Bishop,
                PawnTransformationVariant.Rook => PieceType.Rook,
                PawnTransformationVariant.Queen => PieceType.Queen,
                _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
            };
        }
    }
}