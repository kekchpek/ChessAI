using System;
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

        private readonly IDictionary<Guid, IBoardEnvironment> _boardEnvironments = new Dictionary<Guid, IBoardEnvironment>();

        public BoardManager()
        {
            var pieceFactory = new PieceFactory();
            var boardFactory = new BoardFactory(pieceFactory);
            _boardEnvironmentFactory = new BoardEnvironmentFactory(boardFactory, pieceFactory);
        }
        
        public Guid CreateBoard(
            out IPlayerFacade whitePlayer, 
            out IPlayerFacade blackPlayer,
            out IPlayerFacade universalPlayerFacade)
        {
            var boardEnvironment = _boardEnvironmentFactory.Create();
            whitePlayer = boardEnvironment.WhitePlayerFacade;
            blackPlayer = boardEnvironment.BlackPlayerFacade;
            universalPlayerFacade = boardEnvironment.UniversalPlayerFacade;
            var boardId = Guid.NewGuid();
            _boardEnvironments.Add(boardId, boardEnvironment);
            return boardId;
        }

        public void ReleaseBoard(Guid boardId)
        {
            if (_boardEnvironments.TryGetValue(boardId, out var environment))
            {
                environment.Dispose();
                _boardEnvironments.Remove(boardId);
            }
            else
            {
                // do nothing? throw error? log something?
                // Do nothing for now.
            }
        }
    }
}