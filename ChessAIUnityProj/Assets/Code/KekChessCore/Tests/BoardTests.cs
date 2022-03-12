using System;
using System.Linq;
using ChessAI.Domain;
using ChessAI.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class BoardTests
    {

        private Board CreateBoard()
        {
            return new Board();
        }

        [Test]
        public void Creation_PiecesEmpty()
        {
            // Arrange
            var board = CreateBoard();
            
            // Act 
            // no act
            
            // Assert
            Assert.IsEmpty(board.Pieces);
        }

        [Test]
        public void PlacePiece_AddedToPieces()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            board.PlacePiece(pieceToPlace);
            
            // Assert
            Assert.Contains(pieceToPlace, board.Pieces.ToArray());
        }

        [Test]
        public void PlacePiece_PositionChanged()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            IPiece changedPositionPiece = null;
            board.PositionChanged += x => changedPositionPiece = x;
            board.PlacePiece(pieceToPlace);
            
            // Assert
            Assert.AreEqual(changedPositionPiece, pieceToPlace);
        }

        [Test]
        public void PlacePiece_PieceOnSameCellRemoved()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace1 = Substitute.For<IPiece>();
            pieceToPlace1.Position.Returns("c4");
            var pieceToPlace2 = Substitute.For<IPiece>();
            pieceToPlace2.Position.Returns("c4");
            
            // Act
            board.PlacePiece(pieceToPlace1);
            board.PlacePiece(pieceToPlace2);
            
            // Assert
            Assert.Contains(pieceToPlace2, board.Pieces.ToArray());
            Assert.IsFalse(board.Pieces.Contains(pieceToPlace1));
        }

        [Test]
        public void PieceMoved_EventCalled()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            IPiece movedPiece = null;
            board.PieceMoved += x => movedPiece = x;
            board.PlacePiece(pieceToPlace);
            pieceToPlace.Moved += Raise.Event<Action>();
            
            // Assert
            Assert.AreEqual(pieceToPlace, movedPiece);
        }

        [Test]
        public void PieceMoved_PositionChanged_EventCalled()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            IPiece movedPiece = null;
            board.PositionChanged += x => movedPiece = x;
            board.PlacePiece(pieceToPlace);
            pieceToPlace.Moved += Raise.Event<Action>();
            
            // Assert
            Assert.AreEqual(pieceToPlace, movedPiece);
        }
        
        [Test]
        public void RemovePiece_RemovedFromPieces()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            board.PlacePiece(pieceToPlace);
            board.RemovePiece(pieceToPlace);
            
            // Assert
            Assert.IsFalse(board.Pieces.ToArray().Contains(pieceToPlace));
        }
        
        [Test]
        public void RemovePiece_EventCalled_PositionChanged()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            board.PlacePiece(pieceToPlace);
            IPiece positionChangedPiece = null;
            board.PositionChanged += x => positionChangedPiece = x;
            board.RemovePiece(pieceToPlace);
            
            // Assert
            Assert.AreEqual(pieceToPlace, positionChangedPiece);
        }
        
        [Test]
        public void RemovePiece_EventNotCalled_PieceMoved()
        {
            // Arrange
            var board = CreateBoard();
            var pieceToPlace = Substitute.For<IPiece>();
            
            // Act 
            board.PlacePiece(pieceToPlace);
            board.RemovePiece(pieceToPlace);
            IPiece movedPiece = null;
            board.PieceMoved += x => movedPiece = x;
            pieceToPlace.Moved += Raise.Event<Action>();
            
            // Assert
            Assert.IsNull(movedPiece);
        }
    }
}