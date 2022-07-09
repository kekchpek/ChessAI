using System;
using KChess.Core.Factories;
using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChessUnity.Tests.Helper;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class PawnTransformationUtilityTests
    {
        [Test]
        public void SetTransformingPawn_EventCalled()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);
            var testingPiece = Substitute.For<IPiece>();
            IPiece transformingPiece = null;
            pawnTransformationUtility.TransformationBecomesRequired += x => transformingPiece = x;

            // Act
            pawnTransformationUtility.SetTransformingPiece(testingPiece);

            // Assert
            Assert.AreEqual(testingPiece, transformingPiece);
        }

        [Test]
        public void GetTransformingPiece_PieceSet_ReturnsSetPiece()
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);
            var testingPiece = Substitute.For<IPiece>();
            testingPiece.Position.Returns((3, 6));

            // Act
            pawnTransformationUtility.SetTransformingPiece(testingPiece);

            // Assert
            Assert.AreEqual(testingPiece, pawnTransformationUtility.GetTransformingPiece());
        }

        [TestCase(PawnTransformationVariant.Bishop)]
        [TestCase(PawnTransformationVariant.Knight)]
        [TestCase(PawnTransformationVariant.Queen)]
        [TestCase(PawnTransformationVariant.Rook)]
        public void TryTransformPiece_NoPiece_ReturnsFalse(PawnTransformationVariant variant)
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);

            // Act
            var transformResult = pawnTransformationUtility.TryTransform(variant);

            // Assert
            Assert.IsFalse(transformResult);
        }

        [TestCase(PawnTransformationVariant.Bishop)]
        [TestCase(PawnTransformationVariant.Knight)]
        [TestCase(PawnTransformationVariant.Queen)]
        [TestCase(PawnTransformationVariant.Rook)]
        public void TryTransformPiece_PieceSet_ReturnsTrue(PawnTransformationVariant variant)
        {
            // Arrange
            TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);
            var testingPiece = Substitute.For<IPiece>();
            
            testingPiece.Position.Returns((3, 6));

            // Act
            pawnTransformationUtility.SetTransformingPiece(testingPiece);
            var transformResult = pawnTransformationUtility.TryTransform(variant);

            // Assert
            Assert.IsTrue(transformResult);
        }

        [TestCase(PawnTransformationVariant.Bishop)]
        [TestCase(PawnTransformationVariant.Knight)]
        [TestCase(PawnTransformationVariant.Queen)]
        [TestCase(PawnTransformationVariant.Rook)]
        public void TryTransformPiece_PieceSet_PieceRemoved(PawnTransformationVariant variant)
        {
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);
            var board = container.Resolve<IBoard>();
            
            var testingPiece = Substitute.For<IPiece>();
            testingPiece.Position.Returns((3, 6));

            // Act
            pawnTransformationUtility.SetTransformingPiece(testingPiece);
            pawnTransformationUtility.TryTransform(variant);

            // Assert
            board.Received().RemovePiece(testingPiece);
        }

        [Test]
        public void TryTransformPiece_PieceSet_NewPieceCreatedWithCorrectParams(
            [Values(PawnTransformationVariant.Bishop,
                PawnTransformationVariant.Knight,
                PawnTransformationVariant.Queen,
                PawnTransformationVariant.Rook)] PawnTransformationVariant variant,
            [Values(PieceColor.Black, PieceColor.White)] PieceColor color,
            [Range(0, 7)] int positionX,
            [Range(0, 7)] int positionY)
        {
            // ReSharper disable PossibleInvalidOperationException
            // Arrange
            var container = TestHelper.CreateContainerFor<PawnTransformationUtility>(out var pawnTransformationUtility);
            var pieceFactory = container.Resolve<IPieceFactory>();
            var board = container.Resolve<IBoard>();

            var testingPiece = Substitute.For<IPiece>();
            testingPiece.Color.Returns(color);
            testingPiece.Position.Returns((positionX, positionY));

            // Act
            pawnTransformationUtility.SetTransformingPiece(testingPiece);
            pawnTransformationUtility.TryTransform(variant);

            // Assert
            pieceFactory.Received()
                .Create(MapVariantToPieceType(variant), color, (positionX, positionY), board);
            // ReSharper restore PossibleInvalidOperationException
        }
        
        private static PieceType MapVariantToPieceType(PawnTransformationVariant variant)
        {
            return variant switch
            {
                PawnTransformationVariant.Knight => PieceType.Knight,
                PawnTransformationVariant.Bishop => PieceType.Bishop,
                PawnTransformationVariant.Rook => PieceType.Rook,
                PawnTransformationVariant.Queen => PieceType.Queen,
                _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
            };
        }
    }
}