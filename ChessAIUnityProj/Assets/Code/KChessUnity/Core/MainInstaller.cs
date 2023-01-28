using KChess.Core.API.BoardsManager;
using KChessUnity.Core.Assets;
using KChessUnity.Core.Camera;
using KChessUnity.Core.Screen;
using KChessUnity.Models.HighlightedCells;
using KChessUnity.Models.Startup;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Triggers.BoardClicked;
using KChessUnity.MVVM.Triggers.PieceSelected;
using KChessUnity.MVVM.Views.Board;
using KChessUnity.MVVM.Views.GameEnded;
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
        [SerializeField] private GameObject _gameEndedPopupPrefab;

        [SerializeField] private Transform _mainUiContainer;
        [SerializeField] private Transform _popupsContainer;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.UseAsMvvmContainer(new []
            {
                (ViewLayersIds.Main, _mainUiContainer),
                (ViewLayersIds.Popup, _popupsContainer),
            });
            
            Container.FastBind<IHighlightedCellsMutableModel, IHighlightedCellsModel, HighlightedCellsModel>();
            Container.FastBind<IHighlightedCellsService, HighlightedCellsService>();
            Container.FastBind<IStartupService, StartupService>();
            Container.Bind<IBoardsManager>().To<BoardManager>().AsSingle();

            Container.FastBind<IAssetManager, AssetManager>();
            Container.ProvideAccessForViewLayer<IAssetManager>();

            Container.FastBind<IScreenAdapter, ScreenAdapter>();
            Container.ProvideAccessForViewLayer<IScreenAdapter>();
            
            Container.FastBind<ICameraService, CameraService>();
            Container.FastBind<ICameraMutableModel, ICameraModel, CameraModel>();

            Container.FastBind<IBoardPositionsCalculator, BoardPositionsCalculator>();
            Container.FastBind<IBoardClickedTrigger, BoardClickedTrigger>();
            Container.FastBind<IPieceSelectedTrigger, PieceSelectedTrigger>();

            Container.InstallView<PieceView, IPieceViewModel, PieceViewModel>(ViewNames.Piece, _piecePrefab);
            Container.InstallView<BoardView, IBoardViewModel, BoardViewModel>(ViewNames.Board, _boardPrefab);
            Container.InstallView<MovesDisplayerView, IMovesDisplayerViewModel, MovesDisplayerViewModel>(ViewNames.MovesDisplayer, _movesDisplayerPrefab);
            Container.InstallView<PawnTransformView, IPawnTransformViewModel, PawnTransformViewModel>(ViewNames.TransformationPopup, _pawnTransformPopup);
            Container.InstallView<GameEndedPopupView, IGameEndedPopupViewModel, GameEndedPopupViewModel>(ViewNames.GameEnded, _gameEndedPopupPrefab);
        }
    }
}