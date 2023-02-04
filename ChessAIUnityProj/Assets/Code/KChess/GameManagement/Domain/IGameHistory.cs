using System;
using System.Collections.Generic;
using KChess.Domain;

namespace KChess.GameManagement.Domain
{
    public interface IGameHistory
    {
        event Action Changed;
        
        Position InitialPosition { get; }
        
        IReadOnlyList<IMove> Moves { get; }
        
        GameResult? GameResult { get; }
    }
}