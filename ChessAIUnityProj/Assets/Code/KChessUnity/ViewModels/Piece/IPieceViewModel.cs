using System;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.ViewModels.Piece
{
    public interface IPieceViewModel : IViewModel
    {
        event Action Disposed;
        Vector3 Position { get; }
        Sprite Image { get; }
        void Initialize();
    }
}