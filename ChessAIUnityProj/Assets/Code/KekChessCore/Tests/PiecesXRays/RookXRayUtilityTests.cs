﻿using System.Linq;
using KekChessCore.Domain;
using KekChessCore.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests.PiecesXRays
{
    public class RookXRayUtilityTests
    {
        private RookXRayUtility CreateRookXRayUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new RookXRayUtility(board);
        }

        [Test]
        public void GetXRays_EmptyBoard_ReturnsEmpty()
        {
            // Arrange
            var rookXRayUtility = CreateRookXRayUtility(
                out var board);

            var rook = Substitute.For<IPiece>();
            rook.Position.Returns("d4");
            rook.Color.Returns(PieceColor.Black);

            board.Pieces.Returns(new[] {rook});
            
            // Act
            var xRays = rookXRayUtility.GetXRays(rook);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_SomePiecesOnBoard_ReturnsEmpty()
        {
            // Arrange
            var rookXRayUtility = CreateRookXRayUtility(
                out var board);

            var rook = Substitute.For<IPiece>();
            rook.Position.Returns("d4");
            rook.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("c5");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.White);
            piece2.Position.Returns("a3");
            
            var piece3 = Substitute.For<IPiece>();
            piece3.Color.Returns(PieceColor.White);
            piece3.Position.Returns("a2");

            board.Pieces.Returns(new[] {rook, piece1, piece2, piece3});
            
            // Act
            var xRays = rookXRayUtility.GetXRays(rook);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_AllyPiecesCovered_ReturnsEmpty()
        {
            // Arrange
            var rookXRayUtility = CreateRookXRayUtility(
                out var board);

            var rook = Substitute.For<IPiece>();
            rook.Position.Returns("d4");
            rook.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.Black);
            piece1.Position.Returns("c4");
            
            var piece2 = Substitute.For<IPiece>();
            piece2.Color.Returns(PieceColor.Black);
            piece2.Position.Returns("d3");
            
            var piece3 = Substitute.For<IPiece>();
            piece3.Color.Returns(PieceColor.Black);
            piece3.Position.Returns("a4");

            board.Pieces.Returns(new[] {rook, piece1, piece2, piece3});
            
            // Act
            var xRays = rookXRayUtility.GetXRays(rook);
            
            // Assert
            Assert.IsEmpty(xRays);
        }

        [Test]
        public void GetXRays_EnemyPieceCovered_ReturnsOneXRay()
        {
            // Arrange
            var rookXRayUtility = CreateRookXRayUtility(
                out var board);

            var rook = Substitute.For<IPiece>();
            rook.Position.Returns("d4");
            rook.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("a4");

            board.Pieces.Returns(new[] {rook, piece1});
            
            // Act
            var xRays = rookXRayUtility.GetXRays(rook);
            
            // Assert
            Assert.AreEqual(1, xRays.Length);
            Assert.AreEqual(xRays[0].AttackingPiece, rook);
            Assert.AreEqual(xRays[0].TargetPiece, piece1);
            Assert.IsTrue(xRays[0].CellsBetween.Contains("b4"));
            Assert.IsTrue(xRays[0].CellsBetween.Contains("c4"));
        }

        [Test]
        public void GetXRays_SomePiecesBlocks_ReturnsOneXRayWithBlocks()
        {
            // Arrange
            var rookXRayUtility = CreateRookXRayUtility(
                out var board);

            var rook = Substitute.For<IPiece>();
            rook.Position.Returns("g6");
            rook.Color.Returns(PieceColor.Black);

            var piece1 = Substitute.For<IPiece>();
            piece1.Color.Returns(PieceColor.White);
            piece1.Position.Returns("a6");
            
            var blockingPiece1 = Substitute.For<IPiece>();
            blockingPiece1.Color.Returns(PieceColor.White);
            blockingPiece1.Position.Returns("e6");
            
            var blockingPiece2 = Substitute.For<IPiece>();
            blockingPiece2.Color.Returns(PieceColor.Black);
            blockingPiece2.Position.Returns("d6");

            board.Pieces.Returns(new[] {rook, piece1, blockingPiece1, blockingPiece2});
            
            // Act
            var xRays = rookXRayUtility.GetXRays(rook);
            
            // Assert
            var xRayWithPiece1 = xRays.First(x => x.TargetPiece == piece1);
            Assert.AreEqual(xRayWithPiece1.AttackingPiece, rook);
            Assert.AreEqual(xRayWithPiece1.TargetPiece, piece1);
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("b6"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("c6"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("d6"));
            Assert.IsTrue(xRayWithPiece1.CellsBetween.Contains("e6"));
            Assert.IsTrue(xRayWithPiece1.BlockingPieces.Contains(blockingPiece1));
            Assert.IsTrue(xRayWithPiece1.BlockingPieces.Contains(blockingPiece2));
        }
        
    }
}