using System;
using KChess.Domain;

namespace KChess.Core.BoardStateUtils
{
    internal interface IBoardStateObserver
    {
        event Action<BoardState> StateChanged;
    }
}