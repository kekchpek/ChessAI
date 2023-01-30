using System.Collections.Generic;
using KChess.Auxiliary.WeakReferenceDecorator;
using KChess.Domain.Impl;
using UnityEngine;

namespace KChess.Domain.BoardIndices
{
    internal class BoardIndex : IBoardIndex
    {

        private readonly IDictionary<BoardCoordinates, IPiece> _piecesByPosition = new Dictionary<BoardCoordinates, IPiece>();

        public BoardIndex(TransientWeakReference<IBoard> boardRef)
        {
            if (boardRef.TryGetTarget(out var board))
            {
                board.Updated += OnBoardUpdated;
                foreach (var piece in board.Pieces)
                {
                    if (piece.Position.HasValue)
                    {
                        _piecesByPosition.Add(piece.Position.Value, piece);
                    }
                    else
                    {
                        Debug.LogWarning("Removed piece is still in board pieces collection!");
                    }
                }
            }
            else
            {
                Debug.LogError("Trying to create an index for not board, that was destroyed!");
            }
        }

        private void OnBoardUpdated(IPiece piece)
        {
            if (_piecesByPosition.TryGetValue(piece.PreviousPosition, out var previousPiece) && previousPiece == piece)
            {
                _piecesByPosition.Remove(piece.PreviousPosition); // piece was removed
            }

            if (piece.Position.HasValue)
            {
                _piecesByPosition[piece.Position.Value] = piece;
            }
            else
            {
                // Piece was removed.
            }
        }

        public IPiece GetPieceOn(BoardCoordinates boardCoordinates)
        {
            if (_piecesByPosition.TryGetValue(boardCoordinates, out var piece))
            {
                return piece;
            }

            return null;
        }

        public IReadOnlyDictionary<BoardCoordinates, IPiece> GetPiecePositionMap()
        {
            return (IReadOnlyDictionary<BoardCoordinates, IPiece>) _piecesByPosition;
        }
    }
}