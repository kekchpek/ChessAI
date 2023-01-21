using System.Collections.Generic;
using System.Linq;
using KChess.Domain.Impl;
using KChessUnity.Models.HighlightedCells;
using KChessUnity.MVVM.Common.BoardPositioning;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;
using Zenject;

namespace KChessUnity.MVVM.Views.MovesDisplayer
{
    public class MovesDisplayerViewModel : ViewModel, IInitializable, IMovesDisplayerViewModel
    {
        private readonly IHighlightedCellsModel _highlightedCellsModel;
        private readonly IBoardPositionsCalculator _boardPositionsCalculator;

        private Mutable<IReadOnlyCollection<Vector3>> _highlightedPositions = new();

        public IBindable<IReadOnlyCollection<Vector3>> HighlightedPositions => _highlightedPositions;

        public MovesDisplayerViewModel(IHighlightedCellsModel highlightedCellsModel, 
            IBoardPositionsCalculator boardPositionsCalculator)
        {
            _highlightedCellsModel = highlightedCellsModel;
            _boardPositionsCalculator = boardPositionsCalculator;
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
                highlightedMoves[i] = _boardPositionsCalculator.GetCellCoordsOnBoardRect(array[i]);
            }
            _highlightedPositions.Value = highlightedMoves;
        }
    }
}