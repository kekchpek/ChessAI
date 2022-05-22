using System;

namespace MVVMCore
{
    public interface IViewModel
    {
        internal void SubscribeForProperty<T>(string propertyName, Action<T> changeCallback);
        void ClearSubscriptions();
    }
}