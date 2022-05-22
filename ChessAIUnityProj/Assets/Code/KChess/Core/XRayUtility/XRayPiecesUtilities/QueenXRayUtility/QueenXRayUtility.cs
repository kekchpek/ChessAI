using System.Linq;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using KChess.Domain;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility
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

        public void Dispose()
        {
            // do nothing
        }
    }
}