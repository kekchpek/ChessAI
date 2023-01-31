using KChess.Core.API.PlayerFacade;
using KChess.Core.BoardEnvironment.Utils;
using KChess.Core.Castle;
using KChess.Core.GameStateChanger;
using KChess.Core.PawnTransformation;
using KChess.Core.PositionRepeating;
using KChess.Core.Taking;
using KChess.Core.TurnUtility;
using KChess.Domain;

namespace KChess.Core.BoardEnvironment
{
    internal class BoardEnvironment : IBoardEnvironment
    {
        public IPlayerFacade BlackPlayerFacade => _blackPlayerFacade;
        public IPlayerFacade WhitePlayerFacade => _whitePlayerFacade;

        private readonly IManagedPlayerFacade _whitePlayerFacade;
        private readonly IBoard _board;
        private readonly IManagedPlayerFacade _blackPlayerFacade;

        private readonly IUtilityContainer _utilityContainer;

        private bool _updating;

        public BoardEnvironment(
            IManagedPlayerFacade blackPlayer, 
            IManagedPlayerFacade whitePlayer,
            IBoard board,
            IUtilityContainer components)
        {
            _blackPlayerFacade = blackPlayer;
            _whitePlayerFacade = whitePlayer;
            _board = board;
            _utilityContainer = components;

            _board.Updated += OnBoardUpdated;
        }

        private void OnBoardUpdated(IPiece movedPiece)
        {
            if (_updating)
                return;

            _updating = true;
            
            // IndexPosition
            _utilityContainer.Get<IPositionRepeatingUtility>().IndexPosition(_board);
            
            // Take
            _utilityContainer.Get<ITakeUtility>().TryTake(movedPiece);

            // Castle
            _utilityContainer.Get<ICastleUtility>().TryMakeCastle(movedPiece);
            
            // Pawn Transformation
            _utilityContainer.Get<IPawnTransformationUtility>().UpdateTransformingPiece(movedPiece);

            // Game state
            var state = _utilityContainer.Get<IGameStateChanger>().UpdateBoardState();
            if (state is BoardState.Regular or BoardState.CheckToBlack or BoardState.CheckToWhite)
            {
                _utilityContainer.Get<ITurnUtility>().NextTurn();
            }

            _updating = false;
        }

        public void Dispose()
        {
            _board.Updated -= OnBoardUpdated;
            _utilityContainer.Dispose();
            _whitePlayerFacade.Dispose();
            _blackPlayerFacade.Dispose();
        }
    }
}