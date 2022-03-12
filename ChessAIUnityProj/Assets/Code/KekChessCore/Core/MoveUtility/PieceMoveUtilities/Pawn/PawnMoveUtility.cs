using System.Collections.Generic;
using System.Linq;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.MoveUtility.PieceMoveUtilities.Pawn
{
    public class PawnMoveUtility : IPawnMoveUtility
    {
        private readonly IBoard _board;

        public PawnMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var availableMoves = new List<(int, int)>();
            
            var allyPiecesPositions = _board.Pieces
                .Where(x => x.Color == color)
                .ToDictionary(x => x.Position.ToNumeric());
            var enemyPiecesPositions = _board.Pieces
                .Where(x => x.Color != color)
                .ToDictionary(x => x.Position.ToNumeric());
            
            var moveDirection = color == PieceColor.White ? 1 : -1;
            var startHorizontal = color == PieceColor.White ? 1 : 6;
            var numericPosition = position.ToNumeric();
            var movesCount = numericPosition.Item2 == startHorizontal ? 2 : 1;
            var processingCell = numericPosition;
            while (true)
            {
                processingCell = (processingCell.Item1, processingCell.Item2 + moveDirection);
                if (allyPiecesPositions.ContainsKey(processingCell) || enemyPiecesPositions.ContainsKey(processingCell))
                {
                    break;
                }
                availableMoves.Add(processingCell);
                movesCount--;
                if (movesCount == 0)
                {
                    break;
                }
            }

            // Take
            for (var i = -1; i <= 1; i += 2)
            {
                var processingTakeCell = (numericPosition.Item1 + i, numericPosition.Item2 + moveDirection);
                if (enemyPiecesPositions.ContainsKey(processingTakeCell))
                {
                    availableMoves.Add(processingTakeCell);
                }
            }
            
            // EnPassant
            var enPassantHorizontal = color == PieceColor.Black ? 3 : 4;
            var enemyPreviousPosHorizontal = color == PieceColor.Black ? 1 : 6;
            if (numericPosition.Item2 == enPassantHorizontal)
            {
                for (var i = -1; i <= 1; i += 2)
                {
                    var processingEnPassantCell = (numericPosition.Item1 + i, numericPosition.Item2);
                    if (enemyPiecesPositions.ContainsKey(processingEnPassantCell))
                    {
                        var enemyPiece = enemyPiecesPositions[processingEnPassantCell];
                        if (enemyPiece.Type == PieceType.Pawn && enemyPiece.Color != color
                            && enemyPiece.PreviousPosition.ToNumeric().Item2 == enemyPreviousPosHorizontal)
                        {
                            availableMoves.Add((numericPosition.Item1 + i, numericPosition.Item2 + moveDirection));
                        }
                    }
                }
            }

            return availableMoves.Select(x => (BoardCoordinates)x).ToArray();
        }
        
    }
}