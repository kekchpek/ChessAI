using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardStateUtils;
using KChess.Core.Factories;
using KChess.Core.LastMovedPieceUtils;
using KChess.Core.MoveUtility;
using KChess.Core.MoveUtility.PieceMoveUtilities.Bishop;
using KChess.Core.MoveUtility.PieceMoveUtilities.King;
using KChess.Core.MoveUtility.PieceMoveUtilities.Knight;
using KChess.Core.MoveUtility.PieceMoveUtilities.Pawn;
using KChess.Core.MoveUtility.PieceMoveUtilities.Queen;
using KChess.Core.MoveUtility.PieceMoveUtilities.Rook;
using KChess.Core.PawnTransformation;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using KChess.Domain;

namespace KChess.Core.BoardEnvironment.Factory
{
    public class BoardEnvironmentFactory : IBoardEnvironmentFactory
    {
        private readonly IBoardFactory _boardFactory;
        private readonly IPieceFactory _pieceFactory;

        public BoardEnvironmentFactory(IBoardFactory boardFactory, IPieceFactory pieceFactory)
        {
            _boardFactory = boardFactory;
            _pieceFactory = pieceFactory;
        }
        
        public IBoardEnvironment Create()
        {
            var board = _boardFactory.CreateStandardBoard();
            
            var lastMovedPieceUtility = new LastMovedPieceUtility(board);

            var pawnMoveUtility = new PawnMoveUtility(board, lastMovedPieceUtility);
            var knightMoveUtility = new KnightMoveUtility(board);
            var bishopMoveUtility = new BishopMoveUtility(board);
            var rookMoveUtility = new RookMoveUtility(board);
            var queenMoveUtility = new QueenMoveUtility(bishopMoveUtility, rookMoveUtility);
            var kingMoveUtility = new KingMoveUtility(board);
            var piecesMovesFacade = new PieceMoveUtilityFacade(pawnMoveUtility, rookMoveUtility,
                knightMoveUtility, bishopMoveUtility, queenMoveUtility, kingMoveUtility);

            var bishopXRayUtility = new BishopXRayUtility(board);
            var rookXRayUtility = new RookXRayUtility(board);
            var queenXRayUtility = new QueenXRayUtility(bishopXRayUtility, rookXRayUtility);
            var xRayUtility = new XRayUtility.XRayUtility(board, queenXRayUtility, rookXRayUtility, bishopXRayUtility);
            
            var boardStateContainer = new BoardStateContainer();
            var checkUtility = new CheckUtility.CheckUtility(board, piecesMovesFacade);
            var checkDetector = new CheckDetector.CheckDetector(board, boardStateContainer, checkUtility);
            var checkBlockingUtility = new CheckBlockingUtility.CheckBlockingUtility(xRayUtility);

            var enPassantUtility = new EnPassantUtility.EnPassantUtility(lastMovedPieceUtility);
            var attackedCellUtility = new AttackedCellsUtility.AttackedCellsUtility(piecesMovesFacade, board);
            var castleMoveUtility = new CastleMoveUtility.CastleMoveUtility(board, attackedCellUtility);

            var pawnTransformationUtility = new PawnTransformationUtility(board, _pieceFactory);
            var pawnTransformationDetector = new PawnTransformationDetector(pawnTransformationUtility, board);

            var takeDetector = new TakeDetector.TakeDetector(board, enPassantUtility);
            var moveUtility = new MoveUtility.MoveUtility(piecesMovesFacade, xRayUtility, attackedCellUtility,
                boardStateContainer, checkBlockingUtility, checkUtility, castleMoveUtility,
                pawnTransformationUtility);
            var castleDetector = new CastleDetector.CastleDetector(board);

            var turnContainer = new TurnUtility.TurnUtility(board);

            var whitePlayerFacade = new ManagedPlayerFacade(moveUtility, turnContainer, turnContainer,
                boardStateContainer, boardStateContainer, board, PieceColor.White);
            var blackPlayerFacade = new ManagedPlayerFacade(moveUtility, turnContainer, turnContainer,
                boardStateContainer, boardStateContainer, board, PieceColor.Black);

            var components = new IBoardEnvironmentComponent[]
            {
                pawnMoveUtility,
                knightMoveUtility,
                bishopMoveUtility,
                rookMoveUtility,
                queenMoveUtility,
                kingMoveUtility,
                piecesMovesFacade,
                bishopXRayUtility,
                rookXRayUtility,
                queenXRayUtility,
                xRayUtility,
                boardStateContainer,
                checkUtility,
                checkDetector,
                checkBlockingUtility,
                lastMovedPieceUtility,
                enPassantUtility,
                attackedCellUtility,
                takeDetector,
                moveUtility,
                turnContainer,
                castleDetector,
                pawnTransformationDetector,
                pawnTransformationUtility
            };
            return new BoardEnvironment(blackPlayerFacade, whitePlayerFacade, components);
        }
    }
}