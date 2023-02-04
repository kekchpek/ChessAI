using KChess.Core.API.PlayerFacade;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.GameScreen.Payload
{
    public interface IGameScreenPayload : IPayload
    {
        IPlayerFacade PlayerFacade { get; }
    }
}