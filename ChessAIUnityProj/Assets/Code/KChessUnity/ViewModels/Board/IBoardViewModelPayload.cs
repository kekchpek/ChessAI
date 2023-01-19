using KChess.Core.API.PlayerFacade;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.ViewModels.Board
{
    public interface IBoardViewModelPayload : IPayload
    {
        IPlayerFacade WhitePlayerFacade { get; }
        IPlayerFacade BlackPlayerFacade { get; }
    }
}