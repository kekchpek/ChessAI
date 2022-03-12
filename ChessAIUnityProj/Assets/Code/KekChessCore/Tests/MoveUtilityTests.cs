using System.Collections.Generic;
using ChessAI.Core.AttackedCellsUtility;
using ChessAI.Core.BoardStateUtils;
using ChessAI.Core.CheckBlockingUtility;
using ChessAI.Core.CheckUtility;
using ChessAI.Core.MoveUtility;
using ChessAI.Core.XRayUtility;
using ChessAI.Domain;
using ChessAI.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace ChessAI.Tests
{
    public class MoveUtilityTests
    {
        private MoveUtility CreateMoveUtility(
            out IPieceMoveUtilityFacade pieceMoveUtilityFacade,
            out IXRayUtility xRayUtility,
            out IAttackedCellsUtility attackedCellsUtility,
            out IBoardStateGetter boardStateGetter,
            out ICheckBlockingUtility checkBlockingUtility,
            out ICheckUtility checkUtility)
        {
            pieceMoveUtilityFacade = Substitute.For<IPieceMoveUtilityFacade>();
            xRayUtility = Substitute.For<IXRayUtility>();
            attackedCellsUtility = Substitute.For<IAttackedCellsUtility>();
            boardStateGetter = Substitute.For<IBoardStateGetter>();
            checkBlockingUtility = Substitute.For<ICheckBlockingUtility>();
            checkUtility = Substitute.For<ICheckUtility>();
            return new MoveUtility(pieceMoveUtilityFacade,
                xRayUtility, attackedCellsUtility, boardStateGetter, checkBlockingUtility,
                checkUtility);
        }

        [TestCase(PieceColor.White, PieceColor.Black)]
        [TestCase(PieceColor.Black, PieceColor.White)]
        public void KingCantMoveToAttackedFields(
            PieceColor kingColor,
            PieceColor oppositeColor)
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);
            attackedCellsUtility.IsCellAttacked("d3", oppositeColor).Returns(true);
            attackedCellsUtility.IsCellAttacked("c5", oppositeColor).Returns(true);

            var king = Substitute.For<IPiece>();
            king.Color.Returns(kingColor);
            king.Type.Returns(PieceType.King);

            pieceMoveUtilityFacade.GetAvailableMoves(king).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });
            
            // Act
            var moves = moveUtility.GetAvailableMoves(king);
            
            // Assert
            Assert.AreEqual(3, moves.Length);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
            Assert.Contains((BoardCoordinates)"c6", moves);
        }
        

        [TestCase(PieceColor.White, PieceColor.Black, BoardState.CheckToBlack)]
        [TestCase(PieceColor.Black, PieceColor.White, BoardState.CheckToWhite)]
        public void KingCantMoveToAttackedFields(
            PieceColor kingColor,
            PieceColor oppositeColor,
            BoardState boardState)
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);
            attackedCellsUtility.IsCellAttacked("d3", oppositeColor).Returns(true);
            attackedCellsUtility.IsCellAttacked("c5", oppositeColor).Returns(true);

            boardStateGetter.Get().Returns(boardState);

            var king = Substitute.For<IPiece>();
            king.Color.Returns(kingColor);
            king.Type.Returns(PieceType.King);

            pieceMoveUtilityFacade.GetAvailableMoves(king).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });
            
            // Act
            var moves = moveUtility.GetAvailableMoves(king);
            
            // Assert
            Assert.AreEqual(3, moves.Length);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
            Assert.Contains((BoardCoordinates)"c6", moves);
        }

        [Test]
        public void Check_PieceCanOnlyBlock()
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);

            boardStateGetter.Get().Returns(BoardState.CheckToBlack);

            pieceMoveUtilityFacade.GetAvailableMoves(piece).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });

            checkBlockingUtility.GetMovesForCheckBlocking(PieceColor.Black).Returns(
                new BoardCoordinates[]
                {
                    "b3", "a1"
                });
            
            // Act
            var moves = moveUtility.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(2, moves.Length);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
        }
        
        [Test]
        public void Check_OneFigureChecking_CanTakeIt()
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);

            boardStateGetter.Get().Returns(BoardState.CheckToBlack);

            pieceMoveUtilityFacade.GetAvailableMoves(piece).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });

            var checkingPiece = Substitute.For<IPiece>();
            checkingPiece.Position.Returns("b3");
            checkingPiece.Color.Returns(PieceColor.White);

            checkUtility.GetCheckingPieces().Returns(new[] {checkingPiece});
            
            // Act
            var moves = moveUtility.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(1, moves.Length);
            Assert.Contains((BoardCoordinates)"b3", moves);
        }
        
        [Test]
        public void Check_TwoFigureChecking_NoMoves()
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);

            boardStateGetter.Get().Returns(BoardState.CheckToBlack);

            pieceMoveUtilityFacade.GetAvailableMoves(piece).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });

            var checkingPiece1 = Substitute.For<IPiece>();
            checkingPiece1.Position.Returns("b3");
            checkingPiece1.Color.Returns(PieceColor.White);

            var checkingPiece2 = Substitute.For<IPiece>();
            checkingPiece2.Position.Returns("c6");
            checkingPiece2.Color.Returns(PieceColor.White);

            checkUtility.GetCheckingPieces().Returns(new[] {checkingPiece1, checkingPiece2});
            
            // Act
            var moves = moveUtility.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(0, moves.Length);
        }
        
        [Test]
        public void NoCheck_PieceLinked_MovesBetweenOrTake()
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);

            boardStateGetter.Get().Returns(BoardState.Regular);

            pieceMoveUtilityFacade.GetAvailableMoves(piece).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });

            var king = Substitute.For<IPiece>();
            king.Type.Returns(PieceType.King);
            king.Color.Returns(PieceColor.Black);

            var xRayingPiece = Substitute.For<IPiece>();
            xRayingPiece.Position.Returns("a1");
            xRayingPiece.Color.Returns(PieceColor.White);

            var xRay = Substitute.For<IXRay>();
            xRay.AttackingPiece.Returns(xRayingPiece);
            xRay.TargetPiece.Returns(king);
            xRay.BlockingPieces.Returns(new [] {piece});
            xRay.CellsBetween.Returns(new BoardCoordinates[] {"d3", "c5"});

            xRayUtility.TargetPieces
                .Returns(new Dictionary<IPiece, IReadOnlyList<IXRay>> {{king, new [] {xRay}}});
            
            // Act
            var moves = moveUtility.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(3, moves.Length);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"d3", moves);
            Assert.Contains((BoardCoordinates)"c5", moves);
        }
        
        [Test]
        public void NoCheck_PieceCanMoveAnywhere()
        {
            // Arrange
            var moveUtility = CreateMoveUtility(
                out var pieceMoveUtilityFacade,
                out var xRayUtility,
                out var attackedCellsUtility,
                out var boardStateGetter,
                out var checkBlockingUtility,
                out var checkUtility);

            var piece = Substitute.For<IPiece>();
            piece.Color.Returns(PieceColor.Black);

            boardStateGetter.Get().Returns(BoardState.Regular);

            pieceMoveUtilityFacade.GetAvailableMoves(piece).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });
            
            // Act
            var moves = moveUtility.GetAvailableMoves(piece);
            
            // Assert
            Assert.AreEqual(5, moves.Length);
            Assert.Contains((BoardCoordinates)"d3", moves);
            Assert.Contains((BoardCoordinates)"a1", moves);
            Assert.Contains((BoardCoordinates)"b3", moves);
            Assert.Contains((BoardCoordinates)"c5", moves);
            Assert.Contains((BoardCoordinates)"c6", moves);
        }
    }
}