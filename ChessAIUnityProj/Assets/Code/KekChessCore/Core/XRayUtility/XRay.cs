using System.Collections.Generic;
using System.Linq;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.XRayUtility
{
    public readonly struct XRay : IXRay
    {
        
        public IPiece TargetPiece { get; }
        public IPiece AttackingPiece { get; }
        public IReadOnlyList<IPiece> BlockingPieces { get; }
        public IReadOnlyList<BoardCoordinates> CellsBetween { get; }

        public XRay(IPiece targetPiece,
            IPiece attackingPiece,
            IEnumerable<IPiece> blockingPieces,
            IEnumerable<BoardCoordinates> cellsBetween)
        {
            TargetPiece = targetPiece;
            AttackingPiece = attackingPiece;
            BlockingPieces = blockingPieces.ToArray();
            CellsBetween = cellsBetween.ToArray();
        }
        
    }
}