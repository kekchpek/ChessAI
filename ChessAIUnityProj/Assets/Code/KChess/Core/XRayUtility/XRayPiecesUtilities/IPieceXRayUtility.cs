using KChess.Domain;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities
{
    internal interface IPieceXRayUtility
    {

        IXRay[] GetXRays(IPiece piece);

    }
}