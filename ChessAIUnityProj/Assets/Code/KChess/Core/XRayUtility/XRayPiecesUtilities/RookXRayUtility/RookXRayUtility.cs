﻿using KChess.Domain;

namespace KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility
{
    internal class RookXRayUtility : BasePieceXRayUtility, IRookXRayUtility
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