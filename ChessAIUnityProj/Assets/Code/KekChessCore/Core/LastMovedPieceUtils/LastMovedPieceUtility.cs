using System;
using KekChessCore.Domain;

namespace KekChessCore.LastMovedPieceUtils
{
    public class LastMovedPieceUtility : ILastMovedPieceGetter, ILastMovedPieceObserver
    {

        public event Action<IPiece> LastMovedPieceChanged;

        private IPiece _lastMovedPiece;

        public LastMovedPieceUtility(IBoard board)
        {
            board.PieceMoved += OnPieceMoved;
        }

        public IPiece GetLastMovedPiece() => _lastMovedPiece;

        private void OnPieceMoved(IPiece piece)
        {
            _lastMovedPiece = piece;
            LastMovedPieceChanged?.Invoke(piece);
        }
    }
}