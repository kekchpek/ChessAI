using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using KChessUnity.ViewModels.Board;
using NUnit.Framework;
using UnityEngine;

namespace KChessUnity.Tests
{
    public class BoardViewModelTests
    {

        private const float Delta = 0.0001f;
        
        [TestCase(0.5f, 0.5f, 0, 0)]
        [TestCase(7.5f, 7.5f, 7, 7)]
        [TestCase(2.5f, 3.5f, 2, 3)]
        [TestCase(4.5f, 6.5f, 4, 6)]
        public void GetWorldPosition_CornersSet_PositionCorrect(float x, float y, int vertical, int horizontal)
        {
            // Arrange
            TestHelper.CreateContainerFor<BoardViewModel>(out var boardViewModel);
            
            // Act
            boardViewModel.SetCornerPoints(Vector3.zero, new Vector3(8f, 8f, 0f));
            var worldPosition = boardViewModel.GetWorldPosition((vertical, horizontal));
            
            // Assert
            Assert.IsTrue(x - Delta < worldPosition.x && worldPosition.x < x + Delta);
            Assert.IsTrue(y - Delta < worldPosition.y && worldPosition.y < y + Delta);
        }
        
        
        [TestCase(0.324f, 0.987f, 0, 0)]
        [TestCase(7.001f, 7.555f, 7, 7)]
        [TestCase(2.5f, 3.735f, 2, 3)]
        [TestCase(4.5f, 6.5f, 4, 6)]
        public void GetCellPosition_CornersSet_PositionCorrect(float x, float y, int vertical, int horizontal)
        {
            // Arrange
            TestHelper.CreateContainerFor<BoardViewModel>(out var boardViewModel);
            
            // Act
            boardViewModel.SetCornerPoints(Vector3.zero, new Vector3(8f, 8f, 0f));
            var cellCoords = boardViewModel.GetCellCoords(new Vector3(x, y));
            
            // Assert
            Assert.IsTrue(cellCoords.HasValue);
            Assert.AreEqual(cellCoords.Value, (BoardCoordinates)(vertical, horizontal));
        }

        [TestCase(-1.3f, -2.3f)]
        [TestCase(-1.2f, 3.43f)]
        [TestCase(-1.43f, 10.3f)]
        [TestCase(4.32f, 10.3f)]
        [TestCase(12.3f, 10.3f)]
        [TestCase(12.3f, 2.11f)]
        [TestCase(12.3f, -1.111f)]
        [TestCase(3.23f, -0.13f)]
        public void GetCellPosition_OutOfBoard_ReturnsNull(float x, float y)
        {
            // Arrange
            TestHelper.CreateContainerFor<BoardViewModel>(out var boardViewModel);
            
            // Act
            boardViewModel.SetCornerPoints(Vector3.zero, new Vector3(8f, 8f, 0f));
            var cellCoords = boardViewModel.GetCellCoords(new Vector3(x, y));
            
            // Assert
            Assert.IsFalse(cellCoords.HasValue);
        }
    }
}