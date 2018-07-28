using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractOnButtonPress : MonoBehaviour
{
    public virtual void Interact()
    {
        SpecificInteract();
    }

    public abstract void SpecificInteract();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInteract = collision.GetComponent<PlayerInteract>();
            if (playerInteract != null)
                playerInteract.SetInteractItem(this);
            else
                Debug.LogError("玩家无法进行道具交互");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInteract = collision.GetComponent<PlayerInteract>();
            if (playerInteract != null)
                playerInteract.EmptyInteract(this);
            else
                Debug.LogError("玩家无法进行道具交互");
        }
    }
}
