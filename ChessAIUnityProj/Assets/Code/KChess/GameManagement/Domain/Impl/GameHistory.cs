using System;
using System.Collections.Generic;
using KChess.Domain;

namespace KChess.GameManagement.Domain.Impl
{
    public class GameHistory : IMutableGameHistory
    {
        
        public event Action Changed;

        private List<IMove> _moves = new();
        public Position InitialPosition { get; private set; }
        
        public IReadOnlyList<IMove> Moves => _moves;

        public GameResult? GameResult { get; private set; }

        public void SetInitialPosition(Position position)
        {
            InitialPosition = position;
        }

        public void AddMove(IMove move)
        {
            _moves.Add(move);
            Changed?.Invoke();
        }

        public void SetResult(GameResult result)
        {
            GameResult = result;
            Changed?.Invoke();
        }
    }
}