using System;
using System.Linq;
using KChess.Core.BoardStateUtils;
using KChess.Core.MoveUtility;
using KChess.Core.TurnUtility;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.API.PlayerFacade
{
    public class ManagedPlayerFacade : IManagedPlayerFacade
    {
        private readonly IMoveUtility _moveUtility;
        private readonly ITurnGetter _turnGetter;
        private readonly ITurnObserver _turnObserver;
        private readonly IBoardStateGetter _boardStateGetter;
        private readonly IBoardStateObserver _boardStateObserver;
        private readonly IBoard _board;
        private readonly PieceColor _pieceColor;

        public ManagedPlayerFacade(IMoveUtility moveUtility, ITurnGetter turnGetter,
            ITurnObserver turnObserver, IBoardStateGetter boardStateGetter,
            IBoardStateObserver boardStateObserver, IBoard board,
            PieceColor pieceColor)
        {
            _moveUtility = moveUtility;
            _turnGetter = turnGetter;
            _turnObserver = turnObserver;
            _boardStateGetter = boardStateGetter;
            _boardStateObserver = boardStateObserver;
            _board = board;
            _pieceColor = pieceColor;
            _boardStateObserver.StateChanged += OnBoardStateChanged;
            _turnObserver.TurnChanged += OnTurnChanged;
        }

        public event Action<PieceColor> TurnChanged;
        public event Action<BoardState> BoardStateChanged;

        public IBoard GetBoard()
        {
            return _board;
        }

        public bool TryMovePiece(IPiece piece, BoardCoordinates position)
        {
            if (_turnGetter.GetTurn() != _pieceColor)
                return false;
            if (piece.Color != _pieceColor)
                return false;
            if (!_moveUtility.GetAvailableMoves(piece).Contains(position))
                return false;
            
            piece.MoveTo(position);
            return true;
        }

        public BoardCoordinates[] GetAvailableMoves(IPiece piece)
        {
            return _moveUtility.GetAvailableMoves(piece);
        }

        public PieceColor GetTurn()
        {
            return _turnGetter.GetTurn();
        }

        public BoardState GetBoardState()
        {
            return _boardStateGetter.Get();
        }

        private void OnTurnChanged(PieceColor turn)
        {
            TurnChanged?.Invoke(turn);
        }

        private void OnBoardStateChanged(BoardState boardState)
        {
            BoardStateChanged?.Invoke(boardState);
        }

        public void Dispose()
        {
            _boardStateObserver.StateChanged -= OnBoardStateChanged;
            _turnObserver.TurnChanged -= OnTurnChanged;
        }
    }
}