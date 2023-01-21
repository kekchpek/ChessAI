using KChess.Core.PawnTransformation;
using KChess.Domain;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.MVVM.Views.PawnTransform
{
    public interface IPawnTransformViewModel : IViewModel
    {
        
        PieceColor ColorOfTransformingPiece { get; }

        void OnPieceClicked(PawnTransformationVariant pieceType);

    }
}