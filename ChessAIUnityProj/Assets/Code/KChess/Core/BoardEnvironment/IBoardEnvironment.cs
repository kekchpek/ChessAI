using System;
using KChess.Core.API.PlayerFacade;
using KChess.Domain;

namespace KChess.Core.BoardEnvironment
{
    internal interface IBoardEnvironment : IDisposable
    {
        IBoard Board { get; }
        IPlayerFacade BlackPlayerFacade { get; }
        IPlayerFacade WhitePlayerFacade { get; }
        IPlayerFacade UniversalPlayerFacade { get; }
    }
}