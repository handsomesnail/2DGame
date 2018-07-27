using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractOnButtonPressTrigger : MonoBehaviour
{
    public virtual void Trigger()
    {
        SpecificTrigger();
    }

    protected abstract void SpecificTrigger();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInteract playerInteract = collision.GetComponent<PlayerInteract>();
            if (playerInteract != null)
            {
                playerInteract.SetCurTrigger(this);
            }
            else
            {
                Debug.LogError("Player没有添加PlayerInteract");
            }
        }
    }
}
