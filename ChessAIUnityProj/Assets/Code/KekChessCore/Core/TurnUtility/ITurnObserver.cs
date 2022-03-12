using System;
using KekChessCore.Domain;

namespace KekChessCore.TurnUtility
{
    public interface ITurnObserver
    {
        event Action<PieceColor> TurnChanged;
    }
}