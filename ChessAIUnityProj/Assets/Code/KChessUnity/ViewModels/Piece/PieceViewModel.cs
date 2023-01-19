using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Input;
using KChessUnity.Models;
using KChessUnity.Models.Board;
using KChessUnity.ViewModels.Triggers;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.ViewModels.Piece
{
    public class PieceViewModel : ViewModel, IPieceViewModel
    {
        private readonly IHighlightedCellsService _highlightedCellsService;
        private readonly IBoardWorldPositionsCalculator _boardWorldPositionsCalculator;
        private readonly IResetSelectionTrigger _resetSelectionTrigger;
        private readonly IInputController _inputController;
        private readonly IPiece _piece;
        private readonly IPlayerFacade _playerFacade;
        
        private readonly Mutable<Vector3> _position = new();
        private readonly Mutable<Sprite> _image = new();

        private bool _isDragged;
        private bool _isSelected;

        private BoardCoordinates? _cellToMove;

        public event Action Disposed;

        public IBindable<Vector3> Position => _position;

        public IBindable<Sprite> Image => _image;

        public PieceViewModel(
            IResetSelectionTrigger resetSelectionTrigger,
            IInputController inputController,
            IHighlightedCellsService highlightedCellsService,
            IPieceViewModelPayload payload)
        {
            _highlightedCellsService = highlightedCellsService;
            _boardWorldPositionsCalculator = payload.BoardWorldPositionsCalculator;
            _resetSelectionTrigger = resetSelectionTrigger;
            _inputController = inputController;
            _piece = payload.Piece;
            _playerFacade = payload.PlayerFacade;
        }

        public void Initialize()
        {
            _image.Value = GetSprite(_piece.Color, _piece.Type);
            UpdatePositionBasedOnPiece();
            _inputController.MouseDown += OnMouseDown;
            _inputController.MouseUp += OnMouseUp;
            _resetSelectionTrigger.OnTriggered += ReleaseSelection;
            _piece.Moved += OnPieceMoved;
            _piece.Removed += Dispose;
        }

        private void OnPieceMoved()
        {
            ReleaseSelection();
            ReleaseDrag();
            UpdatePositionBasedOnPiece();
        }

        private void UpdatePositionBasedOnPiece()
        {
            if (_piece.Position.HasValue)
            {
                _position.Value = _boardWorldPositionsCalculator.GetWorldPosition(_piece.Position.Value);
            }
            else
            {
                Dispose();
            }
        }

        private void OnMouseDown(Vector2 mousePosition)
        {
            if (_playerFacade.GetTurn() != _piece.Color)
                return;
            var clickPosition = _boardWorldPositionsCalculator.GetCellCoords(mousePosition);
            if (clickPosition.HasValue)
            {
                if (clickPosition.Value == _piece.Position)
                {
                    ReleaseSelection();
                    Drag();
                }
                else if (_isSelected)
                {
                    _cellToMove = clickPosition.Value;
                }
            }
        }

        private void OnMouseUp(Vector2 mousePosition)
        {
            if (_playerFacade.GetTurn() != _piece.Color)
                return;
            var clickPosition = _boardWorldPositionsCalculator.GetCellCoords(mousePosition);
            if (_isDragged)
            {
                ReleaseDrag();
                if (clickPosition.HasValue)
                {
                    if (clickPosition != _piece.Position)
                    {
                        _playerFacade.TryMovePiece(_piece, clickPosition.Value);
                        _highlightedCellsService.ClearHighlightedCells();
                    }
                    else
                    {
                        Select();
                    }
                }
            }
            else if (_isSelected)
            {
                if (clickPosition.HasValue && _cellToMove.HasValue && clickPosition.Value == _cellToMove.Value)
                {
                    _playerFacade.TryMovePiece(_piece, _cellToMove.Value);
                    ReleaseSelection();
                }
            }
        }

        private void Drag()
        {
            if (_isDragged)
                return;
            _resetSelectionTrigger.Trigger();
            _position.Value = _inputController.MousePosition;
            _isDragged = true;
            _inputController.MousePositionChanged += OnMousePositionChanged;
            _highlightedCellsService.SetHighlightedCells(_playerFacade.GetAvailableMoves(_piece));
        }

        private void ReleaseDrag()
        {
            _isDragged = false;
            _inputController.MousePositionChanged -= OnMousePositionChanged;
            UpdatePositionBasedOnPiece();
        }

        private void Select()
        {
            _resetSelectionTrigger.Trigger();
            _isSelected = true;
            _highlightedCellsService.SetHighlightedCells(_playerFacade.GetAvailableMoves(_piece));
        }

        private void ReleaseSelection()
        {
            if (_isSelected)
            {
                _cellToMove = null;
                _isSelected = false;
                _highlightedCellsService.ClearHighlightedCells();
            }
        }

        private void OnMousePositionChanged(Vector2 mousePosition)
        {
            _position.Value = mousePosition;
        }

        private Sprite GetSprite(PieceColor color, PieceType type)
        {
            switch (color)
            {
                case PieceColor.White:
                    switch (type)
                    {
                        case PieceType.Pawn:
                            return Resources.Load<Sprite>("Sprites/Figures/WhitePawn");
                        case PieceType.Knight:
                            return Resources.Load<Sprite>("Sprites/Figures/WhiteKnight");
                        case PieceType.Bishop:
                            return Resources.Load<Sprite>("Sprites/Figures/WhiteBishop");
                        case PieceType.Rook:
                            return Resources.Load<Sprite>("Sprites/Figures/WhiteRook");
                        case PieceType.Queen:
                            return Resources.Load<Sprite>("Sprites/Figures/WhiteQueen");
                        case PieceType.King:
                            return Resources.Load<Sprite>("Sprites/Figures/WhiteKing");
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                case PieceColor.Black:
                    switch (type)
                    {
                        case PieceType.Pawn:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackPawn");
                        case PieceType.Knight:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackKnight");
                        case PieceType.Bishop:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackBishop");
                        case PieceType.Rook:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackRook");
                        case PieceType.Queen:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackQueen");
                        case PieceType.King:
                            return Resources.Load<Sprite>("Sprites/Figures/BlackKing");
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Dispose() 
        {
            _inputController.MouseDown -= OnMouseDown;
            _inputController.MouseUp -= OnMouseUp;
            _resetSelectionTrigger.OnTriggered -= ReleaseSelection;
            _piece.Moved -= OnPieceMoved;
            _piece.Removed -= Dispose;
            Disposed?.Invoke();
        }
    }
}
