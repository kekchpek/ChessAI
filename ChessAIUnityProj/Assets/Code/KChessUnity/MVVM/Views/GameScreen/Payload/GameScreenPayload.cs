using KChess.Core.API.PlayerFacade;

namespace KChessUnity.MVVM.Views.GameScreen.Payload
{
    public class GameScreenPayload : IGameScreenPayload
    {
        public IPlayerFacade PlayerFacade { get; }

        public GameScreenPayload(IPlayerFacade playerFacade)
        {
            PlayerFacade = playerFacade;
        }
        
    }
}