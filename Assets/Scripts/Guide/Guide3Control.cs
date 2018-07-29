using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide3Control : MonoBehaviour {

    public GameObject guideCanvas;

    public static Guide3Control Instance {
        get; private set;
    }

    private void Awake() {
        Instance = this;
    }

    public void InvokeGuide() {
        guideCanvas.SetActive(true);
    }

    public void Complete() {

    }
}
