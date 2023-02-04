using KChess.Domain;
using KChess.GameManagement.Domain;
using UnityMVVM.ViewModelCore.Bindable;

namespace KChess.GameManagement.Core
{
    public interface IGameHistoryManager : IGameHistoryBuilder
    {
        IBindable<Position> Position { get; }
        IBoard Board { get; }
        IGameHistory History { get; }
    }
}