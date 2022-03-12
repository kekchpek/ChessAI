using System.Collections.Generic;
using KekChessCore.Domain;

namespace KekChessCore.XRayUtility
{
    public interface IXRayUtility
    {
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> TargetPieces { get; }
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> AttackingPieces { get; }
        
    }
}