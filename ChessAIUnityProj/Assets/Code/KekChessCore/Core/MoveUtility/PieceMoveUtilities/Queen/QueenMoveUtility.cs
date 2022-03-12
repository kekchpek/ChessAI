using System.Linq;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.MoveUtility.PieceMoveUtilities.Bishop;
using KekChessCore.MoveUtility.PieceMoveUtilities.Rook;

namespace KekChessCore.MoveUtility.PieceMoveUtilities.Queen
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