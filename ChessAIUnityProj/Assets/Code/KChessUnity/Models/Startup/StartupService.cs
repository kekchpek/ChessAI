using System;
using KChess.Core.API.BoardsManager;
using KChessUnity.Core;
using KChessUnity.MVVM.Views.Board;
using UnityMVVM.ViewManager;

namespace KChessUnity.Models.Startup
{
    public class StartupService : IStartupService
    {
        private readonly IViewManager _viewManager;
        private readonly IBoardsManager _boardsManager;

        private Guid? _currenBoardId;

        public StartupService(IViewManager viewManager, IBoardsManager boardsManager)
        {
            _viewManager = viewManager;
            _boardsManager = boardsManager;
        }
        
        public void StartSingleGame()
        {
            if (_currenBoardId.HasValue)
            {
                _boardsManager.ReleaseBoard(_currenBoardId.Value);
            }
            _currenBoardId = _boardsManager.CreateBoard(out var whitePlayerFacade, out var blackPlayerFacade);
            _viewManager.Open(ViewLayersIds.Main, ViewNames.Board, new BoardViewModelPayload(whitePlayerFacade, blackPlayerFacade));
        }

        public void StartGameWithAI()
        {
            throw new System.NotImplementedException();
        }
    }
}