using System;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.API.PlayerFacade
{
    public interface IPlayerFacade
    {
        event Action<PieceColor> TurnChanged;
        event Action<BoardState> BoardStateChanged;

        IBoard GetBoard();
        
        bool TryMovePiece(IPiece piece, BoardCoordinates position);

        BoardCoordinates[] GetAvailableMoves(IPiece piece);

        PieceColor GetTurn();

        BoardState GetBoardState();
    }
}