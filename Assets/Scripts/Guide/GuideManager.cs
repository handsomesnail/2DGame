using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GuideManager : MonoBehaviour {

    public bool IsGuiding = false; //当前是否是引导状态
    public int CurrentGuideIndex = 0;

    public bool[] CompleteProcess;
    public GameObject[] guideCanvas;

    public static GuideManager Instance {
        get; private set;
    }

    private void Awake() {
        Instance = this;
    }

    public void EnterGuide(int guideIndex) {
        CurrentGuideIndex = guideIndex;
        foreach (var canvas in guideCanvas) {
            canvas.SetActive(false);
        }
        guideCanvas[guideIndex].SetActive(true);
    }

    public void LeaveGuide() {
        CompleteProcess[CurrentGuideIndex] = true;
        foreach (var canvas in guideCanvas) {
            canvas.SetActive(false);
        }
    }


}
