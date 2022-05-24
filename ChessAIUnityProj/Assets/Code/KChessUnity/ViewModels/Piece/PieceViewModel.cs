using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.Triggers;
using MVVMCore;
using MVVMCore.Input;
using UnityEngine;

namespace KChessUnity.ViewModels.Piece
{
    public class PieceViewModel : ViewModel, IPieceViewModel
    {
        private readonly IMovesDisplayerViewModel _movesDisplayerViewModel;
        private readonly IBoardViewModel _boardViewModel;
        private readonly IResetSelectionTrigger _resetSelectionTrigger;
        private readonly IInputController _inputController;
        private readonly IPiece _piece;
        private readonly IPlayerFacade _playerFacade;
        
        private Vector3 _position;
        private Sprite _image;

        private bool _isDragged;
        private bool _isSelected;

        private BoardCoordinates? _cellToMove;

        public event Action Disposed;

        public Vector3 Position
        {
            get => _position;
            set => SetAndRaiseIfChanged(nameof(Position), value, ref _position);
        }
        
        public Sprite Image
        {
            get => _image;
            set => SetAndRaiseIfChanged(nameof(Image), value, ref _image);
        }

        public PieceViewModel(
            IResetSelectionTrigger resetSelectionTrigger,
            IInputController inputController,
            IBoardViewModel boardViewModel,
            IMovesDisplayerViewModel movesDisplayerViewModel,
            IPiece piece,
            IPlayerFacade playerFacade)
        {
            _movesDisplayerViewModel = movesDisplayerViewModel;
            _boardViewModel = boardViewModel;
            _resetSelectionTrigger = resetSelectionTrigger;
            _inputController = inputController;
            _piece = piece;
            _playerFacade = playerFacade;
        }

        public void Initialize()
        {
            Image = GetSprite(_piece.Color, _piece.Type);
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
                Position = _boardViewModel.GetWorldPosition(_piece.Position.Value);
            }
            else
            {
                Dispose();
            }
        }

        private void OnMouseDown(Vector2 mousePosition)
        {
            _resetSelectionTrigger.Trigger();
            var clickPosition = _boardViewModel.GetCellCoords(mousePosition);
            if (clickPosition.HasValue)
            {
                if (!_isSelected)
                {
                    if (clickPosition.Value == _piece.Position)
                    {
                        Drag();
                    }
                }
                else
                {
                    _cellToMove = clickPosition.Value;
                }
            }
        }

        private void OnMouseUp(Vector2 mousePosition)
        {
            var clickPosition = _boardViewModel.GetCellCoords(mousePosition);
            if (_isDragged)
            {
                ReleaseDrag();
                if (clickPosition.HasValue)
                {
                    if (clickPosition != _piece.Position)
                    {
                        _playerFacade.TryMovePiece(_piece, clickPosition.Value);
                        _movesDisplayerViewModel.HideMoves();
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
            Position = _inputController.MousePosition;
            _isDragged = true;
            _inputController.MousePositionChanged += OnMousePositionChanged;
            _movesDisplayerViewModel.ShowMoves(_playerFacade.GetAvailableMoves(_piece));
        }

        private void ReleaseDrag()
        {
            _isDragged = false;
            _inputController.MousePositionChanged -= OnMousePositionChanged;
            UpdatePositionBasedOnPiece();
        }

        private void Select()
        {
            _isSelected = true;
            _movesDisplayerViewModel.ShowMoves(_playerFacade.GetAvailableMoves(_piece));
        }

        private void ReleaseSelection()
        {
            if (_isSelected)
            {
                _cellToMove = null;
                _isSelected = false;
                _movesDisplayerViewModel.HideMoves();
            }
        }

        private void OnMousePositionChanged(Vector2 mousePosition)
        {
            Position = mousePosition;
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
