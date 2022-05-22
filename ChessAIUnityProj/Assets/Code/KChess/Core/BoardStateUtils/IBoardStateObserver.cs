using System;
using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    public interface IBoardStateObserver
    {
        event Action<BoardState> StateChanged;
    }
}