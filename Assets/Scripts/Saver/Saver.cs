using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Saver : MonoBehaviour, ISave {

    private const string saveFolder = "";
    private const string saveFileName = "save.dat";

    public static Saver Instance {
        private set; get;
    }

    private void Awake() {
        Instance = this;
    }

    /// <summary>读档</summary>
    public async void Apply() {
        string content = await Utility.ReadInPresidentAsync(saveFolder, saveFileName);
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(Utility.Decrypt(content));
        Action loadCallback = () => {
            //将数据应用到Player
            //UI显示读档成功
        };
        SceneManager.Instance.ConvertScene(saveData.LevelScene,loadCallback);
    }

    /// <summary>存档</summary>
    public async void Save() {
        SaveData saveData = new SaveData {
            LevelScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            TimeStamp = DateTime.Now.DateTimeToUnixStamp()
        };
        string content = Utility.Encrypt(saveData.ToString());
        await Utility.WriteInPresidentAsync(saveFolder, saveFileName, content);
        //UI显示存档成功
    }
}
