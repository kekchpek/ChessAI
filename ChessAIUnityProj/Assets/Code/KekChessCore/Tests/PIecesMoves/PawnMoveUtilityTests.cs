using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.MoveUtility.PieceMoveUtilities.Pawn;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests.PIecesMoves
{
    public class PawnMoveUtilityTests
    {
        private PawnMoveUtility CreatePawnMoveUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new PawnMoveUtility(board);
        }

        [Test]
        public void White_MiddleOfBoard()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void Black_MiddleOfBoard()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void White_StartMoving()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d2", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void Black_StartMoving()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void StartMoving_BlockedByAlly()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Position.Returns("d6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.AreEqual(0, availableMoves.Length);
        }
        
        [Test]
        public void StartMoving_BlockedByEnemy()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Position.Returns("d6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.AreEqual(0, availableMoves.Length);
        }
        
        [Test]
        public void CanTake_MoveToTakeAvailable()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Position.Returns("c6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.Contains((BoardCoordinates)"c6", availableMoves);
            Assert.AreEqual(3, availableMoves.Length);
        }
        
        [Test]
        public void CanTakeBackward_MoveToTakeNotAvailable()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Position.Returns("c6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d8", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void Black_EnPassant_MoveToTakeAvailable()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c4");
            piece.PreviousPosition.Returns("c2");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"c3", availableMoves);
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void White_EnPassant_MoveToTakeAvailable()
        {
            // Arrange
            var pawnMoveUtility = CreatePawnMoveUtility(
                out var board);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c5");
            piece.PreviousPosition.Returns("c7");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"c6", availableMoves);
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
    }
}