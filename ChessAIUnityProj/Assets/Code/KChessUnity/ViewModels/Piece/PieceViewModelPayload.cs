using KChess.Core.API.PlayerFacade;
using KChess.Domain;
using KChessUnity.Models.Board;

namespace KChessUnity.ViewModels.Piece
{
    public class PieceViewModelPayload : IPieceViewModelPayload
    {
        public IBoardWorldPositionsCalculator BoardWorldPositionsCalculator { get; }
        public IPiece Piece { get; }
        public IPlayerFacade PlayerFacade { get; }

        public PieceViewModelPayload(IPiece piece, IPlayerFacade playerFacade,
            IBoardWorldPositionsCalculator boardWorldPositionsCalculator)
        {
            BoardWorldPositionsCalculator = boardWorldPositionsCalculator;
            Piece = piece;
            PlayerFacade = playerFacade;
        }
    }
}