using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.Core;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Triggers.BoardClicked;
using KChessUnity.MVVM.Views.GameEnded.Payload;
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
            CreateSubView(ViewNames.MovesDisplayer);
            var pieces = _whitePlayerFacade.GetPieces();
            foreach (var piece in pieces)
            {
                CreateSubView(ViewNames.Piece,
                    new PieceViewModelPayload(piece,
                        piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
            }

            _whitePlayerFacade.PieceAddedOnBoard += OnPieceAddedOnBoard;
            _whitePlayerFacade.PieceRequiredToBeTransformed += OnPawnTransform;
            _whitePlayerFacade.BoardStateChanged += OnBoardStateChanged;
        }

        private void OnBoardStateChanged(BoardState state)
        {
            switch (state)
            {
                case BoardState.Regular:
                case BoardState.CheckToWhite:
                case BoardState.CheckToBlack:
                    break;
                case BoardState.MateToWhite:
                case BoardState.MateToBlack:
                case BoardState.Stalemate:
                case BoardState.RepeatDraw:
                    OpenView(ViewLayersIds.Popup, ViewNames.GameEnded, new GameEndedPopupPayload(state));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnPieceAddedOnBoard(IPiece piece)
        {
            CreateSubView(ViewNames.Piece,
                new PieceViewModelPayload(piece,
                    piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
        }

        private void OnPawnTransform(IPiece piece)
        {
            OpenView(ViewLayersIds.Popup, ViewNames.TransformationPopup, new PawnTransformPayload(piece.Color,
                piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
        }

        protected override void OnDestroyInternal()
        {
            _whitePlayerFacade.PieceAddedOnBoard -= OnPieceAddedOnBoard;
            _whitePlayerFacade.PieceRequiredToBeTransformed -= OnPawnTransform;
            _whitePlayerFacade.BoardStateChanged -= OnBoardStateChanged;
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