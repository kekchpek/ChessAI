﻿using KekChessCore.Domain;

namespace KekChessCore.XRayUtility.XRayPiecesUtilities.BishopXRayUtility
{
    public class BishopXRayUtility : BasePieceXRayUtility
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