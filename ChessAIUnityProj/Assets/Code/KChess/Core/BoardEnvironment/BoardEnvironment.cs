using KChess.Core.API.PlayerFacade;

namespace KChess.Core.BoardEnvironment
{
    public readonly struct BoardEnvironment : IBoardEnvironment
    {
        public IPlayerFacade BlackPlayerFacade => _blackPlayerFacade;
        public IPlayerFacade WhitePlayerFacade => _whitePlayerFacade;

        private readonly IManagedPlayerFacade _whitePlayerFacade;
        private readonly IManagedPlayerFacade _blackPlayerFacade;

        private readonly IBoardEnvironmentComponent[] _components;

        public BoardEnvironment(IManagedPlayerFacade blackPlayer, IManagedPlayerFacade whitePlayer,
            params IBoardEnvironmentComponent[] components)
        {
            _blackPlayerFacade = blackPlayer;
            _whitePlayerFacade = whitePlayer;
            _components = components;
        }
        
        public void Dispose()
        {
            foreach (var component in _components)
            {
                component.Dispose();
            }
            _whitePlayerFacade.Dispose();
            _blackPlayerFacade.Dispose();
        }
    }
}