using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsTriggerOBP : InteractOnButtonPress
{
    public UnityEvent OnInteract;

    public override void SpecificInteract()
    {
        OnInteract.Invoke();
    }
}
