using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ItemPickupTrigger : MonoBehaviour {

    public ItemType itemType;

    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Enter");
        if (collision.CompareTag("Player")) {
            ItemManager.Instance.EnterItem(this);
            OnEnter.Invoke();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            ItemManager.Instance.ExitItem(this);
            OnExit.Invoke();
        }
    }

}
