using System;
using System.Linq;
using Castle.Core.Internal;
using KChess.Core.BoardEnvironment;
using KChess.Core.XRayUtility;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.CheckBlockingUtility
{
    public class CheckBlockingUtility : ICheckBlockingUtility, IBoardEnvironmentComponent
    {
        private readonly IXRayUtility _xRayUtility;

        public CheckBlockingUtility(IXRayUtility xRayUtility)
        {
            _xRayUtility = xRayUtility;
        }
        
        public BoardCoordinates[] GetMovesForCheckBlocking(PieceColor checkedPlayer)
        {
            var checkedKing = _xRayUtility.TargetPieces
                .FirstOrDefault(x => x.Key.Type == PieceType.King && x.Key.Color == checkedPlayer
                && x.Value.Any(xray => xray.BlockingPieces.IsNullOrEmpty())).Key;
            if (checkedKing == null)
                return Array.Empty<BoardCoordinates>();
            return _xRayUtility.TargetPieces[checkedKing].First(x => x.BlockingPieces.IsNullOrEmpty()).CellsBetween.ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}