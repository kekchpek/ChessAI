using System;
using System.Linq;
using KChess.Core.XRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using KChess.Domain;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace KChess.Tests
{
    public class XRayUtilityTests
    {

        private XRayUtility CreateXRayUtility(
            out IBoard board,
            out IQueenXRayUtility queenXRayUtility,
            out IRookXRayUtility rookXRayUtility,
            out IBishopXRayUtility bishopXRayUtility)
        {
            board = Substitute.For<IBoard>();
            queenXRayUtility = Substitute.For<IQueenXRayUtility>();
            rookXRayUtility = Substitute.For<IRookXRayUtility>();
            bishopXRayUtility = Substitute.For<IBishopXRayUtility>();
            return new XRayUtility(board, queenXRayUtility, rookXRayUtility, bishopXRayUtility); 
        }

        [Test]
        public void AttackingPiece_Bishop_GetXRaysFromUtility()
        {
            // Arrange
            var xRayUtility = CreateXRayUtility(
                out var board,
                out var queenXRayUtility,
                out var rookXRayUtility,
                out var bishopXRayUtility);

            var whiteBishop = Substitute.For<IPiece>();
            whiteBishop.Type.Returns(PieceType.Bishop);
            whiteBishop.Color.Returns(PieceColor.White);
            whiteBishop.Position.Returns("f4");

            var xRays = new IXRay[3];
            for (var i = 0; i < xRays.Length; i++)
            {
                xRays[i] = Substitute.For<IXRay>();
                xRays[i].AttackingPiece.Returns(whiteBishop);
            }

            board.Pieces.Returns(new[] {whiteBishop});

            bishopXRayUtility.GetXRays(whiteBishop).Returns(xRays);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);

            // Assert
            Assert.AreEqual(xRayUtility.AttackingPieces[whiteBishop].Count, xRays.Length);
            Assert.Contains(xRays[0], xRayUtility.AttackingPieces[whiteBishop].ToList());
            Assert.Contains(xRays[1], xRayUtility.AttackingPieces[whiteBishop].ToList());
            Assert.Contains(xRays[2], xRayUtility.AttackingPieces[whiteBishop].ToList());
        }
        
        [Test]
        public void TargetPieces_Bishop_GetXRaysFromUtility()
        {
            // Arrange
            var xRayUtility = CreateXRayUtility(
                out var board,
                out var queenXRayUtility,
                out var rookXRayUtility,
                out var bishopXRayUtility);

            var whiteBishop = Substitute.For<IPiece>();
            whiteBishop.Type.Returns(PieceType.Bishop);
            whiteBishop.Color.Returns(PieceColor.White);
            whiteBishop.Position.Returns("f4");

            var xRays = new IXRay[3];
            for (var i = 0; i < xRays.Length; i++)
            {
                xRays[i] = Substitute.For<IXRay>();
                xRays[i].TargetPiece.Returns(Substitute.For<IPiece>());
                xRays[i].AttackingPiece.Returns(whiteBishop);
            }

            board.Pieces.Returns(new[] {whiteBishop});

            bishopXRayUtility.GetXRays(whiteBishop).Returns(xRays);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);

            // Assert
            Assert.AreEqual(xRayUtility.TargetPieces.Count, xRays.Length);
            Assert.AreEqual(xRayUtility.TargetPieces[xRays[0].TargetPiece].First(), xRays[0]);
            Assert.AreEqual(xRayUtility.TargetPieces[xRays[1].TargetPiece].First(), xRays[1]);
            Assert.AreEqual(xRayUtility.TargetPieces[xRays[2].TargetPiece].First(), xRays[2]);
        }

        [Test]
        public void AttackingPieces_BishopAndRookMoved_Bishop_Rook_Bishop_FirsBishopXRaysDeleted()
        {
            // Arrange
            var xRayUtility = CreateXRayUtility(
                out var board,
                out var queenXRayUtility,
                out var rookXRayUtility,
                out var bishopXRayUtility);

            var whiteBishop = Substitute.For<IPiece>();
            whiteBishop.Type.Returns(PieceType.Bishop);
            whiteBishop.Color.Returns(PieceColor.White);
            whiteBishop.Position.Returns("f4");
            
            var blackRook = Substitute.For<IPiece>();
            blackRook.Type.Returns(PieceType.Rook);
            blackRook.Color.Returns(PieceColor.Black);
            blackRook.Position.Returns("c4");

            board.Pieces.Returns(new[] {whiteBishop, blackRook});

            var bishopFirstXRays = new IXRay[3];
            for (var i = 0; i < bishopFirstXRays.Length; i++)
            {
                bishopFirstXRays[i] = Substitute.For<IXRay>();
                bishopFirstXRays[i].AttackingPiece.Returns(whiteBishop);
            }
            
            var rookXRays = new IXRay[3];
            for (var i = 0; i < rookXRays.Length; i++)
            {
                rookXRays[i] = Substitute.For<IXRay>();
                rookXRays[i].AttackingPiece.Returns(blackRook);
            }
            
            var bishopSecondXRays = new IXRay[2];
            for (var i = 0; i < bishopSecondXRays.Length; i++)
            {
                bishopSecondXRays[i] = Substitute.For<IXRay>();
                bishopSecondXRays[i].AttackingPiece.Returns(whiteBishop);
            }

            bishopXRayUtility.GetXRays(whiteBishop).Returns(bishopFirstXRays);
            rookXRayUtility.GetXRays(blackRook).Returns(rookXRays);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackRook);
            bishopXRayUtility.Configure().GetXRays(whiteBishop).Returns(bishopSecondXRays);
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);

            // Assert
            Assert.AreEqual(2, xRayUtility.AttackingPieces.Count);
             
            Assert.AreEqual(xRayUtility.AttackingPieces[whiteBishop].Count, bishopSecondXRays.Length);
            Assert.Contains(bishopSecondXRays[0], xRayUtility.AttackingPieces[whiteBishop].ToList());
            Assert.Contains(bishopSecondXRays[1], xRayUtility.AttackingPieces[whiteBishop].ToList());
            
            Assert.AreEqual(xRayUtility.AttackingPieces[blackRook].Count, rookXRays.Length);
            Assert.Contains(rookXRays[0], xRayUtility.AttackingPieces[blackRook].ToList());
            Assert.Contains(rookXRays[1], xRayUtility.AttackingPieces[blackRook].ToList());
            Assert.Contains(rookXRays[2], xRayUtility.AttackingPieces[blackRook].ToList());
        }
        

        [Test]
        public void TargetPieces_BishopAndRookMoved_Bishop_Rook_Bishop_FirsBishopXRaysDeleted()
        {
            // Arrange
            var xRayUtility = CreateXRayUtility(
                out var board,
                out var queenXRayUtility,
                out var rookXRayUtility,
                out var bishopXRayUtility);

            var whiteBishop = Substitute.For<IPiece>();
            whiteBishop.Type.Returns(PieceType.Bishop);
            whiteBishop.Color.Returns(PieceColor.White);
            whiteBishop.Position.Returns("f4");
            
            var blackRook = Substitute.For<IPiece>();
            blackRook.Type.Returns(PieceType.Rook);
            blackRook.Color.Returns(PieceColor.Black);
            blackRook.Position.Returns("c4");

            board.Pieces.Returns(new[] {whiteBishop, blackRook});

            var bishopFirstXRays = new IXRay[3];
            for (var i = 0; i < bishopFirstXRays.Length; i++)
            {
                bishopFirstXRays[i] = Substitute.For<IXRay>();
                bishopFirstXRays[i].AttackingPiece.Returns(whiteBishop);
            }
            
            var rookXRays = new IXRay[3];
            for (var i = 0; i < rookXRays.Length; i++)
            {
                rookXRays[i] = Substitute.For<IXRay>();
                rookXRays[i].AttackingPiece.Returns(blackRook);
            }
            
            var bishopSecondXRays = new IXRay[2];
            for (var i = 0; i < bishopSecondXRays.Length; i++)
            {
                bishopSecondXRays[i] = Substitute.For<IXRay>();
                bishopSecondXRays[i].AttackingPiece.Returns(whiteBishop);
            }

            bishopXRayUtility.GetXRays(whiteBishop).Returns(bishopFirstXRays);
            rookXRayUtility.GetXRays(blackRook).Returns(rookXRays);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackRook);
            bishopXRayUtility.Configure().GetXRays(whiteBishop).Returns(bishopSecondXRays);
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteBishop);

            // Assert
            Assert.AreEqual(bishopSecondXRays.Length + rookXRays.Length, xRayUtility.TargetPieces.Count);
            
            Assert.AreEqual(xRayUtility.TargetPieces[bishopSecondXRays[0].TargetPiece].First(), bishopSecondXRays[0]);
            Assert.AreEqual(xRayUtility.TargetPieces[bishopSecondXRays[1].TargetPiece].First(), bishopSecondXRays[1]);
            
            Assert.AreEqual(xRayUtility.TargetPieces[rookXRays[0].TargetPiece].First(), rookXRays[0]);
            Assert.AreEqual(xRayUtility.TargetPieces[rookXRays[1].TargetPiece].First(), rookXRays[1]);
            Assert.AreEqual(xRayUtility.TargetPieces[rookXRays[2].TargetPiece].First(), rookXRays[2]);
        }
        
    }
}