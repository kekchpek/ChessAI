using System.Linq;
using ChessAI.Core.MoveUtility.PieceMoveUtilities.Bishop;
using ChessAI.Core.MoveUtility.PieceMoveUtilities.Rook;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.MoveUtility.PieceMoveUtilities.Queen
{
    public class QueenMoveUtility : IQueenMoveUtility
    {
        private readonly IBishopMoveUtility _bishopMoveUtility;
        private readonly IRookMoveUtility _rookMoveUtility;

        public QueenMoveUtility(IBishopMoveUtility bishopMoveUtility, IRookMoveUtility rookMoveUtility)
        {
            _bishopMoveUtility = bishopMoveUtility;
            _rookMoveUtility = rookMoveUtility;
        }

        public BoardCoordinates[] GetMoves(BoardCoordinates position, PieceColor color)
        {
            return _bishopMoveUtility.GetMoves(position, color)
                .Concat(_rookMoveUtility.GetMoves(position, color))
                .ToArray();
        }
    }
}