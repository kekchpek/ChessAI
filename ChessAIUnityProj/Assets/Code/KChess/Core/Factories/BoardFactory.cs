using KChess.Domain;
using KChess.Domain.Extensions;
using KChess.Domain.Impl;

namespace KChess.Core.Factories
{
    internal class BoardFactory : IBoardFactory
    {
        private readonly IPieceFactory _pieceFactory;

        public BoardFactory(IPieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        private Board CreateBoard()
        {
            var board = new Board();
            board.GetPiecePositionsMap(); // to start indexing
            return board;
        }
        
        public IBoard CreateStandardBoard()
        {
            var board = CreateBoard();
            
            // White
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "a2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "b2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "c2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "d2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "e2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "f2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "g2", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.White, "h2", board);
            
            _pieceFactory.Create(PieceType.Rook, PieceColor.White, "a1", board);
            _pieceFactory.Create(PieceType.Knight, PieceColor.White, "b1", board);
            _pieceFactory.Create(PieceType.Bishop, PieceColor.White, "c1", board);
            _pieceFactory.Create(PieceType.Queen, PieceColor.White, "d1", board);
            _pieceFactory.Create(PieceType.King, PieceColor.White, "e1", board);
            _pieceFactory.Create(PieceType.Bishop, PieceColor.White, "f1", board);
            _pieceFactory.Create(PieceType.Knight, PieceColor.White, "g1", board);
            _pieceFactory.Create(PieceType.Rook, PieceColor.White, "h1", board);
            
            // Black
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "a7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "b7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "c7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "d7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "e7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "f7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "g7", board);
            _pieceFactory.Create(PieceType.Pawn, PieceColor.Black, "h7", board);
            
            _pieceFactory.Create(PieceType.Rook,   PieceColor.Black, "a8", board);
            _pieceFactory.Create(PieceType.Knight, PieceColor.Black, "b8", board);
            _pieceFactory.Create(PieceType.Bishop, PieceColor.Black, "c8", board);
            _pieceFactory.Create(PieceType.Queen,  PieceColor.Black, "d8", board);
            _pieceFactory.Create(PieceType.King,   PieceColor.Black, "e8", board);
            _pieceFactory.Create(PieceType.Bishop, PieceColor.Black, "f8", board);
            _pieceFactory.Create(PieceType.Knight, PieceColor.Black, "g8", board);
            _pieceFactory.Create(PieceType.Rook,   PieceColor.Black, "h8", board);

            return board;
        }

        public IBoard Copy(IBoard sourceBoard)
        {
            var board = CreateBoard();

            foreach (var piece in sourceBoard.Pieces)
            {
                _pieceFactory.Copy(piece, board);
            }

            return board;
        }
    }
}