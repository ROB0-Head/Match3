using System.Collections.Generic;
using System.IO;
using Match3.Settings;
using Newtonsoft.Json;
using Settings;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveManager
    {
        private static readonly string CHARACTER_DATA_NAME = "LevelsData.json";


        #region Gloval Save And Load Methods

        public static void SaveData<T>(T data, string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }
        
        #endregion

        #region Public Save & Load Method

        public static void SaveLevelsData(List<Level> levelsData)
        {
            SaveData(levelsData, CHARACTER_DATA_NAME);
        }

        public static List<Level> LoadLevelsData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, CHARACTER_DATA_NAME);
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Level>>(jsonData);
            }
            else
            {
                return SettingsProvider.Get<LevelsData>().Tasks;
            }
        }
        
        #endregion
    }
}