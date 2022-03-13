using KekChessCore.Domain;
using KekChessCore.Factories;
using NSubstitute;
using NUnit.Framework;

namespace KekChessCore.Tests
{
    public class BoardFactoryTests
    {

        private BoardFactory CreateBoardFactory(
            out IPieceFactory pieceFactory)
        {
            pieceFactory = Substitute.For<IPieceFactory>();
            return new BoardFactory(pieceFactory);
        }

        [Test]
        public void CreateStandardBoard_PiecesSetCorrectly()
        {
            // Arrange
            var boardFactory = CreateBoardFactory(
                out var pieceFactory);
            
            // Act
            var board = boardFactory.CreateStandardBoard();
            
            // Assert
            
            // White
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "a2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "b2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "c2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "d2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "e2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "f2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "g2", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.White, "h2", board);
            
            pieceFactory.Received().Create(PieceType.Rook, PieceColor.White, "a1", board);
            pieceFactory.Received().Create(PieceType.Knight, PieceColor.White, "b1", board);
            pieceFactory.Received().Create(PieceType.Bishop, PieceColor.White, "c1", board);
            pieceFactory.Received().Create(PieceType.Queen, PieceColor.White, "d1", board);
            pieceFactory.Received().Create(PieceType.King, PieceColor.White, "e1", board);
            pieceFactory.Received().Create(PieceType.Bishop, PieceColor.White, "f1", board);
            pieceFactory.Received().Create(PieceType.Knight, PieceColor.White, "g1", board);
            pieceFactory.Received().Create(PieceType.Rook, PieceColor.White, "h1", board);
            
            // Black
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "a7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "b7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "c7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "d7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "e7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "f7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "g7", board);
            pieceFactory.Received().Create(PieceType.Pawn, PieceColor.Black, "h7", board);
            
            pieceFactory.Received().Create(PieceType.Rook, PieceColor.Black, "a8", board);
            pieceFactory.Received().Create(PieceType.Knight, PieceColor.Black, "b8", board);
            pieceFactory.Received().Create(PieceType.Bishop, PieceColor.Black, "c8", board);
            pieceFactory.Received().Create(PieceType.Queen, PieceColor.Black, "d8", board);
            pieceFactory.Received().Create(PieceType.King, PieceColor.Black, "e8", board);
            pieceFactory.Received().Create(PieceType.Bishop, PieceColor.Black, "f8", board);
            pieceFactory.Received().Create(PieceType.Knight, PieceColor.Black, "g8", board);
            pieceFactory.Received().Create(PieceType.Rook, PieceColor.Black, "h8", board);
        }
        

        [Test]
        public void CopyBoard_PiecesSetCorrectly()
        {
            // Arrange
            var boardFactory = CreateBoardFactory(
                out var pieceFactory);

            var board = Substitute.For<IBoard>();
            var piecesCount = 14;
            var pieces = new IPiece[piecesCount];
            for (var i = 0; i < piecesCount; i++)
            {
                pieces[i] = Substitute.For<IPiece>();
            }

            board.Pieces.Returns(pieces);
            
            // Act
            var copyBoard = boardFactory.Copy(board);
            
            // Assert
            for (var i = 0; i < piecesCount; i++)
            {
                pieceFactory.Received().Copy(pieces[i], copyBoard);
            }
        }
        
    }
}