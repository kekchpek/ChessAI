using System;
using System.Collections.Generic;
using System.Linq;
using KekChessCore.AttackedCellsUtility;
using KekChessCore.BoardEnvironment;
using KekChessCore.Domain;
using KekChessCore.Domain.Extensions;
using KekChessCore.Domain.Impl;

namespace KekChessCore.CastleMoveUtility
{
    public class CastleMoveUtility : ICastleMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;
        private readonly IAttackedCellsUtility _attackedCellsUtility;

        private readonly List<BoardCoordinates> _whiteCastleMoves = new();
        private readonly List<BoardCoordinates> _blackCastleMoves = new();

        public CastleMoveUtility(IBoard board, IAttackedCellsUtility attackedCellsUtility)
        {
            _board = board;
            _attackedCellsUtility = attackedCellsUtility;
            _board.PositionChanged += OnPositionChanged;
            UpdateState();
        }
        
        public BoardCoordinates[] GetCastleMoves(PieceColor color)
        {
            switch (color)
            {
                case PieceColor.White:
                    return _whiteCastleMoves.ToArray();
                case PieceColor.Black:
                    return _blackCastleMoves.ToArray();
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }

        private void OnPositionChanged(IPiece piece)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _whiteCastleMoves.Clear();
            _blackCastleMoves.Clear();
            var whiteKing = _board.Pieces.FirstOrDefault(x => x.Type == PieceType.King && x.Color == PieceColor.White);
            if (whiteKing != null && whiteKing.Position == "e1" && !whiteKing.IsMoved)
            {
                var leftWhiteRook = _board.GetPieceOn("a1");
                if (leftWhiteRook is {Type: PieceType.Rook, Color: PieceColor.White, IsMoved: false} && 
                    !_attackedCellsUtility.IsCellAttacked("a1", PieceColor.Black) &&
                    !_attackedCellsUtility.IsCellAttacked("b1", PieceColor.Black) &&
                    !_attackedCellsUtility.IsCellAttacked("c1", PieceColor.Black) &&
                    _board.GetPieceOn("b1") == null &&
                    _board.GetPieceOn("c1") == null &&
                    _board.GetPieceOn("d1") == null &&
                    !_attackedCellsUtility.IsCellAttacked("d1", PieceColor.Black))
                    _whiteCastleMoves.Add("c1");
                var rightWhiteRook = _board.GetPieceOn("h1");
                if (rightWhiteRook is {Type: PieceType.Rook, Color: PieceColor.White, IsMoved: false} &&
                    !_attackedCellsUtility.IsCellAttacked("f1", PieceColor.Black) &&
                    !_attackedCellsUtility.IsCellAttacked("g1", PieceColor.Black) &&
                    _board.GetPieceOn("f1") == null &&
                    _board.GetPieceOn("g1") == null &&
                    !_attackedCellsUtility.IsCellAttacked("h1", PieceColor.Black))
                    _whiteCastleMoves.Add("g1");
            }
            var blackKing = _board.Pieces.FirstOrDefault(x => x.Type == PieceType.King && x.Color == PieceColor.Black);
            if (blackKing != null && blackKing.Position == "e8" && !blackKing.IsMoved)
            {
                var leftBlackRook = _board.GetPieceOn("a8");
                if (leftBlackRook is {Type: PieceType.Rook, Color: PieceColor.Black, IsMoved: false} &&
                    !_attackedCellsUtility.IsCellAttacked("a8", PieceColor.White) &&
                    !_attackedCellsUtility.IsCellAttacked("b8", PieceColor.White) &&
                    !_attackedCellsUtility.IsCellAttacked("c8", PieceColor.White) &&
                    _board.GetPieceOn("b8") == null &&
                    _board.GetPieceOn("c8") == null &&
                    _board.GetPieceOn("d8") == null &&
                    !_attackedCellsUtility.IsCellAttacked("d8", PieceColor.White))
                    _blackCastleMoves.Add("c8");
                var rightBlackRook = _board.GetPieceOn("h8");
                if (rightBlackRook is {Type: PieceType.Rook, Color: PieceColor.Black, IsMoved: false} &&
                    !_attackedCellsUtility.IsCellAttacked("f8", PieceColor.White) &&
                    !_attackedCellsUtility.IsCellAttacked("g8", PieceColor.White) &&
                    _board.GetPieceOn("f8") == null &&
                    _board.GetPieceOn("g8") == null &&
                    !_attackedCellsUtility.IsCellAttacked("h8", PieceColor.White))
                    _blackCastleMoves.Add("g8");
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}