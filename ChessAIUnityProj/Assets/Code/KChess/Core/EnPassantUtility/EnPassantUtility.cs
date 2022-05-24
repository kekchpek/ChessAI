using System;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.LastMovedPieceUtils;
using KChess.Domain;

namespace KChess.Core.EnPassantUtility
{
    public class EnPassantUtility : IEnPassantUtility, IBoardEnvironmentComponent
    {
        private readonly ILastMovedPieceGetter _lastMovedPieceGetter;

        public EnPassantUtility(ILastMovedPieceGetter lastMovedPieceGetter)
        {
            _lastMovedPieceGetter = lastMovedPieceGetter;
        }
        
        public bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece)
        {
            if (movedPiece.Type != PieceType.Pawn || !movedPiece.Position.HasValue)
            {
                takenPiece = default;
                return false;
            }

            var numericPos = movedPiece.Position.Value.ToNumeric();
            var moveDir = movedPiece.Color == PieceColor.Black ? -1 : 1;
            var enemyPiecesPositions = board.Pieces
                .Where(x => x.Color != movedPiece.Color && x.Position.HasValue)
                .ToDictionary(x => x.Position.Value.ToNumeric());

            var posBehind = (numericPos.Item1, numericPos.Item2 - moveDir);
            if (enemyPiecesPositions.TryGetValue(posBehind, out var enemyPiece) &&
                enemyPiece.Type == PieceType.Pawn &&
                _lastMovedPieceGetter.GetLastMovedPiece() == enemyPiece &&
                enemyPiece.Position.HasValue &&
                Math.Abs(enemyPiece.Position.Value.ToNumeric().Item2 - enemyPiece.PreviousPosition.ToNumeric().Item2) == 2)
            {
                takenPiece = enemyPiece;
                return true;
            }

            takenPiece = default;
            return false;
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}