using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;
using Zenject;

namespace KChessUnity.MVVM.Views.Piece
{
    public interface IPieceViewModel : IViewModel, IInitializable
    {
        IBindable<Vector3> Position { get; }
        IBindable<Sprite> Image { get; }

        void OnPieceClicked();
        void OnPieceDragStart();
        void OnPieceDragEnd(Vector2 rectCoords);
    }
}