using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.Factories
{
    public interface IPieceFactory
    {
        
        /// <summary>
        /// Creates a piece and place it on the board.
        /// </summary>
        /// <param name="pieceType">The type of the piece,</param>
        /// <param name="pieceColor">The color of the piece.</param>
        /// <param name="position">The position of the piece.</param>
        /// <param name="boardToPlace">The board to place the piece.</param>
        /// <returns>Returns created piece.</returns>
        IPiece Create(PieceType pieceType, PieceColor pieceColor,
            BoardCoordinates position, IBoard boardToPlace);

        
        IPiece Copy(IPiece piece, IBoard boardToPlace);

    }
}