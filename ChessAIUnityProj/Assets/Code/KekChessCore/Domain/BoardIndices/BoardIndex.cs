using System.Collections.Generic;
using KekChessCore.Auxiliary.WeakReferenceDecorator;
using KekChessCore.Domain.Impl;
using UnityEngine;

namespace KekChessCore.Domain.BoardIndices
{
    public class BoardIndex : IBoardIndex
    {

        private readonly IDictionary<BoardCoordinates, IPiece> _piecesByPosition = new Dictionary<BoardCoordinates, IPiece>();

        public BoardIndex(TransientWeakReference<IBoard> boardRef)
        {
            if (boardRef.TryGetTarget(out var board))
            {
                board.PositionChanged += OnPositionChanged;
                foreach (var piece in board.Pieces)
                {
                    _piecesByPosition.Add(piece.Position, piece);
                }
            }
            else
            {
                Debug.LogError("Trying to create an index for not board, that was destroyed!");
            }
        }

        private void OnPositionChanged(IPiece piece)
        {
            if (_piecesByPosition.ContainsKey(piece.PreviousPosition))
            {
                _piecesByPosition.Remove(piece.PreviousPosition);
            }
            else
            {
                Debug.LogError("Unexpected behaviour! Piece moved, but previous position was not indexed");
            }

            _piecesByPosition[piece.Position] = piece;
        }

        public IPiece GetPieceOn(BoardCoordinates boardCoordinates)
        {
            if (_piecesByPosition.TryGetValue(boardCoordinates, out var piece))
            {
                return piece;
            }

            return null;
        }
    }
}