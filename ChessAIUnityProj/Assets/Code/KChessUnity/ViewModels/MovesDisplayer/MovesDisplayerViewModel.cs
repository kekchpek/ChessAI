using System;
using System.Collections.Generic;
using KChess.Domain.Impl;
using KChessUnity.ViewModels.Board;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.ViewModels.MovesDisplayer
{
    public class MovesDisplayerViewModel : ViewModel, IMovesDisplayerViewModel
    {
        private readonly IBoardViewModel _boardViewModel;

        private IReadOnlyCollection<Vector3> _highlightedPositions;

        public IReadOnlyCollection<Vector3> HighlightedPositions
        {
            get => _highlightedPositions;
            set => SetAndRaiseIfChanged(nameof(HighlightedPositions), value, ref _highlightedPositions);
        }

        public MovesDisplayerViewModel(IBoardViewModel boardViewModel)
        {
            _boardViewModel = boardViewModel;
        }
        
        public void ShowMoves(BoardCoordinates[] availableMoves)
        {
            var highlightedMoves = new Vector3[availableMoves.Length];
            for (var i = 0; i < availableMoves.Length; i++)
            {
                highlightedMoves[i] = _boardViewModel.GetWorldPosition(availableMoves[i]);
            }
            HighlightedPositions = highlightedMoves;
        }

        public void HideMoves()
        {
            HighlightedPositions = Array.Empty<Vector3>();
        }
    }
}