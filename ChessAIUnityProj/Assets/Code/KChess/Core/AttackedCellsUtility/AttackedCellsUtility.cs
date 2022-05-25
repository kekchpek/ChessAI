using System.Collections.Generic;
using KChess.Core.BoardEnvironment;
using KChess.Core.MoveUtility;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.AttackedCellsUtility
{
    public class AttackedCellsUtility : IAttackedCellsUtility, IBoardEnvironmentComponent
    {
        private readonly IPieceMoveUtilityFacade _pieceMoveUtilityFacade;
        private readonly IBoard _board;

        private readonly HashSet<BoardCoordinates> _attackedByWhite = new();
        private readonly HashSet<BoardCoordinates> _attackedByBlack = new();

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
                var availableMoves = _pieceMoveUtilityFacade.GetAttackedCells(piece);
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

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}