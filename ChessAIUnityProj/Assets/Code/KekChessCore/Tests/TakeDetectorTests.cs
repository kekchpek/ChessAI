using System;
using KekChessCore.Domain;
using KekChessCore.EnPassantUtility;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class TakeDetectorTests
    {
        private TakeDetector.TakeDetector CreateTakeDetector(
            out IBoard board,
            out IEnPassantUtility enPassantUtility)
        {
            board = Substitute.For<IBoard>();
            enPassantUtility = Substitute.For<IEnPassantUtility>();
            return new TakeDetector.TakeDetector(board, enPassantUtility);
        }

        [Test]
        public void FigureNotTaken_NoRemovedFigure()
        {
            // Assert
            var takeDetector = CreateTakeDetector(
                out var board,
                out var enPassantUtility);
            IPiece[] pieces = new IPiece[5];
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Substitute.For<IPiece>();
                pieces[i].Position.Returns((0,i));
            }
            board.Pieces.Returns(pieces);
            
            // Act
            pieces[2].Position.Returns((2,3));
            board.PositionChanged += Raise.Event<Action<IPiece>>(pieces[2]);
            
            // Assert
            board.DidNotReceive().RemovePiece(Arg.Any<IPiece>());
        }

        [Test]
        public void EnPassant_RemovedFigure()
        {
            // Assert
            var takeDetector = CreateTakeDetector(
                out var board,
                out var enPassantUtility);
            IPiece[] pieces = new IPiece[5];
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Substitute.For<IPiece>();
                pieces[i].Position.Returns((0,i));
            }
            board.Pieces.Returns(pieces);
            
            // Act
            var newPos = pieces[1].Position;
            pieces[2].Position.Returns(newPos);
            board.PositionChanged += Raise.Event<Action<IPiece>>(pieces[2]);
            
            // Assert
            board.Received().RemovePiece(pieces[1]);
        }

        [Test]
        public void FigureNotTaken_EnPassant_RemoveFigure()
        {
            // Assert
            var takeDetector = CreateTakeDetector(
                out var board,
                out var enPassantUtility);
            IPiece[] pieces = new IPiece[5];
            for (var i = 0; i < pieces.Length; i++)
            {
                pieces[i] = Substitute.For<IPiece>();
                pieces[i].Position.Returns((0,i));
            }
            board.Pieces.Returns(pieces);
            
            // Act
            pieces[2].Position.Returns((2,3));
            enPassantUtility.IsFigureWasTakenWithEnPassant(pieces[2], board, out _).Returns(x =>
            {
                x[2] = pieces[1];
                return true;
            });
            board.PositionChanged += Raise.Event<Action<IPiece>>(pieces[2]);
            
            // Assert
            board.Received().RemovePiece(pieces[1]);
        }
    }
}