using System.Linq;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility.PieceMoveUtilities.Queen
{
    internal class QueenMoveUtility : IQueenMoveUtility
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

        public BoardCoordinates[] GetAttackedCells(BoardCoordinates position, PieceColor pieceColor)
        {
            return _bishopMoveUtility.GetAttackedCells(position, pieceColor)
                .Concat(_rookMoveUtility.GetAttackedCells(position, pieceColor))
                .ToArray();
        }
    }
}