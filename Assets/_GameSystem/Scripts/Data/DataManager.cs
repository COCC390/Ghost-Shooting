using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class DataManager : IDataManager
    {
        private Dictionary<Type, IDataRecord> _runtimeData;
        private GameSystemConfigs _gameSystemConfig;
        private string _dataPath;

        public DataManager(GameSystemConfigs gameSystemConfig)
        {
            _gameSystemConfig = gameSystemConfig;
            _dataPath = Path.Combine(Application.persistentDataPath, _gameSystemConfig.DataPathConfig.FolderName);
        }

        public IDataRecord GetData(Type type)
        {
            if (_runtimeData.ContainsKey(type))
            {
                return _runtimeData[type];
            }
            else
            {
                return null;
            }
        }

        public T LoadData<T>(string dataFolderDefinitionName) where T : IDataRecord
        {
            string dataName = typeof(T).Name;
            var folderPath = Path.Combine(_dataPath, dataFolderDefinitionName);
            var pathToData = Path.Combine(folderPath, $"{dataName}.json");

            string data = "";
            if (File.Exists(pathToData))
            {
                data = File.ReadAllText(pathToData);
                if (!string.IsNullOrEmpty(data))
                {
                    try
                    {
                        T t = JsonConvert.DeserializeObject<T>(data);
                        t.SetSaver(this);
                        return t;
                    }
                    catch (JsonException ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }

            T t1 = Activator.CreateInstance<T>();
            t1.SetSaver(this);
            return t1;
        }

        public List<T> LoadDatas<T>(string dataFolderDefinitionName) where T : IDataRecord
        {
            string dataName = typeof(T).Name;
            var folderPath = Path.Combine(_dataPath, dataFolderDefinitionName);

            List<T> datas = new();
            var allFiles = GetAllFiles(folderPath);

            foreach (var file in allFiles)
            {
                var data = File.ReadAllText(file);
                if (!string.IsNullOrEmpty(data))
                {
                    try
                    {
                        T t = JsonConvert.DeserializeObject<T>(data);
                        t.SetSaver(this);
                        datas.Add(t);
                    }
                    catch (JsonException ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
            return datas;
        }

        private string[] GetAllFiles(string path)
        {
            if (!Directory.Exists(path))
                return System.Array.Empty<string>();

            return Directory
                .GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(path =>
                    path.EndsWith(".json", System.StringComparison.OrdinalIgnoreCase))
                    //path.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }

        public void SaveData(IDataRecord data, string dataPathDefiniton)
        {
            if (data == null)
                return;
            var jsonData = JsonConvert.SerializeObject(data);
            string dataName = data.GetType().Name;
            var folderPath = Path.Combine(_dataPath, dataPathDefiniton);
            var pathToData = Path.Combine(folderPath, $"{dataName}.json");
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllText(pathToData, jsonData);
        }
    }
}
