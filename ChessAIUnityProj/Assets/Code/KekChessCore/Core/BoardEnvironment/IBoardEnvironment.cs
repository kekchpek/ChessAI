using System;
using KekChessCore.API.PlayerFacade;

namespace KekChessCore.BoardEnvironment
{
    public interface IBoardEnvironment : IDisposable
    {
        IPlayerFacade BlackPlayerFacade { get; }
        IPlayerFacade WhitePlayerFacade { get; }
    }
}