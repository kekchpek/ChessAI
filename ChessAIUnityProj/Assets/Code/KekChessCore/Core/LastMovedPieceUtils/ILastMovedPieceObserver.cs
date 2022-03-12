using System;
using ChessAI.Domain;

namespace ChessAI.Core.LastMovedPieceUtils
{
    public interface ILastMovedPieceObserver
    {
        event Action<IPiece> LastMovedPieceChanged;
    }
}