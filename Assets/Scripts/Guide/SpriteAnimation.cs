using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class SpriteAnimation : MonoBehaviour {

    public float speed = 1;
    public Transform moveTarget;
    public float betweenloopDuration = 0;//每个循环的间隔
    public float afterLoopDelay = 0;
    public int loopTimes = -1;
    public Sprite[] sprites;
    private Image image;
    private int index = 0;
    private Vector3 oriLocalPos;
    private Coroutine coroutine;
    public UnityEvent OnOver;  
    private void Awake() {
        image = GetComponent<Image>();
        oriLocalPos = transform.localPosition;
    }

    // Use this for initialization
    void Start () {
        coroutine = StartCoroutine(Play());
    }
    private IEnumerator Play() {
        while (true) {
            image.sprite = sprites[index % sprites.Length];
            index++;
            if(index % sprites.Length == 0) {
                if (betweenloopDuration != 0) {
                    transform.DOMove(moveTarget.position-transform.position, betweenloopDuration).SetEase(Ease.OutQuart).SetRelative(true).OnComplete(() => {
                        transform.localPosition = oriLocalPos;
                    });
                    yield return new WaitForSeconds(betweenloopDuration);
                }
                loopTimes--;
                if (loopTimes == 0) {
                    StopCoroutine(coroutine);
                    OnOver.Invoke();
                }
            }
            yield return new WaitForSeconds(1/speed);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
