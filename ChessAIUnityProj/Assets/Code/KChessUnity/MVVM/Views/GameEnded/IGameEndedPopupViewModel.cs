using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.MVVM.Views.GameEnded
{
    public interface IGameEndedPopupViewModel : IViewModel
    {
        IBindable<string> WinText { get; }

        void OnRestartClicked();
    }
}