using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleControl : MonoBehaviour
{
    public static TimeScaleControl Instance;

    private TimeScaleControl _instance
    {
        get
        {
            if (_instance == null)
                Instance = FindObjectOfType<TimeScaleControl>();
            if (_instance == null)
                Debug.LogError("不存在时间管理Singleton");

            return Instance;
        }
    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void StarTime()
    {
        Time.timeScale = 1.0f;
    }
}
