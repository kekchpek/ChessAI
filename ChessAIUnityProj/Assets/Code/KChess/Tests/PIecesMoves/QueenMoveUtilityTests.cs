using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.Queen;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests.PiecesMoves
{
    public class QueenMoveUtilityTests
    {

        private QueenMoveUtility CreateQueenMoveUtility(
            out IRookMoveUtility rookMoveUtility,
            out IBishopMoveUtility bishopMoveUtility)
        {
            rookMoveUtility = Substitute.For<IRookMoveUtility>();
            bishopMoveUtility = Substitute.For<IBishopMoveUtility>();
            return new QueenMoveUtility(bishopMoveUtility, rookMoveUtility);
        }

        [Test]
        public void GetMoves_ReturnsRookAndBishopMoves()
        {
            // Arrange
            var queenMoveUtility = CreateQueenMoveUtility(
                out var rookMoveUtility,
                out var bishopMoveUtility);
            var position = (BoardCoordinates) "d3";
            rookMoveUtility.GetMoves(position, PieceColor.Black).Returns(
                new BoardCoordinates[]
                {
                    "a3", "d3", "a1"
                });
            bishopMoveUtility.GetMoves(position, PieceColor.Black).Returns(
                new BoardCoordinates[]
                {
                    "c3", "d6", "a8"
                });

            // Act
            var moves = queenMoveUtility.GetMoves(position, PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"a3", moves);
            Assert.Contains((BoardCoordinates)"d3", moves);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"c3", moves);
            Assert.Contains((BoardCoordinates)"d6", moves);
            Assert.Contains((BoardCoordinates)"a8", moves);
            Assert.AreEqual(6, moves.Length);
        }

        [Test]
        public void GetAttackedCells_ReturnsRookAndBishopAttackedCells()
        {
            // Arrange
            var queenMoveUtility = CreateQueenMoveUtility(
                out var rookMoveUtility,
                out var bishopMoveUtility);
            var position = (BoardCoordinates) "d3";
            rookMoveUtility.GetAttackedCells(position, PieceColor.Black).Returns(
                new BoardCoordinates[]
                {
                    "a3", "d3", "a1"
                });
            bishopMoveUtility.GetAttackedCells(position, PieceColor.Black).Returns(
                new BoardCoordinates[]
                {
                    "c3", "d6", "a8"
                });

            // Act
            var attackedCells = queenMoveUtility.GetAttackedCells(position, PieceColor.Black);

            // Assert
            Assert.Contains((BoardCoordinates)"a3", attackedCells);
            Assert.Contains((BoardCoordinates)"d3", attackedCells);
            Assert.Contains((BoardCoordinates)"a1", attackedCells);
            Assert.Contains((BoardCoordinates)"c3", attackedCells);
            Assert.Contains((BoardCoordinates)"d6", attackedCells);
            Assert.Contains((BoardCoordinates)"a8", attackedCells);
            Assert.AreEqual(6, attackedCells.Length);
        }
        
    }
}