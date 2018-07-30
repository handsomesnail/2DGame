using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractOnButtonPress : MonoBehaviour
{

    public ItemType itemType;
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
            if (playerInteract == null) {
                throw new System.Exception("玩家无法进行道具交互");
            }
            //满足道具条件
            if (ItemManager.Instance.currentItem == itemType) {
                ItemManager.Instance.EnterItemTrigger();
                playerInteract.SetInteractItem(this);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInteract = collision.GetComponent<PlayerInteract>();
            if (playerInteract == null) {
                throw new System.Exception("玩家无法进行道具交互");
            }
            //满足道具条件
            if (ItemManager.Instance.currentItem == itemType) {
                ItemManager.Instance.ExitItemTrigger();
                playerInteract.EmptyInteract(this);
            }

        }
    }
}
