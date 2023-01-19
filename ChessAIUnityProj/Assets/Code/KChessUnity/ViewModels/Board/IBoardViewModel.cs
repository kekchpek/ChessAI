using KChess.Domain.Impl;
using UnityEngine;
using UnityMVVM.ViewModelCore;

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
    }
}