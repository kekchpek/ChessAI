using System;

namespace KChess.Core.BoardEnvironment.Utils
{
    internal interface IUtilityContainer : IDisposable
    {
        T Get<T>();
    }
}