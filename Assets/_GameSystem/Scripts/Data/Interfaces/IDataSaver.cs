using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IDataSaver
    {
        void SaveData(IDataRecord data, string dataPathDefinition);
    }
}
