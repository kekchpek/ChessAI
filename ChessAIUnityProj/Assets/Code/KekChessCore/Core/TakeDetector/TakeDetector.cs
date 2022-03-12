using System.Linq;
using KekChessCore.Domain;
using KekChessCore.EnPassantUtility;

namespace KekChessCore.TakeDetector
{
    public class TakeDetector : ITakeDetector
    {
        private readonly IBoard _board;
        private readonly IEnPassantUtility _enPassantUtility;

        public TakeDetector(IBoard board, IEnPassantUtility enPassantUtility)
        {
            _board = board;
            _enPassantUtility = enPassantUtility;
            board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece piece)
        {
            var newPiecePosition = piece.Position;
            var takenPiece = _board.Pieces.FirstOrDefault(x => x.Position == newPiecePosition && x != piece);
            if (takenPiece != null)
            {
                _board.RemovePiece(takenPiece);
            }
            else
            {
                if (_enPassantUtility.IsFigureWasTakenWithEnPassant(piece, _board, out var enPassantTakenPiece))
                {
                    _board.RemovePiece(enPassantTakenPiece);
                }
            }
        }
        
    }
}