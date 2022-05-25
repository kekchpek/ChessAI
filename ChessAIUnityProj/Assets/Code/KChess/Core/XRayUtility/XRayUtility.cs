using System.Collections.Generic;
using KChess.Core.BoardEnvironment;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using KChess.Domain;

namespace KChess.Core.XRayUtility
{
    public class XRayUtility : IXRayUtility, IBoardEnvironmentComponent
    {

        public IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> TargetPieces => _targetPieces;
        public IReadOnlyDictionary<IPiece, IReadOnlyList<IXRay>> AttackingPieces => _attackingPieces;
        
        
        private readonly Dictionary<IPiece, IReadOnlyList<IXRay>> _targetPieces =
            new();
        private readonly Dictionary<IPiece, IReadOnlyList<IXRay>> _attackingPieces =
            new();

        private readonly IBoard _board;
        private readonly IQueenXRayUtility _queenMoveUtility;
        private readonly IRookXRayUtility _rookXRayUtility;
        private readonly IBishopXRayUtility _bishopXRayUtility;

        public XRayUtility(
            IBoard board,
            IQueenXRayUtility queenMoveUtility,
            IRookXRayUtility rookXRayUtility,
            IBishopXRayUtility bishopXRayUtility)
        {
            _board = board;
            _queenMoveUtility = queenMoveUtility;
            _rookXRayUtility = rookXRayUtility;
            _bishopXRayUtility = bishopXRayUtility;
            _board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece _)
        {
            _targetPieces.Clear();
            _attackingPieces.Clear();
            foreach (var piece in _board.Pieces)
            {
                ProcessPieceXRay(piece);
            }
        }

        private void ProcessPieceXRay(IPiece piece)
        {
            IXRay[] xRays = piece.Type switch
            {
                PieceType.Rook => _rookXRayUtility.GetXRays(piece),
                PieceType.Bishop => _bishopXRayUtility.GetXRays(piece),
                PieceType.Queen => _queenMoveUtility.GetXRays(piece),
                _ => null
            };
            if (xRays == null) return;
            _attackingPieces.Add(piece, xRays);
            foreach (var xRay in xRays)
            {
                if (!_targetPieces.ContainsKey(xRay.TargetPiece))
                    _targetPieces.Add(xRay.TargetPiece, new List<IXRay>());
                var targetXRayList = (List<IXRay>)_targetPieces[xRay.TargetPiece];
                targetXRayList.Add(xRay);
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}