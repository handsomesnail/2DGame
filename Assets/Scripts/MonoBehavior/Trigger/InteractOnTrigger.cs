using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractOnTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;

    public TriggerType triggerType = TriggerType.Once;

    private bool already_Trigger = false;

    public LayerMask triggerLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled)
            return;

        if (already_Trigger && triggerType == TriggerType.Once)
            return;

        if (triggerLayer.Contains(other.gameObject))
            ExeTriggerEnter(other);
        already_Trigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled)
            return;

        if (triggerLayer.Contains(other.gameObject))
            ExeTriggerExit(other);
    }

    private void ExeTriggerEnter(Collider2D other)
    {
        OnTriggerEnter.Invoke();
    }

    private void ExeTriggerExit(Collider2D other)
    {
        OnTriggerExit.Invoke();
    }

}
