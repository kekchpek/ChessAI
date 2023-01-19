using KChess.Domain.Impl;
using UnityEngine;

namespace KChessUnity.Models.Board
{
    public interface IBoardWorldPositionsCalculator
    {
        
        /// <summary>
        /// Gets the center of a cell with specified coordinates.
        /// </summary>
        /// <param name="coords">Coordinates of a cell.</param>
        /// <returns>Returns the center of the specified cell.</returns>
        Vector3 GetWorldPosition(BoardCoordinates coords);
        
        /// <summary>
        /// Determines cell coordinates by world position if specified point is inside board borders. Returns null otherwise.
        /// </summary>
        /// <param name="worldPosition">A point in world coordinates.</param>
        /// <returns>Returns cell contains specified point. If no such cell exists returns null.</returns>
        BoardCoordinates? GetCellCoords(Vector3 worldPosition);
    }
}