using ChessAI.Core.MoveUtility.PieceMoveUtilities.Bishop;
using ChessAI.Domain;
using ChessAI.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class BishopMoveUtilityTest
    {
        private BishopMoveUtility CreateBishopMoveUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new BishopMoveUtility(board);
        }

        [Test]
        public void EmptyBoard_C3()
        {
            // Arrange
            var bishopMoveUtility = CreateBishopMoveUtility(
                out var board);
            
            // Act
            var availableMoves = bishopMoveUtility.GetMoves("c3", PieceColor.Black);

            // Arrange
            Assert.Contains((BoardCoordinates)"d2", availableMoves);
            Assert.Contains((BoardCoordinates)"e1", availableMoves);
            Assert.Contains((BoardCoordinates)"a5", availableMoves);
            Assert.Contains((BoardCoordinates)"b4", availableMoves);
            
            Assert.Contains((BoardCoordinates)"a1", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"f6", availableMoves);
            Assert.Contains((BoardCoordinates)"g7", availableMoves);
            Assert.Contains((BoardCoordinates)"h8", availableMoves);
            Assert.AreEqual(11, availableMoves.Length);
        }

        [Test]
        public void BlocksByAllyPieces_C3()
        {
            // Arrange
            var bishopMoveUtility = CreateBishopMoveUtility(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("d2");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("f6");

            board.Pieces.Returns(new[] {piece1, piece2});
            
            // Act
            var availableMoves = bishopMoveUtility.GetMoves("c3", PieceColor.Black);

            // Arrange
            Assert.Contains((BoardCoordinates)"a5", availableMoves);
            Assert.Contains((BoardCoordinates)"b4", availableMoves);
            
            Assert.Contains((BoardCoordinates)"a1", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.AreEqual(6, availableMoves.Length);
        }

        [Test]
        public void BlocksByEnemyPieces_C3()
        {
            // Arrange
            var bishopMoveUtility = CreateBishopMoveUtility(
                out var board);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("d2");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.White);
            piece2.Position.Returns("f6");

            board.Pieces.Returns(new[] {piece1, piece2});
            
            // Act
            var availableMoves = bishopMoveUtility.GetMoves("c3", PieceColor.Black);

            // Arrange
            Assert.Contains((BoardCoordinates)"a5", availableMoves);
            Assert.Contains((BoardCoordinates)"b4", availableMoves);
            Assert.Contains((BoardCoordinates)"d2", availableMoves);
            
            Assert.Contains((BoardCoordinates)"a1", availableMoves);
            Assert.Contains((BoardCoordinates)"b2", availableMoves);
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.Contains((BoardCoordinates)"e5", availableMoves);
            Assert.Contains((BoardCoordinates)"f6", availableMoves);
            Assert.AreEqual(8, availableMoves.Length);
        }
    }
}