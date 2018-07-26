using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileButton : Button {

    /// <summary>当前帧被按下</summary>
    public bool IsDowned {
        get; private set;
    }

    public bool IsUped {
        get; private set;
    }

    /// <summary>当前是否是点击状态</summary>
    public bool Pressed {
        get {
            return IsPressed();
        }
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        IsDowned = true;
    }

    public override void OnPointerUp(PointerEventData eventData) {
        base.OnPointerUp(eventData);
        IsUped = true;
    }

    private void FixedUpdate() {
        IsDowned = false;
        IsUped = false;
    }

}
