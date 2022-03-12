using System;
using KekChessCore.Domain;

namespace KekChessCore.LastMovedPieceUtils
{
    public interface ILastMovedPieceObserver
    {
        event Action<IPiece> LastMovedPieceChanged;
    }
}