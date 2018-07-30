using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Guide1Control : MonoBehaviour {

    public GameObject guideCanvas;
    public bool Completed = false;
    public bool isGuiding = false;

    public static Guide1Control Instance {
        get; private set;
    }

    private void Awake() {
        Instance = this;
    }

    public void InvokeGuide() {
        //InputManager.Instance.Interactable = false;
        InputManager.Instance.Buttons.SetActive(false);
        InputManager.Instance.MoveJoyStick.gameObject.SetActive(false);
        guideCanvas.SetActive(true);
        isGuiding = true;
    }

    public void Complete() {
        InputManager.Instance.Buttons.SetActive(true);
        InputManager.Instance.MoveJoyStick.gameObject.SetActive(true);
        Completed = true;
        guideCanvas.SetActive(false);
        Guide2Control.Instance.InvokeGuide();
        SplitSceneController2.splitScenes.Where((scene) => scene.gameObject.name == "Level-1").First().allowEnter = false;
    }
    
}
