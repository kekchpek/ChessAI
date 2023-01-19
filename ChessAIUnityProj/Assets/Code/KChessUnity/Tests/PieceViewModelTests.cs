using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain.Impl;
using KChessUnity.Input;
using KChessUnity.Models;
using KChessUnity.Tests.Helper;
using KChessUnity.ViewModels.Piece;
using KChessUnity.ViewModels.Triggers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace KChessUnity.Tests
{
    public class PieceViewModelTests
    {
        [Test]
        public void Initialization_BoardMocked_PositionSet()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var piece = payload.Piece;
            piece.Position.Returns("d3");
            var boardPos = new Vector3(234f, 1f, 12f);
            // ReSharper disable once PossibleInvalidOperationException
            positionCalculator.GetWorldPosition(piece.Position.Value).Returns(boardPos);

            // Act
            pieceViewModel.Initialize();

            // Assert
            Assert.AreEqual(boardPos, pieceViewModel.Position.Value);
        }
        
        [Test]
        public void Initialization_BoardMocked_ImageSetSet()
        {
            // Arrange
            TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);

            // Act
            pieceViewModel.Initialize();

            // Assert
            Assert.IsNotNull(pieceViewModel.Image);
        }

        [Test]
        public void PieceRemovedFromBoard_EventFired_DisposedEventFired()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var piece = payload.Piece;
            var isDisposed = false;
            pieceViewModel.Disposed += () => isDisposed = true;

            // Act
            pieceViewModel.Initialize();
            piece.Removed += Raise.Event<Action>();

            // Assert
            Assert.IsTrue(isDisposed);
        }
        
        [Test]
        public void PieceDragged_ReleaseOnTheOtherCell_PieceMoved()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var playerFacade = payload.PlayerFacade;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);
            var movedPosition = new Vector2(333f, 111f);
            var newCell = (BoardCoordinates) "d6";

            piece.Position.Returns(piecePosition);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);
            positionCalculator.GetCellCoords(movedPosition).Returns(newCell);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(movedPosition);

            // Assert
            playerFacade.Received().TryMovePiece(piece, newCell);
        }
        
        [Test]
        public void PieceClicked_ResetSelectionTrigger_Triggered()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var resetSelectionTrigger = container.Resolve<IResetSelectionTrigger>();
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(clickPosition);

            // Assert
            resetSelectionTrigger.Received().Trigger();
        }
        
        [Test]
        public void PieceClickDown_AvailableMovesShown()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var movesDisplayer = container.Resolve<IHighlightedCellsService>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var playerFacade = payload.PlayerFacade;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);

            var availableMoves = new[]
            {
                (BoardCoordinates) "d1",
                (BoardCoordinates) "d2",
                (BoardCoordinates) "d3",
            };
            playerFacade.GetAvailableMoves(piece).Returns(availableMoves);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);

            // Assert
            movesDisplayer.Received().SetHighlightedCells(availableMoves);
        }
        
        [Test]
        public void CellClicked_NotSameAsPiecePosition_AvailableMovesHid(
            [Values(true, false)] bool isPieceMoved)
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var movesDisplayer = container.Resolve<IHighlightedCellsService>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var playerFacade = payload.PlayerFacade;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);
            var movedPosition = new Vector2(333f, 111f);
            var newCell = (BoardCoordinates) "d6";

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);
            positionCalculator.GetCellCoords(movedPosition).Returns(newCell);

            piece.Position.Returns(piecePosition);

            playerFacade.TryMovePiece(default, default).ReturnsForAnyArgs(isPieceMoved);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(movedPosition);

            // Assert
            movesDisplayer.Received().ClearHighlightedCells();
        }
        
        [Test]
        public void PieceSelected_AvailableMovesNotHid()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var movesDisplayer = container.Resolve<IHighlightedCellsService>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var playerFacade = payload.PlayerFacade;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);

            var availableMoves = new[]
            {
                (BoardCoordinates) "d1",
                (BoardCoordinates) "d2",
                (BoardCoordinates) "d3",
            };
            playerFacade.GetAvailableMoves(piece).Returns(availableMoves);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(clickPosition);

            // Assert
            movesDisplayer.DidNotReceive().ClearHighlightedCells();
        }

        [Test]
        public void PieceClicked_MousePositionChanged_PiecePositionChanged()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mousePos = new Vector2(123f, 234f);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MousePositionChanged += Raise.Event<Action<Vector2>>(mousePos);
            
            // Assert 
            Assert.AreEqual(mousePos.x, pieceViewModel.Position.Value.x);
            Assert.AreEqual(mousePos.y, pieceViewModel.Position.Value.y);
        }
        
        [Test]
        public void PieceSelected_MousePositionChanged_PiecePositionNotChanged()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            positionCalculator.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

            piece.Position.Returns(piecePosition);
            
            var mousePos = new Vector2(123f, 234f);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            
            inputController.MousePositionChanged += Raise.Event<Action<Vector2>>(mousePos);
            
            // Assert 
            Assert.AreEqual((Vector3)pieceWorldPosition, pieceViewModel.Position.Value);
        }

        [Test]
        public void BoardClicked_DownUpAtSameCell_PieceMoved()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var inputController = container.Resolve<IInputController>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var playerFacade = payload.PlayerFacade;
            var piece = payload.Piece;

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            positionCalculator.GetCellCoords(mouseDownPos).Returns(boardCoords);
            positionCalculator.GetCellCoords(mouseUpPos).Returns(boardCoords);

            // Act
            pieceViewModel.Initialize();
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(clickPosition);
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(mouseDownPos);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(mouseUpPos);
            
            // Assert
            playerFacade.Received().TryMovePiece(piece, boardCoords);
        }

        [Test]
        public void PieceSelected_SelectionResetTriggered_MovesHid()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var selectionResetTrigger = container.Resolve<IResetSelectionTrigger>();
            var movesDisplayer = container.Resolve<IHighlightedCellsService>();

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            positionCalculator.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

            piece.Position.Returns(piecePosition);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            selectionResetTrigger.OnTriggered += Raise.Event<Action>();

            // Assert
            movesDisplayer.ClearHighlightedCells();
        }

        [Test]
        public void PieceSelected_SelectionResetTriggered_PieceDoesntMove()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var inputController = container.Resolve<IInputController>();
            var piece = payload.Piece;
            var selectionResetTrigger = container.Resolve<IResetSelectionTrigger>();
            var playerFacade = payload.PlayerFacade;

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            positionCalculator.GetCellCoords(mouseDownPos).Returns(boardCoords);
            positionCalculator.GetCellCoords(mouseUpPos).Returns(boardCoords);
            positionCalculator.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            positionCalculator.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

            piece.Position.Returns(piecePosition);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            
            selectionResetTrigger.OnTriggered += Raise.Event<Action>();
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(mouseDownPos);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(mouseUpPos);

            // Assert
            playerFacade.DidNotReceiveWithAnyArgs().TryMovePiece(default, default);
        }

        [Test]
        public void BoardClicked_DownUpAtDifferentCells_PieceMoved()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var inputController = container.Resolve<IInputController>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var playerFacade = payload.PlayerFacade;
            var piece = payload.Piece;
            
            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardDownCoords = (BoardCoordinates) "g5";
            var boardUpCoords = (BoardCoordinates) "f5";
            positionCalculator.GetCellCoords(mouseDownPos).Returns(boardDownCoords);
            positionCalculator.GetCellCoords(mouseUpPos).Returns(boardUpCoords);

            // Act
            pieceViewModel.Initialize();
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(clickPosition);
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(mouseDownPos);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(mouseUpPos);
            
            // Assert
            playerFacade.DidNotReceiveWithAnyArgs().TryMovePiece(default, default);
        }

        [Test]
        public void BoardClicked_PieceCantBeMoved_SelectionReset()
        {
            // Arrange
            var container = TestHelper.CreateContainerForViewModel<PieceViewModel>(out var pieceViewModel);
            var payload = container.Resolve<IPieceViewModelPayload>();
            var inputController = container.Resolve<IInputController>();
            var positionCalculator = payload.BoardWorldPositionsCalculator;
            var playerFacade = payload.PlayerFacade;
            var resetSelectionTrigger = container.Resolve<IResetSelectionTrigger>();
            var piece = payload.Piece;
            
            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            positionCalculator.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            positionCalculator.GetCellCoords(mouseDownPos).Returns(boardCoords);
            positionCalculator.GetCellCoords(mouseUpPos).Returns(boardCoords);

            playerFacade.TryMovePiece(default, default).ReturnsForAnyArgs(false);

            // Act
            pieceViewModel.Initialize();
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(clickPosition);
            
            inputController.MouseDown += Raise.Event<Action<Vector2>>(mouseDownPos);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(mouseUpPos);
            
            // Assert
            resetSelectionTrigger.Received().Trigger();
        }
    }
}