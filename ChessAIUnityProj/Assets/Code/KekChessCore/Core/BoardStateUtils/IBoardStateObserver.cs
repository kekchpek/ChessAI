using System;
using KekChessCore.Domain;

namespace KekChessCore.BoardStateUtils
{
    public interface IBoardStateObserver
    {
        event Action<BoardState> StateChanged;
    }
}