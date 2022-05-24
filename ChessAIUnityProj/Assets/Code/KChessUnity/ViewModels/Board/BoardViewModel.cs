using KChess.Domain.Impl;
using MVVMCore;
using UnityEngine;

namespace KChessUnity.ViewModels.Board
{
    public class BoardViewModel : ViewModel, IBoardViewModel
    {

        private const float BoardSize = 8f;

        private Vector3 _bottomLeftCorner;
        private Vector3 _size;

        public void SetCornerPoints(Vector3 bottomLeft, Vector3 topRight)
        {
            _bottomLeftCorner = bottomLeft;
            _size = new Vector3(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y, topRight.z - bottomLeft.z);
        }

        public Vector3 GetWorldPosition(BoardCoordinates coords)
        {
            var numericCoords = coords.ToNumeric();
            return _bottomLeftCorner +
                   Vector3.Scale(_size,
                       new Vector3(numericCoords.Item1 / BoardSize, numericCoords.Item2 / BoardSize, 0f)) +
                   new Vector3(_size.x / 16f, _size.y / 16f);
        }

        public BoardCoordinates? GetCellCoords(Vector3 worldPosition)
        {
            var x = worldPosition.x - _bottomLeftCorner.x;
            var y = worldPosition.y - _bottomLeftCorner.y;
            
            if (!(x < _size.x) || !(x > 0f) ||
                !(y < _size.y) || !(y > 0f)) 
                return null;
            
            var vertical = (int) Mathf.Floor(x * BoardSize / _size.x);
            var horizontal = (int) Mathf.Floor(y * BoardSize / _size.x);
            return (vertical, horizontal);

        }
    }
}