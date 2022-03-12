using ChessAI.Core.BoardStateUtils;
using ChessAI.Core.CheckUtility;
using ChessAI.Domain;

namespace ChessAI.Core.CheckDetector
{
    public class CheckDetector: ICheckDetector
    {
        private readonly IBoard _board;
        private readonly IBoardStateSetter _boardStateSetter;
        private readonly ICheckUtility _checkUtility;

        public CheckDetector(IBoard board, IBoardStateSetter boardStateSetter, ICheckUtility checkUtility)
        {
            _board = board;
            _boardStateSetter = boardStateSetter;
            _checkUtility = checkUtility;

            _board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece piece)
        {
            if (_checkUtility.IsPositionWithCheck(out var checkedColor))
            {
                switch (checkedColor)
                {
                    case PieceColor.Black:
                        _boardStateSetter.Set(BoardState.CheckToBlack);
                        break;
                    case PieceColor.White:
                        _boardStateSetter.Set(BoardState.CheckToWhite);
                        break;
                }
            }
        }
    }
}