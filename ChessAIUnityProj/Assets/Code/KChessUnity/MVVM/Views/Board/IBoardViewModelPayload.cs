using KChess.Core.API.PlayerFacade;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.Board
{
    public interface IBoardViewModelPayload : IPayload
    {
        IPlayerFacade PlayerFacade { get; }
    }
}