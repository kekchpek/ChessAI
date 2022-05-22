using System.Collections.Generic;
using KChess.Domain.Impl;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.ViewModels.MovesDisplayer
{
    public interface IMovesDisplayerViewModel : IViewModel
    {
        IReadOnlyCollection<Vector3> HighlightedPositions { get; }
        void ShowMoves(BoardCoordinates[] availableMoves);
        void HideMoves();
    }
}