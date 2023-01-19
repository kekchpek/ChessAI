using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.Input;
using KChessUnity.Models;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Piece;
using KChessUnity.ViewModels.Triggers;
using KChessUnity.Views;
using UnityEngine;
using UnityMVVM.DI;
using Zenject;

namespace KChessUnity.Core
{
    public class MainInstaller : MonoInstaller
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

            var modelLayerContainer = Container.CreateSubContainer();
            modelLayerContainer.Bind(typeof(IHighlightedCellsModel), typeof(IHighlightedCellsMutableModel))
                .To<HighlightedCellsModel>().AsSingle();
            modelLayerContainer.Bind<IHighlightedCellsService>().To<HighlightedCellsService>().AsSingle();

            Container.Bind<IHighlightedCellsModel>()
                .FromMethod(x => modelLayerContainer.Resolve<IHighlightedCellsModel>());
            Container.Bind<IHighlightedCellsService>()
                .FromMethod(x => modelLayerContainer.Resolve<IHighlightedCellsService>());
            
            Container.Bind<IInputController>().FromComponentInNewPrefab(_inputController).AsSingle().OnInstantiated<InputController>(
                (_, inputController) =>
                {
                    inputController.SetCamera(_camera);
                });
            Container.Bind<IResetSelectionTrigger>().To<ResetSelectionTrigger>().AsSingle();
            
            
            var mvvmContainer = new MvvmSubContainer(Container, new [] { (VIewLayersIds.Main, _mainCanvas) });
            mvvmContainer.InstallFactoryFor<PieceView, IPieceViewModel, PieceViewModel>(_piecePrefab);
            mvvmContainer.InstallFactoryFor<BoardView, IBoardViewModel, BoardViewModel>(_boardPrefab);
            mvvmContainer.InstallFactoryFor<MovesDisplayerView, IMovesDisplayerViewModel, MovesDisplayerViewModel>(_movesDisplayerPrefab);
        }
    }
}