using KChess.Core.API.BoardsManager;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.ViewModels.Board;
using UnityEngine;
using UnityMVVM.ViewManager;
using Zenject;

namespace KChessUnity.Core
{
    public class StartupBehaviour : MonoBehaviour
    {

        private IViewManager _viewManager;

        private IBoardsManager _boardsManager;

        private IPlayerFacade _whitePlayerFacade;
        private IPlayerFacade _blackPlayerFacade;
        
        [Inject]
        public void Construct(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }
        
        private void Awake()
        {
            _boardsManager = new BoardManager();
            _boardsManager.CreateBoard(out _whitePlayerFacade, out _blackPlayerFacade);
            _viewManager.Open<IBoardViewModel>(VIewLayersIds.Main, new BoardViewModelPayload(_whitePlayerFacade, _blackPlayerFacade));
        }
    }
}