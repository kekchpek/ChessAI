using KChess.Domain;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities
{
    public interface IPieceXRayUtility
    {

        IXRay[] GetXRays(IPiece piece);

    }
}