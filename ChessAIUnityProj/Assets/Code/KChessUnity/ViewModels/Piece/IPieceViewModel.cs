using System;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;
using Zenject;

namespace KChessUnity.ViewModels.Piece
{
    public interface IPieceViewModel : IViewModel, IInitializable
    {
        event Action Disposed;
        IBindable<Vector3> Position { get; }
        IBindable<Sprite> Image { get; }
    }
}