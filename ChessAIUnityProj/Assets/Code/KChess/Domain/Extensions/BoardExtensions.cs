using KChess.Domain.BoardIndices;
using KChess.Domain.Impl;

namespace KChess.Domain.Extensions
{
    public static class BoardExtensions
    {
        
        public static IPiece GetPieceOn(this IBoard board, BoardCoordinates boardCoordinates)
        {
            return BoardIndicesProvider.GetIndex(board).GetPieceOn(boardCoordinates);
        }
    }
}