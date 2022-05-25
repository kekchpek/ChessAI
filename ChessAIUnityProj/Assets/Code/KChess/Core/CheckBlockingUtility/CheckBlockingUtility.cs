using System;
using System.Linq;
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
            var checkedKing = _xRayUtility.TargetPieces.Keys.FirstOrDefault(x => x.Type == PieceType.King && x.Color == checkedPlayer);
            if (checkedKing == null)
                return Array.Empty<BoardCoordinates>();
            return _xRayUtility.TargetPieces[checkedKing].First().CellsBetween.ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}