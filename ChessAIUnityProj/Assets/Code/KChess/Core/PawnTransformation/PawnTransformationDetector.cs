using KChess.Core.BoardEnvironment;
using KChess.Domain;

namespace KChess.Core.PawnTransformation
{
    public class PawnTransformationDetector : IBoardEnvironmentComponent
    {
        private readonly IPawnTransformationUtilityWrite _pawnTransformationUtilityWrite;
        private readonly IBoard _board;

        internal PawnTransformationDetector(
            IPawnTransformationUtilityWrite pawnTransformationUtilityWrite, IBoard board)
        {
            _pawnTransformationUtilityWrite = pawnTransformationUtilityWrite;
            _board = board;

            _board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece piece)
        {
            if (piece.Position.HasValue && piece.Type == PieceType.Pawn)
            {
                var position = piece.Position.Value.ToNumeric();
                if (piece.Color == PieceColor.White && position.Item2 == 7 ||
                    piece.Color == PieceColor.Black && position.Item2 == 0)
                {
                    _pawnTransformationUtilityWrite.SetTransformingPiece(piece);
                }
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}