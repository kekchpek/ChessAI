using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class PieceTests
    {

        private Piece CreatePiece(
            PieceType pieceType,
            PieceColor pieceColor,
            BoardCoordinates boardCoordinates)
        {
            return new Piece(pieceType, boardCoordinates, pieceColor);
        }

        [Test]
        public void Creation_TypeSet()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            
            // Act
            // No act. Test creation.
            
            // Assert
            Assert.AreEqual(pieceType, piece.Type);
        }

        [Test]
        public void Creation_ColorSet()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            
            // Act
            // No act. Test creation.
            
            // Assert
            Assert.AreEqual(pieceColor, piece.Color);
        }

        [Test]
        public void Creation_CoordinatesSet()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            
            // Act
            // No act. Test creation.
            
            // Assert
            Assert.AreEqual(boardCoordinates, piece.Position);
        }

        [Test]
        public void Move_CorrectCoordinates_EventCalled()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            BoardCoordinates newPosition = "e1";
            
            // Act
            var eventCalled = false;
            piece.Moved += () => eventCalled = true;
            ((IPiece)piece).MoveTo(newPosition);
            
            // Assert
            Assert.IsTrue(eventCalled, "Event was not called, but should");
        }
        
        [Test]
        public void Move_CorrectCoordinates_CoordinateChanged()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            BoardCoordinates newPosition = "e1";
            
            // Act
            ((IPiece)piece).MoveTo(newPosition);
            
            // Assert
            Assert.AreEqual(newPosition, piece.Position);
        }
        
        [Test]
        public void Move_CorrectCoordinates_PreviousCoordinateNotChanged()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            BoardCoordinates newPosition = "e1";
            
            // Act
            ((IPiece)piece).MoveTo(newPosition);
            
            // Assert
            Assert.AreEqual(boardCoordinates, piece.PreviousPosition);
        }
        
        [Test]
        public void MoveTwice_CorrectCoordinates_PreviousCoordinateChanged()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            BoardCoordinates newPosition1 = "e1";
            BoardCoordinates newPosition2 = "e6";
            
            // Act
            ((IPiece)piece).MoveTo(newPosition1);
            ((IPiece)piece).MoveTo(newPosition2);
            
            // Assert
            Assert.AreEqual(newPosition1, piece.PreviousPosition);
        }
        
        [Test]
        public void Creation_PreviousCoordsEqualsToCurrent()
        {
            // Assert
            var pieceType = PieceType.King;
            var pieceColor = PieceColor.White;
            BoardCoordinates boardCoordinates = "a1";
            var piece = CreatePiece(pieceType,
                pieceColor,
                boardCoordinates);
            
            // Assert
            Assert.AreEqual(boardCoordinates, piece.PreviousPosition);
        }
    }
}