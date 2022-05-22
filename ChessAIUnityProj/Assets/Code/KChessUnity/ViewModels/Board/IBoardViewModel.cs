using KChess.Domain.Impl;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.ViewModels.Board
{
    public interface IBoardViewModel : IViewModel
    {
        
        /// <summary>
        /// Sets the corner points for the board. They are required to determine cells world positions.
        /// </summary>
        /// <param name="bottomLeft">Bottom left point of the board.</param>
        /// <param name="topRight">Top right point of the board.</param>
        void SetCornerPoints(Vector3 bottomLeft, Vector3 topRight);
        
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