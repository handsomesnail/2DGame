using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IndexTrigger : MonoBehaviour
{
    public int index = 0;

    public UnityEvent OnIndexTrigger;

    public void ActiveOnce()
    {
        index++;
    }

    public void CheckIndex()
    {
        if (index >= 2)
            OnIndexTrigger.Invoke();
    }
}
