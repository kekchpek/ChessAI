using KChess.Core.Factories;
using KChess.Domain;
using NSubstitute;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace KChess.Tests
{
    public class PieceFactoryTests
    {
        private PieceFactory CreatePieceFactory()
        {
            return new PieceFactory();
        }

        [Test]
        public void CreatePiece_ColorSetCorrectly(
            [Values(PieceColor.White, PieceColor.Black)] PieceColor pieceColor)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Create(PieceType.King, pieceColor, "d3", board);
            
            // Assert
            Assert.AreEqual(pieceColor, createdPiece.Color);
        }

        [Test]
        public void CreatePiece_PieceTypeSetCorrectly(
            [Values(PieceType.Bishop, PieceType.King, PieceType.Knight, PieceType.Pawn,
                PieceType.Queen, PieceType.Rook)] PieceType pieceType)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Create(pieceType, PieceColor.Black, "d3", board);
            
            // Assert
            Assert.AreEqual(pieceType, createdPiece.Type);
        }

        [Test]
        public void CreatePiece_PositionSetCorrectly(
            [Values("c1", "f3", "a2", "a1")] string position)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Create(PieceType.Queen, PieceColor.White, position, board);
            
            // Assert
            Assert.AreEqual(position, createdPiece.Position);
        }

        [Test]
        public void CreatePiece_PiecePlacedOnBoard()
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Create(PieceType.Queen, PieceColor.White, "d3", board);
            
            // Assert
            board.Received().PlacePiece(createdPiece);
        }

        [Test]
        public void CopyPiece_ColorSetCorrectly(
            [Values(PieceColor.White, PieceColor.Black)] PieceColor pieceColor)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var copiedPiece = Substitute.For<IPiece>();
            copiedPiece.Color.Returns(pieceColor);
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Copy(copiedPiece, board);
            
            // Assert
            Assert.AreEqual(pieceColor, createdPiece.Color);
        }

        [Test]
        public void CopyPiece_PieceTypeSetCorrectly(
            [Values(PieceType.Bishop, PieceType.King, PieceType.Knight, PieceType.Pawn,
                PieceType.Queen, PieceType.Rook)] PieceType pieceType)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var copiedPiece = Substitute.For<IPiece>();
            copiedPiece.Type.Returns(pieceType);
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Copy(copiedPiece, board);
            
            // Assert
            Assert.AreEqual(pieceType, createdPiece.Type);
        }

        [Test]
        public void CopyPiece_PositionSetCorrectly(
            [Values("c1", "f3", "a2", "a1")] string position)
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var copiedPiece = Substitute.For<IPiece>();
            copiedPiece.Position.Returns(position);
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Copy(copiedPiece, board);
            
            // Assert
            Assert.AreEqual(position, createdPiece.Position);
        }

        [Test]
        public void CopyPiece_PiecePlacedOnBoard()
        {
            // Arrange
            var pieceFactory = CreatePieceFactory();
            var board = Substitute.For<IBoard>();

            // Act 
            var createdPiece = pieceFactory.Copy(Substitute.For<IPiece>(), board);
            
            // Assert
            board.Received().PlacePiece(createdPiece);
        }

    }
}