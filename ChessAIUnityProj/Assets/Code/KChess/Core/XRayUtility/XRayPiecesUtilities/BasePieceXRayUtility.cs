using System.Collections.Generic;
using System.Linq;
using KChess.Domain;
using KChess.Domain.Extensions;
using KChess.Domain.Impl;
using UnityEngine.Assertions;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities
{
    internal abstract class BasePieceXRayUtility : IPieceXRayUtility
    {
        private readonly IBoard _board;

        protected BasePieceXRayUtility(IBoard board)
        {
            _board = board;
        }

        public IXRay[] GetXRays(IPiece piece)
        {
            var boardPiecesMap = _board.GetPiecePositionsMap();
            var xRays = Enumerable.Empty<IXRay>();
            xRays = GetDirections()
                .Aggregate(xRays, (current, direction) => current.Concat(GetXRaysFromDirection(direction, piece, boardPiecesMap)));
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
            var cellsBehind = new List<BoardCoordinates>();
            (IPiece TargetPiece, IPiece AttackingPiece, IEnumerable<IPiece> BlockingPieces,
                IEnumerable<BoardCoordinates> CellsBetween)? 
                xRayData = null;
            while (true)
            {
                numericPosition = (numericPosition.Item1 + direction.Item1, numericPosition.Item2 + direction.Item2);
                if (numericPosition.Item1 < 0 || numericPosition.Item1 > 7 ||
                    numericPosition.Item2 < 0 || numericPosition.Item2 > 7)
                {
                    break;
                }
                
                cellsBetween.Add(numericPosition);
                cellsBehind.Add(numericPosition);

                if (pieces.ContainsKey(numericPosition))
                {
                    if (pieces[numericPosition].Color != piece.Color)
                    {
                        if (xRayData.HasValue)
                        {
                            xRays.Add(new XRay(xRayData.Value.TargetPiece, xRayData.Value.AttackingPiece,
                                xRayData.Value.BlockingPieces, xRayData.Value.CellsBetween, cellsBehind.ToArray()));
                        }

                        xRayData = (pieces[numericPosition],
                            piece, blockingPieces.ToArray(),
                            cellsBetween.ToArray());
                        cellsBehind.Clear();
                    }
                    blockingPieces.Add(pieces[numericPosition]);
                }
            }
            if (xRayData.HasValue) // process last xRay
            {
                xRays.Add(new XRay(xRayData.Value.TargetPiece, xRayData.Value.AttackingPiece,
                    xRayData.Value.BlockingPieces, xRayData.Value.CellsBetween, cellsBehind.ToArray()));
                cellsBehind.Clear();
            }

            return xRays.ToArray();
        }
    }
}