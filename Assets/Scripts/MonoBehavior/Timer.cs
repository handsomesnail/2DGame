using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private bool isStarting = false;

    public float timeGap = 3.0f;
    private float timer = 0.0f;

    public UnityEvent OnStartTimer;
    public UnityEvent OnEndTimer;

    private void Update()
    {
        if (isStarting)
            timer += Time.deltaTime;

        if (timer > timeGap)
            EndTimer();
    }

    public void StartTimer()
    {
        isStarting = true;
        OnStartTimer.Invoke();
    }

    public void EndTimer()
    {
        isStarting = false;
        timer = 0.0f;
        OnEndTimer.Invoke();
    }
}
