using System;
using KChess.Domain;

namespace KChess.Core.LastMovedPieceUtils
{
    internal class LastMovedPieceUtility : 
        ILastMovedPieceGetter, 
        ILastMovedPieceObserver, 
        ILastMovedPieceSetter
    {
        public event Action<IPiece> LastMovedPieceChanged;

        private readonly IBoard _board;

        private IPiece _lastMovedPiece;

        public IPiece GetLastMovedPiece() => _lastMovedPiece;
        
        public void SetLastMovedPiece(IPiece piece)
        {
            _lastMovedPiece = piece;
            LastMovedPieceChanged?.Invoke(piece);
        }
    }
}