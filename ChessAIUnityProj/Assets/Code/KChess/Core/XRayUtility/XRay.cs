using System.Collections.Generic;
using System.Linq;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.XRayUtility
{
    public readonly struct XRay : IXRay
    {
        public IPiece TargetPiece { get; }
        public IPiece AttackingPiece { get; }
        public IReadOnlyList<IPiece> BlockingPieces { get; }
        public IReadOnlyList<BoardCoordinates> CellsBetween { get; }
        public IReadOnlyList<BoardCoordinates> CellsBehind { get; }
        
        public XRay(IPiece targetPiece,
            IPiece attackingPiece,
            IEnumerable<IPiece> blockingPieces,
            IEnumerable<BoardCoordinates> cellsBetween, 
            IReadOnlyList<BoardCoordinates> cellsBehind)
        {
            TargetPiece = targetPiece;
            AttackingPiece = attackingPiece;
            CellsBehind = cellsBehind;
            BlockingPieces = blockingPieces.ToArray();
            CellsBetween = cellsBetween.ToArray();
        }
        
    }
}