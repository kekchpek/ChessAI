using System.Collections.Generic;
using ChessAI.Core.MoveUtility;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.AttackedCellsUtility
{
    public class AttackedCellsUtility : IAttackedCellsUtility
    {
        private readonly IPieceMoveUtilityFacade _pieceMoveUtilityFacade;
        private readonly IBoard _board;

        private readonly HashSet<BoardCoordinates> _attackedByWhite = new HashSet<BoardCoordinates>();
        private readonly HashSet<BoardCoordinates> _attackedByBlack = new HashSet<BoardCoordinates>();

        public AttackedCellsUtility(
            IPieceMoveUtilityFacade pieceMoveUtilityFacade,
            IBoard board)
        {
            _pieceMoveUtilityFacade = pieceMoveUtilityFacade;
            _board = board;
            UpdateData();
            _board.PositionChanged += OnPositionChanged;
        }

        public bool IsCellAttacked(BoardCoordinates coordinates, PieceColor attackingColor)
        {
            return attackingColor == PieceColor.Black && _attackedByBlack.Contains(coordinates) ||
                   attackingColor == PieceColor.White && _attackedByWhite.Contains(coordinates);
        }

        private void OnPositionChanged(IPiece _)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            _attackedByBlack.Clear();
            _attackedByWhite.Clear();
            foreach (var piece in _board.Pieces)
            {
                var availableMoves = _pieceMoveUtilityFacade.GetAvailableMoves(piece);
                foreach (var availableMove in availableMoves)
                {
                    if (piece.Color == PieceColor.Black && !_attackedByBlack.Contains(availableMove))
                    {
                        _attackedByBlack.Add(availableMove);
                    }
                    if (piece.Color == PieceColor.White && !_attackedByWhite.Contains(availableMove))
                    {
                        _attackedByWhite.Add(availableMove);
                    }
                }
            }
        }
    }
}