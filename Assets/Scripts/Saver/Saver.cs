using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public class Saver : MonoBehaviour, ISave {

    private const string saveFolder = "";
    private const string saveFileName = "save.dat";
    private SaveData saveData;

    public static Saver Instance {
        private set; get;
    }

    public LevelPassData GetLevelSaveData(string levelSceneName) {
        return saveData.Data[levelSceneName];
    }

    private void Awake() {
        Instance = this;
        Init();
    }

    private async void Init() {
        try {
            //获取本地记录
            await Apply();
        }
        catch (Exception) {
            //建立空档
            saveData = new SaveData(new Dictionary<string, LevelPassData>());
            await Save();
        }
    }

    /// <summary>更新存档 </summary>
    public async void UpdateRecord(string levelSceneName, float duration) {
        saveData.Data[levelSceneName] = new LevelPassData {
            LevelSceneName = levelSceneName,
            TimeStamp = DateTime.Now.DateTimeToUnixStamp(),
            Duration = duration
        };
        await Save();
    }

    /// <summary>读</summary>
    private async Task Apply() {
        string content = await Utility.ReadInPresidentAsync(saveFolder, saveFileName);
        saveData = JsonConvert.DeserializeObject<SaveData>(Utility.Decrypt(content));
    }

    /// <summary>存</summary>
    private async Task Save() {
        string content = Utility.Encrypt(saveData.ToString());
        await Utility.WriteInPresidentAsync(saveFolder, saveFileName, content);
    }

}
