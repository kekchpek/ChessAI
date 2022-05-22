using System;
using KChess.Domain;

namespace KChess.Core.LastMovedPieceUtils
{
    public interface ILastMovedPieceObserver
    {
        event Action<IPiece> LastMovedPieceChanged;
    }
}