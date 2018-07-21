using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButton : Button {

    /// <summary>当前是否是点击状态</summary>
    public bool Pressed {
        get {
            return IsPressed();
        }
    }

}
