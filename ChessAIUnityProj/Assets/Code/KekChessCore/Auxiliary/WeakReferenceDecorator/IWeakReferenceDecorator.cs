using System.Runtime.Serialization;

namespace KekChessCore.Auxiliary.WeakReferenceDecorator
{
    public interface IWeakReferenceDecorator<T> where T : class
    {
        public void SetTarget(T target);
        public bool TryGetTarget(out T target);
    }
}