using ChessAI.Core.MoveUtility.PieceMoveUtilities.Knight;
using ChessAI.Domain;
using ChessAI.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class KnightMoveUtilityTests
    {
        private KnightMoveUtility CreateKnightMoveUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new KnightMoveUtility(board);
        }

        [Test]
        public void GetMoves_NoBorder_ReturnsCorrectMoves()
        {
            // Arrange
            var knightMoveUtility = CreateKnightMoveUtility(
                out var board);

            // Act
            var moves = knightMoveUtility.GetMoves("d4", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"b5", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
            Assert.Contains((BoardCoordinates)"c6", moves);
            Assert.Contains((BoardCoordinates)"e6", moves);
            Assert.Contains((BoardCoordinates)"f5", moves);
            Assert.Contains((BoardCoordinates)"f3", moves);
            Assert.Contains((BoardCoordinates)"c2", moves);
            Assert.Contains((BoardCoordinates)"e2", moves);
            Assert.AreEqual(8, moves.Length);
        }
        
        [Test]
        public void GetMoves_Border_ReturnsCorrectMoves()
        {
            // Arrange
            var knightMoveUtility = CreateKnightMoveUtility(
                out var board);

            // Act
            var moves = knightMoveUtility.GetMoves("d1", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"b2", moves);
            Assert.Contains((BoardCoordinates)"c3", moves);
            Assert.Contains((BoardCoordinates)"e3", moves);
            Assert.Contains((BoardCoordinates)"f2", moves);
            Assert.AreEqual(4, moves.Length);
        }
        

        [Test]
        public void GetMoves_AllyPiecesBlocks_ReturnsCorrectMoves()
        {
            // Arrange
            var knightMoveUtility = CreateKnightMoveUtility(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("c2");

            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("e6");

            board.Pieces.Returns(new[] {piece1, piece2});

            // Act
            var moves = knightMoveUtility.GetMoves("d4", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"b5", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
            Assert.Contains((BoardCoordinates)"c6", moves);
            Assert.Contains((BoardCoordinates)"f5", moves);
            Assert.Contains((BoardCoordinates)"f3", moves);
            Assert.Contains((BoardCoordinates)"e2", moves);
            Assert.AreEqual(6, moves.Length);
        }
        
    }
}