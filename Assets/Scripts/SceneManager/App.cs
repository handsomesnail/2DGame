using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class App : MonoBehaviour {

    public UnityEvent OnGameEntrance;

    public static App Instance {
        private set; get;
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        if (GetComponent<SceneManager>() == null) {
            gameObject.AddComponent<SceneManager>();
        }
    }

    private void Start() {
        GameEntrance();
    }

    /// <summary>全局游戏入口</summary>
    private void GameEntrance() {
        OnGameEntrance.Invoke();
    }

#if UNITY_EDITOR
    [ContextMenu("Test")]
    public void Test() {

    }
#endif

}
