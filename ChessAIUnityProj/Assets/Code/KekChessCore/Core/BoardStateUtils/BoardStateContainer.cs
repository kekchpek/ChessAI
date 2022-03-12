using System;
using KekChessCore.Domain;

namespace KekChessCore.BoardStateUtils
{
    public class BoardStateContainer : IBoardStateSetter, IBoardStateGetter, IBoardStateObserver
    {

        private BoardState _state = BoardState.Regular;

        public BoardState Get() => _state;

        public void Set(BoardState boardState)
        {
            _state = boardState;
            StateChanged?.Invoke(_state);
        }

        public event Action<BoardState> StateChanged;
    }
}