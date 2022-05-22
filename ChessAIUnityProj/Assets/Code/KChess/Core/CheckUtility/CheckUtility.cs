using System;
using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.MoveUtility;
using KChess.Domain;

namespace KChess.Core.CheckUtility
{
    public class CheckUtility : ICheckUtility, IBoardEnvironmentComponent
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
            _board.PositionChanged += OnPositionChanged;
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

        private void OnPositionChanged(IPiece piece)
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
                var checkingPieces = _board.Pieces
                    .Where(x => x.Color == PieceColor.Black)
                    .Where(x => _pieceMoveUtilityFacade.GetAvailableMoves(x)
                        .Contains(whiteKing.Position)).ToArray();
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
                var checkingPieces = _board.Pieces
                    .Where(x => x.Color == PieceColor.White)
                    .Where(x => _pieceMoveUtilityFacade.GetAvailableMoves(x)
                        .Contains(blackKing.Position)).ToArray();
                if (checkingPieces.Any())
                {
                    _checkingPieces = checkingPieces;
                    _checkedColor = PieceColor.Black;
                }
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}