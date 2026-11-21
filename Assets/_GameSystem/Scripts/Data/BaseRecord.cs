using System;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class BaseRecord : IDataRecord
    {
        private IDataSaver _saver;
        float recordVersion;
        string recordTime;

        public void SetSaver(IDataSaver saver)
        { 
            this._saver = saver; 
        }

        public virtual void RecordSave(string folderName)
        {
            recordVersion += .1f;
            DateTime now = DateTime.Now;
            recordTime = now.ToString("yyyy-MM-dd-HH-mm");

            _saver.SaveData(this, folderName);
        }
    }
}
