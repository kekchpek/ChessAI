using System;
using System.Collections.Generic;
using System.Linq;
using KChess.Domain.Impl;
using KChessUnity.Models.HighlightedCells;
using KChessUnity.MVVM.Common.BoardPositioning;
using KChessUnity.MVVM.Views.MovesDisplayer;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.Tests
{
    public class MovesDisplayerViewModelTests
    {
        [Test]
        public void ShowMoves_PositionsHighlighted()
        {
            // Arrange 
            var container = TestHelper.CreateContainerForViewModel<MovesDisplayerViewModel>(out var movesDisplayer);
            var positionCalculator = container.Resolve<IBoardPositionsCalculator>();
            var highlightedMovesModel = container.Resolve<IHighlightedCellsModel>();

            var cellsProp = new Mutable<IEnumerable<BoardCoordinates>>(Array.Empty<BoardCoordinates>());
            highlightedMovesModel.HighlightedCells.Returns(cellsProp);

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
                positionCalculator.GetCellCoordsOnBoardRect(availableMoves[i]).Returns(availableMovesPositions[i]);
            }

            // Act
            movesDisplayer.Initialize();
            cellsProp.Value = availableMoves;
            
            // Assert
            Assert.AreEqual(availableMoves.Length, movesDisplayer.HighlightedPositions.Value.Count);
            for (var i = 0; i < availableMoves.Length; i++)
            {
                Assert.IsTrue(movesDisplayer.HighlightedPositions.Value.Contains(availableMovesPositions[i]));
            }
        }
        
        [Test]
        public void HideMoves_HighlightedMovesCleared()
        {
            // Arrange 
            var container = TestHelper.CreateContainerForViewModel<MovesDisplayerViewModel>(out var movesDisplayer);
            var positionCalculator = container.Resolve<IBoardPositionsCalculator>();
            var highlightedMovesModel = container.Resolve<IHighlightedCellsModel>();

            var cellsProp = new Mutable<IEnumerable<BoardCoordinates>>(Array.Empty<BoardCoordinates>());
            highlightedMovesModel.HighlightedCells.Returns(cellsProp);

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
                positionCalculator.GetCellCoordsOnBoardRect(availableMoves[i]).Returns(availableMovesPositions[i]);
            }

            // Act
            movesDisplayer.Initialize();
            cellsProp.Value = availableMoves;
            cellsProp.Value = Array.Empty<BoardCoordinates>();
            
            // Assert
            Assert.IsEmpty(movesDisplayer.HighlightedPositions.Value);
        }
    }
}