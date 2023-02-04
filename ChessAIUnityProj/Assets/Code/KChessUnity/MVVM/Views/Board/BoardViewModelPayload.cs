using KChess.Core.API.PlayerFacade;

namespace KChessUnity.MVVM.Views.Board
{
    public class BoardViewModelPayload : IBoardViewModelPayload
    {
        public IPlayerFacade PlayerFacade { get; }

        public BoardViewModelPayload(IPlayerFacade playerFacade)
        {
            PlayerFacade = playerFacade;
        }
    }
}