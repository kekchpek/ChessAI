using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardEnvironment.Utils;
using KChess.Core.BoardStateUtils;
using KChess.Core.Castle;
using KChess.Core.CheckMate;
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
using KChess.Core.Taking;
using KChess.Core.TurnUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.BishopXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.QueenXRayUtility;
using KChess.Core.XRayUtility.XRayPiecesUtilities.RookXRayUtility;
using KChess.Domain;

namespace KChess.Core.BoardEnvironment.Factory
{
    internal class BoardEnvironmentFactory : IBoardEnvironmentFactory
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
            var utilityContainer = new UtilityContainer();
            
            // Common
            var lastMovedPieceUtility = new LastMovedPieceUtility();
            var boardStateContainer = new BoardStateContainer();
            var turnUtility = new TurnUtility.TurnUtility();
            utilityContainer.Add<ITurnUtility>(turnUtility);
            

            // Moves
            var pawnMoveUtility = new PawnMoveUtility(board, lastMovedPieceUtility);
            var knightMoveUtility = new KnightMoveUtility(board);
            var bishopMoveUtility = new BishopMoveUtility(board);
            var rookMoveUtility = new RookMoveUtility(board);
            var queenMoveUtility = new QueenMoveUtility(bishopMoveUtility, rookMoveUtility);
            var kingMoveUtility = new KingMoveUtility(board);
            var piecesMovesFacade = new PieceMoveUtilityFacade(pawnMoveUtility, rookMoveUtility,
                knightMoveUtility, bishopMoveUtility, queenMoveUtility, kingMoveUtility);
            
            // Attacked cells
            var attackedCellUtility = new AttackedCellsUtility.AttackedCellsUtility(piecesMovesFacade, board);
            
            // Special moves
            var enPassantUtility = new EnPassantUtility.EnPassantUtility(lastMovedPieceUtility);
            var castleMoveUtility = new CastleMoveUtility.CastleMoveUtility(board, attackedCellUtility);
            var castleUtility = new CastleUtility(board);
            utilityContainer.Add<ICastleUtility>(castleUtility);
            
            // Taking
            var takeUtility = new TakeUtility(board, enPassantUtility);
            utilityContainer.Add<ITakeUtility>(takeUtility);
            
            // XRays
            var bishopXRayUtility = new BishopXRayUtility(board);
            var rookXRayUtility = new RookXRayUtility(board);
            var queenXRayUtility = new QueenXRayUtility(bishopXRayUtility, rookXRayUtility);
            var xRayUtility = new XRayUtility.XRayUtility(board, queenXRayUtility, rookXRayUtility, bishopXRayUtility);
            
            // Check
            var checkBlockingUtility = new CheckBlockingUtility.CheckBlockingUtility(xRayUtility);
            var checkUtility = new CheckUtility.CheckUtility(board, piecesMovesFacade);

            // Pawn transformation
            var pawnTransformationUtility = new PawnTransformationUtility(board, _pieceFactory);
            utilityContainer.Add<IPawnTransformationUtility>(pawnTransformationUtility);

            // Available moves
            var moveUtility = new MoveUtility.MoveUtility(piecesMovesFacade, xRayUtility, attackedCellUtility,
                boardStateContainer, checkBlockingUtility, checkUtility, castleMoveUtility,
                pawnTransformationUtility);
            
            // check-mate
            var mateUtility = new MateUtility.MateUtility(board, moveUtility, turnUtility);
            var checkMateUtility = new CheckMateUtility(boardStateContainer, 
                checkUtility, mateUtility, pawnTransformationUtility);
            utilityContainer.Add<ICheckMateUtility>(checkMateUtility);
            
            // Player facades
            var whitePlayerFacade = new ManagedPlayerFacade(moveUtility, turnUtility, turnUtility,
                boardStateContainer, boardStateContainer, board, pawnTransformationUtility, PieceColor.White);
            var blackPlayerFacade = new ManagedPlayerFacade(moveUtility, turnUtility, turnUtility,
                boardStateContainer, boardStateContainer, board, pawnTransformationUtility, PieceColor.Black);
            return new BoardEnvironment(blackPlayerFacade, whitePlayerFacade, board, utilityContainer);
        }
    }
}