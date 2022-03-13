using System;
using System.Collections.Generic;
using System.Linq;
using KekChessCore.AttackedCellsUtility;
using KekChessCore.BoardEnvironment;
using KekChessCore.BoardStateUtils;
using KekChessCore.CheckBlockingUtility;
using KekChessCore.CheckUtility;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;
using KekChessCore.XRayUtility;

namespace KekChessCore.MoveUtility
{
    public class MoveUtility : IMoveUtility, IBoardEnvironmentComponent
    {
        private readonly IPieceMoveUtilityFacade _pieceMoveUtilityFacade;
        private readonly IXRayUtility _xRayUtility;
        private readonly IAttackedCellsUtility _attackedCellsUtility;
        private readonly IBoardStateGetter _boardStateGetter;
        private readonly ICheckBlockingUtility _checkBlockingUtility;
        private readonly ICheckUtility _checkUtility;

        public MoveUtility(
            IPieceMoveUtilityFacade pieceMoveUtilityFacade,
            IXRayUtility xRayUtility,
            IAttackedCellsUtility attackedCellsUtility,
            IBoardStateGetter boardStateGetter,
            ICheckBlockingUtility checkBlockingUtility,
            ICheckUtility checkUtility)
        {
            _pieceMoveUtilityFacade = pieceMoveUtilityFacade;
            _xRayUtility = xRayUtility;
            _attackedCellsUtility = attackedCellsUtility;
            _boardStateGetter = boardStateGetter;
            _checkBlockingUtility = checkBlockingUtility;
            _checkUtility = checkUtility;
        }
        
        public BoardCoordinates[] GetAvailableMoves(IPiece piece)
        {
            var oppositeColor = piece.Color switch
            {
                PieceColor.Black => PieceColor.White,
                PieceColor.White => PieceColor.Black,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            // king
            if (piece.Type == PieceType.King)
            {
                return _pieceMoveUtilityFacade.GetAvailableMoves(piece)
                    .Where(x => !_attackedCellsUtility.IsCellAttacked(x, oppositeColor))
                    .ToArray();
            }

            // not check
            var isCheck =
                piece.Color == PieceColor.Black && _boardStateGetter.Get() == BoardState.CheckToBlack ||
                piece.Color == PieceColor.White && _boardStateGetter.Get() == BoardState.CheckToWhite;
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
                        availableLinkMoves.Add(linkXRay.AttackingPiece.Position);
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
            availableMovesOnCheck.Add(checkingPieces[0].Position);
            return defaultMoves.Intersect(availableMovesOnCheck).ToArray();
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}