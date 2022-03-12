using System.Collections.Generic;
using KekChessCore.Domain;
using KekChessCore.Domain.Impl;

namespace KekChessCore.XRayUtility
{
    public interface IXRay
    {

        IPiece TargetPiece { get; }

        IPiece AttackingPiece { get; }

        IReadOnlyList<IPiece> BlockingPieces { get; }

        IReadOnlyList<BoardCoordinates> CellsBetween { get; }

    }
}