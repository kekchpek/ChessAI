using System;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.MoveUtility;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class AttackedCellsUtilityTests
    {

        private AttackedCellsUtility.AttackedCellsUtility CreateAttackedCellsUtility(
            out IPieceMoveUtilityFacade pieceMoveUtilityFacade,
            out IBoard board)
        {
            pieceMoveUtilityFacade = Substitute.For<IPieceMoveUtilityFacade>();
            board = Substitute.For<IBoard>();
            return new AttackedCellsUtility.AttackedCellsUtility(pieceMoveUtilityFacade, board);
        }

        [Test]
        public void NoPiecesOnBoard_NoAttack(
            [Values(PieceColor.Black, PieceColor.White)] PieceColor attackingColor, 
            [Range(0, 7)] int x,
            [Range(0, 7)] int y)
        {
            // Arrange
            var attackedCellsUtility = CreateAttackedCellsUtility(
                out var pieceMoveUtilityFacade,
                out var board);
            
            // Act
            var isAttacked = attackedCellsUtility.IsCellAttacked((x, y), attackingColor);
            
            // Assert
            Assert.IsFalse(isAttacked);
        }

        [Test]
        public void PiecesWithoutMoves_NoAttack(
            [Values(PieceColor.Black, PieceColor.White)] PieceColor attackingColor, 
            [Range(0, 7)] int x,
            [Range(0, 7)] int y)
        {
            // Arrange
            var attackedCellsUtility = CreateAttackedCellsUtility(
                out var pieceMoveUtilityFacade,
                out var board);

            var pieces = new IPiece[4];
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Substitute.For<IPiece>();
            }

            pieces[0].Color.Returns(PieceColor.White);
            pieces[1].Color.Returns(PieceColor.White);
            pieces[2].Color.Returns(PieceColor.Black);
            pieces[3].Color.Returns(PieceColor.Black);

            board.Pieces.Returns(pieces);

            pieceMoveUtilityFacade.GetAvailableMoves(Arg.Any<IPiece>()).Returns(Array.Empty<BoardCoordinates>());
            
            // Act
            var isAttacked = attackedCellsUtility.IsCellAttacked((x, y), attackingColor);
            
            // Assert
            Assert.IsFalse(isAttacked);
        }

        [TestCase(PieceColor.Black, false)]
        [TestCase(PieceColor.White, true)]
        public void WhitePieceAttacks(PieceColor attackingColor, bool expectedResult)
        {
            // Arrange
            var attackedCellsUtility = CreateAttackedCellsUtility(
                out var pieceMoveUtilityFacade,
                out var board);

            var whitePiece = Substitute.For<IPiece>();
            whitePiece.Color.Returns(PieceColor.White);

            board.Pieces.Returns(new[] {whitePiece});

            pieceMoveUtilityFacade.GetAvailableMoves(whitePiece).Returns(new BoardCoordinates[]{"c3"});
            
            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(whitePiece);
            var isAttacked = attackedCellsUtility.IsCellAttacked("c3", attackingColor);
            
            // Assert
            Assert.AreEqual(expectedResult, isAttacked);
        }
        
    }
}