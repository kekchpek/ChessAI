using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessAI.Domain.Impl
{
    public class Board : IBoard
    {
        public event Action<IPiece> PositionChanged;
        public event Action<IPiece> PieceMoved;

        public IReadOnlyList<IPiece> Pieces => _pieces;

        private readonly List<IPiece> _pieces = new List<IPiece>();

        private readonly Dictionary<IPiece, Action> _pieceMoveCallbacks = new Dictionary<IPiece, Action>();
        
        public void RemovePiece(IPiece piece)
        {
            _pieces.Remove(piece);
            piece.Moved -= _pieceMoveCallbacks[piece];
            _pieceMoveCallbacks.Remove(piece);
            PositionChanged?.Invoke(piece);
        }

        public void PlacePiece(IPiece piece)
        {
            var pieceOnSamePosition = _pieces.FirstOrDefault(x => x.Position == piece.Position);
            if (pieceOnSamePosition != null)
            {
                RemovePiece(pieceOnSamePosition);
            }
            _pieces.Add(piece);
            _pieceMoveCallbacks.Add(piece, () => PieceMoved?.Invoke(piece));
            piece.Moved += _pieceMoveCallbacks[piece];
            
            PositionChanged?.Invoke(piece);
        }
    }
}