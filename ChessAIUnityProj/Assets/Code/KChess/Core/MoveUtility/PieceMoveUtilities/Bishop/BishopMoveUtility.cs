using System.Collections.Generic;
using System.Linq;
using KChess.Domain;
using KChess.Domain.Extensions;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Bishop
{
    internal class BishopMoveUtility : IBishopMoveUtility
    {
        private readonly IBoard _board;

        public BishopMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var allyPiecesPositions = _board.Pieces.Where(x => x.Color == color)
                .Where(x => x.Position.HasValue)
                .Select(x => x.Position.Value);

            return GetAttackedCells(position, color).Except(allyPiecesPositions).ToArray();
        }

        public BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor)
        {
            var availableMoves = new List<BoardCoordinates>();

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
                availableMoves.Add(processingCell);
                if (_board.GetPieceOn(processingCell) is not null)
                {
                    break;
                }
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
                availableMoves.Add(processingCell);
                if (_board.GetPieceOn(processingCell) is not null)
                {
                    break;
                }
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
                availableMoves.Add(processingCell);
                if (_board.GetPieceOn(processingCell) is not null)
                {
                    break;
                }
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
                availableMoves.Add(processingCell);
                if (_board.GetPieceOn(processingCell) is not null)
                {
                    break;
                }
            }

            return availableMoves.ToArray();
        }

        private static bool OutOfBorder((int, int) cell)
        {
            var (item1, item2) = cell;
            return item1 < 0 || item1 > 7 || item2 < 0 || item2 > 7;
        }
    }
}