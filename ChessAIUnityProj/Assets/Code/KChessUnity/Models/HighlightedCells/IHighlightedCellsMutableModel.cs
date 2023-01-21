using System.Collections.Generic;
using KChess.Domain.Impl;

namespace KChessUnity.Models.HighlightedCells
{
    public interface IHighlightedCellsMutableModel : IHighlightedCellsModel
    {
        
        void SetHighlightedCells(IEnumerable<BoardCoordinates> cells);
        
    }
}