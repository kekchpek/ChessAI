using KChess.Core.PawnTransformation;
using KChess.Domain;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.ViewModels.PawnTransform
{
    public interface IPawnTransformViewModel : IViewModel
    {
        
        PieceColor ColorOfTransformingPiece { get; }

        void OnPieceClicked(PawnTransformationVariant pieceType);

    }
}