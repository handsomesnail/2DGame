using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance {
        get; private set;
    }

    public int levelIndex;

    private void Awake() {
        Instance = this;
    }

}
