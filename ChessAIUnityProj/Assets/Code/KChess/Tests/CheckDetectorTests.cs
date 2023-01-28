using System;
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
        private CheckMateDetector CreateCheckDetector(
            out IBoard board,
            out IBoardStateSetter boardStateSetter,
            out ICheckUtility checkUtility)
        {
            board = Substitute.For<IBoard>();
            boardStateSetter = Substitute.For<IBoardStateSetter>();
            checkUtility = Substitute.For<ICheckUtility>();
            var mateUtility = Substitute.For<IMateUtility>();
            var pawnTransformation = Substitute.For<IPawnTransformationUtility>();
            pawnTransformation.GetTransformingPiece().Returns((IPiece)null);
            return new CheckMateDetector(board, boardStateSetter, checkUtility, mateUtility, pawnTransformation);
        }
        
        [Test]
        public void CheckToWhite_BoardStateSet()
        {
            // Arrange 
            var checkDecorator = CreateCheckDetector(
                out var board,
                out var boardStateSetter,
                out var checkUtility);
            checkUtility.IsPositionWithCheck(out Arg.Any<PieceColor>()).Returns(x =>
            {
                x[0] = PieceColor.White;
                return true;
            });

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(Substitute.For<IPiece>());

            // Assert
            boardStateSetter.Received().Set(BoardState.CheckToWhite);
        }
        
        [Test]
        public void CheckToBlack_BoardStateSet()
        {
            // Arrange 
            var checkDecorator = CreateCheckDetector(
                out var board,
                out var boardStateSetter,
                out var checkUtility);
            checkUtility.IsPositionWithCheck(out Arg.Any<PieceColor>()).Returns(x =>
            {
                x[0] = PieceColor.Black;
                return true;
            });

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(Substitute.For<IPiece>());

            // Assert
            boardStateSetter.Received().Set(BoardState.CheckToBlack);
        }
    }
}