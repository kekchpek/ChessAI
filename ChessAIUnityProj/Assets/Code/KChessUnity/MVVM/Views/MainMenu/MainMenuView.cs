using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityMVVM;

namespace KChessUnity.MVVM.Views.MainMenu
{
    public class MainMenuView : ViewBehaviour<IMainMenuViewModel>
    {

        [SerializeField] private Button _startSingleBtn;
        [SerializeField] private Button _reviewGameBtn;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _startSingleBtn.onClick.AddListener(() => ViewModel.OnStartSingleBtnClicked());
            _reviewGameBtn.onClick.AddListener(() => ViewModel.OnReviewGameBtnClicked());
        }

        protected override void OnViewModelClear()
        {
            base.OnViewModelClear();
            _startSingleBtn.onClick.RemoveAllListeners();
            _reviewGameBtn.onClick.RemoveAllListeners();
        }
    }
}
