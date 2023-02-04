using KChessUnity.Models.Startup;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.MainMenu
{
    public class MainMenuViewModel : ViewModel, IMainMenuViewModel
    {
        private readonly IStartGameService _startGameService;

        public MainMenuViewModel(IStartGameService startGameService)
        {
            _startGameService = startGameService;
        }
        
        public void OnStartSingleBtnClicked()
        {
            _startGameService.StartSingleGame();
        }

        public void OnReviewGameBtnClicked()
        {
            _startGameService.StartGameWithAI();
        }
    }
}