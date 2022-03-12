using KekChessCore.Domain;

namespace KekChessCore.XRayUtility.XRayPiecesUtilities.RookXRayUtility
{
    public class RookXRayUtility : BasePieceXRayUtility
    {
        public RookXRayUtility(IBoard board) : base(board)
        {
        }

        protected override (int, int)[] GetDirections()
        {
            return new[]
            {
                (0, 1),
                (1, 0),
                (-1, 0),
                (0, -1)
            };
        }
    }
}