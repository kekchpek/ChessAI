using KChess.Core.API.PlayerFacade;

namespace KChessUnity.ViewModels.Board
{
    public class BoardViewModelPayload : IBoardViewModelPayload
    {
        public IPlayerFacade WhitePlayerFacade { get; }
        public IPlayerFacade BlackPlayerFacade { get; }

        public BoardViewModelPayload(IPlayerFacade whitePlayerFacade, IPlayerFacade blackPlayerFacade)
        {
            WhitePlayerFacade = whitePlayerFacade;
            BlackPlayerFacade = blackPlayerFacade;
        }
    }
}