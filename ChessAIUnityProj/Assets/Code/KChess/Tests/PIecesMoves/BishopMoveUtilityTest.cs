using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests.PiecesMoves
{
    public class BishopMoveUtilityTest
    {
        [Test]
        public void GetMoves_EmptyBoard_ReturnsCorrectMoves()
        {
            // Arrange
            TestHelper.CreateContainerFor<BishopMoveUtility>(out var bishopMoveUtility);
            
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
        public void GetMoves_BlocksByAllyPieces_AllyPiecesPositionsExcept()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<BishopMoveUtility>(out var bishopMoveUtility);
            var board = container.Resolve<IBoard>();

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
        public void GetAttackedCells_BlocksByAllyPieces_AllyPiecesPositionsExcept()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<BishopMoveUtility>(out var bishopMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("d2");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("f6");

            board.Pieces.Returns(new[] {piece1, piece2});
            
            // Act
            var attackedCells = bishopMoveUtility.GetAttackedCells("c3", PieceColor.Black);

            // Arrange
            Assert.Contains((BoardCoordinates)"a5", attackedCells);
            Assert.Contains((BoardCoordinates)"b4", attackedCells);
            
            Assert.Contains((BoardCoordinates)"a1", attackedCells);
            Assert.Contains((BoardCoordinates)"b2", attackedCells);
            Assert.Contains((BoardCoordinates)"d4", attackedCells);
            Assert.Contains((BoardCoordinates)"e5", attackedCells);
            Assert.Contains((BoardCoordinates)"d2", attackedCells);
            Assert.Contains((BoardCoordinates)"f6", attackedCells);
            Assert.AreEqual(8, attackedCells.Length);
        }

        [Test]
        public void GetMoves_BlocksByAllyPieces_EnemyPositionsInclude()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<BishopMoveUtility>(out var bishopMoveUtility);
            var board = container.Resolve<IBoard>();

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