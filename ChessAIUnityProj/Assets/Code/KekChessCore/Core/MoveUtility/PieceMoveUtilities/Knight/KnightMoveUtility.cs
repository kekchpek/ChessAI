﻿using System.Linq;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.MoveUtility.PieceMoveUtilities.Knight
{
    public class KnightMoveUtility : IKnightMoveUtility
    {
        private readonly IBoard _board;

        public KnightMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
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

            var allyPiecesPositions = _board.Pieces.Where(x => x.Color == color)
                .Select(x => x.Position);
            
            return availableMoves
                .Where(x => x.Item1 >= 0 && x.Item1 <= 7 && x.Item2 >= 0 && x.Item2 <= 7)
                .Select(x => (BoardCoordinates)x)
                .Except(allyPiecesPositions).ToArray();
        }
    }
}