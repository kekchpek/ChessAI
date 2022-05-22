using System.Linq;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace KChessUnity.Tests
{
    public class MovesDisplayerViewModelTests
    {
        [Test]
        public void ShowMoves_PositionsHighlighted()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<MovesDisplayerViewModel>(out var movesDisplayer);
            var boardViewModel = container.Resolve<IBoardViewModel>();

            var availableMoves = new[]
            {
                (BoardCoordinates) "d3",
                (BoardCoordinates) "a1",
                (BoardCoordinates) "f6",
                (BoardCoordinates) "c3",
            };

            var availableMovesPositions = new[]
            {
                new Vector3(12f, 33f, 1234f),
                new Vector3(43f, 32f, 1.333f),
                new Vector3(1f, 3f, 4f),
                new Vector3(0.12343f, 99999.99999f, 34.43f),
            };
            
            for (var i = 0; i < availableMoves.Length; i++)
            {
                boardViewModel.GetWorldPosition(availableMoves[i]).Returns(availableMovesPositions[i]);
            }

            // Act
            movesDisplayer.ShowMoves(availableMoves);
            
            // Assert
            Assert.AreEqual(availableMoves.Length, movesDisplayer.HighlightedPositions.Count);
            for (var i = 0; i < availableMoves.Length; i++)
            {
                Assert.IsTrue(movesDisplayer.HighlightedPositions.Contains(availableMovesPositions[i]));
            }
        }
        
        [Test]
        public void HideMoves_HighlightedMovesCleared()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<MovesDisplayerViewModel>(out var movesDisplayer);
            var boardViewModel = container.Resolve<IBoardViewModel>();

            var availableMoves = new[]
            {
                (BoardCoordinates) "d3",
                (BoardCoordinates) "a1",
                (BoardCoordinates) "f6",
                (BoardCoordinates) "c3",
            };

            var availableMovesPositions = new[]
            {
                new Vector3(12f, 33f, 1234f),
                new Vector3(43f, 32f, 1.333f),
                new Vector3(1f, 3f, 4f),
                new Vector3(0.12343f, 99999.99999f, 34.43f),
            };
            
            for (var i = 0; i < availableMoves.Length; i++)
            {
                boardViewModel.GetWorldPosition(availableMoves[i]).Returns(availableMovesPositions[i]);
            }

            // Act
            movesDisplayer.ShowMoves(availableMoves);
            movesDisplayer.HideMoves();
            
            // Assert
            Assert.IsEmpty(movesDisplayer.HighlightedPositions);
        }
    }
}