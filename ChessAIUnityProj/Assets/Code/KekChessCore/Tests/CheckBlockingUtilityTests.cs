using System.Collections.Generic;
using System.Linq;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.XRayUtility;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class CheckBlockingUtilityTests
    {
        private CheckBlockingUtility.CheckBlockingUtility CreateCheckBlockingUtility(
            out IXRayUtility xRayUtility)
        {
            xRayUtility = Substitute.For<IXRayUtility>();
            return new CheckBlockingUtility.CheckBlockingUtility(xRayUtility);
        }

        [Test]
        public void NoDirectXRay_ReturnsEmpty(
            [Values(PieceColor.Black, PieceColor.White)]
            PieceColor checkedPlayer)
        {
            // Arrange 
            var checkBlockingUtility = CreateCheckBlockingUtility(
                out var xRayUtility);
            
            // Act
            var blockingMoves = checkBlockingUtility.GetMovesForCheckBlocking(checkedPlayer);
            
            // Assert
            Assert.IsEmpty(blockingMoves);
        }

        [Test]
        public void BlackCheckingWhite_ReturnsCellsBetween()
        {
            // Arrange 
            var checkBlockingUtility = CreateCheckBlockingUtility(
                out var xRayUtility);

            var blackPiece = Substitute.For<IPiece>();
            blackPiece.Color.Returns(PieceColor.Black);

            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);

            var xRay = Substitute.For<IXRay>();
            xRay.AttackingPiece.Returns(blackPiece);
            xRay.TargetPiece.Returns(whiteKing);
            xRay.CellsBetween.Returns(Substitute.For<IReadOnlyList<BoardCoordinates>>());

            xRayUtility.TargetPieces.Returns(new Dictionary<IPiece, IReadOnlyList<IXRay>>
            {
                {whiteKing, new List<IXRay> {xRay}}
            });
            
            // Act
            var blockingMoves = checkBlockingUtility.GetMovesForCheckBlocking(PieceColor.White);
            
            // Assert
            Assert.IsTrue(xRay.CellsBetween.SequenceEqual(blockingMoves));
        }
        

        [Test]
        public void DoubleCheck_ReturnsEmpty()
        {
            // Arrange
            var checkBlockingUtility = CreateCheckBlockingUtility(
                out var xRayUtility);

            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Color.Returns(PieceColor.Black);
            
            var blackPiece2 = Substitute.For<IPiece>();
            blackPiece2.Color.Returns(PieceColor.Black);

            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);

            var xRay1 = Substitute.For<IXRay>();
            xRay1.AttackingPiece.Returns(blackPiece1);
            xRay1.TargetPiece.Returns(whiteKing);
            xRay1.CellsBetween.Returns(Substitute.For<IReadOnlyList<BoardCoordinates>>());
            
            var xRay2 = Substitute.For<IXRay>();
            xRay2.AttackingPiece.Returns(blackPiece2);
            xRay2.TargetPiece.Returns(whiteKing);
            xRay2.CellsBetween.Returns(Substitute.For<IReadOnlyList<BoardCoordinates>>());

            xRayUtility.TargetPieces.Returns(new Dictionary<IPiece, IReadOnlyList<IXRay>>
            {
                {whiteKing, new List<IXRay> {xRay1, xRay2}}
            });
            
            // Act
            var blockingMoves = checkBlockingUtility.GetMovesForCheckBlocking(PieceColor.White);
            
            // Assert
            Assert.IsEmpty(blockingMoves);
        }
    }
}