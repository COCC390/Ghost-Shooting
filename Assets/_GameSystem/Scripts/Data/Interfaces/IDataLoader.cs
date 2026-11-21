using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IDataLoader
    {
        T LoadData<T>(string dataFolderDefinitionName) where T : IDataRecord;
        List<T> LoadDatas<T>(string dataFolderDefinitionName) where T : IDataRecord;
    }
}
