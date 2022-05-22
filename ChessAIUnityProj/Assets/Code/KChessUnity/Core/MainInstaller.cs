using KChess.Core.API.PlayerFacade;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Piece;
using KChessUnity.Views;
using MVVMCore.DI;
using UnityEngine;

namespace KChessUnity.Core
{
    public class MainInstaller : ScriptableViewInstaller
    {

        [SerializeField] private GameObject _piecePrefab;
        [SerializeField] private GameObject _boardPrefab;
        [SerializeField] private GameObject _movesDisplayerPrefab;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            InstallFactoryFor<PieceView, IPieceViewModel, PieceViewModel>(_piecePrefab);
            InstallFactoryFor<BoardView, IBoardViewModel, BoardViewModel>(_boardPrefab);
            InstallFactoryFor<MovesDisplayerView, IMovesDisplayerViewModel, MovesDisplayerViewModel>(_movesDisplayerPrefab);
        }
    }
}