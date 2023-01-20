using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Models.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using KChessUnity.ViewModels.PawnTransform;
using KChessUnity.ViewModels.Piece;
using UnityEngine;
using UnityMVVM.ViewModelCore;
using Zenject;

namespace KChessUnity.ViewModels.Board
{
    public class BoardViewModel : ViewModel, IBoardViewModel, IInitializable, IBoardWorldPositionsCalculator
    {

        private const float BoardSize = 8f;

        private Vector3 _bottomLeftCorner;
        private Vector3 _size;

        private readonly IPlayerFacade _whitePlayerFacade;
        private readonly IPlayerFacade _blackPlayerFacade;
        

        public BoardViewModel(IBoardViewModelPayload payload)
        {
            _whitePlayerFacade = payload.WhitePlayerFacade;
            _blackPlayerFacade = payload.BlackPlayerFacade;
        }
        
        public void Initialize()
        {
            CreateSubView<IMovesDisplayerViewModel>(new MovesDisplayerPayload(this));

            foreach (var piece in _whitePlayerFacade.GetBoard().Pieces)
            {
                CreateSubView<IPieceViewModel>(
                    new PieceViewModelPayload(piece,
                        piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade,
                        this));
            }

            _whitePlayerFacade.PieceAddedOnBoard += OnPieceAddedOnBoard;
            _blackPlayerFacade.PieceRequiredToBeTransformed += OnPawnTransform;
        }

        private void OnPieceAddedOnBoard(IPiece piece)
        {
            CreateSubView<IPieceViewModel>(
                new PieceViewModelPayload(piece,
                    piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade,
                    this));
        }

        private void OnPawnTransform(IPiece piece)
        {
            CreateSubView<IPawnTransformViewModel>(new PawnTransformPayload(piece.Color,
                piece.Color == PieceColor.White ? _whitePlayerFacade : _blackPlayerFacade));
        }

        public void SetCornerPoints(Vector3 bottomLeft, Vector3 topRight)
        {
            _bottomLeftCorner = bottomLeft;
            _size = new Vector3(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y, topRight.z - bottomLeft.z);
        }

        public Vector3 GetWorldPosition(BoardCoordinates coords)
        {
            var numericCoords = coords.ToNumeric();
            return _bottomLeftCorner +
                   Vector3.Scale(_size,
                       new Vector3(numericCoords.Item1 / BoardSize, numericCoords.Item2 / BoardSize, 0f)) +
                   new Vector3(_size.x / 16f, _size.y / 16f);
        }

        public BoardCoordinates? GetCellCoords(Vector3 worldPosition)
        {
            var x = worldPosition.x - _bottomLeftCorner.x;
            var y = worldPosition.y - _bottomLeftCorner.y;
            
            if (!(x < _size.x) || !(x > 0f) ||
                !(y < _size.y) || !(y > 0f)) 
                return null;
            
            var vertical = (int) Mathf.Floor(x * BoardSize / _size.x);
            var horizontal = (int) Mathf.Floor(y * BoardSize / _size.x);
            return (vertical, horizontal);

        }

        protected override void OnDestroyInternal()
        {
            _whitePlayerFacade.PieceAddedOnBoard -= OnPieceAddedOnBoard;
            _blackPlayerFacade.PieceRequiredToBeTransformed -= OnPawnTransform;
            base.OnDestroyInternal();
        }
    }
}