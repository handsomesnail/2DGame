using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

/// <summary>关卡通关数据</summary>
public struct LevelPassData {
    public string LevelSceneName;//关卡场景名
    public int TimeStamp;//存档时间戳
    public float Duration;//通关消耗时间
}

//各个关卡的通关时间 存档时间
public struct SaveData  {

    public Dictionary<string, LevelPassData> Data;

    public SaveData(Dictionary<string, LevelPassData> data) {
        this.Data = data;
    }

    public override string ToString() {
        return JsonConvert.SerializeObject(this);
    }



}
