using System.Collections.Generic;
using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardEnvironment;
using KChess.Core.BoardEnvironment.Factory;
using KChess.Core.Factories;

namespace KChess.Core.API.BoardsManager
{
    public class BoardManager : IBoardsManager
    {
        private readonly IBoardEnvironmentFactory _boardEnvironmentFactory;

        private readonly IList<IBoardEnvironment> _boardEnvironments = new List<IBoardEnvironment>();

        public BoardManager()
        {
            var pieceFactory = new PieceFactory();
            var boardFactory = new BoardFactory(pieceFactory);
            _boardEnvironmentFactory = new BoardEnvironmentFactory(boardFactory, pieceFactory);
        }
        
        public void CreateBoard(out IPlayerFacade whitePlayer, out IPlayerFacade blackPlayer)
        {
            var boardEnvironment = _boardEnvironmentFactory.Create();
            whitePlayer = boardEnvironment.WhitePlayerFacade;
            blackPlayer = boardEnvironment.BlackPlayerFacade;
            _boardEnvironments.Add(boardEnvironment);
        }
    }
}