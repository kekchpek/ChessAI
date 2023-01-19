using System;
using System.Collections.Generic;
using KChess.Domain.Impl;

namespace KChessUnity.Models
{
    public class HighlightedCellsService : IHighlightedCellsService
    {
        private readonly IHighlightedCellsMutableModel _highlightedCellsMutableModel;

        public HighlightedCellsService(IHighlightedCellsMutableModel highlightedCellsMutableModel)
        {
            _highlightedCellsMutableModel = highlightedCellsMutableModel;
        }
        
        public void SetHighlightedCells(IEnumerable<BoardCoordinates> cells)
        {
            _highlightedCellsMutableModel.SetHighlightedCells(cells);
        }

        public void ClearHighlightedCells()
        {
            _highlightedCellsMutableModel.SetHighlightedCells(Array.Empty<BoardCoordinates>());
        }
    }
}