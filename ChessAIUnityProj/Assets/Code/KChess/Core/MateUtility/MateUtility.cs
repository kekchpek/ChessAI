using System.Linq;
using KChess.Core.MoveUtility;
using KChess.Core.TurnUtility;
using KChess.Domain;

namespace KChess.Core.MateUtility
{
    public class MateUtility : IMateUtility
    {
        private readonly IBoard _board;
        private readonly IMoveUtility _moveUtility;
        private readonly ITurnGetter _turnGetter;

        internal MateUtility(
            IBoard board, 
            IMoveUtility moveUtility,
            ITurnGetter turnGetter)
        {
            _board = board;
            _moveUtility = moveUtility;
            _turnGetter = turnGetter;
        }
        
        public bool IsMate()
        {
            var turnColor = _turnGetter.GetTurn() == PieceColor.White ? PieceColor.Black : PieceColor.White;
            foreach (var piece in _board.Pieces.Where(p => p.Color == turnColor))
            {
                if (_moveUtility.GetAvailableMoves(piece).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}