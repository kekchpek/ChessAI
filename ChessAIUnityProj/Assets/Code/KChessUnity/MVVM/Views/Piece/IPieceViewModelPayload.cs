using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.Piece
{
    public interface IPieceViewModelPayload : IPayload
    {
        IPiece Piece { get; }
        IPlayerFacade PlayerFacade { get; }
    }
}