﻿using System.Collections.Generic;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.MVVM.Views.MovesDisplayer
{
    public interface IMovesDisplayerViewModel : IViewModel
    {
        IBindable<IReadOnlyCollection<Vector3>> HighlightedPositions { get; }
    }
}