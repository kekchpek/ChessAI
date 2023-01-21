using KChess.Domain.Impl;
using UnityEngine;

namespace KChessUnity.MVVM.Common.BoardPositioning
{
    public class BoardPositionsCalculator : IBoardPositionsCalculator
    {
        private const float BoardSize = 8f;

        private readonly Vector3 _bottomLeftCorner = new(-400f, -400f);
        private readonly Vector3 _size = new(800f, 800f);
        
        public Vector3 GetCellCoordsOnBoardRect(BoardCoordinates coords)
        {
            var numericCoords = coords.ToNumeric();
            return _bottomLeftCorner +
                   Vector3.Scale(_size,
                       new Vector3(numericCoords.Item1 / BoardSize, numericCoords.Item2 / BoardSize, 0f)) +
                   new Vector3(_size.x / 16f, _size.y / 16f);
        }

        public BoardCoordinates? GetCellFromBoardRectCoords(Vector3 rectPosition)
        {
            var x = rectPosition.x - _bottomLeftCorner.x;
            var y = rectPosition.y - _bottomLeftCorner.y;
            
            if (!(x < _size.x) || !(x > 0f) ||
                !(y < _size.y) || !(y > 0f)) 
                return null;
            
            var vertical = (int) Mathf.Floor(x * BoardSize / _size.x);
            var horizontal = (int) Mathf.Floor(y * BoardSize / _size.x);
            return (vertical, horizontal);
        }
    }
}