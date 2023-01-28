using KChess.Domain;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.GameEnded.Payload
{
    public interface IGameEndedPopupPayload : IPayload
    {
        BoardState State { get; }
    }
}