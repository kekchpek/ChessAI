using System;
using System.Collections.Generic;

namespace KChess.Domain
{
    public interface IBoard
    {
        event Action<IPiece> PositionChanged;
        event Action<IPiece> PieceMoved;
        
        IReadOnlyList<IPiece> Pieces { get; }

        void RemovePiece(IPiece piece);

        internal void PlacePiece(IPiece piece);
    }
}
