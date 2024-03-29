﻿using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility
{
    internal interface IMoveUtility
    {
        /// <summary>
        /// Returns available moves for the specified figure
        /// </summary>
        /// <param name="piece">The piece, which moves should be calculated</param>
        /// <returns>Available moves</returns>
        BoardCoordinates[] GetAvailableMoves(IPiece piece);
        
        /// <summary>
        /// Returns available moves for the specified figure on empty board.
        /// </summary>
        /// <param name="piece">The piece, which moves should be calculated</param>
        /// <returns>Available moves</returns>
        BoardCoordinates[] GetAttackedMoves(IPiece piece);
    }
}