using System;
using System.Collections.Generic;
using KChess.Core.PawnTransformation;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.API.PlayerFacade
{
    public interface IPlayerFacade
    {
        event Action<PieceColor> TurnChanged;
        event Action<BoardState> BoardStateChanged;
        event Action<IPiece> PieceRequiredToBeTransformed;
        event Action<IPiece> PieceAddedOnBoard;

        IEnumerable<IPiece> GetPieces();
        
        bool TryMovePiece(IPiece piece, BoardCoordinates position);

        bool TryTransform(PawnTransformationVariant pawnTransformationVariant);

        IPiece GetTransformingPiece();

        BoardCoordinates[] GetAvailableMoves(IPiece piece);

        PieceColor GetTurn();

        BoardState GetBoardState();
    }
}