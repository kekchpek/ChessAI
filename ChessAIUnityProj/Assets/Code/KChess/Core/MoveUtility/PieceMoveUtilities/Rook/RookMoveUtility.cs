using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Domain;
using KChess.Domain.Extensions;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Rook
{
    public class RookMoveUtility : IRookMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;

        public RookMoveUtility(IBoard board)
        {
            _board = board;
        }

        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var allyPiecesPositions = _board.Pieces
                .Where(x => x.Color == color && x.Position.HasValue)
                .Select(x => x.Position.Value).ToArray();

            return GetAttackedCells(position, color).Except(allyPiecesPositions).ToArray();
        }

        public BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor)
        {
            var numericPosition = position.ToNumeric();

            var availableMoves = new List<BoardCoordinates>();
            
            // up
            for (var i = numericPosition.Item1 + 1; i < 8; i++)
            {
                var cellPos = (i, numericPosition.Item2);
                availableMoves.Add(cellPos);
                if (_board.GetPieceOn(cellPos) is not null)
                {
                    break;
                }
            }
            
            // down
            for (var i = numericPosition.Item1 - 1; i >=0; i--)
            {
                var cellPos = (i, numericPosition.Item2);
                availableMoves.Add(cellPos);
                if (_board.GetPieceOn(cellPos) is not null)
                {
                    break;
                }
            }
            
            // right
            for (var i = numericPosition.Item2 + 1; i < 8; i++)
            {
                var cellPos = (numericPosition.Item1, i);
                availableMoves.Add(cellPos);
                if (_board.GetPieceOn(cellPos) is not null)
                {
                    break;
                }
            }
            
            // left
            for (var i = numericPosition.Item2 - 1; i >= 0; i--)
            {
                var cellPos = (numericPosition.Item1, i);
                availableMoves.Add(cellPos);
                if (_board.GetPieceOn(cellPos) is not null)
                {
                    break;
                }
            }

            return availableMoves.ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}