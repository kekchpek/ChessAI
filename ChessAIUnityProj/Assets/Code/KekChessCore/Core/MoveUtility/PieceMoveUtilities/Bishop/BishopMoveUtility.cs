using System.Collections.Generic;
using System.Linq;
using KekChessCore.BoardEnvironment;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.MoveUtility.PieceMoveUtilities.Bishop
{
    public class BishopMoveUtility : IBishopMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;

        public BishopMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var availableMoves = new List<BoardCoordinates>();

            var allyPiecesPositions = _board.Pieces
                .Where(x => x.Color == color)
                .Select(x => x.Position.ToNumeric()).ToArray();
            
            var enemyPiecesPositions = _board.Pieces
                .Where(x => x.Color != color)
                .Select(x => x.Position.ToNumeric()).ToArray();

            (int, int) processingCell;
            
            // up-right
            processingCell = position.ToNumeric();
            while (true)
            {
                processingCell.Item1++;
                processingCell.Item2++;
                if (OutOfBorder(processingCell))
                {
                    break;
                }
                if (allyPiecesPositions.Contains(processingCell))
                {
                    break;
                }
                if (enemyPiecesPositions.Contains(processingCell))
                {
                    availableMoves.Add(processingCell);
                    break;
                }
                availableMoves.Add(processingCell);
            }
            
            // down-right
            processingCell = position.ToNumeric();
            while (true)
            {
                processingCell.Item1++;
                processingCell.Item2--;
                if (OutOfBorder(processingCell))
                {
                    break;
                }
                if (allyPiecesPositions.Contains(processingCell))
                {
                    break;
                }
                if (enemyPiecesPositions.Contains(processingCell))
                {
                    availableMoves.Add(processingCell);
                    break;
                }
                availableMoves.Add(processingCell);
            }
            
            // down-left
            processingCell = position.ToNumeric();
            while (true)
            {
                processingCell.Item1--;
                processingCell.Item2--;
                if (OutOfBorder(processingCell))
                {
                    break;
                }
                if (allyPiecesPositions.Contains(processingCell))
                {
                    break;
                }
                if (enemyPiecesPositions.Contains(processingCell))
                {
                    availableMoves.Add(processingCell);
                    break;
                }
                availableMoves.Add(processingCell);
            }
            
            // up-left
            processingCell = position.ToNumeric();
            while (true)
            {
                processingCell.Item1--;
                processingCell.Item2++;
                if (OutOfBorder(processingCell))
                {
                    break;
                }
                if (allyPiecesPositions.Contains(processingCell))
                {
                    break;
                }
                if (enemyPiecesPositions.Contains(processingCell))
                {
                    availableMoves.Add(processingCell);
                    break;
                }
                availableMoves.Add(processingCell);
            }

            return availableMoves.ToArray();
        }

        private static bool OutOfBorder((int, int) cell)
        {
            var (item1, item2) = cell;
            return item1 < 0 || item1 > 7 || item2 < 0 || item2 > 7;
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}