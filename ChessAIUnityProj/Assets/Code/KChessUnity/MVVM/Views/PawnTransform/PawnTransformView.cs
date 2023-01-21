using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChessUnity.Core.Assets;
using UnityEngine;
using UnityEngine.UI;
using UnityMVVM;
using Zenject;

namespace KChessUnity.MVVM.Views.PawnTransform
{
    public class PawnTransformView : ViewBehaviour<IPawnTransformViewModel>
    {
        
        [SerializeField] private Button _knightButton;
        [SerializeField] private Button _bishopButton;
        [SerializeField] private Button _rookButton;
        [SerializeField] private Button _queenButton;

        private IAssetManager _assetManager;

        [Inject]
        public void Construct(IAssetManager assetManager)
        {
            _assetManager = assetManager;
        }
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            if (ViewModel.ColorOfTransformingPiece == PieceColor.Black)
            {
                _knightButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.BlackKnight);
                _bishopButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.BlackBishop);
                _rookButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.BlackRook);
                _queenButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.BlackQueen);
            }
            if (ViewModel.ColorOfTransformingPiece == PieceColor.White)
            {
                _knightButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.WhiteKnight);
                _bishopButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.WhiteBishop);
                _rookButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.WhiteRook);
                _queenButton.image.sprite = _assetManager.Get<Sprite>(AssetPaths.WhiteQueen);
            }
            _knightButton.onClick.AddListener(() =>ViewModel.OnPieceClicked(PawnTransformationVariant.Knight));
            _bishopButton.onClick.AddListener(() =>ViewModel.OnPieceClicked(PawnTransformationVariant.Bishop));
            _rookButton.onClick.AddListener(() =>ViewModel.OnPieceClicked(PawnTransformationVariant.Rook));
            _queenButton.onClick.AddListener(() =>ViewModel.OnPieceClicked(PawnTransformationVariant.Queen));
        }

        protected override void OnViewModelClear()
        {
            base.OnViewModelClear();
            _knightButton.onClick.RemoveAllListeners();
            _bishopButton.onClick.RemoveAllListeners();
            _rookButton.onClick.RemoveAllListeners();
            _queenButton.onClick.RemoveAllListeners();
        }
    }
}
