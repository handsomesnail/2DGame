using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeBacker {

    /// <summary>逻辑暂停</summary>
    void OnTimePause();

    /// <summary>逻辑恢复</summary>
    void OnTimeResume();

    /// <summary>时间倒流对应的Update</summary>
    void B_Update();


}
