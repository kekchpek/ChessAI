using System.Linq;
using KekChessCore.BoardEnvironment;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.CastleDetector
{
    public class CastleDetector : ICastleDetector, IBoardEnvironmentComponent
    {

        private static readonly BoardCoordinates WhiteKingStartPos = "e1";
        
        private static readonly BoardCoordinates WhiteKingLeftCastlePos = "c1";
        private static readonly BoardCoordinates LeftWhiteRookStartPos = "a1";
        private static readonly BoardCoordinates LeftWhiteRookCastlePos = "d1";

        private static readonly BoardCoordinates WhiteKingRightCastlePos = "g1";
        private static readonly BoardCoordinates RightWhiteRookStartPos = "h1";
        private static readonly BoardCoordinates RightWhiteRookCastlePos = "f1";
        
        
        private static readonly BoardCoordinates BlackKingStartPos = "e8";
        
        private static readonly BoardCoordinates BlackKingLeftCastlePos = "c8";
        private static readonly BoardCoordinates LeftBlackRookStartPos = "a8";
        private static readonly BoardCoordinates LeftBlackRookCastlePos = "d8";

        private static readonly BoardCoordinates BlackKingRightCastlePos = "g8";
        private static readonly BoardCoordinates RightBlackRookStartPos = "h8";
        private static readonly BoardCoordinates RightBlackRookCastlePos = "f8";
        
        private readonly IBoard _board;

        public CastleDetector(IBoard board)
        {
            _board = board;
            board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece piece)
        {
            if (piece.Type != PieceType.King)
            {
                return;
            }

            if (piece.PreviousPosition == WhiteKingStartPos && piece.Color == PieceColor.White)
            {
                if (piece.Position == WhiteKingLeftCastlePos)
                {
                    var rook = _board.Pieces.FirstOrDefault(x => x.Position == LeftWhiteRookStartPos);
                    rook!.MoveTo(LeftWhiteRookCastlePos);
                }
                if (piece.Position.Equals(WhiteKingRightCastlePos))
                {
                    var rook = _board.Pieces.FirstOrDefault(x => x.Position == RightWhiteRookStartPos);
                    rook!.MoveTo(RightWhiteRookCastlePos);
                }
            }

            if (piece.PreviousPosition == BlackKingStartPos && piece.Color == PieceColor.Black)
            {
                if (piece.Position == BlackKingLeftCastlePos)
                {
                    var rook = _board.Pieces.FirstOrDefault(x => x.Position == LeftBlackRookStartPos);
                    rook!.MoveTo(LeftBlackRookCastlePos);
                }
                if (piece.Position.Equals(BlackKingRightCastlePos))
                {
                    var rook = _board.Pieces.FirstOrDefault(x => x.Position == RightBlackRookStartPos);
                    rook!.MoveTo(RightBlackRookCastlePos);
                }
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}