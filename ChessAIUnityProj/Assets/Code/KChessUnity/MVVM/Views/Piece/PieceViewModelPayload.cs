using KChess.Core.API.PlayerFacade;
using KChess.Domain;

namespace KChessUnity.MVVM.Views.Piece
{
    public class PieceViewModelPayload : IPieceViewModelPayload
    {
        public IPiece Piece { get; }
        public IPlayerFacade PlayerFacade { get; }

        public PieceViewModelPayload(IPiece piece, IPlayerFacade playerFacade)
        {
            Piece = piece;
            PlayerFacade = playerFacade;
        }
    }
}