using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Core.Assets;
using KChessUnity.Models.HighlightedCells;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Triggers.BoardClicked;
using KChessUnity.MVVM.Triggers.PieceSelected;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.MVVM.Views.Piece
{
    public class PieceViewModel : ViewModel, IPieceViewModel
    {
        private readonly IHighlightedCellsService _highlightedCellsService;
        private readonly IBoardPositionsCalculator _boardPositionsCalculator;
        private readonly IAssetManager _assetManager;
        private readonly IBoardClickedTrigger _boardClickedTrigger;
        private readonly IPieceSelectedTrigger _pieceSelectedTrigger;
        private readonly IPiece _piece;
        private readonly IPlayerFacade _playerFacade;
        
        private readonly Mutable<Vector3> _position = new();
        private readonly Mutable<Sprite> _image = new();

        private bool _isDragged;
        private bool _isSelected;

        private BoardCoordinates? _cellToMove;

        public IBindable<Vector3> Position => _position;

        public IBindable<Sprite> Image => _image;

        public PieceViewModel(
            IBoardClickedTrigger boardClickedTrigger,
            IHighlightedCellsService highlightedCellsService,
            IBoardPositionsCalculator boardPositionsCalculator,
            IPieceSelectedTrigger pieceSelectedTrigger,
            IPieceViewModelPayload payload,
            IAssetManager assetManager)
        {
            _highlightedCellsService = highlightedCellsService;
            _boardPositionsCalculator = boardPositionsCalculator;
            _pieceSelectedTrigger = pieceSelectedTrigger;
            _assetManager = assetManager;
            _boardClickedTrigger = boardClickedTrigger;
            _piece = payload.Piece;
            _playerFacade = payload.PlayerFacade;
        }

        public void Initialize()
        {
            _image.Value = GetSprite(_piece.Color, _piece.Type);
            UpdatePositionBasedOnPiece();
            _boardClickedTrigger.Triggered += OnBoardClicked;
            _piece.Moved += OnPieceMoved;
            _piece.Removed += Destroy;
        }
        
        public void OnPieceClicked()
        {
            Select();
        }

        public void OnPieceDragStart()
        {
            Select();
        }

        public void OnPieceDragEnd(Vector2 rectCoords)
        {
            var cell = _boardPositionsCalculator.GetCellFromBoardRectCoords(rectCoords);
            if (cell.HasValue)
            {
                if (_playerFacade.TryMovePiece(_piece, cell.Value)) {
                    ReleaseSelection();
                }
            }
        }

        private void OnPieceMoved()
        {
            ReleaseSelection();
            UpdatePositionBasedOnPiece();
        }

        private void UpdatePositionBasedOnPiece()
        {
            if (_piece.Position.HasValue)
            {
                _position.Value = _boardPositionsCalculator.GetCellCoordsOnBoardRect(_piece.Position.Value);
            }
            else
            {
                Destroy();
            }
        }

        private void Select()
        {
            _pieceSelectedTrigger.Trigger();
            _isSelected = true;
            _highlightedCellsService.SetHighlightedCells(_playerFacade.GetAvailableMoves(_piece));
        }

        private void OnBoardClicked(BoardCoordinates cell)
        {
            if (_isSelected)
            {
                _playerFacade.TryMovePiece(_piece, cell);
                ReleaseSelection();
            }
        }

        private void ReleaseSelection()
        {
            if (_isSelected)
            {
                _isSelected = false;
                _highlightedCellsService.ClearHighlightedCells();
            }
        }

        private Sprite GetSprite(PieceColor color, PieceType type)
        {
            switch (color)
            {
                case PieceColor.White:
                    switch (type)
                    {
                        case PieceType.Pawn:
                            return _assetManager.Get<Sprite>(AssetPaths.WhitePawn);
                        case PieceType.Knight:
                            return _assetManager.Get<Sprite>(AssetPaths.WhiteKnight);
                        case PieceType.Bishop:
                            return _assetManager.Get<Sprite>(AssetPaths.WhiteBishop);
                        case PieceType.Rook:
                            return _assetManager.Get<Sprite>(AssetPaths.WhiteRook);
                        case PieceType.Queen:
                            return _assetManager.Get<Sprite>(AssetPaths.WhiteQueen);
                        case PieceType.King:
                            return _assetManager.Get<Sprite>(AssetPaths.WhiteKing);
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                case PieceColor.Black:
                    switch (type)
                    {
                        case PieceType.Pawn:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackPawn);
                        case PieceType.Knight:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackKnight);
                        case PieceType.Bishop:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackBishop);
                        case PieceType.Rook:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackRook);
                        case PieceType.Queen:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackQueen);
                        case PieceType.King:
                            return _assetManager.Get<Sprite>(AssetPaths.BlackKing);
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnDestroyInternal()
        {
            _pieceSelectedTrigger.Triggered -= ReleaseSelection;
            _piece.Moved -= OnPieceMoved;
            _piece.Removed -= Destroy;
            base.OnDestroyInternal();
        }
    }
}
