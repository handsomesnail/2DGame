using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer)),DisallowMultipleComponent]
public class BlingRender : MonoBehaviour {

    public bool playOnAwake = true;
    public float speed = 1;
    public float startValue = 0;
    public float endValue = 1;
    private new SpriteRenderer renderer;

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        if (playOnAwake) {
            Play();
        }
    }

    public void Play() {
        FadeOut();
    }

    //淡出
    private void FadeOut() {
        renderer.DOFade(startValue, 1/speed).OnComplete(() => {
            FadeIn();
        });
    }

    //淡入
    private void FadeIn() {
        renderer.DOFade(endValue, 1/speed).OnComplete(() => {
            FadeOut();
        });
    }

}
