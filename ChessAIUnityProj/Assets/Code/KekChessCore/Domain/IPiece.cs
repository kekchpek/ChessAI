using System;
using ChessAI.Domain.Impl;

namespace ChessAI.Domain
{
    public interface IPiece
    {

        event Action Moved;
        
        PieceType Type { get; }
        BoardCoordinates Position { get; }
        PieceColor Color { get; }
        /// <summary>
        /// Equals to <see cref="Position"/> if there was no any move.
        /// </summary>
        BoardCoordinates PreviousPosition { get; }
        bool IsMoved { get; }

        internal void MoveTo(BoardCoordinates boardCoordinates);
    }
}