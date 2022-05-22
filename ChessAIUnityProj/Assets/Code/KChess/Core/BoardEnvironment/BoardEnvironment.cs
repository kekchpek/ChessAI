using KChess.Core.API.PlayerFacade;

namespace KChess.Core.BoardEnvironment
{
    public readonly struct BoardEnvironment : IBoardEnvironment
    {
        public IPlayerFacade BlackPlayerFacade { get; }
        public IPlayerFacade WhitePlayerFacade { get; }

        public void Dispose()
        {
        }
    }
}