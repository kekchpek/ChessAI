using KChess.Core.Castle;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class CastleDetectorTests
    {
        private CastleUtility CreateCastleDetector(
            out IBoard board)
        {
            board = Substitute.For<IBoard>();
            return new CastleUtility(board);
        }

        [TestCase(0)]
        [TestCase(7)]
        public void KingMovedForTwoCells_LeftSide_RookMovedToo(int yCoord)
        {
            // Arrange
            var detecotr = CreateCastleDetector(
                out var board);

            var king = Substitute.For<IPiece>();
            king.Type.Returns(PieceType.King);
            if (yCoord == 7)
            {
                king.Color.Returns(PieceColor.Black);
            }
            BoardCoordinates kingPosition = (4, yCoord);
            king.Position.Returns(kingPosition);
            king.PreviousPosition.Returns(kingPosition);
            
            var rightRook = Substitute.For<IPiece>();
            BoardCoordinates rightRookPosition = (7, yCoord);
            rightRook.Position.Returns(rightRookPosition);
            rightRook.PreviousPosition.Returns(rightRookPosition);

            var leftRook = Substitute.For<IPiece>();
            BoardCoordinates leftRookPosition = (0, yCoord);
            leftRook.Position.Returns(leftRookPosition);
            leftRook.PreviousPosition.Returns(leftRookPosition);

            board.Pieces.Returns(new[] {king, rightRook, leftRook});

            // Act
            BoardCoordinates newKingPos = (2, yCoord);
            king.Position.Returns(newKingPos);
            detecotr.TryMakeCastle(king);

            // Assert
            leftRook.Received().MoveTo(Arg.Is<BoardCoordinates>(x => x == BoardCoordinates.FromNumeric(3, yCoord)));  
        }
        
        [TestCase(0)]
        [TestCase(7)]
        public void KingMovedForTwoCells_RightSide_RookMovedToo(int yCoord)
        {
            // Arrange
            var detecotr = CreateCastleDetector(
                out var board);

            var king = Substitute.For<IPiece>();
            king.Type.Returns(PieceType.King);
            if (yCoord == 7)
            {
                king.Color.Returns(PieceColor.Black);
            }
            BoardCoordinates kingPosition = (4, yCoord);
            king.Position.Returns(kingPosition);
            king.PreviousPosition.Returns(kingPosition);
            
            var rightRook = Substitute.For<IPiece>();
            BoardCoordinates rightRookPosition = (7, yCoord);
            rightRook.Position.Returns(rightRookPosition);
            rightRook.PreviousPosition.Returns(rightRookPosition);

            var leftRook = Substitute.For<IPiece>();
            BoardCoordinates leftRookPosition = (0, yCoord);
            leftRook.Position.Returns(leftRookPosition);
            leftRook.PreviousPosition.Returns(leftRookPosition);

            board.Pieces.Returns(new[] {king, rightRook, leftRook});

            // Act
            BoardCoordinates newKingPos = (6, yCoord);
            king.Position.Returns(newKingPos);
            detecotr.TryMakeCastle(king);

            // Assert
            rightRook.Received().MoveTo(Arg.Is<BoardCoordinates>(x => x == BoardCoordinates.FromNumeric(5, yCoord)));  
            
        }
    }
}