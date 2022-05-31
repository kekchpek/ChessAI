using KChess.Core.LastMovedPieceUtils;
using KChess.Core.MoveUtility.PieceMoveUtilities.Pawn;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests.PiecesMoves
{
    public class PawnMoveUtilityTests
    {
        [Test]
        public void White_MiddleOfBoard()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void Black_MiddleOfBoard()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void White_StartMoving()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d2", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.Contains((BoardCoordinates)"d4", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void Black_StartMoving()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            
            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void StartMoving_BlockedByAlly()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Position.Returns("d6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.AreEqual(0, availableMoves.Length);
        }
        
        [Test]
        public void StartMoving_BlockedByEnemy()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Position.Returns("d6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.AreEqual(0, availableMoves.Length);
        }
        
        [Test]
        public void CanTake_MoveToTakeAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Position.Returns("c6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d5", availableMoves);
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.Contains((BoardCoordinates)"c6", availableMoves);
            Assert.AreEqual(3, availableMoves.Length);
        }
        
        [Test]
        public void CanTakeBackward_MoveToTakeNotAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Position.Returns("c6");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d7", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d8", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void Black_EnemyPawnMovedNotLast_NoEnPassantMove()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c4");
            piece.PreviousPosition.Returns("c2");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void White_EnemyPawnMovedNotLast_NoEnPassantMove()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c5");
            piece.PreviousPosition.Returns("c7");
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.AreEqual(1, availableMoves.Length);
        }
        
        [Test]
        public void Black_EnPassant_MoveToTakeAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();
            var lastMovedPieceUtility = container.Resolve<ILastMovedPieceGetter>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.White);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c4");
            piece.PreviousPosition.Returns("c2");

            lastMovedPieceUtility.GetLastMovedPiece().Returns(piece);
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d4", PieceColor.Black);
            
            // Assert
            Assert.Contains((BoardCoordinates)"c3", availableMoves);
            Assert.Contains((BoardCoordinates)"d3", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }
        
        [Test]
        public void White_EnPassant_MoveToTakeAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();
            var lastMovedPieceUtility = container.Resolve<ILastMovedPieceGetter>();

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);
            piece.Type.Returns(PieceType.Pawn);
            piece.Position.Returns("c5");
            piece.PreviousPosition.Returns("c7");

            lastMovedPieceUtility.GetLastMovedPiece().Returns(piece);
            
            board.Pieces.Returns(new [] {piece});

            // Act
            var availableMoves = pawnMoveUtility.GetMoves("d5", PieceColor.White);
            
            // Assert
            Assert.Contains((BoardCoordinates)"c6", availableMoves);
            Assert.Contains((BoardCoordinates)"d6", availableMoves);
            Assert.AreEqual(2, availableMoves.Length);
        }

        [TestCase(PieceColor.Black, -1)]
        [TestCase(PieceColor.White, 1)]
        public void GetAttackedCells_AllyPieceOnAttackedPosition_ReturnsAllyPosition(
            PieceColor pieceColor, int direction)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            var board = container.Resolve<IBoard>();

            var pawnPosition = (BoardCoordinates) "c5";
            
            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(pieceColor);
            piece.Type.Returns(PieceType.Bishop);
            // ReSharper disable once PossibleInvalidOperationException
            var piecePosition = pawnPosition.ToNumeric();
            piecePosition = (piecePosition.Item1 - 1, piecePosition.Item2 + direction);
            piece.Position.Returns(piecePosition);

            board.Pieces.Returns(new[] {piece});
            
            // Act
            var attackedCells = pawnMoveUtility.GetAttackedCells(pawnPosition, pieceColor);
            
            // Assert
            // ReSharper disable once PossibleInvalidOperationException
            Assert.Contains(piece.Position.Value, attackedCells);
        }
        

        [TestCase(PieceColor.Black, -1)]
        [TestCase(PieceColor.White, 1)]
        public void GetAttackedCells_PawnOnBoardCenter_ReturnsFrontDiagonals(
            PieceColor pieceColor, int direction)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnMoveUtility>(out var pawnMoveUtility);
            
            var pawnPosition = (BoardCoordinates) "c5";
            
            // Act
            var attackedCells = pawnMoveUtility.GetAttackedCells(pawnPosition, pieceColor);
            
            // Assert
            var pawnPositionNumeric = pawnPosition.ToNumeric();
            var attackedPosition1 =
                (BoardCoordinates) (pawnPositionNumeric.Item1 + 1, pawnPositionNumeric.Item2 + direction);
            var attackedPosition2 =
                (BoardCoordinates) (pawnPositionNumeric.Item1 - 1, pawnPositionNumeric.Item2 + direction);
            Assert.AreEqual(2, attackedCells.Length);
            Assert.Contains(attackedPosition1, attackedCells);
            Assert.Contains(attackedPosition2, attackedCells);
        }
        
    }
}