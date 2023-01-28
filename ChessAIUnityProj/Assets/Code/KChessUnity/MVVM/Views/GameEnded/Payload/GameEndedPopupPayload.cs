using KChess.Domain;

namespace KChessUnity.MVVM.Views.GameEnded.Payload
{
    public class GameEndedPopupPayload : IGameEndedPopupPayload
    {
        public BoardState State { get; }

        public GameEndedPopupPayload(BoardState state)
        {
            State = state;
        }
        
    }
}