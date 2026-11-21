using System;
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class BaseContext : IContext
    {
        private Dictionary<Type, object> _modules;

        public BaseContext()
        {
            _modules = new Dictionary<Type, object>();
        }

        public void AddModule<T>(T module)
        {
            var type = typeof(T);
            if (_modules.TryAdd(type, module))
            {
#if UNITY_EDITOR
                Debug.Log($"Adding {type.Name} to context");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"Duplicate module with type: {type.Name}");
#endif
            }
        }

        public T GetModule<T>()
        {
            if (_modules.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }
            else
            {
                Debug.Log($"Cant module with type: {typeof(T)}");
                return default(T);
            }
        }

        public void RemoveModule<T>()
        {
            if (_modules.ContainsKey(typeof(T)))
            {
                _modules.Remove(typeof(T));
            }
        }
    }
}
