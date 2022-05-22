using System;
using KChess.Core.BoardEnvironment;
using KChess.Domain;

namespace KChess.Core.TurnUtility
{
    public class TurnUtility : ITurnGetter, ITurnObserver, ITurnSetter, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;
        public event Action<PieceColor> TurnChanged;

        private PieceColor _turnColor = PieceColor.White;
        
        public PieceColor GetTurn() => _turnColor;
        
        public TurnUtility(IBoard board)
        {
            _board = board;
            _board.PieceMoved += OnPieceMoved;
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

        public void Dispose()
        {
            _board.PieceMoved -= OnPieceMoved;
        }
    }
}