using System.Collections.Generic;
using ChessAI.Domain;
using ChessAI.Domain.Impl;

namespace ChessAI.Core.XRayUtility
{
    public interface IXRay
    {

        IPiece TargetPiece { get; }

        IPiece AttackingPiece { get; }

        IReadOnlyList<IPiece> BlockingPieces { get; }

        IReadOnlyList<BoardCoordinates> CellsBetween { get; }

    }
}