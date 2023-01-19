using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.Models.Board;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.ViewModels.Piece
{
    public interface IPieceViewModelPayload : IPayload
    {
        IBoardWorldPositionsCalculator BoardWorldPositionsCalculator { get; }
        IPiece Piece { get; }
        IPlayerFacade PlayerFacade { get; }
    }
}