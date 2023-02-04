using KChessUnity.Core;
using KChessUnity.MVVM.Views.Board;
using KChessUnity.MVVM.Views.GameScreen.Payload;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace KChessUnity.MVVM.Views.GameScreen
{
    public class GameScreenViewModel : ViewModel, IGameScreenViewModel, IInitializable
    {
        private readonly IGameScreenPayload _payload;

        public GameScreenViewModel(IGameScreenPayload payload)
        {
            _payload = payload;
        }
        
        public void Initialize()
        {
            CreateSubView(ViewNames.Board, new BoardViewModelPayload(_payload.PlayerFacade));
        }
    }
}