using System;
using System.Collections.Generic;
using System.Linq;
using KChess.Domain.Impl;
using KChessUnity.Models;
using KChessUnity.Models.Board;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;
using Zenject;

namespace KChessUnity.ViewModels.MovesDisplayer
{
    public class MovesDisplayerViewModel : ViewModel, IInitializable, IMovesDisplayerViewModel
    {
        private readonly IHighlightedCellsModel _highlightedCellsModel;
        private readonly IBoardWorldPositionsCalculator _boardWorldPositionsCalculator;

        private Mutable<IReadOnlyCollection<Vector3>> _highlightedPositions = new();

        public IBindable<IReadOnlyCollection<Vector3>> HighlightedPositions => _highlightedPositions;

        public MovesDisplayerViewModel(IHighlightedCellsModel highlightedCellsModel, IMovesDisplayerPayload payload)
        {
            _highlightedCellsModel = highlightedCellsModel;
            _boardWorldPositionsCalculator = payload.BoardWorldPositionsCalculator;
        }

        public void Initialize()
        {
            _highlightedCellsModel.HighlightedCells.Bind(ShowMoves);
        }
        
        private void ShowMoves(IEnumerable<BoardCoordinates> availableMoves)
        {
            var array = availableMoves.ToArray();
            var highlightedMoves = new Vector3[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                highlightedMoves[i] = _boardWorldPositionsCalculator.GetWorldPosition(array[i]);
            }
            _highlightedPositions.Value = highlightedMoves;
        }
    }
}