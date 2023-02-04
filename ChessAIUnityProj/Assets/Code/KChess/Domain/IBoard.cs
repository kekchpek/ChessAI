using System;
using System.Collections.Generic;

namespace KChess.Domain
{
    public interface IBoard
    {
        event Action<IPiece> Updated;
        public event Action<IPiece> PieceAddedOnBoard;
        
        IReadOnlyList<IPiece> Pieces { get; }

        void RemovePiece(IPiece piece);

        void PlacePiece(IPiece piece);

        int GetPositionHash();
    }
}
