using System.Collections.Generic;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Domain;
using KChess.Domain.Extensions;
using KChess.Domain.Impl;
using UnityEngine.Assertions;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities
{
    public abstract class BasePieceXRayUtility : IPieceXRayUtility, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;

        public BasePieceXRayUtility(IBoard board)
        {
            _board = board;
        }

        public IXRay[] GetXRays(IPiece piece)
        {
            var boardPiecesMap = _board.GetPiecePositionsMap();
            var xRays = Enumerable.Empty<IXRay>();
            xRays = GetDirections().Aggregate(xRays, (current, direction) => current.Concat(GetXRaysFromDirection(direction, piece, boardPiecesMap)));
            return xRays.ToArray();
        }

        protected abstract (int, int)[] GetDirections();
        
        protected IXRay[] GetXRaysFromDirection((int, int) direction, IPiece piece, IReadOnlyDictionary<BoardCoordinates, IPiece> pieces)
        {
            var xRays = new List<IXRay>();
            Assert.IsTrue(piece.Position.HasValue);
            var numericPosition = piece.Position.Value.ToNumeric();
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

        public void Dispose()
        {
            // do nothing
        }
    }
}