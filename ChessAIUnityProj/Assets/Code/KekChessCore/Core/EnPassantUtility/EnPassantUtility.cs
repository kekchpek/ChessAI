using System;
using System.Linq;
using KekChessCore.Domain;
using KekChessCore.LastMovedPieceUtils;

namespace KekChessCore.EnPassantUtility
{
    public class EnPassantUtility : IEnPassantUtility
    {
        private readonly ILastMovedPieceGetter _lastMovedPieceGetter;

        public EnPassantUtility(ILastMovedPieceGetter lastMovedPieceGetter)
        {
            _lastMovedPieceGetter = lastMovedPieceGetter;
        }
        
        public bool IsFigureWasTakenWithEnPassant(IPiece movedPiece, IBoard board, out IPiece takenPiece)
        {
            if (movedPiece.Type != PieceType.Pawn)
            {
                takenPiece = default;
                return false;
            }

            var numericPos = movedPiece.Position.ToNumeric();
            var moveDir = movedPiece.Color == PieceColor.Black ? -1 : 1;
            var enemyPiecesPositions = board.Pieces
                .Where(x => x.Color != movedPiece.Color)
                .ToDictionary(x => x.Position.ToNumeric());

            var posBehind = (numericPos.Item1, numericPos.Item2 - moveDir);
            if (enemyPiecesPositions.TryGetValue(posBehind, out var enemyPiece) &&
                enemyPiece.Type == PieceType.Pawn &&
                _lastMovedPieceGetter.GetLastMovedPiece() == enemyPiece &&
                Math.Abs(enemyPiece.Position.ToNumeric().Item2 - enemyPiece.PreviousPosition.ToNumeric().Item2) == 2)
            {
                takenPiece = enemyPiece;
                return true;
            }

            takenPiece = default;
            return false;
        }
    }
}