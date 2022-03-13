﻿using System;
using KekChessCore.BoardEnvironment;
using KekChessCore.Domain;

namespace KekChessCore.LastMovedPieceUtils
{
    public class LastMovedPieceUtility : ILastMovedPieceGetter, ILastMovedPieceObserver, IBoardEnvironmentComponent
    {

        public event Action<IPiece> LastMovedPieceChanged;

        private readonly IBoard _board;

        private IPiece _lastMovedPiece;

        public LastMovedPieceUtility(IBoard board)
        {
            _board = board;
            _board.PieceMoved += OnPieceMoved;
        }

        public IPiece GetLastMovedPiece() => _lastMovedPiece;

        private void OnPieceMoved(IPiece piece)
        {
            _lastMovedPiece = piece;
            LastMovedPieceChanged?.Invoke(piece);
        }

        public void Dispose()
        {
            _board.PieceMoved -= OnPieceMoved;
        }
    }
}