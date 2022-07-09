using System;
using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class PawnTransformationDetectorTests
    {

        [Test]
        public void PositionChanged_NotPawnsReachesBorderEdge_TransformationPieceNotSet(
            [Range(0,7)] int movingVertical,
            [Values(0, 7)] int movingHorizontal,
            [Values(PieceType.Bishop, PieceType.King, PieceType.Knight, PieceType.Queen, PieceType.Rook)]
            PieceType pieceType,
            [Values(PieceColor.Black, PieceColor.White)] PieceColor color)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnTransformationDetector>(out var detector);
            var board = container.Resolve<IBoard>();
            var pawnTransformationUtilityWrite = container.Resolve<IPawnTransformationUtilityWrite>();

            var movedPiece = Substitute.For<IPiece>();
            movedPiece.Position.Returns((movingVertical, movingHorizontal));
            movedPiece.Color.Returns(color);
            movedPiece.Type.Returns(pieceType);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(movedPiece);

            // Assert
            pawnTransformationUtilityWrite.DidNotReceiveWithAnyArgs().SetTransformingPiece(default);
        }
        

        [Test]
        public void PositionChanged_BlackPawnReachEdge_TransformationPieceNotSet(
            [Range(0,7)] int movingVertical)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnTransformationDetector>(out var detector);
            var board = container.Resolve<IBoard>();
            var pawnTransformationUtilityWrite = container.Resolve<IPawnTransformationUtilityWrite>();

            var movedPiece = Substitute.For<IPiece>();
            movedPiece.Position.Returns((movingVertical, 0));
            movedPiece.Color.Returns(PieceColor.Black);
            movedPiece.Type.Returns(PieceType.Pawn);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(movedPiece);

            // Assert
            pawnTransformationUtilityWrite.Received().SetTransformingPiece(movedPiece);
        }
        
        [Test]
        public void PositionChanged_WhitePawnReachEdge_TransformationPieceNotSet(
            [Range(0,7)] int movingVertical)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnTransformationDetector>(out var detector);
            var board = container.Resolve<IBoard>();
            var pawnTransformationUtilityWrite = container.Resolve<IPawnTransformationUtilityWrite>();

            var movedPiece = Substitute.For<IPiece>();
            movedPiece.Position.Returns((movingVertical, 7));
            movedPiece.Color.Returns(PieceColor.White);
            movedPiece.Type.Returns(PieceType.Pawn);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(movedPiece);

            // Assert
            pawnTransformationUtilityWrite.Received().SetTransformingPiece(movedPiece);
        }
        
    }
}