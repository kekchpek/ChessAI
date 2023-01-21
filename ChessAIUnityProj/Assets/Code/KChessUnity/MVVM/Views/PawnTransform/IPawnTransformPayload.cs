using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.PawnTransform
{
    public interface IPawnTransformPayload : IPayload
    {
        IPlayerFacade PlayerFacade { get; }
        PieceColor Color { get; }
    }
}