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
        private readonly IPlayerFacade _playerFacade;
        

        public BoardViewModel(IBoardViewModelPayload payload,
            IBoardPositionsCalculator boardPositionsCalculator,
            IBoardClickedTrigger boardClickedTrigger)
        {
            _boardPositionsCalculator = boardPositionsCalculator;
            _boardClickedTrigger = boardClickedTrigger;
            _playerFacade = payload.PlayerFacade;
        }
        
        public void Initialize()
        {
            CreateSubView(ViewNames.MovesDisplayer);
            var pieces = _playerFacade.GetPieces();
            foreach (var piece in pieces)
            {
                CreateSubView(ViewNames.Piece,
                    new PieceViewModelPayload(piece, _playerFacade));
            }

            _playerFacade.PieceAddedOnBoard += OnPieceAddedOnBoard;
            _playerFacade.PieceRequiredToBeTransformed += OnPawnTransform;
            _playerFacade.BoardStateChanged += OnBoardStateChanged;
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
                new PieceViewModelPayload(piece, _playerFacade));
        }

        private void OnPawnTransform(IPiece piece)
        {
            OpenView(ViewLayersIds.Popup, ViewNames.TransformationPopup, 
                new PawnTransformPayload(piece.Color, _playerFacade));
        }

        protected override void OnDestroyInternal()
        {
            _playerFacade.PieceAddedOnBoard -= OnPieceAddedOnBoard;
            _playerFacade.PieceRequiredToBeTransformed -= OnPawnTransform;
            _playerFacade.BoardStateChanged -= OnBoardStateChanged;
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