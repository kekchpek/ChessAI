using System;
using KChess.Domain;

namespace KChess.Core.TurnUtility
{
    public interface ITurnObserver
    {
        event Action<PieceColor> TurnChanged;
    }
}