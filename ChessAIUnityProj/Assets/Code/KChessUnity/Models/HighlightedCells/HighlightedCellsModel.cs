using System;
using System.Collections.Generic;
using KChess.Domain.Impl;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.Models
{
    public class HighlightedCellsModel : IHighlightedCellsMutableModel
    {

        private readonly IMutable<IEnumerable<BoardCoordinates>> _highlightedCells =
            new Mutable<IEnumerable<BoardCoordinates>>(Array.Empty<BoardCoordinates>());

        public IBindable<IEnumerable<BoardCoordinates>> HighlightedCells => _highlightedCells;
        
        public void SetHighlightedCells(IEnumerable<BoardCoordinates> cells)
        {
            _highlightedCells.Value = cells;
        }
        
    }
}