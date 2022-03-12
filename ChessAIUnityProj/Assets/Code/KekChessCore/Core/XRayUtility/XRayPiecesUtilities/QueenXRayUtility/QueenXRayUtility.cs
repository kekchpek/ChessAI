using System.Linq;
using ChessAI.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using ChessAI.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using ChessAI.Domain;

namespace ChessAI.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility
{
    public class QueenXRayUtility : IQueenXRayUtility
    {
        private readonly IBishopXRayUtility _bishopXRayUtility;
        private readonly IRookXRayUtility _rookXRayUtility;

        public QueenXRayUtility(IBishopXRayUtility bishopXRayUtility, IRookXRayUtility rookXRayUtility)
        {
            _bishopXRayUtility = bishopXRayUtility;
            _rookXRayUtility = rookXRayUtility;
        }
        
        public IXRay[] GetXRays(IPiece piece)
        {
            return _bishopXRayUtility.GetXRays(piece)
                .Concat(_rookXRayUtility.GetXRays(piece))
                .ToArray();
        }
    }
}