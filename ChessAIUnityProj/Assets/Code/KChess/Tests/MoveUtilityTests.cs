using System.Collections.Generic;
using KChess.Core.AttackedCellsUtility;
using KChess.Core.BoardStateUtils;
using KChess.Core.CastleMoveUtility;
using KChess.Core.CheckBlockingUtility;
using KChess.Core.CheckUtility;
using KChess.Core.MoveUtility;
using KChess.Core.XRayUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class MoveUtilityTests
    {

        [Test]
        public void GetAttackedCells_ReturnsFormFacade()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();

            var pieceSub = Substitute.For<IPiece>();

            pieceMoveUtilityFacade.GetAttackedCells(pieceSub).Returns(new BoardCoordinates[]
            {
                "d3", "a1", "b3", "c5", "c6"
            });

            // Act
            var attackedCells = moveUtility.GetAttackedMoves(pieceSub);

            // Assert
            Assert.AreEqual(pieceMoveUtilityFacade.GetAttackedCells(pieceSub), attackedCells);
        }

        [TestCase(PieceColor.White, PieceColor.Black)]
        [TestCase(PieceColor.Black, PieceColor.White)]
        public void KingCantMoveToAttackedFields(
            PieceColor kingColor,
            PieceColor oppositeColor)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var attackedCellsUtility = container.Resolve<IAttackedCellsUtility>();
            
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
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var attackedCellsUtility = container.Resolve<IAttackedCellsUtility>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            
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
        public void NoCheck_CastleMovesAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var attackedCellsUtility = container.Resolve<IAttackedCellsUtility>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var castleUtility = container.Resolve<ICastleMoveUtility>();

            boardStateGetter.Get().Returns(BoardState.Regular);
            attackedCellsUtility.IsCellAttacked(Arg.Any<BoardCoordinates>(), PieceColor.Black).Returns(false);

            var king = Substitute.For<IPiece>();
            king.Color.Returns(PieceColor.White);
            king.Position.Returns("a1");
            king.Type.Returns(PieceType.King);
            
            pieceMoveUtilityFacade.GetAvailableMoves(king).Returns(new BoardCoordinates[] {"d3", "c1", "h3"});
            castleUtility.GetCastleMoves(PieceColor.White).Returns(new BoardCoordinates[] {"a2", "a3"});

            // Act
            var moves = moveUtility.GetAvailableMoves(king);

            // Assert
            Assert.AreEqual(5, moves.Length);
            Assert.Contains((BoardCoordinates) "d3", moves);
            Assert.Contains((BoardCoordinates) "c1", moves);
            Assert.Contains((BoardCoordinates) "h3", moves);
            Assert.Contains((BoardCoordinates) "a2", moves);
            Assert.Contains((BoardCoordinates) "a3", moves);
        }

        [Test]
        public void Check_CastleMovesNotAvailable()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var attackedCellsUtility = container.Resolve<IAttackedCellsUtility>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var castleUtility = container.Resolve<ICastleMoveUtility>();

            boardStateGetter.Get().Returns(BoardState.CheckToWhite);
            attackedCellsUtility.IsCellAttacked(Arg.Any<BoardCoordinates>(), PieceColor.Black).Returns(false);

            var king = Substitute.For<IPiece>();
            king.Color.Returns(PieceColor.White);
            king.Position.Returns("a1");
            king.Type.Returns(PieceType.King);
            
            pieceMoveUtilityFacade.GetAvailableMoves(king).Returns(new BoardCoordinates[] {"d3", "c1", "h3"});
            castleUtility.GetCastleMoves(PieceColor.White).Returns(new BoardCoordinates[] {"a2", "a3"});

            // Act
            var moves = moveUtility.GetAvailableMoves(king);

            // Assert
            Assert.AreEqual(3, moves.Length);
            Assert.Contains((BoardCoordinates) "d3", moves);
            Assert.Contains((BoardCoordinates) "c1", moves);
            Assert.Contains((BoardCoordinates) "h3", moves);
        }

        [Test]
        public void Check_PieceCanOnlyBlock()
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var checkBlockingUtility = container.Resolve<ICheckBlockingUtility>();
            var checkUtility = container.Resolve<ICheckUtility>();

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

            checkUtility.GetCheckingPieces().Returns(new []{Substitute.For<IPiece>()});
            checkUtility.GetCheckingPieces()[0].Position.Returns("h8");
            
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
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var checkUtility = container.Resolve<ICheckUtility>();

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
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var checkUtility = container.Resolve<ICheckUtility>();

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
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();
            var xRayUtility = container.Resolve<IXRayUtility>();

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
            var container = TestHelper.CreateContainerFor<MoveUtility>(out var moveUtility);
            var pieceMoveUtilityFacade = container.Resolve<IPieceMoveUtilityFacade>();
            var boardStateGetter = container.Resolve<IBoardStateGetter>();

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