using System.Collections.Generic;
using System.Linq;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.XRayUtility.XRayPiecesUtilities
{
    public abstract class BasePieceXRayUtility : IPieceXRayUtility
    {
        private readonly IBoard _board;

        public BasePieceXRayUtility(IBoard board)
        {
            _board = board;
        }

        public IXRay[] GetXRays(IPiece piece)
        {
            var boardPiecesMap = _board.Pieces.ToDictionary(x => x.Position);
            var xRays = Enumerable.Empty<IXRay>();
            xRays = GetDirections().Aggregate(xRays, (current, direction) => current.Concat(GetXRaysFromDirection(direction, piece, boardPiecesMap)));
            return xRays.ToArray();
        }

        protected abstract (int, int)[] GetDirections();
        
        protected IXRay[] GetXRaysFromDirection((int, int) direction, IPiece piece, IReadOnlyDictionary<BoardCoordinates, IPiece> pieces)
        {
            var xRays = new List<IXRay>();
            var numericPosition = piece.Position.ToNumeric();
            var blockingPieces = new List<IPiece>();
            var cellsBetween = new List<BoardCoordinates>();
            while (true)
            {
                numericPosition = (numericPosition.Item1 + direction.Item1, numericPosition.Item2 + direction.Item2);
                if (numericPosition.Item1 < 0 || numericPosition.Item1 > 7 ||
                    numericPosition.Item2 < 0 || numericPosition.Item2 > 7)
                {
                    break;
                }
                
                cellsBetween.Add(numericPosition);

                if (pieces.ContainsKey(numericPosition))
                {
                    if (pieces[numericPosition].Color != piece.Color)
                    {
                        xRays.Add(new XRay(pieces[numericPosition], piece, blockingPieces, cellsBetween));
                    }
                    blockingPieces.Add(pieces[numericPosition]);
                }
            }

            return xRays.ToArray();
        }
    }
}