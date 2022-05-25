using System;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.LastMovedPieceUtils;
using KChess.Domain;
using KChess.Domain.Extensions;

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

            var posBehind = (numericPos.Item1, numericPos.Item2 - moveDir);
            var pieceBehind = board.GetPieceOn(posBehind);
            if (pieceBehind != null &&
                pieceBehind.Color != movedPiece.Color &&
                pieceBehind.Type == PieceType.Pawn &&
                _lastMovedPieceGetter.GetLastMovedPiece() == pieceBehind &&
                pieceBehind.Position.HasValue &&
                Math.Abs(pieceBehind.Position.Value.ToNumeric().Item2 - pieceBehind.PreviousPosition.ToNumeric().Item2) == 2)
            {
                takenPiece = pieceBehind;
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