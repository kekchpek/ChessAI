using KChess.Core.BoardEnvironment;
using KChess.Core.BoardStateUtils;
using KChess.Core.CheckUtility;
using KChess.Core.MateUtility;
using KChess.Core.PawnTransformation;
using KChess.Domain;

namespace KChess.Core.CheckMate
{
    public class CheckMateDetector: ICheckMateDetector, IBoardEnvironmentComponent
    {
        private readonly IBoard _board;
        private readonly IBoardStateSetter _boardStateSetter;
        private readonly ICheckUtility _checkUtility;
        private readonly IMateUtility _mateUtility;
        private readonly IPawnTransformationUtility _pawnTransformationUtility;

        public CheckMateDetector(
            IBoard board, 
            IBoardStateSetter boardStateSetter, 
            ICheckUtility checkUtility,
            IMateUtility mateUtility,
            IPawnTransformationUtility pawnTransformationUtility)
        {
            _board = board;
            _boardStateSetter = boardStateSetter;
            _checkUtility = checkUtility;
            _mateUtility = mateUtility;
            _pawnTransformationUtility = pawnTransformationUtility;

            _board.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(IPiece piece)
        {
            if (_checkUtility.IsPositionWithCheck(out var checkedColor))
            {
                switch (checkedColor)
                {
                    case PieceColor.Black:
                        if (_mateUtility.IsMate() && _pawnTransformationUtility.GetTransformingPiece() != null)
                        {
                            _boardStateSetter.Set(BoardState.MateToBlack);
                        }
                        else
                        {
                            _boardStateSetter.Set(BoardState.CheckToBlack);
                        }
                        break;
                    case PieceColor.White:
                        if (_mateUtility.IsMate() && _pawnTransformationUtility.GetTransformingPiece() != null)
                        {
                            _boardStateSetter.Set(BoardState.MateToWhite);
                        }
                        else
                        {
                            _boardStateSetter.Set(BoardState.MateToWhite);
                        }
                        break;
                }
            }
            else
            {
                if (_pawnTransformationUtility.GetTransformingPiece() == null &&
                    _mateUtility.IsMate())
                {
                    _boardStateSetter.Set(BoardState.Stalemate);
                }
                else
                {
                    _boardStateSetter.Set(BoardState.Regular);
                }
            }
        }

        public void Dispose()
        {
            _board.PositionChanged -= OnPositionChanged;
        }
    }
}