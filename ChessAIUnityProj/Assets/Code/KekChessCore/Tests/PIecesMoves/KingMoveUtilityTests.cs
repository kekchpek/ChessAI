using ChessAI.Core.MoveUtility.PieceMoveUtilities.King;
using ChessAI.Domain;
using ChessAI.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class KingMoveUtilityTests
    {

        private KingMoveUtility CreateKingMoveUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new KingMoveUtility(board);
        }

        [Test]
        public void EmptyBoard_NoBorder()
        {
            // Arrange
            var kingMoveUtility = CreateKingMoveUtility(
                out var board);
            
            // Act
            var availableMoves = kingMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.Contains((BoardCoordinates)"c3", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.Contains((BoardCoordinates)"c5", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"c4", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.AreEqual(8, availableMoves.Length);
        }

        [Test]
        public void EmptyBoard_Border()
        {
            // Arrange
            var kingMoveUtility = CreateKingMoveUtility(
                out var board);
            
            // Act
            var availableMoves = kingMoveUtility.GetMoves("a1", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"a2", availableMoves);
            Assert.Contains((BoardCoordinates)"b1", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.AreEqual(3, availableMoves.Length);
        }

        [Test]
        public void PiecesBlocks_Ally()
        {
            // Arrange
            var kingMoveUtility = CreateKingMoveUtility(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("c3");

            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("e5");
            
            board.Pieces.Returns(new []{piece1, piece2});
            
            // Act
            var availableMoves = kingMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.Contains((BoardCoordinates)"c5", availableMoves);
            Assert.Contains((BoardCoordinates)"c4", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.AreEqual(6, availableMoves.Length);
        }

        [Test]
        public void PiecesBlocks_Enemy()
        {
            // Arrange
            var kingMoveUtility = CreateKingMoveUtility(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("c3");

            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.White);
            piece2.Position.Returns("e5");
            
            // Act
            var availableMoves = kingMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.Contains((BoardCoordinates)"c3", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.Contains((BoardCoordinates)"c5", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"c4", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.AreEqual(8, availableMoves.Length);
        }
        
    }
}