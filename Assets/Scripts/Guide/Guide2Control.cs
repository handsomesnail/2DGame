using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide2Control : MonoBehaviour {

    public GameObject guideCanvas;

    public static Guide2Control Instance {
        get; private set;
    }

    private void Awake() {
        Instance = this;
    }

    public void InvokeGuide() {
        StartCoroutine(WaitInvoke());
    }

    public IEnumerator WaitInvoke() {
        yield return new WaitForSeconds(2.0f);
        guideCanvas.SetActive(true);
    }


    public void Complete() {

    }

}
