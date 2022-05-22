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
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(rook);
            
            // Assert
            Assert.AreEqual(rookMoves, availableMoves);
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
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(knight);
            
            // Assert
            Assert.AreEqual(knightMoves, availableMoves);
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
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(bishop);
            
            // Assert
            Assert.AreEqual(bishopMoves, availableMoves);
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
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(queen);
            
            // Assert
            Assert.AreEqual(queenMoves, availableMoves);
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
            
            // Act
            var availableMoves = pieceMoveUtilityFacade.GetAvailableMoves(king);
            
            // Assert
            Assert.AreEqual(kingMoves, availableMoves);
        }
        
    }
}