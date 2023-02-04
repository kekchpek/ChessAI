using System;
using KChess.Domain;
using KChessUnity.Models.Startup;
using KChessUnity.MVVM.Views.GameEnded.Payload;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;
using Zenject;

namespace KChessUnity.MVVM.Views.GameEnded
{
    public class GameEndedPopupViewModel : ViewModel, IGameEndedPopupViewModel, IInitializable
    {

        private readonly IGameEndedPopupPayload _payload;
        private readonly IStartGameService _startGameService;

        private readonly IMutable<string> _winText = new Mutable<string>();
        public IBindable<string> WinText => _winText;

        public GameEndedPopupViewModel(
            IGameEndedPopupPayload payload,
            IStartGameService startGameService)
        {
            _payload = payload;
            _startGameService = startGameService;
        }

        public void Initialize()
        {
            switch (_payload.State)
            {
                case BoardState.Regular:
                case BoardState.CheckToWhite:
                case BoardState.CheckToBlack:
                    Debug.LogError("Board state is not final!");
                    Destroy();
                    break;
                case BoardState.MateToWhite:
                    _winText.Value = "Black wins!";
                    break;
                case BoardState.MateToBlack:
                    _winText.Value = "White wins!";
                    break;
                case BoardState.Stalemate:
                    _winText.Value = "Stalemate!";
                    break;
                case BoardState.RepeatDraw:
                    _winText.Value = "Draw cause repeating!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void OnRestartClicked()
        {
            _startGameService.StartSingleGame();
        }
    }
}