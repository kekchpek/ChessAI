using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.Core;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Triggers.BoardClicked;
using KChessUnity.MVVM.Views.MovesDisplayer;
using KChessUnity.MVVM.Views.PawnTransform;
using KChessUnity.MVVM.Views.Piece;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace KChessUnity.MVVM.Views.Board
{
    public class BoardViewModel : ViewModel, IBoardViewModel, IInitializable
    {
        private readonly IBoardPositionsCalculator _boardPositionsCalculator;
        private readonly IBoardClickedTrigger _boardClickedTrigger;
        private readonly IPlayerFacade _whitePlayerFacade;
        private readonly IPlayerFacade _blackPlayerFacade;
        

        public BoardViewModel(IBoardViewModelPayload payload,
            IBoardPositionsCalculator boardPositionsCalculator,
            IBoardClickedTrigger boardClickedTrigger)
        {
            _boardPositionsCalculator = boardPositionsCalculator;
            _boardClickedTrigger = boardClickedTrigger;
            _whitePlayerFacade = payload.WhitePlayerFacade;
            _blackPlayerFacade = payload.BlackPlayerFacade;
        }
        
        public void Initialize()
        {
            CreateSubView<IMovesDisplayerViewModel>();

            foreach (var piece in _whitePlayerFacade.GetPieces())
            {
                CreateSubView<IPieceViewModel>(
                    new PieceViewModelPayload(piece,
                        piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
            }

            _whitePlayerFacade.PieceAddedOnBoard += OnPieceAddedOnBoard;
            _blackPlayerFacade.PieceRequiredToBeTransformed += OnPawnTransform;
        }

        private void OnPieceAddedOnBoard(IPiece piece)
        {
            OpenView<IPieceViewModel>(VIewLayersIds.Popup,
                new PieceViewModelPayload(piece,
                    piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
        }

        private void OnPawnTransform(IPiece piece)
        {
            CreateSubView<IPawnTransformViewModel>(new PawnTransformPayload(piece.Color,
                piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
        }

        protected override void OnDestroyInternal()
        {
            _whitePlayerFacade.PieceAddedOnBoard -= OnPieceAddedOnBoard;
            _blackPlayerFacade.PieceRequiredToBeTransformed -= OnPawnTransform;
            base.OnDestroyInternal();
        }

        public void OnBoardClicked(Vector2 boardRectPosition)
        {
            var cell = _boardPositionsCalculator.GetCellFromBoardRectCoords(boardRectPosition);
            if (cell.HasValue)
            {
                _boardClickedTrigger.Trigger(cell.Value);
            }
            else
            {
                Debug.LogError($"Can not find cell for coordinates = {boardRectPosition}");
            }
        }
    }
}