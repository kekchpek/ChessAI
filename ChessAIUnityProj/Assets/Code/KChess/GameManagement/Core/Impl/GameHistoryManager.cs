using System;
using System.Linq;
using KChess.Core.BoardEnvironment;
using KChess.Core.BoardEnvironment.Factory;
using KChess.Domain;
using KChess.GameManagement.Domain;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChess.GameManagement.Core.Impl
{
    internal class GameHistoryManager : IGameHistoryManager
    {
        private readonly IBoardEnvironmentFactory _boardEnvFactory;

        private IBoardEnvironment _boardEnv;

        private IMutable<Position> _position = new Mutable<Position>();
        private IMutableGameHistory _history;

        public IBindable<Position> Position => _position;
        public IBoard Board => _boardEnv?.Board;
        public IGameHistory History => _history;

        public GameHistoryManager(IBoardEnvironmentFactory boardEnvFactory)
        {
            _boardEnvFactory = boardEnvFactory;
        }
        
        public IGameHistoryBuilder SetPosition(Position position)
        {
            _boardEnv = _boardEnvFactory.Create(position);
            _history.SetInitialPosition(position);
            return this;
        }

        public IGameHistoryBuilder Move(IMove move)
        {
            CheckBoard();
            var piece = _boardEnv.Board.Pieces
                .FirstOrDefault(x => x.Position == move.PositionFrom && x.Type == move.Piece && x.Color == move.Color);
            if (piece == null)
            {
                throw new Exception($"No piece of type {move.Piece} and color {move.Color} at {move.PositionFrom}");
            }

            if (!_boardEnv.UniversalPlayerFacade.TryMovePiece(piece, move.PositionTo))
            {
                throw new Exception($"Can not move piece from {move.PositionFrom} to {move.PositionTo}");
            }
            
            _history.AddMove(move);
            return this;
        }

        public IGameHistoryBuilder SetResult(GameResult result)
        {
            CheckBoard();
            _history.SetResult(result);
            return this;
        }

        public IGameHistory Build() => History;

        private void CheckBoard()
        {
            if (_boardEnv == null)
            {
                throw new InvalidOperationException($"Position should be set first! Use {nameof(SetPosition)} method");
            }
        }
    }
}