using KChess.Core.MoveUtility;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.King;
using KChess.Core.MoveUtility.PieceMoveUtilities.Knight;
using KChess.Core.MoveUtility.PieceMoveUtilities.Pawn;
using KChess.Core.MoveUtility.PieceMoveUtilities.Queen;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class PieceMoveUtilityFacadeTests
    {

        [Test]
        public void GetAttackedCells_Pawn_ReturnsFromPawnMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var pawnMoveUtility = container.Resolve<IPawnMoveUtility>();

            var pawnAttackingCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            pawnMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(pawnAttackingCells);

            var pawn = Substitute.For<IPiece>();
            pawn.Type.Returns(PieceType.Pawn);
            pawn.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAttackedCells(pawn);
            
            // Assert
            Assert.AreEqual(pawnAttackingCells, availableMoves);
        }

        [Test]
        public void GetMoves_Pawn_ReturnsFromPawnMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var pawnMoveUtility = container.Resolve<IPawnMoveUtility>();

            var pawnMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            pawnMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(pawnMoves);

            var pawn = Substitute.For<IPiece>();
            pawn.Type.Returns(PieceType.Pawn);
            pawn.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(pawn);
            
            // Assert
            Assert.AreEqual(pawnMoves, availableMoves);
        }

        [Test]
        public void GetMoves_Rook_ReturnsFromRookMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var rookMoveUtility = container.Resolve<IRookMoveUtility>();

            var rookMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            rookMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(rookMoves);

            var rook = Substitute.For<IPiece>();
            rook.Type.Returns(PieceType.Rook);
            rook.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(rook);
            
            // Assert
            Assert.AreEqual(rookMoves, availableMoves);
        }

        [Test]
        public void GetAttackedCells_Rook_ReturnsFromRookMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var rookMoveUtility = container.Resolve<IRookMoveUtility>();

            var rookAttackedCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            rookMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(rookAttackedCells);

            var rook = Substitute.For<IPiece>();
            rook.Type.Returns(PieceType.Rook);
            rook.Position.Returns("h8");
            
            // Act
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(rook);
            
            // Assert
            Assert.AreEqual(rookAttackedCells, attackedCells);
        }

        [Test]
        public void GetMoves_Knight_ReturnsFromKnightMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var knightMoveUtility = container.Resolve<IKnightMoveUtility>();

            var knightMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            knightMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(knightMoves);

            var knight = Substitute.For<IPiece>();
            knight.Type.Returns(PieceType.Knight);
            knight.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(knight);
            
            // Assert
            Assert.AreEqual(knightMoves, availableMoves);
        }

        [Test]
        public void GetAttackedCells_Knight_ReturnsFromKnightMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var knightMoveUtility = container.Resolve<IKnightMoveUtility>();

            var knightAttackedCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            knightMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(knightAttackedCells);

            var knight = Substitute.For<IPiece>();
            knight.Type.Returns(PieceType.Knight);
            knight.Position.Returns("h8");
            
            // Act
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(knight);
            
            // Assert
            Assert.AreEqual(knightAttackedCells, attackedCells);
        }

        [Test]
        public void GetMoves_Bishop_ReturnsFromBishopMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var bishopMoveUtility = container.Resolve<IBishopMoveUtility>();

            var bishopMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            bishopMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(bishopMoves);

            var bishop = Substitute.For<IPiece>();
            bishop.Type.Returns(PieceType.Bishop);
            bishop.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(bishop);
            
            // Assert
            Assert.AreEqual(bishopMoves, availableMoves);
        }

        [Test]
        public void GetAttackedCells_Bishop_ReturnsFromBishopMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var bishopMoveUtility = container.Resolve<IBishopMoveUtility>();

            var bishopAttackedCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            bishopMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(bishopAttackedCells);

            var bishop = Substitute.For<IPiece>();
            bishop.Type.Returns(PieceType.Bishop);
            bishop.Position.Returns("h8");
            
            // Act
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(bishop);
            
            // Assert
            Assert.AreEqual(bishopAttackedCells, attackedCells);
        }

        [Test]
        public void GetMoves_Queen_ReturnsFromQueenMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var queenMoveUtility = container.Resolve<IQueenMoveUtility>();

            var queenMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            queenMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(queenMoves);

            var queen = Substitute.For<IPiece>();
            queen.Type.Returns(PieceType.Queen);
            queen.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(queen);
            
            // Assert
            Assert.AreEqual(queenMoves, availableMoves);
        }

        [Test]
        public void GetAttackedCells_Queen_ReturnsFromQueenMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var queenMoveUtility = container.Resolve<IQueenMoveUtility>();

            var queenAttackedCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            queenMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(queenAttackedCells);

            var queen = Substitute.For<IPiece>();
            queen.Type.Returns(PieceType.Queen);
            queen.Position.Returns("h8");
            
            // Act
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(queen);
            
            // Assert
            Assert.AreEqual(queenAttackedCells, attackedCells);
        }

        [Test]
        public void GetMoves_King_ReturnsFromKingMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var kingMoveUtility = container.Resolve<IKingMoveUtility>();

            var kingMoves = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            kingMoveUtility.GetMoves(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(kingMoves);

            var king = Substitute.For<IPiece>();
            king.Type.Returns(PieceType.King);
            king.Position.Returns("h8");
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(king);
            
            // Assert
            Assert.AreEqual(kingMoves, availableMoves);
        }

        [Test]
        public void GetAttackedCells_King_ReturnsFromKingMovesUtility()
        {
            // Arrange 
            var container = TestHelper.CreateContainerFor<PieceMoveUtilityFacade>(out var pieceMoveUtilityFacade);
            var kingMoveUtility = container.Resolve<IKingMoveUtility>();

            var kingAttackedCells = new BoardCoordinates[]
            {
                "a1", "a2", "a3", "f7",
            };
            kingMoveUtility.GetAttackedCells(Arg.Any<BoardCoordinates>(), Arg.Any<PieceColor>())
                .Returns(kingAttackedCells);

            var king = Substitute.For<IPiece>();
            king.Type.Returns(PieceType.King);
            king.Position.Returns("h8");
            
            // Act
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(king);
            
            // Assert
            Assert.AreEqual(kingAttackedCells, attackedCells);
        }
        
    }
}