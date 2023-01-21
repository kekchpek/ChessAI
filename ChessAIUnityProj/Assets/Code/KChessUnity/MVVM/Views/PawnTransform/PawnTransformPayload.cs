using KChess.Core.API.PlayerFacade;
using KChess.Domain;

namespace KChessUnity.MVVM.Views.PawnTransform
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