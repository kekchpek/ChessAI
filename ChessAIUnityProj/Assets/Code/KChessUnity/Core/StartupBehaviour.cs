using System;
using KChess.Core.API.PlayerFacade;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Piece;
using MVVMCore.ViewModelsFactory;
using UnityEngine;

namespace KChessUnity.Core
{
    public class StartupBehaviour : MonoBehaviour
    {

        public void Construct(IViewFactory<IBoardViewModel> boardFactory,
            IViewFactory<IPieceViewModel> pieceModel,
            IViewFactory<IMovesDisplayerViewModel> movesDisplayerFactory,
            IPlayerFacade playerFacade)
        {
            
        }
        
        private void Awake()
        {
            throw new NotImplementedException();
        }
    }
}