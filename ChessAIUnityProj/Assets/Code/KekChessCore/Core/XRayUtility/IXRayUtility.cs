using System.Collections.Generic;
using ChessAI.Domain;

namespace ChessAI.Core.XRayUtility
{
    public interface IXRayUtility
    {
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> TargetPieces { get; }
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> AttackingPieces { get; }
        
    }
}