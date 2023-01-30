namespace KChess.Core.BoardEnvironment.Utils
{
    internal interface IMutableUtilityContainer : IUtilityContainer
    {
        void Add<T>(T utility);
    }
}