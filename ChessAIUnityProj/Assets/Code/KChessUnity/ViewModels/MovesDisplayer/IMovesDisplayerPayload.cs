using KChessUnity.Models.Board;
using UnityMVVM.ViewModelCore;

namespace KChessUnity.ViewModels.MovesDisplayer
{
    public interface IMovesDisplayerPayload : IPayload
    {
        IBoardWorldPositionsCalculator BoardWorldPositionsCalculator { get; }
    }
}