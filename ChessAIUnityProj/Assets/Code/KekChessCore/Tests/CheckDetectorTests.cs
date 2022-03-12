using System;
using KekChessCore.BoardStateUtils;
using KekChessCore.CheckUtility;
using KekChessCore.Domain;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class CheckDetectorTests
    {
        private CheckDetector.CheckDetector CreateCheckDetector(
            out IBoard board,
            out IBoardStateSetter boardStateSetter,
            out ICheckUtility checkUtility)
        {
            board = Substitute.For<IBoard>();
            boardStateSetter = Substitute.For<IBoardStateSetter>();
            checkUtility = Substitute.For<ICheckUtility>();
            return new CheckDetector.CheckDetector(board, boardStateSetter, checkUtility);
        }

        [Test]
        public void NoCheck_BoardStateNotSet()
        {
            // Arrange 
            var checkDecorator = CreateCheckDetector(
                out var board,
                out var boardStateSetter,
                out var checkUtility);
            checkUtility.IsPositionWithCheck(out Arg.Any<PieceColor>()).Returns(false);

            // Act
            board.PositionChanged += Raise.Event<Action<IPiece>>(Substitute.For<IPiece>());

            // Assert
            boardStateSetter.DidNotReceiveWithAnyArgs().Set(default);
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