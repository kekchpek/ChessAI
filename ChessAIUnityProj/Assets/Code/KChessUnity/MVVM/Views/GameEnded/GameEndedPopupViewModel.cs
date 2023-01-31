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
        private readonly IStartupService _startupService;

        private readonly IMutable<string> _winText = new Mutable<string>();
        public IBindable<string> WinText => _winText;

        public GameEndedPopupViewModel(
            IGameEndedPopupPayload payload,
            IStartupService startupService)
        {
            _payload = payload;
            _startupService = startupService;
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
            _startupService.StartSingleGame();
        }
    }
}