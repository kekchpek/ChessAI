using System;
using KChess.Auxiliary.WeakReferenceDecorator;

namespace KChess.Auxiliary.WeakReferenceDecorator
{
    public class TransientWeakReference<T> : IWeakReferenceDecorator<T> where T : class
    {
        private readonly WeakReference<T> _weakReference;
        
        public TransientWeakReference(T target)
        {
            _weakReference = new WeakReference<T>(target);
        }

        public TransientWeakReference(T target, bool trackResurrection)
        {
            _weakReference = new WeakReference<T>(target, trackResurrection);
        }

        public TransientWeakReference(WeakReference<T> weakReference)
        {
            _weakReference = weakReference;
        }

        public void SetTarget(T target)
        {
            _weakReference.SetTarget(target);
        }

        public bool TryGetTarget(out T target)
        {
            return _weakReference.TryGetTarget(out target);
        }

        public static bool operator ==(TransientWeakReference<T> ref1, TransientWeakReference<T> ref2)
        {
            if (ref1 is null && ref2 is null)
                return true;
            return ref1 is not null &&
                   ref2 is not null &&
                   ref1._weakReference.TryGetTarget(out var target1) &&
                   ref2._weakReference.TryGetTarget(out var target2) &&
                   target1 == target2;
        }

        public static bool operator !=(TransientWeakReference<T> ref1, TransientWeakReference<T> ref2)
        {
            return !(ref1 == ref2);
        }

        public override bool Equals(object obj)
        {
            return obj != null && GetHashCode() == obj.GetHashCode();
        }
        
        public override int GetHashCode()
        {
            return (_weakReference != null && _weakReference.TryGetTarget(out var target) ? target.GetHashCode() : 0);
        }
    }
}