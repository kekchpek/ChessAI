using System;
using KChess.Core.API.PlayerFacade;

namespace KChess.Core.BoardEnvironment
{
    internal interface IBoardEnvironment : IDisposable
    {
        IPlayerFacade BlackPlayerFacade { get; }
        IPlayerFacade WhitePlayerFacade { get; }
    }
}