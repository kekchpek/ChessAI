using UnityMVVM.ViewModelCore.Bindable;

namespace KChessUnity.Core.Camera
{
    public interface ICameraModel
    {
        IBindable<UnityEngine.Camera> CurrenCamera { get; }
    }
}