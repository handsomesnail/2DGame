using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

[DisallowMultipleComponent]
public class SceneManager : MonoBehaviour {

    public event Action<string> StartConvertScene;
    public event Action<string> EndConvertScene;

    public static SceneManager Instance {
        private set; get;
    }

    private void Awake() {
        Instance = this;
        StartConvertScene = (sceneName) => { };
        EndConvertScene = (sceneName) => { };
        DontDestroyOnLoad(this);
    }

    /// <summary>切换到目标场景 </summary>
    public IEnumerator LoadSceneAsync(string sceneName, Action completeCallback) {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        StartConvertScene(sceneName);
        GameObject ConvertSceneMask = Resources.Load<GameObject>("ConvertSceneMask");
        GameObject mask = Instantiate(ConvertSceneMask);
        DontDestroyOnLoad(mask);
        Image maskImage = mask.transform.Find("Panel").GetComponent<Image>();
        maskImage.DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            asyncOperation.allowSceneActivation = true;
            Action LoadCallBack =() => maskImage.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                EndConvertScene(sceneName);
                Destroy(mask);
                completeCallback();
            });
            StartCoroutine(WaitAsyncOperationDone(asyncOperation, LoadCallBack));
        });
        yield return asyncOperation;
    }

    /// <summary>切换到目标场景 </summary>
    public IEnumerator LoadSceneAsync(string sceneName) {
        yield return LoadSceneAsync(sceneName, ()=> {
            Saver.Instance.Save(); //自动存档
        });
    }

    private IEnumerator WaitAsyncOperationDone(AsyncOperation asyncOperation, Action callBack) {
        while (!asyncOperation.isDone) {
            yield return new WaitForEndOfFrame();
        }
        callBack();
    }

#if UNITY_EDITOR
    public string loadNextScene;
    [ContextMenu("LoadScene")]
    public void LoadScene() {
        StartCoroutine(LoadSceneAsync(loadNextScene));
    }
#endif

}
