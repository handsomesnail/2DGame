using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskController : MonoBehaviour {

    private Vector3 oriPos;

    public float speed = 1;

    public void Awake() {
        oriPos = transform.position;
    }

    public void Start() {
        Roll();
    }

    private void Roll() {
        transform.DOMoveY(228, 1.0f/speed).SetRelative(true).SetEase(Ease.Linear).OnComplete(() => {
            transform.position = oriPos;
            Roll();
        });
    }

}
