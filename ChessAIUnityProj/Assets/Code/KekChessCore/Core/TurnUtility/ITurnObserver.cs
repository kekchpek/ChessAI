using System;
using ChessAI.Domain;

namespace ChessAI.Core.TurnUtility
{
    public interface ITurnObserver
    {
        event Action<PieceColor> TurnChanged;
    }
}