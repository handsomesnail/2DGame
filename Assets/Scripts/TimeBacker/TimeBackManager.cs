using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class TimeBackManager : MonoBehaviour {

    public const int MaxFrameCount = 300;//最多存300帧
    private int currentFrame;//当前存储帧数

    private int backSpeed;//倒流速度

    public static TimeBackManager Instance { private set; get; }

    private List<ITimeBacker> managedTimeBackers;

    /// <summary>当前是否是倒流状态</summary>
    public bool IsBacking {
        private set; get;
    }

    private void Awake() {
        Instance = this;
        managedTimeBackers = new List<ITimeBacker>();
        IsBacking = false;
        currentFrame = 0;
        backSpeed = 1;
        DontDestroyOnLoad(this);
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            StartTimeBack(1);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            StopTimeBack();
            return;
        }


        if (!IsBacking) {
            currentFrame = ++currentFrame > MaxFrameCount ? MaxFrameCount : currentFrame;
            return;
        }

        for (int i = 0; i < backSpeed; i++) {
            currentFrame = --currentFrame < 0 ? 0 : currentFrame;
            foreach (var timeBacker in managedTimeBackers) {
#if UNITY_EDITOR || _Safe
                try {
                    timeBacker.B_Update();
                }
                catch (Exception e) {
                    Debug.LogError(e);
                }
#else
            timeBacker.B_Update();
#endif
            }
            //倒流到存储最久前自动停止倒流
            if (currentFrame == 0) {
                StopTimeBack();
                return;
            }
        }

    }

    public void AddTimeBacker(ITimeBacker timeBacker) {
        managedTimeBackers.Add(timeBacker);
    }

    public void RemoveTimeBacker(ITimeBacker timeBacker) {
        managedTimeBackers.Remove(timeBacker);
    }

    /// <summary>开始倒流(默认1倍速)</summary>
    [ContextMenu("StartTimeBack")]
    public void StartTimeBack() {
        StartTimeBack(1);
    }

    /// <summary>开始倒流</summary>
    public void StartTimeBack(int backSpeed) {
        if (IsBacking == true) {
            throw new Exception("当前已是倒流状态");
        }
        IsBacking = true;
        this.backSpeed = backSpeed;
        foreach (var timeBacker in managedTimeBackers) {
#if UNITY_EDITOR || _Safe
            try {
                timeBacker.OnTimePause();
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
#else
            timeBacker.OnTimePause();
#endif
        }
    }

    /// <summary>停止倒流</summary>
    [ContextMenu("StopTimeBack")]
    public void StopTimeBack() {
        if (IsBacking == false) {
            throw new Exception("当前不是倒流状态");
        }
        IsBacking = false;
        foreach (var timeBacker in managedTimeBackers) {
#if UNITY_EDITOR || _Safe
            try {
                timeBacker.OnTimeResume();
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
#else
            timeBacker.OnTimeResume();
#endif
        }
    }

}
