using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.LastMovedPieceUtils;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Pawn
{
    public class PawnMoveUtility : IPawnMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;
        private readonly ILastMovedPieceGetter _lastMovedPieceGetter;

        public PawnMoveUtility(IBoard board, ILastMovedPieceGetter lastMovedPieceGetter)
        {
            _board = board;
            _lastMovedPieceGetter = lastMovedPieceGetter;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var availableMoves = new List<(int, int)>();
            
            var allyPiecesPositions = _board.Pieces
                .Where(x => x.Color == color && x.Position.HasValue)
                .ToDictionary(x => x.Position.Value.ToNumeric());
            var enemyPiecesPositions = _board.Pieces
                .Where(x => x.Color != color && x.Position.HasValue)
                .ToDictionary(x => x.Position.Value.ToNumeric());
            
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
            foreach (var attackedCell in GetAttackedCells(position, color))
            {
                if (enemyPiecesPositions.ContainsKey(attackedCell.ToNumeric()))
                {
                    availableMoves.Add(attackedCell.ToNumeric());
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
                            && enemyPiece.PreviousPosition.ToNumeric().Item2 == enemyPreviousPosHorizontal
                            && _lastMovedPieceGetter.GetLastMovedPiece() == enemyPiece)
                        {
                            availableMoves.Add((numericPosition.Item1 + i, numericPosition.Item2 + moveDirection));
                        }
                    }
                }
            }

            return availableMoves.Select(x => (BoardCoordinates)x).ToArray();
        }

        public BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor)
        {
            var moveDirection = pieceColor == PieceColor.White ? 1 : -1;
            var numericPosition = position.ToNumeric();
            var attackingCells = new List<BoardCoordinates>();
            for (var i = -1; i <= 1; i += 2)
            {
                var attackingCoords = (numericPosition.Item1 + i, numericPosition.Item2 + moveDirection);
                if (attackingCoords.Item1 is <= 7 and >= 0 && attackingCoords.Item2 is <= 7 and >= 0)
                {
                    attackingCells.Add(attackingCoords);
                }
            }

            return attackingCells.ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
        
    }
}