using System.Collections.Generic;
using KChess.Domain.Impl;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.Models.HighlightedCells
{
    public interface IHighlightedCellsModel
    {
        
        IBindable<IEnumerable<BoardCoordinates>> HighlightedCells { get; }

    }
}