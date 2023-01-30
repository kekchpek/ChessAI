using System.Linq;
using KChess.Core.EnPassantUtility;
using KChess.Domain;

namespace KChess.Core.Taking
{
    internal class TakeUtility : ITakeUtility
    {
        private readonly IBoard _board;
        private readonly IEnPassantUtility _enPassantUtility;

        public TakeUtility(IBoard board, IEnPassantUtility enPassantUtility)
        {
            _board = board;
            _enPassantUtility = enPassantUtility;
        }

        public void TryTake(IPiece piece)
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