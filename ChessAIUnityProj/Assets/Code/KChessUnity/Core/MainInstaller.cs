using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Piece;
using KChessUnity.ViewModels.Triggers;
using KChessUnity.Views;
using MVVMCore.DI;
using MVVMCore.Input;
using UnityEngine;

namespace KChessUnity.Core
{
    public class MainInstaller : MonoMvvmInstaller
    {
        [SerializeField] private GameObject _piecePrefab;
        [SerializeField] private GameObject _boardPrefab;
        [SerializeField] private GameObject _movesDisplayerPrefab;

        [SerializeField] private InputController _inputController;

        [SerializeField] private Transform _mainCanvas;
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<IInputController>().FromComponentInNewPrefab(_inputController).AsSingle().OnInstantiated<InputController>(
                (_, inputController) =>
                {
                    inputController.SetCamera(_camera);
                });
            Container.Bind<IResetSelectionTrigger>().To<ResetSelectionTrigger>().AsSingle();
            InstallFactoryFor<PieceView, IPieceViewModel, PieceViewModel, IBoardViewModel, IMovesDisplayerViewModel, IPlayerFacade, IPiece>(_piecePrefab, _mainCanvas);
            InstallFactoryFor<BoardView, IBoardViewModel, BoardViewModel>(_boardPrefab, _mainCanvas);
            InstallFactoryFor<MovesDisplayerView, IMovesDisplayerViewModel, MovesDisplayerViewModel, IBoardViewModel>(_movesDisplayerPrefab, _mainCanvas);
        }
    }
}