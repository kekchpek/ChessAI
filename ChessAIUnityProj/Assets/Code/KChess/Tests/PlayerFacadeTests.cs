using System;
using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardStateUtils;
using KChess.Core.MoveUtility;
using KChess.Core.TurnUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace KChess.Tests
{
    public class PlayerFacadeTests
    {

        private ManagedPlayerFacade CreatePlayerFacade(
            out IMoveUtility moveUtility,
            out ITurnGetter turnGetter,
            out ITurnObserver turnObserver, 
            out IBoardStateGetter boardStateGetter,
            out IBoardStateObserver boardStateObserver, 
            out IBoard board,
            PieceColor pieceColor)
        {
            moveUtility = Substitute.For<IMoveUtility>();
            turnGetter = Substitute.For<ITurnGetter>();
            turnObserver = Substitute.For<ITurnObserver>();
            boardStateGetter = Substitute.For<IBoardStateGetter>();
            boardStateObserver = Substitute.For<IBoardStateObserver>();
            board = Substitute.For<IBoard>();
            return new ManagedPlayerFacade(moveUtility, turnGetter, turnObserver,
                boardStateGetter, boardStateObserver, board, pieceColor);
        }

        [TestCase(PieceColor.Black, PieceColor.White)]
        [TestCase(PieceColor.White, PieceColor.Black)]
        public void TryMovePiece_EnemyTurn_ReturnsFalse(PieceColor playerColor, PieceColor turn)
        {
            // Arrange
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                playerColor);
            
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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                playerColor);
            
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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                playerColor);
            
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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                playerColor);
            
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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

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
            var playerFacade = CreatePlayerFacade(
                out var moveUtility,
                out var turnGetter,
                out var turnObserver,
                out var boardStateGetter,
                out var boardStateObserver,
                out var board,
                PieceColor.Black);

            turnGetter.GetTurn().Returns(turn);

            // Act
            var getTurn = playerFacade.GetTurn();
            
            // Assert
            Assert.AreEqual(turn, getTurn);
        }

    }
}