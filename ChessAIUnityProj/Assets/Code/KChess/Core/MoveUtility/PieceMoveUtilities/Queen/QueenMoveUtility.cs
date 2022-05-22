using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Queen
{
    public class QueenMoveUtility : IQueenMoveUtility, IBoardEnvironmentComponent
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

        public void Dispose()
        {
            // do nothing
        }
    }
}