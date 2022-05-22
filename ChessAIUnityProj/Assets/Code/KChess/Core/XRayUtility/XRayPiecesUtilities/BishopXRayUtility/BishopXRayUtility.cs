using KChess.Domain;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility
{
    public class BishopXRayUtility : BasePieceXRayUtility, IBishopXRayUtility
    {
        public BishopXRayUtility(IBoard board) : base(board)
        {
        }

        protected override (int, int)[] GetDirections()
        {
            return new[]
            {
                (-1, 1),
                (1, -1),
                (1, 1),
                (-1, -1)
            };
        }
    }
}