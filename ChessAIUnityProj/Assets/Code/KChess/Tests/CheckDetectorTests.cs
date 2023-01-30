using KChess.Core.BoardStateUtils;
using KChess.Core.CheckMate;
using KChess.Core.CheckUtility;
using KChess.Core.MateUtility;
using KChess.Core.PawnTransformation;
using KChess.Domain;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class CheckDetectorTests
    {
        private CheckMateUtility CreateCheckDetector(
            out IBoardStateSetter boardStateSetter,
            out ICheckUtility checkUtility)
        {
            boardStateSetter = Substitute.For<IBoardStateSetter>();
            checkUtility = Substitute.For<ICheckUtility>();
            var mateUtility = Substitute.For<IMateUtility>();
            var pawnTransformation = Substitute.For<IPawnTransformationUtility>();
            pawnTransformation.GetTransformingPiece().Returns((IPiece)null);
            return new CheckMateUtility(boardStateSetter, checkUtility, mateUtility, pawnTransformation);
        }
        
        [Test]
        public void CheckToWhite_BoardStateSet()
        {
            // Arrange 
            var checkMateUtility = CreateCheckDetector(
                out var boardStateSetter,
                out var checkUtility);
            checkUtility.IsPositionWithCheck(out Arg.Any<PieceColor>()).Returns(x =>
            {
                x[0] = PieceColor.White;
                return true;
            });

            // Act
            checkMateUtility.UpdateBoardState();

            // Assert
            boardStateSetter.Received().Set(BoardState.CheckToWhite);
        }
        
        [Test]
        public void CheckToBlack_BoardStateSet()
        {
            // Arrange 
            var checkMateUtility = CreateCheckDetector(
                out var boardStateSetter,
                out var checkUtility);
            checkUtility.IsPositionWithCheck(out Arg.Any<PieceColor>()).Returns(x =>
            {
                x[0] = PieceColor.Black;
                return true;
            });

            // Act
            checkMateUtility.UpdateBoardState();

            // Assert
            boardStateSetter.Received().Set(BoardState.CheckToBlack);
        }
    }
}