using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests.PIecesMoves
{
    public class RookMoveUtilityTests
    {
        private RookMoveUtility CreateRookMoveUtilityTests(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new RookMoveUtility(board);
        }

        [Test]
        public void GetMoves_EmptyBoard()
        {
            // Arrange
            var rookMoveUtility = CreateRookMoveUtilityTests(
                out var board);

            // Act
            var availableMoves = rookMoveUtility.GetMoves("e2", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"e1", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"e6", availableMoves);
            Assert.Contains((BoardCoordinates)"e7", availableMoves);
            Assert.Contains((BoardCoordinates)"e8", availableMoves);
            Assert.Contains((BoardCoordinates)"a2", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.Contains((BoardCoordinates)"c2", availableMoves);
            Assert.Contains((BoardCoordinates)"d2", availableMoves);
            Assert.Contains((BoardCoordinates)"f2", availableMoves);
            Assert.Contains((BoardCoordinates)"g2", availableMoves);
            Assert.Contains((BoardCoordinates)"h2", availableMoves);
            Assert.AreEqual(14, availableMoves.Length);
        }
        
        [Test]
        public void GetMoves_AllyPiecesBocks_EmptyBoard()
        {
            // Arrange
            var rookMoveUtility = CreateRookMoveUtilityTests(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("e5");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("b2");

            board.Pieces.Returns(new[] {piece1, piece2});

            // Act
            var availableMoves = rookMoveUtility.GetMoves("e2", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"e1", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.Contains((BoardCoordinates)"c2", availableMoves);
            Assert.Contains((BoardCoordinates)"d2", availableMoves);
            Assert.Contains((BoardCoordinates)"f2", availableMoves);
            Assert.Contains((BoardCoordinates)"g2", availableMoves);
            Assert.Contains((BoardCoordinates)"h2", availableMoves);
            Assert.AreEqual(8, availableMoves.Length);
        }
        
        
        [Test]
        public void GetMoves_EnemyPiecesBocks_EmptyBoard()
        {
            // Arrange
            var rookMoveUtility = CreateRookMoveUtilityTests(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("e5");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.White);
            piece2.Position.Returns("b2");

            board.Pieces.Returns(new[] {piece1, piece2});

            // Act
            var availableMoves = rookMoveUtility.GetMoves("e2", PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"e1", availableMoves);
            Assert.Contains((BoardCoordinates)"e3", availableMoves);
            Assert.Contains((BoardCoordinates)"e4", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.Contains((BoardCoordinates)"c2", availableMoves);
            Assert.Contains((BoardCoordinates)"d2", availableMoves);
            Assert.Contains((BoardCoordinates)"f2", availableMoves);
            Assert.Contains((BoardCoordinates)"g2", availableMoves);
            Assert.Contains((BoardCoordinates)"h2", availableMoves);
            Assert.AreEqual(10, availableMoves.Length);
        }
    }
}