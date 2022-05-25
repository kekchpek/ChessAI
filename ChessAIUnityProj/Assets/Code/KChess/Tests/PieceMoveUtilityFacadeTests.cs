using KChess.Core.MoveUtility;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.King;
using KChess.Core.MoveUtility.PieceMoveUtilities.Knight;
using KChess.Core.MoveUtility.PieceMoveUtilities.Pawn;
using KChess.Core.MoveUtility.PieceMoveUtilities.Queen;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class PieceMoveUtilityFacadeTests
    {

        private PieceMoveUtilityFacade CreatePieceMoveUtilityFacade(
            out IPawnMoveUtility pawnMoveUtility,
            out IRookMoveUtility rookMoveUtility,
            out IKnightMoveUtility knightMoveUtility,
            out IBishopMoveUtility bishopMoveUtility,
            out IQueenMoveUtility queenMoveUtility,
            out IKingMoveUtility kingMoveUtility)
        {
            pawnMoveUtility = Substitute.For<IPawnMoveUtility>();
            rookMoveUtility = Substitute.For<IRookMoveUtility>();
            knightMoveUtility = Substitute.For<IKnightMoveUtility>();
            bishopMoveUtility = Substitute.For<IBishopMoveUtility>();
            queenMoveUtility = Substitute.For<IQueenMoveUtility>();
            kingMoveUtility = Substitute.For<IKingMoveUtility>();
            return new PieceMoveUtilityFacade(pawnMoveUtility, rookMoveUtility,
                knightMoveUtility, bishopMoveUtility, queenMoveUtility,
                kingMoveUtility);
        }

        [Test]
        public void PawnAttackingCells()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
        public void Pawn()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
        public void Rook()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(rook);
            
            // Assert
            Assert.AreEqual(rookMoves, availableMoves);
            Assert.AreEqual(rookMoves, attackedCells);
        }

        [Test]
        public void Knight()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(knight);
            
            // Assert
            Assert.AreEqual(knightMoves, availableMoves);
            Assert.AreEqual(knightMoves, attackedCells);
        }

        [Test]
        public void Bishop()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(bishop);
            
            // Assert
            Assert.AreEqual(bishopMoves, availableMoves);
            Assert.AreEqual(bishopMoves, attackedCells);
        }

        [Test]
        public void Queen()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(queen);
            
            // Assert
            Assert.AreEqual(queenMoves, availableMoves);
            Assert.AreEqual(queenMoves, attackedCells);
        }

        [Test]
        public void King()
        {
            // Arrange 
            var pieceMoveUtilityFacade = CreatePieceMoveUtilityFacade(
                out var pawnMoveUtility,
                out var rookMoveUtility,
                out var knightMoveUtility,
                out var bishopMoveUtility,
                out var queenMoveUtility,
                out var kingMoveUtility);

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
            var attackedCells = pieceMoveUtilityFacade.GetAttackedCells(king);
            
            // Assert
            Assert.AreEqual(kingMoves, availableMoves);
            Assert.AreEqual(kingMoves, attackedCells);
        }
        
    }
}