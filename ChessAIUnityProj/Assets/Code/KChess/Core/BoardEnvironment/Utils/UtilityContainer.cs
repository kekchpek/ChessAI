using System;
using System.Collections.Generic;

namespace KChess.Core.BoardEnvironment.Utils
{
    internal class UtilityContainer : IMutableUtilityContainer
    {

        private readonly IDictionary<Type, object> _utils = new Dictionary<Type, object>();

        public T Get<T>()
        {
            var type = typeof(T);
            if (_utils.TryGetValue(type, out var util))
            {
                return (T)util;
            }

            throw new Exception($"Utility {type.Name} is not added to the container.");
        }

        public void Add<T>(T utility)
        {
            var type = typeof(T);
            if (_utils.ContainsKey(type))
            {
                throw new Exception($"Utility {type.Name} is already added to the container.");
            }
            
            _utils.Add(type, utility);
        }

        public void Dispose()
        {
            foreach (var utility in _utils.Values)
            {
                if (utility is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}