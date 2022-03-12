using ChessAI.Domain;

namespace ChessAI.Core.XRayUtility.XRayPiecesUtilities
{
    public interface IPieceXRayUtility
    {

        IXRay[] GetXRays(IPiece piece);

    }
}