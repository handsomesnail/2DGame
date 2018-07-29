using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class SpriteAnimation : MonoBehaviour {

    public float speed = 1;
    public Vector3 moveDistance;
    public float betweenloopDuration = 0;//每个循环的间隔
    public Sprite[] sprites;
    private Image image;
    private int index = 0;
    private Vector3 oriLocalPos;
    private void Awake() {
        image = GetComponent<Image>();
        oriLocalPos = transform.localPosition;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(Play());
    }
    private IEnumerator Play() {
        while (true) {
            image.sprite = sprites[index % sprites.Length];
            index++;
            if(index % sprites.Length == 0 && betweenloopDuration != 0) {
                transform.DOLocalMove(moveDistance, betweenloopDuration).SetEase(Ease.OutQuart).SetRelative(true).OnComplete(() => {
                    transform.localPosition = oriLocalPos;
                });
                yield return new WaitForSeconds(betweenloopDuration);
            }
            yield return new WaitForSeconds(1/speed);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
