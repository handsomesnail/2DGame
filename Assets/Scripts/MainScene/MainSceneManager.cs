using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {

    public bool ShowOnStart = true;
    public float duration = 1.0f;
    public GameObject logo;
    public GameObject start;
   

    private void Awake() {
        
    }

    private void Start() {
        StartCoroutine(LoadLevel1());
    }

    private IEnumerator LoadLevel1() {
        yield return ResourceManager.LoadAsync("Level-1");
        yield return ResourceManager.LoadAsync("Level-2");
        yield return ResourceManager.LoadAsync("Level-3");
        Debug.Log("加载资源完成");
        if (ShowOnStart) {
            Show();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Show() {
        FadeIn(logo.transform, duration);
        logo.transform.DOLocalMoveY(30, duration).SetEase(Ease.OutQuart).SetRelative(true).OnComplete(() => {
            //加载一波资源 然后动Start
            FadeIn(start.transform, duration);
            start.transform.DOLocalMoveY(30, duration).SetEase(Ease.OutQuart).SetRelative(true).OnComplete(() => {
                StartCoroutine(SceneManager.Instance.LoadSceneAsync("Zone1 - 副本"));
            });
        });
    }

    private void FadeIn(Transform transform, float duration) {
        Image image = transform.GetComponent<Image>();
        if (image != null) {
            image.DOFade(transform.gameObject.name == "Light" ? 0.375f : 1.0f, duration);
        }
        foreach(Transform child in transform) {
            FadeIn(child, duration);
        }
    }

}
