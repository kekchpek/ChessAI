using System;
using KChess.Core.AttackedCellsUtility;
using KChess.Core.CastleMoveUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace KChess.Tests
{
    public class CastleMoveUtilityTests
    {
        private CastleMoveUtility CreateCastleMoveUtility(
            out IBoard board,
            out IAttackedCellsUtility attackedCellsUtility)
        {
            board = Substitute.For<IBoard>();
            attackedCellsUtility = Substitute.For<IAttackedCellsUtility>();
            return new CastleMoveUtility(board, attackedCellsUtility);
        }

        [Test]
        public void GetCastleMoves_WhiteKingMoved_NoMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);

            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(true);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);

            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.IsEmpty(castleMoves);
        }

        [Test]
        public void GetCastleMoves_WhiteRightRookMoved_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(true);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c1", castleMoves);
        }

        [Test]
        public void GetCastleMoves_WhiteLeftRookMoved_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(true);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g1", castleMoves);
        }

        [TestCase("b1")]
        [TestCase("c1")]
        [TestCase("d1")]
        public void GetCastleMoves_WhiteLeftCellAttacked_OnlyRightMoves(string attackedCell)
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            attackedCellsUtility.Configure().IsCellAttacked(attackedCell, PieceColor.Black).Returns(true);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g1", castleMoves);
        }

        [TestCase("f1")]
        [TestCase("g1")]
        public void GetCastleMoves_WhiteRightCellAttacked_OnlyLeftMoves(string attackedCell)
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            attackedCellsUtility.Configure().IsCellAttacked(attackedCell, PieceColor.Black).Returns(true);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c1", castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_WhiteRightRookPlacedOnOtherPosition_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("c5");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c1", castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_WhiteLeftRookPlacedOnOtherPosition_OnlyRightMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a6");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g1", castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_PieceBlocksRight_OnlyRightMoves(
            [Values(PieceColor.Black, PieceColor.White)] PieceColor castleColor,
            [Values(5, 6)] int blockingVertical,
            [Values(PieceColor.Black, PieceColor.White)] PieceColor blockingPieceColor
        )
        {
            // Arrange
            var horizontal = castleColor == PieceColor.Black ? 7 : 0;
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var castleKing = Substitute.For<IPiece>();
            castleKing.Color.Returns(castleColor);
            castleKing.Type.Returns(PieceType.King);
            castleKing.Position.Returns((4, horizontal));
            castleKing.IsMoved.Returns(false);
            
            var castleRightRook = Substitute.For<IPiece>();
            castleRightRook.Color.Returns(castleColor);
            castleRightRook.Type.Returns(PieceType.Rook);
            castleRightRook.Position.Returns((7, horizontal));
            castleRightRook.IsMoved.Returns(false);
            
            var castleLeftRook = Substitute.For<IPiece>();
            castleLeftRook.Color.Returns(castleColor);
            castleLeftRook.Type.Returns(PieceType.Rook);
            castleLeftRook.Position.Returns((0, horizontal));
            castleLeftRook.IsMoved.Returns(false);

            var blockingPiece = Substitute.For<IPiece>();
            blockingPiece.Color.Returns(blockingPieceColor);
            blockingPiece.Position.Returns((blockingVertical, horizontal));
            blockingPiece.Type.Returns(PieceType.Bishop);
            
            board.Pieces.Returns(new[] {castleKing, castleLeftRook, castleRightRook, blockingPiece});
            board.PositionChanged += Raise.Event<Action<IPiece>>(castleKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(castleColor);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)(2, horizontal), castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_PieceBlocksLeft_OnlyRightMoves(
            [Values(1, 2, 3)] int blockingVertical,
            [Values(PieceColor.Black, PieceColor.White)] PieceColor blockingPieceColor,
            [Values(PieceColor.White, PieceColor.Black)] PieceColor castleColor
        )
        {
            // Arrange
            var horizontal = castleColor == PieceColor.Black ? 7 : 0;
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var castleKing = Substitute.For<IPiece>();
            castleKing.Color.Returns(castleColor);
            castleKing.Type.Returns(PieceType.King);
            castleKing.Position.Returns((4, horizontal));
            castleKing.IsMoved.Returns(false);
            
            var castleRightRook = Substitute.For<IPiece>();
            castleRightRook.Color.Returns(castleColor);
            castleRightRook.Type.Returns(PieceType.Rook);
            castleRightRook.Position.Returns((7, horizontal));
            castleRightRook.IsMoved.Returns(false);
            
            var castleLeftRook = Substitute.For<IPiece>();
            castleLeftRook.Color.Returns(castleColor);
            castleLeftRook.Type.Returns(PieceType.Rook);
            castleLeftRook.Position.Returns((0, horizontal));
            castleLeftRook.IsMoved.Returns(false);

            var blockingPiece = Substitute.For<IPiece>();
            blockingPiece.Color.Returns(blockingPieceColor);
            blockingPiece.Position.Returns((blockingVertical, horizontal));
            blockingPiece.Type.Returns(PieceType.Bishop);
            
            board.Pieces.Returns(new[] {castleKing, castleLeftRook, castleRightRook, blockingPiece});
            board.PositionChanged += Raise.Event<Action<IPiece>>(castleKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(castleColor);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)(6, horizontal), castleMoves);
        }

        [Test]
        public void GetCastleMoves_WhiteAllGood_BothMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.Black).ReturnsForAnyArgs(false);
            
            var whiteKing = Substitute.For<IPiece>();
            whiteKing.Color.Returns(PieceColor.White);
            whiteKing.Type.Returns(PieceType.King);
            whiteKing.Position.Returns("e1");
            whiteKing.IsMoved.Returns(false);
            
            var whiteRightRook = Substitute.For<IPiece>();
            whiteRightRook.Color.Returns(PieceColor.White);
            whiteRightRook.Type.Returns(PieceType.Rook);
            whiteRightRook.Position.Returns("h1");
            whiteRightRook.IsMoved.Returns(false);
            
            var whiteLeftRook = Substitute.For<IPiece>();
            whiteLeftRook.Color.Returns(PieceColor.White);
            whiteLeftRook.Type.Returns(PieceType.Rook);
            whiteLeftRook.Position.Returns("a1");
            whiteLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {whiteKing, whiteLeftRook, whiteRightRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(whiteKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.White);
            
            // Assert
            Assert.AreEqual(2, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c1", castleMoves);
            Assert.Contains((BoardCoordinates)"g1", castleMoves);
        }

        [Test]
        public void GetCastleMoves_BlackKingMoved_NoMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);

            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(true);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.IsEmpty(castleMoves);
        }

        [Test]
        public void GetCastleMoves_BlackRightRookMoved_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(true);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c8", castleMoves);
        }

        [Test]
        public void GetCastleMoves_BlackLeftRookMoved_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(true);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g8", castleMoves);
        }

        [TestCase("b8")]
        [TestCase("c8")]
        [TestCase("d8")]
        public void GetCastleMoves_BlackLeftCellAttacked_OnlyRightMoves(string attackedCell)
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            attackedCellsUtility.Configure().IsCellAttacked(attackedCell, PieceColor.White).Returns(true);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g8", castleMoves);
        }

        [TestCase("f8")]
        [TestCase("g8")]
        public void GetCastleMoves_BlackRightCellAttacked_OnlyLeftMoves(string attackedCell)
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            attackedCellsUtility.Configure().IsCellAttacked(attackedCell, PieceColor.White).Returns(true);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c8", castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_BlackRightRookPlacedOnOtherPosition_OnlyLeftMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("c5");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c8", castleMoves);
        }
        
        [Test]
        public void GetCastleMoves_BlackLeftRookPlacedOnOtherPosition_OnlyRightMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a6");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(1, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"g8", castleMoves);
        }

        [Test]
        public void GetCastleMoves_BlackAllGood_BothMoves()
        {
            // Arrange
            var castleMoveUtility = CreateCastleMoveUtility(
                out var board,
                out var attackedCellsUtility);
            
            attackedCellsUtility.IsCellAttacked(default, PieceColor.White).ReturnsForAnyArgs(false);
            
            var blackKing = Substitute.For<IPiece>();
            blackKing.Color.Returns(PieceColor.Black);
            blackKing.Type.Returns(PieceType.King);
            blackKing.Position.Returns("e8");
            blackKing.IsMoved.Returns(false);
            
            var blackRightRook = Substitute.For<IPiece>();
            blackRightRook.Color.Returns(PieceColor.Black);
            blackRightRook.Type.Returns(PieceType.Rook);
            blackRightRook.Position.Returns("h8");
            blackRightRook.IsMoved.Returns(false);
            
            var blackLeftRook = Substitute.For<IPiece>();
            blackLeftRook.Color.Returns(PieceColor.Black);
            blackLeftRook.Type.Returns(PieceType.Rook);
            blackLeftRook.Position.Returns("a8");
            blackLeftRook.IsMoved.Returns(false);
            
            board.Pieces.Returns(new[] {blackKing, blackRightRook, blackLeftRook});
            board.PositionChanged += Raise.Event<Action<IPiece>>(blackKing);

            // Act
            var castleMoves = castleMoveUtility.GetCastleMoves(PieceColor.Black);
            
            // Assert
            Assert.AreEqual(2, castleMoves.Length);
            Assert.Contains((BoardCoordinates)"c8", castleMoves);
            Assert.Contains((BoardCoordinates)"g8", castleMoves);
        }
    }
}