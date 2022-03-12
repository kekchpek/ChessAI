using System;
using KekChessCore.Domain;
using KekChessCore.LastMovedPieceUtils;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class LastMovedPieceUtilityTests
    {
        private LastMovedPieceUtility CreateLastMovedPieceUtility(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new LastMovedPieceUtility(board);
        }

        [Test]
        public void PieceMoved_EventCalled()
        {
            // Arrange
            var lastMovedPieceUtility = CreateLastMovedPieceUtility(
                out var board);
            var somePiece = Substitute.For<IPiece>();
            
            // Act
            IPiece eventPiece = null;
            lastMovedPieceUtility.LastMovedPieceChanged += x => eventPiece = x;
            board.PieceMoved += Raise.Event<Action<IPiece>>(somePiece);
            
            // Assert
            Assert.AreEqual(somePiece, eventPiece);
        }

        [Test]
        public void PieceMoved_LastMovedPieceSet()
        {
            // Arrange
            var lastMovedPieceUtility = CreateLastMovedPieceUtility(
                out var board);
            var somePiece = Substitute.For<IPiece>();
            
            // Act
            board.PieceMoved += Raise.Event<Action<IPiece>>(somePiece);
            
            // Assert
            Assert.AreEqual(somePiece, lastMovedPieceUtility.GetLastMovedPiece());
            
        }
    }
}