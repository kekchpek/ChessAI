using System;
using KChess.Domain;

namespace KChess.Core.TurnUtility
{
    public class TurnUtility : ITurnUtility
    {
        public event Action<PieceColor> TurnChanged;

        private PieceColor _turnColor = PieceColor.White;
        
        public PieceColor GetTurn() => _turnColor;
        
        public void NextTurn()
        {
            _turnColor = _turnColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            TurnChanged?.Invoke(_turnColor);
        }
    }
}