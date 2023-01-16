using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Input;
using KChessUnity.Tests.Helper;
using KChessUnity.ViewModels.Board;
using KChessUnity.ViewModels.MovesDisplayer;
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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var piece = container.Resolve<IPiece>();
            piece.Position.Returns("d3");
            var boardPos = new Vector3(234f, 1f, 12f);
            // ReSharper disable once PossibleInvalidOperationException
            boardViewModel.GetWorldPosition(piece.Position.Value).Returns(boardPos);

            // Act
            pieceViewModel.Initialize();

            // Assert
            Assert.AreEqual(boardPos, pieceViewModel.Position);
        }
        
        [Test]
        public void Initialization_BoardMocked_ImageSetSet()
        {
            // Arrange
            TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);

            // Act
            pieceViewModel.Initialize();

            // Assert
            Assert.IsNotNull(pieceViewModel.Image);
        }

        [Test]
        public void PieceRemovedFromBoard_EventFired_DisposedEventFired()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var piece = container.Resolve<IPiece>();
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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var playerFacade = container.Resolve<IPlayerFacade>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);
            var movedPosition = new Vector2(333f, 111f);
            var newCell = (BoardCoordinates) "d6";

            piece.Position.Returns(piecePosition);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);
            boardViewModel.GetCellCoords(movedPosition).Returns(newCell);

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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var resetSelectionTrigger = container.Resolve<IResetSelectionTrigger>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var movesDisplayer = container.Resolve<IMovesDisplayerViewModel>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var playerFacade = container.Resolve<IPlayerFacade>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

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
            movesDisplayer.Received().ShowMoves(availableMoves);
        }
        
        [Test]
        public void CellClicked_NotSameAsPiecePosition_AvailableMovesHid(
            [Values(true, false)] bool isPieceMoved)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var movesDisplayer = container.Resolve<IMovesDisplayerViewModel>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var playerFacade = container.Resolve<IPlayerFacade>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);
            var movedPosition = new Vector2(333f, 111f);
            var newCell = (BoardCoordinates) "d6";

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);
            boardViewModel.GetCellCoords(movedPosition).Returns(newCell);

            piece.Position.Returns(piecePosition);

            playerFacade.TryMovePiece(default, default).ReturnsForAnyArgs(isPieceMoved);

            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(movedPosition);

            // Assert
            movesDisplayer.Received().HideMoves();
        }
        
        [Test]
        public void PieceSelected_AvailableMovesNotHid()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var movesDisplayer = container.Resolve<IMovesDisplayerViewModel>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var playerFacade = container.Resolve<IPlayerFacade>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

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
            movesDisplayer.DidNotReceive().HideMoves();
        }

        [Test]
        public void PieceClicked_MousePositionChanged_PiecePositionChanged()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mousePos = new Vector2(123f, 234f);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(clickPosition);
            inputController.MousePositionChanged += Raise.Event<Action<Vector2>>(mousePos);
            
            // Assert 
            Assert.AreEqual(mousePos.x, pieceViewModel.Position.x);
            Assert.AreEqual(mousePos.y, pieceViewModel.Position.y);
        }
        
        [Test]
        public void PieceSelected_MousePositionChanged_PiecePositionNotChanged()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            boardViewModel.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

            piece.Position.Returns(piecePosition);
            
            var mousePos = new Vector2(123f, 234f);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            
            inputController.MousePositionChanged += Raise.Event<Action<Vector2>>(mousePos);
            
            // Assert 
            Assert.AreEqual((Vector3)pieceWorldPosition, pieceViewModel.Position);
        }

        [Test]
        public void BoardClicked_DownUpAtSameCell_PieceMoved()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var inputController = container.Resolve<IInputController>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var playerFacade = container.Resolve<IPlayerFacade>();
            var piece = container.Resolve<IPiece>();

            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            boardViewModel.GetCellCoords(mouseDownPos).Returns(boardCoords);
            boardViewModel.GetCellCoords(mouseUpPos).Returns(boardCoords);

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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var selectionResetTrigger = container.Resolve<IResetSelectionTrigger>();
            var movesDisplayer = container.Resolve<IMovesDisplayerViewModel>();

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            boardViewModel.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

            piece.Position.Returns(piecePosition);
            
            // Act
            pieceViewModel.Initialize();
            inputController.MouseDown += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            inputController.MouseUp += Raise.Event<Action<Vector2>>(pieceWorldPosition);
            selectionResetTrigger.OnTriggered += Raise.Event<Action>();

            // Assert
            movesDisplayer.HideMoves();
        }

        [Test]
        public void PieceSelected_SelectionResetTriggered_PieceDoesntMove()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var inputController = container.Resolve<IInputController>();
            var piece = container.Resolve<IPiece>();
            var selectionResetTrigger = container.Resolve<IResetSelectionTrigger>();
            var playerFacade = container.Resolve<IPlayerFacade>();

            var piecePosition = (BoardCoordinates) "a3";
            var pieceWorldPosition = new Vector2(34f, 223f);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            boardViewModel.GetCellCoords(mouseDownPos).Returns(boardCoords);
            boardViewModel.GetCellCoords(mouseUpPos).Returns(boardCoords);
            boardViewModel.GetCellCoords(pieceWorldPosition).Returns(piecePosition);
            boardViewModel.GetWorldPosition(piecePosition).Returns((Vector3)pieceWorldPosition);

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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var inputController = container.Resolve<IInputController>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var playerFacade = container.Resolve<IPlayerFacade>();
            var piece = container.Resolve<IPiece>();
            
            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardDownCoords = (BoardCoordinates) "g5";
            var boardUpCoords = (BoardCoordinates) "f5";
            boardViewModel.GetCellCoords(mouseDownPos).Returns(boardDownCoords);
            boardViewModel.GetCellCoords(mouseUpPos).Returns(boardUpCoords);

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
            var container = TestHelper.CreateContainerFor<PieceViewModel>(out var pieceViewModel);
            var inputController = container.Resolve<IInputController>();
            var boardViewModel = container.Resolve<IBoardViewModel>();
            var playerFacade = container.Resolve<IPlayerFacade>();
            var resetSelectionTrigger = container.Resolve<IResetSelectionTrigger>();
            var piece = container.Resolve<IPiece>();
            
            var piecePosition = (BoardCoordinates) "a3";
            var clickPosition = new Vector2(34f, 223f);

            boardViewModel.GetCellCoords(clickPosition).Returns(piecePosition);

            piece.Position.Returns(piecePosition);
            
            var mouseDownPos = new Vector2(123f, 234f);
            var mouseUpPos = new Vector2(12f, 23f);

            var boardCoords = (BoardCoordinates) "g5";
            boardViewModel.GetCellCoords(mouseDownPos).Returns(boardCoords);
            boardViewModel.GetCellCoords(mouseUpPos).Returns(boardCoords);

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