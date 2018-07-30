using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        SplitSceneController2.splitScenes.Where((scene) => scene.gameObject.name == "Level-1").First().allowEnter = true;
        guideCanvas.SetActive(true);
    }


    public void Complete() {

    }

}
