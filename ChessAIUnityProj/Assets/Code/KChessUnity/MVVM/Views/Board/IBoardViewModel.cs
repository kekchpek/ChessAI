using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.Board
{
    public interface IBoardViewModel : IViewModel
    {

        void OnBoardClicked(Vector2 boardRectPosition);

    }
}