using System;
using System.Collections.Generic;
using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardStateUtils;
using KChess.Core.MoveUtility;
using KChess.Core.PawnTransformation;
using KChess.Core.TurnUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace KChess.Tests
{
    public class PlayerFacadeTests
    {

        [TestCase(PieceColor.Black, PieceColor.White)]
        [TestCase(PieceColor.White, PieceColor.Black)]
        public void TryMovePiece_EnemyTurn_ReturnsFalse(PieceColor playerColor, PieceColor turn)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), playerColor}});
            var turnGetter = container.Resolve<ITurnGetter>();
            var moveUtility = container.Resolve<IMoveUtility>();
            
            turnGetter.GetTurn().Returns(turn);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(playerColor);

            moveUtility.GetAvailableMoves(piece).Returns(new BoardCoordinates[] {"c3"});
            
            // Act
            var moved = playerFacade.TryMovePiece(piece, "c3");
            
            // Assert
            Assert.IsFalse(moved);
        }
        

        [TestCase(PieceColor.Black, PieceColor.White)]
        [TestCase(PieceColor.White, PieceColor.Black)]
        public void TryMovePiece_EnemyPiece_ReturnsFalse(PieceColor playerColor, PieceColor pieceColor)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), playerColor}});
            var turnGetter = container.Resolve<ITurnGetter>();
            var moveUtility = container.Resolve<IMoveUtility>();
            
            turnGetter.GetTurn().Returns(playerColor);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(pieceColor);

            moveUtility.GetAvailableMoves(piece).Returns(new BoardCoordinates[] {"c3"});
            
            // Act
            var moved = playerFacade.TryMovePiece(piece, "c3");
            
            // Assert
            Assert.IsFalse(moved);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void TryMovePiece_EnemyPiece_ReturnsFalse(PieceColor playerColor)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), playerColor}});
            var turnGetter = container.Resolve<ITurnGetter>();
            var moveUtility = container.Resolve<IMoveUtility>();
            
            turnGetter.GetTurn().Returns(playerColor);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(playerColor);

            moveUtility.GetAvailableMoves(piece).Returns(new BoardCoordinates[] {"c3"});
            
            // Act
            var moved = playerFacade.TryMovePiece(piece, "c5");
            
            // Assert
            Assert.IsFalse(moved);
        }
        
        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void TryMovePiece_ValidMove_ReturnsTrue(PieceColor playerColor)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), playerColor}});
            var turnGetter = container.Resolve<ITurnGetter>();
            var moveUtility = container.Resolve<IMoveUtility>();
            
            turnGetter.GetTurn().Returns(playerColor);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(playerColor);

            moveUtility.GetAvailableMoves(piece).Returns(new BoardCoordinates[] {"c3"});
            
            // Act
            var moved = playerFacade.TryMovePiece(piece, "c3");
            
            // Assert
            Assert.IsTrue(moved);
        }

        [Test]
        public void GetAvailableMoves_GetsFromMoveUtility()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var moveUtility = container.Resolve<IMoveUtility>();

            var piece = Substitute.For<IPiece>();

            moveUtility.GetAvailableMoves(piece).Returns(new BoardCoordinates[] {"c3", "a1", "d7"});
            
            // Act
            var moves = playerFacade.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(moveUtility.GetAvailableMoves(piece), moves);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void TurnChanged_CalledFromTurnObserver(PieceColor newTurn)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var turnObserver = container.Resolve<ITurnObserver>();

            // Act
            PieceColor? changedTurn = null;
            playerFacade.TurnChanged += x => changedTurn = x;
            turnObserver.TurnChanged += Raise.Event<Action<PieceColor>>(newTurn);
            
            // Assert
            Assert.AreEqual(newTurn, changedTurn);
        }

        [TestCase(BoardState.Regular)]
        [TestCase(BoardState.Stalemate)]
        [TestCase(BoardState.RepeatDraw)]
        [TestCase(BoardState.CheckToBlack)]
        [TestCase(BoardState.CheckToWhite)]
        [TestCase(BoardState.MateToBlack)]
        [TestCase(BoardState.MateToWhite)]
        public void BoardStateChanged_CalledFromBoardStateObserver(BoardState newState)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var boardStateObserver = container.Resolve<IBoardStateObserver>();

            // Act
            BoardState? changedState = null;
            playerFacade.BoardStateChanged += x => changedState = x;
            boardStateObserver.StateChanged += Raise.Event<Action<BoardState>>(newState);
            
            // Assert
            Assert.AreEqual(newState, changedState);
        }
        
        [Test]
        public void GetBoard_ReturnsConstructorBoard()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var board = container.Resolve<IBoard>();

            // Act
            var getBoard = playerFacade.GetBoard();
            
            // Assert
            Assert.AreEqual(board, getBoard);
        }
        
        [TestCase(BoardState.Regular)]
        [TestCase(BoardState.Stalemate)]
        [TestCase(BoardState.RepeatDraw)]
        [TestCase(BoardState.CheckToBlack)]
        [TestCase(BoardState.CheckToWhite)]
        [TestCase(BoardState.MateToBlack)]
        [TestCase(BoardState.MateToWhite)]
        public void GetBoardState_ReturnsFromStateGetter(BoardState boardState)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var boardStateGetter = container.Resolve<IBoardStateGetter>();

            boardStateGetter.Get().Returns(boardState);

            // Act
            var getBoardStated = playerFacade.GetBoardState();
            
            // Assert
            Assert.AreEqual(boardState, getBoardStated);
        }

        [TestCase(PieceColor.Black)]
        [TestCase(PieceColor.White)]
        public void GetTurn_ReturnsFormTurnGetter(PieceColor turn)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var turnGetter = container.Resolve<ITurnGetter>();

            turnGetter.GetTurn().Returns(turn);

            // Act
            var getTurn = playerFacade.GetTurn();
            
            // Assert
            Assert.AreEqual(turn, getTurn);
        }
        
        public void GetTransformingPiece_ReturnsFromPawnTransformationUtility()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var pawnTransformationUtility = container.Resolve<IPawnTransformationUtility>();

            pawnTransformationUtility.GetTransformingPiece().Returns(Substitute.For<IPiece>());

            // Act
            var transformingPiece = playerFacade.GetTransformingPiece();
            
            // Assert
            Assert.AreEqual(pawnTransformationUtility.GetTransformingPiece(), transformingPiece);
        }
        
        [TestCase(true)]
        [TestCase(false)]
        public void GetTransformingPiece_ReturnsFromPawnTransformationUtility(bool isTransformed)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var pawnTransformationUtility = container.Resolve<IPawnTransformationUtility>();

            pawnTransformationUtility.TryTransform(Arg.Any<PawnTransformationVariant>()).Returns(isTransformed);

            // Act
            var returnValue = playerFacade.TryTransform(PawnTransformationVariant.Bishop);
            
            // Assert
            Assert.AreEqual(isTransformed, returnValue);
        }
        
        [TestCase(PawnTransformationVariant.Bishop)]
        [TestCase(PawnTransformationVariant.Knight)]
        [TestCase(PawnTransformationVariant.Rook)]
        [TestCase(PawnTransformationVariant.Queen)]
        public void GetTransformingPiece_TryTransformCalled(PawnTransformationVariant transformationVariant)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var pawnTransformationUtility = container.Resolve<IPawnTransformationUtility>();

            // Act
            playerFacade.TryTransform(transformationVariant);
            
            // Assert
            pawnTransformationUtility.Received().TryTransform(transformationVariant);
        }
        
        [Test]
        public void PieceRequiredToBeTransformed_TransUtilityEventRaised_EventFired()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<ManagedPlayerFacade>(out var playerFacade,
                new Dictionary<Type, object> {{typeof(PieceColor), PieceColor.Black}});
            var pawnTransformationUtility = container.Resolve<IPawnTransformationUtility>();

            var testingPiece = Substitute.For<IPiece>();
            
            IPiece pieceToTransform = null;
            playerFacade.PieceRequiredToBeTransformed += x => pieceToTransform = x;

            // Act
            pawnTransformationUtility.TransformationBecomesRequired += Raise.Event<Action<IPiece>>(testingPiece);
            
            // Assert
            Assert.AreEqual(testingPiece, pieceToTransform);
        }

    }
}