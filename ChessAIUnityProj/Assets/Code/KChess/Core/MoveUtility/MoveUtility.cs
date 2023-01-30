using System;
using System.Collections.Generic;
using System.Linq;
using KChess.Core.AttackedCellsUtility;
using KChess.Core.BoardStateUtils;
using KChess.Core.CastleMoveUtility;
using KChess.Core.CheckBlockingUtility;
using KChess.Core.CheckUtility;
using KChess.Core.PawnTransformation;
using KChess.Core.XRayUtility;
using KChess.Domain;
using KChess.Domain.Impl;
using UnityEngine.Assertions;

namespace KChess.Core.MoveUtility
{
    internal class MoveUtility : IMoveUtility
    {
        private readonly IPieceMoveUtilityFacade _pieceMoveUtilityFacade;
        private readonly IXRayUtility _xRayUtility;
        private readonly IAttackedCellsUtility _attackedCellsUtility;
        private readonly IBoardStateGetter _boardStateGetter;
        private readonly ICheckBlockingUtility _checkBlockingUtility;
        private readonly ICheckUtility _checkUtility;
        private readonly ICastleMoveUtility _castleMoveUtility;
        private readonly IPawnTransformationUtility _pawnTransformationUtility;

        public MoveUtility(
            IPieceMoveUtilityFacade pieceMoveUtilityFacade,
            IXRayUtility xRayUtility,
            IAttackedCellsUtility attackedCellsUtility,
            IBoardStateGetter boardStateGetter,
            ICheckBlockingUtility checkBlockingUtility,
            ICheckUtility checkUtility,
            ICastleMoveUtility castleMoveUtility,
            IPawnTransformationUtility pawnTransformationUtility)
        {
            _pieceMoveUtilityFacade = pieceMoveUtilityFacade;
            _xRayUtility = xRayUtility;
            _attackedCellsUtility = attackedCellsUtility;
            _boardStateGetter = boardStateGetter;
            _checkBlockingUtility = checkBlockingUtility;
            _checkUtility = checkUtility;
            _castleMoveUtility = castleMoveUtility;
            _pawnTransformationUtility = pawnTransformationUtility;
        }
        
        public BoardCoordinates[] GetAvailableMoves(IPiece piece)
        {
            if (_pawnTransformationUtility.GetTransformingPiece() != null)
                return Array.Empty<BoardCoordinates>();
            
            var oppositeColor = piece.Color switch
            {
                PieceColor.Black => PieceColor.White,
                PieceColor.White => PieceColor.Black,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            var isCheck =
                piece.Color == PieceColor.Black && _boardStateGetter.Get() == BoardState.CheckToBlack ||
                piece.Color == PieceColor.White && _boardStateGetter.Get() == BoardState.CheckToWhite;
            
            // king
            if (piece.Type == PieceType.King)
            {
                var moves = _pieceMoveUtilityFacade.GetAvailableMoves(piece)
                    // cell is not attacked
                    .Where(x => !_attackedCellsUtility.IsCellAttacked(x, oppositeColor))
                    // cell is not placed behind attacked king
                    .Where(x => !_xRayUtility.TargetPieces.ContainsKey(piece) ||
                                             _xRayUtility.TargetPieces[piece].All(xRay => !(xRay.BlockingPieces.Count == 0 &&
                                                                             xRay.CellsBehind.Contains(x))))
                    .ToArray();
                if (!isCheck)
                {
                    moves = moves.Concat(_castleMoveUtility.GetCastleMoves(piece.Color)).ToArray();
                }

                return moves;
            }

            // not check
            var defaultMoves = _pieceMoveUtilityFacade.GetAvailableMoves(piece);
            if (!isCheck)
            {
                var kingXRays = _xRayUtility.TargetPieces
                    .Where(x => x.Key.Color == piece.Color && x.Key.Type == PieceType.King)
                    .ToArray();
                if (kingXRays.Length > 0)
                {
                    var linkXRay = kingXRays.First().Value
                        .FirstOrDefault(x => x.BlockingPieces.Count == 1 && x.BlockingPieces[0] == piece);
                    if (linkXRay != null)
                    {
                        var availableLinkMoves = new List<BoardCoordinates>(linkXRay.CellsBetween); 
                        Assert.IsTrue(linkXRay.AttackingPiece.Position.HasValue);
                        availableLinkMoves.Add(linkXRay.AttackingPiece.Position.Value);
                        var moves = defaultMoves.Intersect(availableLinkMoves);
                        return moves.ToArray();
                    }
                }

                return defaultMoves;
            }

            // check
            var checkingPieces = _checkUtility.GetCheckingPieces();
            if (checkingPieces.Count > 1)
                return Array.Empty<BoardCoordinates>();
            var checkBlockingMoves = _checkBlockingUtility.GetMovesForCheckBlocking(piece.Color);
            var availableMovesOnCheck = new List<BoardCoordinates>(checkBlockingMoves);
            Assert.IsTrue(checkingPieces[0].Position.HasValue);
            availableMovesOnCheck.Add(checkingPieces[0].Position.Value);
            return defaultMoves.Intersect(availableMovesOnCheck).ToArray();
        }

        public BoardCoordinates[] GetAttackedMoves(IPiece piece)
        {
            return _pieceMoveUtilityFacade.GetAttackedCells(piece);
        }
    }
}