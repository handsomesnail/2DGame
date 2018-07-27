using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IndexTrigger : MonoBehaviour
{
    private int index = 0;
    public int maxIndex = 2;

    public UnityEvent OnTriggerStart;
    
    public void TriggerOnce()
    {
        index++;
        Check();
    }

    public void Reset()
    {
        index = 0;
    }

    public void Check()
    {
        if (index < maxIndex)
            return;

        OnTriggerStart.Invoke();
        
    }
}
