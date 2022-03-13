using System;
using KekChessCore.BoardEnvironment;
using KekChessCore.Domain;

namespace KekChessCore.BoardStateUtils
{
    public class BoardStateContainer : IBoardStateSetter, IBoardStateGetter, IBoardStateObserver, IBoardEnvironmentComponent
    {

        private BoardState _state = BoardState.Regular;

        public BoardState Get() => _state;

        public void Set(BoardState boardState)
        {
            _state = boardState;
            StateChanged?.Invoke(_state);
        }

        public event Action<BoardState> StateChanged;

        public void Dispose()
        {
            // do nothing
        }
    }
}