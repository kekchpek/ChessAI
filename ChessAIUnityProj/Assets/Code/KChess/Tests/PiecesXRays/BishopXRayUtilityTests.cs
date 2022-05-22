using System.Linq;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Domain;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests.PiecesXRays
{
    public class BishopXRayUtilityTests
    {
        private BishopXRayUtility CreateBishopXRayUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new BishopXRayUtility(board);
        }

        [Test]
        public void GetXRays_EmptyBoard_ReturnsEmpty()
        {
            // Arrange
            var bishopXRayUtility = CreateBishopXRayUtility(
                out var board);

            var bishop = Substitute.For<IPiece>();
            bishop.Position.Returns("d4");
            bishop.Color.Returns(PieceColor.Black);

            board.Pieces.Returns(new[] {bishop});
            
            // Act
            var xRays = bishopXRayUtility.GetXRays(bishop);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_SomePiecesOnBoard_ReturnsEmpty()
        {
            // Arrange
            var bishopXRayUtility = CreateBishopXRayUtility(
                out var board);

            var bishop = Substitute.For<IPiece>();
            bishop.Position.Returns("d4");
            bishop.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("d5");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.White);
            piece2.Position.Returns("c4");
            
            var piece3 = Substitute.For<IPiece>();
            piece3.Color.Returns(PieceColor.White);
            piece3.Position.Returns("a2");

            board.Pieces.Returns(new[] {bishop, piece1, piece2, piece3});
            
            // Act
            var xRays = bishopXRayUtility.GetXRays(bishop);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_AllyPiecesCovered_ReturnsEmpty()
        {
            // Arrange
            var bishopXRayUtility = CreateBishopXRayUtility(
                out var board);

            var bishop = Substitute.For<IPiece>();
            bishop.Position.Returns("d4");
            bishop.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("c5");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("c3");
            
            var piece3 = Substitute.For<IPiece>();
            piece3.Color.Returns(PieceColor.Black);
            piece3.Position.Returns("a1");

            board.Pieces.Returns(new[] {bishop, piece1, piece2, piece3});
            
            // Act
            var xRays = bishopXRayUtility.GetXRays(bishop);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_EnemyPieceCovered_ReturnsOneXRay()
        {
            // Arrange
            var bishopXRayUtility = CreateBishopXRayUtility(
                out var board);

            var bishop = Substitute.For<IPiece>();
            bishop.Position.Returns("d4");
            bishop.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("a1");

            board.Pieces.Returns(new[] {bishop, piece1});
            
            // Act
            var xRays = bishopXRayUtility.GetXRays(bishop);
            
            // Assert
            Assert.AreEqual(1, xRays.Length);
            Assert.AreEqual(xRays[0].AttackingPiece, bishop);
            Assert.AreEqual(xRays[0].TargetPiece, piece1);
            Assert.IsTrue(xRays[0].CellsBetween.Contains("b2"));
            Assert.IsTrue(xRays[0].CellsBetween.Contains("c3"));
        }

        [Test]
        public void GetXRays_SomePiecesBlocks_ReturnsOneXRayWithBlocks()
        {
            // Arrange
            var bishopXRayUtility = CreateBishopXRayUtility(
                out var board);

            var bishop = Substitute.For<IPiece>();
            bishop.Position.Returns("f6");
            bishop.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("a1");
            
            var blockingPiece1 = Substitute.For<IPiece>();
            blockingPiece1.Color.Returns(PieceColor.White);
            blockingPiece1.Position.Returns("e5");
            
            var blockingPiece2 = Substitute.For<IPiece>();
            blockingPiece2.Color.Returns(PieceColor.Black);
            blockingPiece2.Position.Returns("d4");

            board.Pieces.Returns(new[] {bishop, piece1, blockingPiece1, blockingPiece2});
            
            // Act
            var xRays = bishopXRayUtility.GetXRays(bishop);
            
            // Assert
            var xRayWithPiece1 = xRays.First(x => x.TargetPiece == piece1);
            Assert.AreEqual(xRayWithPiece1.AttackingPiece, bishop);
            Assert.AreEqual(xRayWithPiece1.TargetPiece, piece1);
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("b2"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("c3"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("d4"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("e5"));
            Assert.IsTrue(xRayWithPiece1.BlockingPieces.Contains(blockingPiece1));
            Assert.IsTrue(xRayWithPiece1.BlockingPieces.Contains(blockingPiece2));
        }
    }
}