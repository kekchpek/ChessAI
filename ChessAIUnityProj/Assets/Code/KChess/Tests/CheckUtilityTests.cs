using System;
using System.Linq;
using KChess.Core.CheckUtility;
using KChess.Core.MoveUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class CheckUtilityTests
    {
        private CheckUtility CreateCheckUtility(
            out IBoard board,
            out IPieceMoveUtilityFacade pieceMoveUtilityFacade)
        {
            board = Substitute.For<IBoard>();
            pieceMoveUtilityFacade = Substitute.For<IPieceMoveUtilityFacade>();
            return new CheckUtility(board, pieceMoveUtilityFacade);
        }

        [Test] 
        public void IsPositionWithCheck_NoCheck1_ReturnsFalse()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var whitePiece1 = Substitute.For<IPiece>();
            whitePiece1.Position.Returns((0, 0));
            whitePiece1.Color.Returns(PieceColor.White);
            pieceMoveUtilityFacade.GetAvailableMoves(whitePiece1).Returns(new BoardCoordinates[] { (0, 2) });
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Position.Returns((0, 1));
            blackKing.Type.Returns(PieceType.King);
            board.Pieces.Returns(new[] {whitePiece1, blackKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(whitePiece1);
            var isCheck = checkUtility.IsPositionWithCheck(out var checkedColor);

            // Assert
            Assert.IsFalse(isCheck);
        }

        [Test] 
        public void GetCheckingPieces_NoCheck1_ReturnsEmpty()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var whitePiece1 = Substitute.For<IPiece>();
            whitePiece1.Position.Returns((0, 0));
            whitePiece1.Color.Returns(PieceColor.White);
            pieceMoveUtilityFacade.GetAvailableMoves(whitePiece1).Returns(new BoardCoordinates[] { (0, 2) });
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Position.Returns((0, 1));
            blackKing.Type.Returns(PieceType.King);
            board.Pieces.Returns(new[] {whitePiece1, blackKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(whitePiece1);
            var checkingPieces = checkUtility.GetCheckingPieces();

            // Assert
            Assert.IsEmpty(checkingPieces);
        }

        [Test] 
        public void IsPositionWithCheck_NoCheck2_ReturnsFalse()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Position.Returns((0, 0));
            blackPiece1.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns((0, 1));
            board.Pieces.Returns(new[] {blackPiece1, blackKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(blackPiece1);
            var isCheck = checkUtility.IsPositionWithCheck(out var checkedColor);

            // Assert
            Assert.IsFalse(isCheck);
        }

        [Test] 
        public void GetCheckingPieces_NoCheck2_ReturnsEmpty()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Position.Returns((0, 0));
            blackPiece1.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns((0, 1));
            board.Pieces.Returns(new[] {blackPiece1, blackKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(blackPiece1);
            var checkingPieces = checkUtility.GetCheckingPieces();

            // Assert
            Assert.IsEmpty(checkingPieces);
        }
        
        [Test] 
        public void IsPositionWithCheck_CheckToWhite_ReturnsTrueAndWhite()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Position.Returns((0, 0));
            blackPiece1.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns((0, 1));
            
            board.Pieces.Returns(new[] {blackPiece1, whiteKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(blackPiece1);
            var isCheck = checkUtility.IsPositionWithCheck(out var checkedColor);

            // Assert
            Assert.IsTrue(isCheck);
            Assert.AreEqual(PieceColor.White, checkedColor);
        }
        
        
        [Test] 
        public void GetCheckingPieces_CheckToWhite_ReturnsCheckingFigure()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Position.Returns((0, 0));
            blackPiece1.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns((0, 1));
            
            board.Pieces.Returns(new[] {blackPiece1, whiteKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(blackPiece1);
            var checkingPieces = checkUtility.GetCheckingPieces();

            // Assert
            Assert.AreEqual(1, checkingPieces.Count);
            Assert.IsTrue(checkingPieces.Contains(blackPiece1));
        }

        [Test] 
        public void IsPositionWithCheck_CheckToBlack_ReturnsTrueAndBlack()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var whitePiece1 = Substitute.For<IPiece>();
            whitePiece1.Position.Returns((0, 0));
            whitePiece1.Color.Returns(PieceColor.White);
            pieceMoveUtilityFacade.GetAvailableMoves(whitePiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns((0, 1));

            board.Pieces.Returns(new[] {whitePiece1, blackKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(whitePiece1);
            var isCheck = checkUtility.IsPositionWithCheck(out var checkedColor);

            // Assert
            Assert.IsTrue(isCheck);
            Assert.AreEqual(PieceColor.Black, checkedColor);
        }

        [Test]
        public void GetCheckingPieces_SeveralCheckingPieces_ReturnsAllCheckingPieces()
        {
            // Arrange
            var checkUtility = CreateCheckUtility(
                out var board,
                out var pieceMoveUtilityFacade);
            
            var blackPiece1 = Substitute.For<IPiece>();
            blackPiece1.Position.Returns((0, 0));
            blackPiece1.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece1).Returns(new BoardCoordinates[] { (0, 1) });
            
            var blackPiece2 = Substitute.For<IPiece>();
            blackPiece2.Position.Returns((1, 0));
            blackPiece2.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece2).Returns(new BoardCoordinates[] { (0, 1) });
            
            var blackPiece3 = Substitute.For<IPiece>();
            blackPiece3.Position.Returns((2, 0));
            blackPiece3.Color.Returns(PieceColor.Black);
            pieceMoveUtilityFacade.GetAvailableMoves(blackPiece3).Returns(new BoardCoordinates[] { (0, 1) });
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns((0, 1));
            
            board.Pieces.Returns(new[] {blackPiece1, blackPiece2,
                blackPiece3, whiteKing});

            // Act
            board.Updated += Raise.Event<Action<IPiece>>(blackPiece1);
            var checkingPieces = checkUtility.GetCheckingPieces();

            // Assert
            Assert.AreEqual(3, checkingPieces.Count);
            Assert.IsTrue(checkingPieces.Contains(blackPiece1));
            Assert.IsTrue(checkingPieces.Contains(blackPiece2));
            Assert.IsTrue(checkingPieces.Contains(blackPiece3));
        }
        
    }
}