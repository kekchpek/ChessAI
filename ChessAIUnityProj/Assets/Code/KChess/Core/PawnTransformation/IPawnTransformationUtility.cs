using System;
using KChess.Domain;

namespace KChess.Core.PawnTransformation
{
    public interface IPawnTransformationUtility
    {

        /// <summary>
        /// Fired when some pawn starts needing to be transformed.
        /// </summary>
        event Action<IPiece> TransformationBecomesRequired;
        
        /// <summary>
        /// Returns piece needs to be transformed. Or null if there is not such piece.
        /// </summary>
        /// <returns>Piece needs to be transformed or null.</returns>
        IPiece GetTransformingPiece();

        /// <summary>
        /// Tries to transform pawn into some piece.
        /// </summary>
        /// <param name="transformationVariant">The variant of transformation to apply.</param>
        /// <returns>True if transformation available.</returns>
        bool TryTransform(PawnTransformationVariant transformationVariant);

    }
}