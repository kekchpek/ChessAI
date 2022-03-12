using System;
using KekChessCore.Domain;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class TurnUtilityTests
    {
        private TurnUtility.TurnUtility CreateTurnUtility(out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new TurnUtility.TurnUtility(board);
        }

        [Test]
        public void Creation_WhiteTurn()
        {
            // Arrange
            var turnUtility = CreateTurnUtility(
                out var board);
            
            // Act 
            // no act
            
            // Assert
            Assert.AreEqual(PieceColor.White, turnUtility.GetTurn());
        }

        [Test]
        public void SetTurn_EventCalled()
        {
            // Arrange
            var turnUtility = CreateTurnUtility(
                out var board);
            
            // Act
            PieceColor? turnColor = null;
            turnUtility.TurnChanged += x => turnColor = x;
            turnUtility.SetTurn(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(PieceColor.Black, turnColor);
        }

        [Test]
        public void PieceMoved_EventCalled()
        {
            // Arrange
            var turnUtility = CreateTurnUtility(
                out var board);
            
            // Act
            PieceColor? turnColor = null;
            turnUtility.TurnChanged += x => turnColor = x;
            var whitePiece = Substitute.For<IPiece>();
            whitePiece.Color.Returns(PieceColor.White);
            board.PieceMoved += Raise.Event<Action<IPiece>>(whitePiece);
            
            // Assert
            Assert.AreEqual(PieceColor.Black, turnColor);
        }
        

        [Test]
        public void SetTurn_TurnChanged()
        {
            // Arrange
            var turnUtility = CreateTurnUtility(
                out var board);
            
            // Act
            turnUtility.SetTurn(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(PieceColor.Black, turnUtility.GetTurn());
        }

        [TestCase(PieceColor.White, PieceColor.Black)]
        [TestCase(PieceColor.Black, PieceColor.White)]
        public void PieceMoved_TurnChanged(PieceColor movedColor, PieceColor newTurnColor)
        {
            // Arrange
            var turnUtility = CreateTurnUtility(
                out var board);
            
            // Act
            turnUtility.SetTurn(movedColor);
            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(movedColor);
            board.PieceMoved += Raise.Event<Action<IPiece>>(piece);
            
            // Assert
            Assert.AreEqual(newTurnColor, turnUtility.GetTurn());
        }
    }
}