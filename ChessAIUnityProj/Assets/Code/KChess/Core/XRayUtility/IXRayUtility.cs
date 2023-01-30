using System.Collections.Generic;
using KChess.Domain;

namespace KChess.Core.XRayUtility
{
    internal interface IXRayUtility
    {
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> TargetPieces { get; }
        
        IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> AttackingPieces { get; }
        
    }
}