using KekChessCore.API.PlayerFacade;

namespace KekChessCore.BoardEnvironment
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