using System;

namespace KekChessCore.Domain.Impl
{
    public class Piece : IPiece
    {
        public event Action Moved;
        
        public PieceType Type => _pieceType;
        public BoardCoordinates Position => _position;
        public PieceColor Color => _color;
        public BoardCoordinates PreviousPosition => _previousPosition;
        public bool IsMoved => _isMoved;

        private readonly PieceType _pieceType;
        private readonly PieceColor _color;

        private BoardCoordinates _position;
        private BoardCoordinates _previousPosition;
        private bool _isMoved;

        public Piece(PieceType pieceType, BoardCoordinates position, PieceColor color)
        {
            _pieceType = pieceType;
            _color = color;
            _previousPosition = position;
            _position = position;
        }

        void IPiece.MoveTo(BoardCoordinates boardCoordinates)
        {
            _previousPosition = _position;
            _position = boardCoordinates;
            _isMoved = true;
            Moved?.Invoke();
        }
    }
}