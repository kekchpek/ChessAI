using System;
using System.Collections.Generic;

namespace KekChessCore.Domain
{
    public interface IBoard
    {
        event Action<IPiece> PositionChanged;
        event Action<IPiece> PieceMoved;
        
        IReadOnlyList<IPiece> Pieces { get; }

        void RemovePiece(IPiece piece);

        void PlacePiece(IPiece piece);
    }
}
