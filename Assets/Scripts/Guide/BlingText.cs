using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Text)), DisallowMultipleComponent]
public class BlingText : MonoBehaviour {

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {
        FadeOut();
    }

    //淡出
    private void FadeOut() {
        text.DOFade(0.2f, 0.8f).OnComplete(() => {
            FadeIn();
        });
    }
    //淡入
    private void FadeIn() {
        text.DOFade(1.0f, 0.8f).OnComplete(() => {
            FadeOut();
        });
    }


}
