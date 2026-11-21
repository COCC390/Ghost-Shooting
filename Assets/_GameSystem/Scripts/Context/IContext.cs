using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IContext
    {
        void AddModule<T>(T module);
        T GetModule<T>();
        void RemoveModule<T>();
    }
}
