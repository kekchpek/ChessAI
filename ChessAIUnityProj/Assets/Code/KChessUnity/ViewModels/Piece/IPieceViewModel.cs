using System;
using MVVMCore;
using UnityEngine;
using Zenject;

namespace KChessUnity.ViewModels.Piece
{
    public interface IPieceViewModel : IViewModel, IInitializable
    {
        event Action Disposed;
        Vector3 Position { get; }
        Sprite Image { get; }
    }
}