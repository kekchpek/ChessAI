using System;
using KChess.Core.BoardStateUtils;
using KChess.Core.CheckUtility;
using KChess.Core.MateUtility;
using KChess.Core.PawnTransformation;
using KChess.Domain;

namespace KChess.Core.CheckMate
{
    internal class CheckMateUtility: ICheckMateUtility
    {
        private readonly IBoardStateSetter _boardStateSetter;
        private readonly ICheckUtility _checkUtility;
        private readonly IMateUtility _mateUtility;
        private readonly IPawnTransformationUtility _pawnTransformationUtility;

        public CheckMateUtility(
            IBoardStateSetter boardStateSetter, 
            ICheckUtility checkUtility,
            IMateUtility mateUtility,
            IPawnTransformationUtility pawnTransformationUtility)
        {
            _boardStateSetter = boardStateSetter;
            _checkUtility = checkUtility;
            _mateUtility = mateUtility;
            _pawnTransformationUtility = pawnTransformationUtility;
        }

        public BoardState UpdateBoardState()
        {
            if (_checkUtility.IsPositionWithCheck(out var checkedColor))
            {
                switch (checkedColor)
                {
                    case PieceColor.Black:
                        _boardStateSetter.Set(BoardState.CheckToBlack);
                        if (_mateUtility.IsMate() && _pawnTransformationUtility.GetTransformingPiece() == null)
                        {
                            _boardStateSetter.Set(BoardState.MateToBlack);
                            return BoardState.MateToBlack;
                        }

                        return BoardState.CheckToBlack;
                    case PieceColor.White:
                        _boardStateSetter.Set(BoardState.CheckToWhite);
                        if (_mateUtility.IsMate() && _pawnTransformationUtility.GetTransformingPiece() == null)
                        {
                            _boardStateSetter.Set(BoardState.MateToWhite);
                            return BoardState.MateToWhite;
                        }

                        return BoardState.CheckToWhite;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                if (_pawnTransformationUtility.GetTransformingPiece() == null &&
                    _mateUtility.IsMate())
                {
                    _boardStateSetter.Set(BoardState.Stalemate);
                    return BoardState.Stalemate;
                }
                else
                {
                    _boardStateSetter.Set(BoardState.Regular);
                    return BoardState.Regular;
                }
            }
        }
    }
}