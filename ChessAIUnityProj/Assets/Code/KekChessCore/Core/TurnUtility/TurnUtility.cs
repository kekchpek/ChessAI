using System;
using KekChessCore.Domain;

namespace KekChessCore.TurnUtility
{
    public class TurnUtility : ITurnGetter, ITurnObserver, ITurnSetter
    {
        public event Action<PieceColor> TurnChanged;

        private PieceColor _turnColor = PieceColor.White;
        
        public PieceColor GetTurn() => _turnColor;
        
        public TurnUtility(IBoard board)
        {
            board.PieceMoved += OnPieceMoved;
        }
        
        public void SetTurn(PieceColor pieceColor)
        {
            _turnColor = pieceColor;
            TurnChanged?.Invoke(_turnColor);
        }

        private void OnPieceMoved(IPiece movedPiece)
        {
            if (movedPiece.Color == PieceColor.White)
            {
                SetTurn(PieceColor.Black);
            }
            if (movedPiece.Color == PieceColor.Black)
            {
                SetTurn(PieceColor.White);
            }
        }
    }
}