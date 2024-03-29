﻿using System.Collections.Generic;
using KChess.Domain;
using KChess.Domain.Impl;

namespace KChess.Core.XRayUtility
{
    internal interface IXRay
    {

        IPiece TargetPiece { get; }

        IPiece AttackingPiece { get; }

        IReadOnlyList<IPiece> BlockingPieces { get; }

        IReadOnlyList<BoardCoordinates> CellsBetween { get; }
        
        IReadOnlyList<BoardCoordinates> CellsBehind { get; }

    }
}