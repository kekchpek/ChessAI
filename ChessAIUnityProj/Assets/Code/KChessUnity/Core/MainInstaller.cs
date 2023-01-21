using KChess.Core.API.BoardsManager;
using KChessUnity.Core.Assets;
using KChessUnity.Core.Camera;
using KChessUnity.Core.Screen;
using KChessUnity.Models.HighlightedCells;
using KChessUnity.Models.Startup;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Triggers;
using KChessUnity.MVVM.Triggers.BoardClicked;
using KChessUnity.MVVM.Triggers.PieceSelected;
using KChessUnity.MVVM.Views.Board;
using KChessUnity.MVVM.Views.MovesDisplayer;
using KChessUnity.MVVM.Views.PawnTransform;
using KChessUnity.MVVM.Views.Piece;
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
        [SerializeField] private GameObject _pawnTransformPopup;

        [SerializeField] private Transform _mainUiContainer;
        [SerializeField] private Transform _popupsContainer;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            var modelLayerContainer = Container.CreateSubContainer();
            modelLayerContainer.Bind(typeof(IHighlightedCellsModel), typeof(IHighlightedCellsMutableModel))
                .To<HighlightedCellsModel>().AsSingle();
            modelLayerContainer.Bind<IHighlightedCellsService>().To<HighlightedCellsService>().AsSingle();
            modelLayerContainer.Bind<IStartupService>().To<StartupService>().AsSingle();
            modelLayerContainer.Bind<IBoardsManager>().To<BoardManager>().AsSingle().WhenInjectedInto<StartupService>();

            Container.Bind<IStartupService>()
                .FromMethod(_ => modelLayerContainer.Resolve<IStartupService>());
            Container.Bind<IHighlightedCellsModel>()
                .FromMethod(_ => modelLayerContainer.Resolve<IHighlightedCellsModel>());
            Container.Bind<IHighlightedCellsService>()
                .FromMethod(_ => modelLayerContainer.Resolve<IHighlightedCellsService>());

            Container.Bind<IAssetManager>().To<AssetManager>().AsSingle();

            Container.Bind<IScreenAdapter>().To<ScreenAdapter>().AsSingle();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
            Container.Bind<ICameraModel>().To<CameraModel>().AsSingle();
            Container.Bind<ICameraMutableModel>()
                .FromMethod(_ => (ICameraMutableModel)Container.Resolve<ICameraModel>())
                .AsSingle().WhenInjectedInto<CameraService>();

            Container.Bind<IBoardPositionsCalculator>().To<BoardPositionsCalculator>().AsSingle();
            Container.Bind<IBoardClickedTrigger>().To<BoardClickedTrigger>().AsSingle();
            Container.Bind<IPieceSelectedTrigger>().To<PieceSelectedTrigger>().AsSingle();
            
            
            var mvvmContainer = new MvvmSubContainer(Container, new []
            {
                (VIewLayersIds.Main, _mainUiContainer),
                (VIewLayersIds.Popup, _popupsContainer),
            });
            
            
            mvvmContainer.InstallFactoryFor<PieceView, IPieceViewModel, PieceViewModel>(_piecePrefab);
            mvvmContainer.InstallFactoryFor<BoardView, IBoardViewModel, BoardViewModel>(_boardPrefab);
            mvvmContainer.InstallFactoryFor<MovesDisplayerView, IMovesDisplayerViewModel, MovesDisplayerViewModel>(_movesDisplayerPrefab);
            mvvmContainer.InstallFactoryFor<PawnTransformView, IPawnTransformViewModel, PawnTransformViewModel>(_pawnTransformPopup);
        }
    }
}