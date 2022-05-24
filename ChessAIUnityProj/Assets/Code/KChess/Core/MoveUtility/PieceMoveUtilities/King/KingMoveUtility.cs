using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.King
{
    public class KingMoveUtility : IKingMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;

        public KingMoveUtility(IBoard board)
        {
            _board = board;
        }
        
        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            var numericPos = position.ToNumeric();
            var availableMoves = new List<(int, int)>();
            
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        availableMoves.Add((numericPos.Item1 + i, numericPos.Item2 + j));
                    }
                }
            }

            availableMoves = availableMoves
                .Where(x => x.Item1 is >= 0 and <= 7 && x.Item2 is >= 0 and <= 7)
                .ToList();

            var allyPiecesPositions = _board.Pieces.Where(x => x.Position.HasValue).Select(x => x.Position.Value.ToNumeric());
            return availableMoves.Except(allyPiecesPositions).Select(x => (BoardCoordinates)x).ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}