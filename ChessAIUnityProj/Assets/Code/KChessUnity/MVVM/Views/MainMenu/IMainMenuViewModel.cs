using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.MainMenu
{
    public interface IMainMenuViewModel : IViewModel
    {
        void OnStartSingleBtnClicked();
        void OnReviewGameBtnClicked();
    }
}