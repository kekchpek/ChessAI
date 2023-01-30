using System;
using KChess.Core.EnPassantUtility;
using KChess.Core.Taking;
using KChess.Domain;
using NSubstitute;
using NUnit.Framework;

namespace KChess.Tests
{
    public class TakeDetectorTests
    {
        private TakeUtility CreateTakeDetector(
            out IBoard board,
            out IEnPassantUtility enPassantUtility)
        {
            board = Substitute.For<IBoard>();
            enPassantUtility = Substitute.For<IEnPassantUtility>();
            return new TakeUtility(board, enPassantUtility);
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
            takeDetector.TryTake(pieces[2]);
            
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
            takeDetector.TryTake(pieces[2]);
            
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
            takeDetector.TryTake(pieces[2]);
            
            // Assert
            board.Received().RemovePiece(pieces[1]);
        }
    }
}