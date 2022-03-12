using System;
using ChessAI.Domain;

namespace ChessAI.Core.BoardStateUtils
{
    public interface IBoardStateObserver
    {
        event Action<BoardState> StateChanged;
    }
}