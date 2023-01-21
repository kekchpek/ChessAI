using System.Collections.Generic;
using KChess.Domain.Impl;

namespace KChessUnity.Models.HighlightedCells
{
    public interface IHighlightedCellsService
    {
        
        void SetHighlightedCells(IEnumerable<BoardCoordinates> cells);

        void ClearHighlightedCells();

    }
}