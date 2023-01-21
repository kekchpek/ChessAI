using KChess.Core.API.PlayerFacade;
using KChess.Core.PawnTransformation;
using KChess.Domain;
using UnityEngine;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.PawnTransform
{
    public class PawnTransformViewModel : ViewModel, IPawnTransformViewModel
    {

        private readonly IPlayerFacade _playerFacade;
        
        public PieceColor ColorOfTransformingPiece { get; }

        public PawnTransformViewModel(IPawnTransformPayload payload)
        {
            ColorOfTransformingPiece = payload.Color;
            _playerFacade = payload.PlayerFacade;
        }
        
        public void OnPieceClicked(PawnTransformationVariant pieceType)
        {
            if (_playerFacade.TryTransform(pieceType))
            {
                Destroy();
            }
            else
            {
                Debug.LogError($"Can not transform pawn to {nameof(PawnTransformationVariant)}. Probably no pawn is transforming.");
            }
        }
    }
}