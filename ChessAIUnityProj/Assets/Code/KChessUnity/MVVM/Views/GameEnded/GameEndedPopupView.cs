using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;

namespace KChessUnity.MVVM.Views.GameEnded
{
    public class GameEndedPopupView : ViewBehaviour<IGameEndedPopupViewModel>
    {

        [SerializeField] private TMP_Text _winText;
        [SerializeField] private Button _restartButton;

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.WinText.Bind(OnTextChanged);
            _restartButton.onClick.AddListener(() => ViewModel.OnRestartClicked());
        }

        private void OnTextChanged(string s)
        {
            _winText.text = s;
        }

        protected override void OnViewModelClear()
        {
            base.OnViewModelClear();
            ViewModel.WinText.Unbind(OnTextChanged);
            _restartButton.onClick.RemoveAllListeners();
        }
        
    }
}
