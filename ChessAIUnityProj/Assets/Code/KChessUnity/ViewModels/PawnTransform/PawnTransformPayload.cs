using KChess.Core.API.PlayerFacade;
using KChess.Domain;

namespace KChessUnity.ViewModels.PawnTransform
{
    public class PawnTransformPayload : IPawnTransformPayload
    {
        public PieceColor Color { get; }
        public IPlayerFacade PlayerFacade { get; }

        public PawnTransformPayload(PieceColor color, IPlayerFacade playerFacade)
        {
            Color = color;
            PlayerFacade = playerFacade;
        }

    }
}