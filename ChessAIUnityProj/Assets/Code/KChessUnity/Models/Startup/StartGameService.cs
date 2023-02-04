using System;
using KChess.Core.API.BoardsManager;
using KChessUnity.Core;
using KChessUnity.MVVM.Views.GameScreen.Payload;
using UnityAuxiliaryTools.Promises;
using UnityMVVM.ViewManager;

namespace KChessUnity.Models.Startup
{
    public class StartGameService : IStartGameService
    {
        private readonly IViewManager _viewManager;
        private readonly IBoardsManager _boardsManager;

        private Guid? _currenBoardId;

        public StartGameService(IViewManager viewManager, IBoardsManager boardsManager)
        {
            _viewManager = viewManager;
            _boardsManager = boardsManager;
        }
        
        public async IPromise StartSingleGame()
        {
            if (_currenBoardId.HasValue)
            {
                _boardsManager.ReleaseBoard(_currenBoardId.Value);
            }
            _currenBoardId = _boardsManager.CreateBoard(
                out var whitePlayerFacade, 
                out var blackPlayerFacade,
                out var universalPlayerFacade);
            await _viewManager.Open(ViewLayersIds.Main, ViewNames.GameScreen, new GameScreenPayload(universalPlayerFacade));

        }

        public IPromise StartGameWithAI()
        {
            throw new System.NotImplementedException();
        }
    }
}