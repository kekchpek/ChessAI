using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Domain;
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
            var numericPosition = position.ToNumeric();

            var availableMoves = new List<BoardCoordinates>();

            var allyPiecesPositions = _board.Pieces
                .Where(x => x.Color == color)
                .Select(x => x.Position.ToNumeric()).ToArray();
            
            var enemyPiecesPositions = _board.Pieces
                .Where(x => x.Color != color)
                .Select(x => x.Position.ToNumeric()).ToArray();
            
            // up
            for (var i = numericPosition.Item1 + 1; i < 8; i++)
            {
                var cellPos = (i, numericPosition.Item2);
                if (allyPiecesPositions.Contains(cellPos))
                {
                    break;
                }

                if (enemyPiecesPositions.Contains(cellPos))
                {
                    availableMoves.Add(cellPos);
                    break;
                }
                
                availableMoves.Add(cellPos);
            }
            
            // down
            for (var i = numericPosition.Item1 - 1; i >=0; i--)
            {
                var cellPos = (i, numericPosition.Item2);
                if (allyPiecesPositions.Contains(cellPos))
                {
                    break;
                }

                if (enemyPiecesPositions.Contains(cellPos))
                {
                    availableMoves.Add(cellPos);
                    break;
                }
                
                availableMoves.Add(cellPos);
            }
            
            // right
            for (var i = numericPosition.Item2 + 1; i < 8; i++)
            {
                var cellPos = (numericPosition.Item1, i);
                if (allyPiecesPositions.Contains(cellPos))
                {
                    break;
                }

                if (enemyPiecesPositions.Contains(cellPos))
                {
                    availableMoves.Add(cellPos);
                    break;
                }
                
                availableMoves.Add(cellPos);
            }
            
            // left
            for (var i = numericPosition.Item2 - 1; i >= 0; i--)
            {
                var cellPos = (numericPosition.Item1, i);
                if (allyPiecesPositions.Contains(cellPos))
                {
                    break;
                }

                if (enemyPiecesPositions.Contains(cellPos))
                {
                    availableMoves.Add(cellPos);
                    break;
                }
                
                availableMoves.Add(cellPos);
            }

            return availableMoves.ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}