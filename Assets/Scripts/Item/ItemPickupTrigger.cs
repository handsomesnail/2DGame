using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickupTrigger : MonoBehaviour {

    public ItemType itemType;

    private void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Enter");
        if (collision.CompareTag("Player")) {
            ItemManager.Instance.EnterItem(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ItemManager.Instance.ExitItem(this);
        }
    }

}
