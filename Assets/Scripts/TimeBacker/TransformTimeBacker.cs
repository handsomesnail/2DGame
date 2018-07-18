using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform)),DisallowMultipleComponent]
public sealed class TransformTimeBacker : MonoBehaviour, ITimeBacker {

    private LinkedList<PhysicFrameInfo> physicFrameInfos;//物理帧

    private void Awake() {
        physicFrameInfos = new LinkedList<PhysicFrameInfo>();
    }

    private void Start() {
        TimeBackManager.Instance.AddTimeBacker(this);
    }

    private void OnDestroy() {
        TimeBackManager.Instance.RemoveTimeBacker(this);
    }

    private void Update() {
        if (TimeBackManager.Instance.IsBacking) {
            return;
        }
        if (physicFrameInfos.Count == TimeBackManager.MaxFrameCount) {
            physicFrameInfos.RemoveFirst();
        }
        PhysicFrameInfo physicInfo = transform.GetPhysicInfo();
        physicFrameInfos.AddLast(physicInfo);
    }

    private void FixedUpdate() {
        if (TimeBackManager.Instance.IsBacking) {
            return;
        }
    }

    public void OnTimePause() {
        enabled = false;
    }

    public void OnTimeResume() {
        enabled = true;
    }

    public void B_Update() {
        PhysicFrameInfo physicFrameInfo = physicFrameInfos.Last.Value;
        physicFrameInfos.RemoveLast();
        transform.SetPhysicInfo(physicFrameInfo);
    }

}
