using System.Collections.Generic;
using KChess.Domain.Impl;

namespace KChessUnity.Models
{
    public interface IHighlightedCellsMutableModel : IHighlightedCellsModel
    {
        
        void SetHighlightedCells(IEnumerable<BoardCoordinates> cells);
        
    }
}