using System;
using System.Collections.Generic;
using System.Linq;
using KChess.Core.MoveUtility;
using KChess.Domain;
using UnityEngine.Assertions;

namespace KChess.Core.CheckUtility
{
    internal class CheckUtility : ICheckUtility, IDisposable
    {
        private readonly IBoard _board;
        private readonly IPieceMoveUtilityFacade _pieceMoveUtilityFacade;

        private PieceColor? _checkedColor = null;
        private IPiece[] _checkingPieces = null;

        public CheckUtility(IBoard board,
            IPieceMoveUtilityFacade pieceMoveUtilityFacade)
        {
            _board = board;
            _pieceMoveUtilityFacade = pieceMoveUtilityFacade;
            _checkedColor = null;
            _board.Updated += OnBoardUpdated;
        }
        
        public bool IsPositionWithCheck(out PieceColor checkedColor)
        {
            if (_checkedColor.HasValue)
            {
                checkedColor = _checkedColor.Value;
                return true;
            }

            checkedColor = default;
            return false;
        }

        public IReadOnlyList<IPiece> GetCheckingPieces()
        {
            if (_checkingPieces != null)
            {
                return _checkingPieces;
            }

            return Array.Empty<IPiece>();
        }

        private void OnBoardUpdated(IPiece piece)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _checkedColor = null;
            _checkingPieces = null;

            var whiteKing = _board.Pieces.FirstOrDefault(x => x.Color == PieceColor.White
                                                              && x.Type == PieceType.King);
            if (whiteKing != null)
            {
                Assert.IsTrue(whiteKing.Position.HasValue);
                var checkingPieces = _board.Pieces
                    .Where(x => x.Color == PieceColor.Black)
                    .Where(x => _pieceMoveUtilityFacade.GetAvailableMoves(x)
                        .Contains(whiteKing.Position.Value)).ToArray();
                if (checkingPieces.Any())
                {
                    _checkingPieces = checkingPieces;
                    _checkedColor = PieceColor.White;
                }
            }
            
            var blackKing = _board.Pieces.FirstOrDefault(x => x.Color == PieceColor.Black
                                                              && x.Type == PieceType.King);
            if (blackKing != null)
            {
                Assert.IsTrue(blackKing.Position.HasValue);
                var checkingPieces = _board.Pieces
                    .Where(x => x.Color == PieceColor.White)
                    .Where(x => _pieceMoveUtilityFacade.GetAvailableMoves(x)
                        .Contains(blackKing.Position.Value)).ToArray();
                if (checkingPieces.Any())
                {
                    _checkingPieces = checkingPieces;
                    _checkedColor = PieceColor.Black;
                }
            }
        }

        public void Dispose()
        {
            _board.Updated -= OnBoardUpdated;
        }
    }
}