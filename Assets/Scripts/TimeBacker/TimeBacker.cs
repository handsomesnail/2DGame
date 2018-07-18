﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>默认基类实现</summary>
public class TimeBacker : MonoBehaviour, ITimeBacker {

    private LinkedList<Action> inverseFrames;//需要倒退的业务

    protected Action currentInverseFrames;

    protected virtual void Awake() {
        inverseFrames = new LinkedList<Action>();
    }

    protected virtual void Start() {
        TimeBackManager.Instance.AddTimeBacker(this);
    }

    protected virtual void OnDestroy() {
        TimeBackManager.Instance.RemoveTimeBacker(this);
    }

    protected virtual void Update() {
        if (TimeBackManager.Instance.IsBacking) {
            return;
        }
        if (inverseFrames.Count == TimeBackManager.MaxFrameCount) {
            inverseFrames.RemoveFirst();
        }
        currentInverseFrames = () => { };//默认为空
        inverseFrames.AddLast(currentInverseFrames);
    }
    

    public virtual void OnTimePause() {
        enabled = false;
    }

    public virtual void OnTimeResume() {
        enabled = true;
    }

    public virtual void B_Update() {

        Delegate[] invocationList = inverseFrames.Last.Value.GetInvocationList();
        inverseFrames.RemoveLast();
        for (int i = invocationList.Length - 1; i >= 0; i--) {
#if UNITY_EDITOR || _Safe
            try {
                (invocationList[i] as Action)();
            }
            catch(Exception e) {
                Debug.LogError(e);
            }
#else
            (invocationList[i] as Action)();
#endif
        }
    }


}
