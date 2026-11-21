using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IDataRecord
    {
        void SetSaver(IDataSaver saver);
        void RecordSave(string folderName);
    }
}
