using KekChessCore.Domain;

namespace KekChessCore.XRayUtility.XRayPiecesUtilities
{
    public interface IPieceXRayUtility
    {

        IXRay[] GetXRays(IPiece piece);

    }
}