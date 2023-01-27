using System;
using System.IO;
using Cysharp.Threading.Tasks;
using ExternalTools.Scripts.Utilities;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace ConveyorGame.Services.PlayerData
{
    public class PlayerDataService : ServiceBase
    {
        private string _path;

        public Data Data { get; private set; }
        private const string SAVE_FILE_NAME = "Save.json";

        public override UniTask InitializeAsync()
        {
            _path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            Data = LoadData();
            return base.InitializeAsync();
        }

        private Data LoadData()
        {
            try
            {
                if (File.Exists(_path))
                {
                    string dataString = Encryption.EncryptDecryptString(File.ReadAllText(_path));
                    var jsonSettings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    return JsonConvert.DeserializeObject<Data>(dataString, jsonSettings);
                }
            }
            catch (Exception)
            {
                return new Data();
            }

            return new Data();
        }


        private void SaveData()
        {
            Data ??= LoadData();
            File.WriteAllText(_path, Encryption.EncryptDecryptString(JsonConvert.SerializeObject(Data, Formatting.Indented)));
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }


        private void OnApplicationPause(bool pauseStatus)
        {
            SaveData();
        }
#if UNITY_EDITOR
        [MenuItem("Tools/Player Data/Delete save")]
        public static void DeleteSave()
        {
            var path = Path.Combine(Application.persistentDataPath, "Save.json");
            if (File.Exists(path))
                File.Delete(path);
        }

        [MenuItem("Tools/Player Data/Open save path")]
        public static void OpenSavePath()
        {
            if (Directory.Exists(Application.persistentDataPath))
                Application.OpenURL(Application.persistentDataPath);
        }

        [MenuItem("Tools/Player Data/Print save")]
        public static void PrintSave()
        {
            var path = Path.Combine(Application.persistentDataPath, "Save.json");
            if (File.Exists(path))
                Debug.Log(File.ReadAllText(path));
        }
#endif
    }
}