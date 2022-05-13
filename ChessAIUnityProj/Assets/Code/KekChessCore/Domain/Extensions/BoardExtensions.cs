using KekChessCore.Domain.BoardIndices;
using KekChessCore.Domain.Impl;

namespace KekChessCore.Domain.Extensions
{
    public static class BoardExtensions
    {
        
        public static IPiece GetPieceOn(this IBoard board, BoardCoordinates boardCoordinates)
        {
            return BoardIndicesProvider.GetIndex(board).GetPieceOn(boardCoordinates);
        }
    }
}