using System.Collections.Generic;
using KChess.Domain.Impl;

namespace KChessUnity.Models
{
    public interface IHighlightedCellsService
    {
        
        void SetHighlightedCells(IEnumerable<BoardCoordinates> cells);

        void ClearHighlightedCells();

    }
}