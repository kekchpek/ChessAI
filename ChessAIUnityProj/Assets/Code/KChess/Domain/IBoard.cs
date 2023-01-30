using System;
using System.Collections.Generic;

namespace KChess.Domain
{
    internal interface IBoard
    {
        event Action<IPiece> Updated;
        public event Action<IPiece> PieceAddedOnBoard;
        
        IReadOnlyList<IPiece> Pieces { get; }

        void RemovePiece(IPiece piece);

        internal void PlacePiece(IPiece piece);
    }
}
