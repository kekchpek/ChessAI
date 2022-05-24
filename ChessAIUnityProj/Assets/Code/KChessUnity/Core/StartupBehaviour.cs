using KChess.Core.API.BoardsManager;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Piece;
using MVVMCore.ViewModelsFactory;
using UnityEngine;
using Zenject;

namespace KChessUnity.Core
{
    public class StartupBehaviour : MonoBehaviour
    {

        private IViewFactory<IBoardViewModel> _boardFactory;
        private IViewFactory<IPieceViewModel, IBoardViewModel, IMovesDisplayerViewModel, IPlayerFacade, IPiece> _pieceFactory;
        private IViewFactory<IMovesDisplayerViewModel, IBoardViewModel> _movesDisplayerFactory;

        private IBoardsManager _boardsManager;

        private IPlayerFacade _whitePlayerFacade;
        private IPlayerFacade _blackPlayerFacade;
        
        [Inject]
        public void Construct(IViewFactory<IBoardViewModel> boardFactory,
            IViewFactory<IPieceViewModel, IBoardViewModel, IMovesDisplayerViewModel, IPlayerFacade, IPiece> pieceFactory,
            IViewFactory<IMovesDisplayerViewModel, IBoardViewModel> movesDisplayerFactory)
        {
            _boardFactory = boardFactory;
            _pieceFactory = pieceFactory;
            _movesDisplayerFactory = movesDisplayerFactory;
        }
        
        private void Awake()
        {
            _boardsManager = new BoardManager();
            _boardsManager.CreateBoard(out _whitePlayerFacade, out _blackPlayerFacade);
            var board = _boardFactory.Create();
            var movesDisplayer = _movesDisplayerFactory.Create(board);

            foreach (var piece in _whitePlayerFacade.GetBoard().Pieces)
            {
                    _pieceFactory.Create(
                        board, movesDisplayer, piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade, piece);
            }
        }
    }
}