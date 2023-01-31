using System.Collections.Generic;
using KChess.Domain;

namespace KChess.Core.PositionRepeating
{
    internal class PositionRepeatingUtility : IPositionRepeatingUtility
    {

        private readonly Dictionary<int, int> _positionsCounter = new();

        public void IndexPosition(IBoard board)
        {
            var positionHash = board.GetPositionHash();
            if (_positionsCounter.ContainsKey(positionHash))
            {
                _positionsCounter[positionHash]++;
            }
            else
            {
                _positionsCounter.Add(positionHash, 1);
            }
        }

        public int GetPositionRepeatingTimes(IBoard board)
        {
            return _positionsCounter.TryGetValue(board.GetPositionHash(), out var times) ? times : 0;
        }
    }
}