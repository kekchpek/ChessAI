﻿using System.Linq;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Knight
{
    internal class KnightMoveUtility : IKnightMoveUtility
    {
        private readonly IBoard _board;

        public KnightMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var attackedCells = GetAttackedPositionsNumeric(position);

            var allyPiecesPositions = _board.Pieces.Where(x => x.Color == color)
                .Where(x => x.Position.HasValue)
                .Select(x => x.Position.Value);
            
            return attackedCells
                .Where(x => x.Item1 >= 0 && x.Item1 <= 7 && x.Item2 >= 0 && x.Item2 <= 7)
                .Select(x => (BoardCoordinates)x)
                .Except(allyPiecesPositions).ToArray();
        }

        public BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor)
        {
            return GetAttackedPositionsNumeric(position).Select(x => (BoardCoordinates) x).ToArray();
        }

        private (int, int)[] GetAttackedPositionsNumeric(BoardCoordinates position)
        {
            var numericCoords = position.ToNumeric();

            var availableMoves = new (int, int)[8];
            availableMoves[0] = (numericCoords.Item1 - 2, numericCoords.Item2 - 1);
            availableMoves[1] = (numericCoords.Item1 - 2, numericCoords.Item2 + 1);
            availableMoves[2] = (numericCoords.Item1 + 2, numericCoords.Item2 - 1);
            availableMoves[3] = (numericCoords.Item1 + 2, numericCoords.Item2 + 1);
            availableMoves[4] = (numericCoords.Item1 - 1, numericCoords.Item2 - 2);
            availableMoves[5] = (numericCoords.Item1 - 1, numericCoords.Item2 + 2);
            availableMoves[6] = (numericCoords.Item1 + 1, numericCoords.Item2 - 2);
            availableMoves[7] = (numericCoords.Item1 + 1, numericCoords.Item2 + 2);

            return availableMoves;
        }
    }
}