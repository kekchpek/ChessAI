using System;
using KChess.Domain.Impl;

namespace KChess.Domain
{
    public interface IPiece
    {

        /// <summary>
        /// Fired every time, when piece position changed
        /// </summary>
        event Action Moved;

        /// <summary>
        /// Fired when the piece leaves board by any reason.
        /// </summary>
        event Action Removed;
        
        PieceType Type { get; }
        
        /// <summary>
        /// Equals to null if it is removed from a board.
        /// </summary>
        BoardCoordinates? Position { get; }
        
        PieceColor Color { get; }
        
        /// <summary>
        /// Equals to <see cref="Position"/> if there was no any move.
        /// </summary>
        BoardCoordinates PreviousPosition { get; }
        
        bool IsMoved { get; }

        internal void Remove();

        internal void MoveTo(BoardCoordinates boardCoordinates);
    }
}