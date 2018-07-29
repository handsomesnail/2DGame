using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide1Trigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")&&!Guide1Control.Instance.Completed) {
            Guide1Control.Instance.InvokeGuide();
        }
    }

}
