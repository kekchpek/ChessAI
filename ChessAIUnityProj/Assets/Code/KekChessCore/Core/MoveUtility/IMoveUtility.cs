﻿using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.MoveUtility
{
    public interface IMoveUtility
    {
        /// <summary>
        /// Returns available moves for the specified figure
        /// </summary>
        /// <param name="piece">The piece, which moves should be calculated</param>
        /// <returns>Available moves</returns>
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
    }
}