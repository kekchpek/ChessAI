using KChess.Domain;
using KChess.GameManagement.Domain;

namespace KChess.Core.BoardEnvironment.Factory
{
    internal interface IBoardEnvironmentFactory
    {
        IBoardEnvironment Create();
        IBoardEnvironment Create(Position specificPosition);
    }
}