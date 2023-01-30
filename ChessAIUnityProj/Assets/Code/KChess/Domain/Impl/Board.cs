using System;
using System.Collections.Generic;
using System.Linq;

namespace KChess.Domain.Impl
{
    public class Board : IBoard
    {
        private event Action<IPiece> Updated;

        event Action<IPiece> IBoard.Updated
        {
            add => Updated += value;
            remove => Updated -= value;
        }
        
        public event Action<IPiece> PieceAddedOnBoard;

        public IReadOnlyList<IPiece> Pieces => _pieces;

        private readonly List<IPiece> _pieces = new();

        private readonly Dictionary<IPiece, Action> _pieceMoveCallbacks = new();
        
        public void RemovePiece(IPiece piece)
        {
            if (_pieces.Contains(piece))
            {
                _pieces.Remove(piece);
                piece.Moved -= _pieceMoveCallbacks[piece];
                piece.Remove();
                _pieceMoveCallbacks.Remove(piece);
                Updated?.Invoke(piece);
            }
        }

        void IBoard.PlacePiece(IPiece piece)
        {
            var pieceOnSamePosition = _pieces.FirstOrDefault(x => x.Position == piece.Position);
            if (pieceOnSamePosition != null)
            {
                RemovePiece(pieceOnSamePosition);
            }
            _pieces.Add(piece);
            _pieceMoveCallbacks.Add(piece, () =>
            {
                Updated?.Invoke(piece);
            });
            piece.Moved += _pieceMoveCallbacks[piece];
            PieceAddedOnBoard?.Invoke(piece);
            Updated?.Invoke(piece);
        }
    }
}