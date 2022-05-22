using System;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.King;
using KChess.Core.MoveUtility.PieceMoveUtilities.Knight;
using KChess.Core.MoveUtility.PieceMoveUtilities.Pawn;
using KChess.Core.MoveUtility.PieceMoveUtilities.Queen;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.MoveUtility
{
    public class PieceMoveUtilityFacade : IPieceMoveUtilityFacade
    {
        private readonly IPawnMoveUtility _pawnMoveUtility;
        private readonly IRookMoveUtility _rookMoveUtility;
        private readonly IKnightMoveUtility _knightMoveUtility;
        private readonly IBishopMoveUtility _bishopXRayUtility;
        private readonly IQueenMoveUtility _queenMoveUtility;
        private readonly IKingMoveUtility _kingMoveUtility;

        public PieceMoveUtilityFacade(
            IPawnMoveUtility pawnMoveUtility,
            IRookMoveUtility rookMoveUtility,
            IKnightMoveUtility knightMoveUtility,
            IBishopMoveUtility bishopXRayUtility,
            IQueenMoveUtility queenMoveUtility,
            IKingMoveUtility kingMoveUtility)
        {
            _pawnMoveUtility = pawnMoveUtility;
            _rookMoveUtility = rookMoveUtility;
            _knightMoveUtility = knightMoveUtility;
            _bishopXRayUtility = bishopXRayUtility;
            _queenMoveUtility = queenMoveUtility;
            _kingMoveUtility = kingMoveUtility;
        }
        
        public BoardCoordinates[] GetAvailableMoves(IPiece piece)
        {
            return piece.Type switch
            {
                PieceType.Pawn => _pawnMoveUtility.GetMoves(piece.Position, piece.Color),
                PieceType.Rook => _rookMoveUtility.GetMoves(piece.Position, piece.Color),
                PieceType.Knight => _knightMoveUtility.GetMoves(piece.Position, piece.Color),
                PieceType.Bishop => _bishopXRayUtility.GetMoves(piece.Position, piece.Color),
                PieceType.Queen => _queenMoveUtility.GetMoves(piece.Position, piece.Color),
                PieceType.King => _kingMoveUtility.GetMoves(piece.Position, piece.Color),
                _ => Array.Empty<BoardCoordinates>()
            };
        }
    }
}