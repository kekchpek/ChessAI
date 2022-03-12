using ChessAI.Core.EnPassantUtility;
using ChessAI.Core.LastMovedPieceUtils;
using ChessAI.Domain;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class EnPassantUtilityTests
    {
        private EnPassantUtility CreateEnPassantUtility(
            out ILastMovedPieceGetter lastMovedPieceGetter)
        {
            lastMovedPieceGetter = Substitute.For<ILastMovedPieceGetter>();
            return new EnPassantUtility(lastMovedPieceGetter);
        }

        [Test]
        public void NoEnPassant_JustMoveDiagonal_ReturnFalse()
        {
            // Arrange
            var enPassantUtility = CreateEnPassantUtility(
                out var lastMovedPieceGetter);

            var pawn = Substitute.For<IPiece>();
            pawn.Type.Returns(PieceType.Pawn);
            pawn.Color.Returns(PieceColor.White);
            pawn.Position.Returns("e5");
            pawn.PreviousPosition.Returns("d4");
            
            var enemyPawn = Substitute.For<IPiece>();
            enemyPawn.Type.Returns(PieceType.Pawn);
            enemyPawn.Color.Returns(PieceColor.Black);
            enemyPawn.Position.Returns("e4");
            enemyPawn.PreviousPosition.Returns("e5");

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(new[] {pawn, enemyPawn});


            // Act
            var isEnPassant = enPassantUtility.IsFigureWasTakenWithEnPassant(pawn, board, out var taken);

            // Assert
            Assert.IsFalse(isEnPassant);
        }
        
        [Test]
        public void NoEnPassant_PawnWasNotMovedLast_ReturnFalse()
        {
            // Arrange
            var enPassantUtility = CreateEnPassantUtility(
                out var lastMovedPieceGetter);

            var pawn = Substitute.For<IPiece>();
            pawn.Type.Returns(PieceType.Pawn);
            pawn.Color.Returns(PieceColor.White);
            pawn.Position.Returns("e5");
            pawn.PreviousPosition.Returns("d4");
            
            var enemyPawn = Substitute.For<IPiece>();
            enemyPawn.Type.Returns(PieceType.Pawn);
            enemyPawn.Color.Returns(PieceColor.Black);
            enemyPawn.Position.Returns("e4");
            enemyPawn.PreviousPosition.Returns("e6");

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(new[] {pawn, enemyPawn});


            // Act
            var isEnPassant = enPassantUtility.IsFigureWasTakenWithEnPassant(pawn, board, out var taken);

            // Assert
            Assert.IsFalse(isEnPassant);
        }
        
        [Test]
        public void EnPassant_ReturnsTrue_TakenPieceCorrect()
        {
            // Arrange
            var enPassantUtility = CreateEnPassantUtility(
                out var lastMovedPieceGetter);

            var pawn = Substitute.For<IPiece>();
            pawn.Type.Returns(PieceType.Pawn);
            pawn.Color.Returns(PieceColor.White);
            pawn.Position.Returns("e5");
            pawn.PreviousPosition.Returns("d4");
            
            var enemyPawn = Substitute.For<IPiece>();
            enemyPawn.Type.Returns(PieceType.Pawn);
            enemyPawn.Color.Returns(PieceColor.Black);
            enemyPawn.Position.Returns("e4");
            enemyPawn.PreviousPosition.Returns("e6");

            lastMovedPieceGetter.GetLastMovedPiece().Returns(enemyPawn);

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(new[] {pawn, enemyPawn});


            // Act
            var isEnPassant = enPassantUtility.IsFigureWasTakenWithEnPassant(pawn, board, out var taken);

            // Assert
            Assert.IsTrue(isEnPassant);
            Assert.AreEqual(enemyPawn, taken);
        }
    }
}