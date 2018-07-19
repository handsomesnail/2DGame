using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class SaveData  {

    public string LevelScene;//当前关卡
    public int TimeStamp;//存档时间戳

    public override string ToString() {
        return JsonConvert.SerializeObject(this);
    }



}
