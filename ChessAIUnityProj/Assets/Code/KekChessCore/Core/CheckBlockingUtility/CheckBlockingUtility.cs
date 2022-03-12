using System;
using System.Linq;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.XRayUtility;

namespace KekChessCore.CheckBlockingUtility
{
    public class CheckBlockingUtility : ICheckBlockingUtility
    {
        private readonly IXRayUtility _xRayUtility;

        public CheckBlockingUtility(IXRayUtility xRayUtility)
        {
            _xRayUtility = xRayUtility;
        }
        
        public BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer)
        {
            var checkedKing = _xRayUtility.TargetPieces.Keys.FirstOrDefault(x => x.Type == PieceType.King && x.Color == checkedPlayer);
            if (checkedKing == null)
                return Array.Empty<BoardCoordinates>();
            if (_xRayUtility.TargetPieces[checkedKing].Count > 1)
                return Array.Empty<BoardCoordinates>();
            return _xRayUtility.TargetPieces[checkedKing].First().CellsBetween.ToArray();
        }
    }
}