using System.Collections.Generic;
using System.Linq;
using KChess.Domain.Impl;

namespace KChess.Domain
{
    public struct Position
    {
        private (PieceType, PieceColor, BoardCoordinates)[] _pieces;
        public IReadOnlyList<(PieceType type, PieceColor color, BoardCoordinates position)> Pieces => _pieces;

        public Position(IBoard board)
        {
            _pieces = board.Pieces.Aggregate(
                new List<(PieceType, PieceColor, BoardCoordinates)>(board.Pieces.Count),
                (list, piece) =>
                {
                    list.Add((piece.Type, piece.Color, piece.Position!.Value));
                    return list;
                }).ToArray();
        }
    }
}