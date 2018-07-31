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

    public GameObject lightObject;
    public GameObject darkObejct;

    public bool hasStarted = false;

    [SerializeField]
    private AudioSource audioSource;

    public AudioSource rainAudio;

    public float[] distance;

    private void Awake() {
        
    }

    private void Start() {
        rainAudio.Play();
        rainAudio.DOFade(1.0f, 6.0f).SetEase(Ease.InQuart);
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
                StartCoroutine(SceneManager.Instance.LoadSceneAsync("Comic"));
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

    [ContextMenu("StartGame")]
    public void StartGame() {
        if (hasStarted) {
            return;
        }
        hasStarted = true;
        StartCoroutine(Bling());
    }

    private IEnumerator Bling() {
        //放音乐
        yield return new WaitForSeconds(distance[0]);
        audioSource.Play();
        SwitchLight(true);
        yield return new WaitForSeconds(distance[1]);
        SwitchLight(false);
        yield return new WaitForSeconds(distance[2]);

        SwitchLight(true);
        yield return new WaitForSeconds(distance[3]);
        SwitchLight(false);
        yield return new WaitForSeconds(distance[4]);

        SwitchLight(true);
        yield return new WaitForSeconds(distance[5]);
        SwitchLight(false);
        yield return new WaitForSeconds(distance[6]);

        SwitchLight(true);
        yield return new WaitForSeconds(distance[7]);
        SwitchLight(false);
        yield return new WaitForSeconds(distance[8]);

        SwitchLight(true);
        yield return new WaitForSeconds(distance[9]);

        StartCoroutine(SceneManager.Instance.ConvertSceneAsync("Comic"));
    }

    private void SwitchLight(bool isLight) {
        lightObject.SetActive(isLight);
        darkObejct.SetActive(!isLight);
    }

}
