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

    private string sceneCache;
    private AsyncOperation loadAsyncOperationCache;

    public static SceneManager Instance {
        private set; get;
    }

    private void Awake() {
        Instance = this;
        StartConvertScene = (sceneName) => { };
        EndConvertScene = (sceneName) => { };
        DontDestroyOnLoad(this);
        sceneCache = "NULL";
    }

    //可以避免切换的第一个波峰 在预加载场景之前先预加载资源
    /// <summary>预加载场景 </summary>
    public IEnumerator LoadSceneAsync(string sceneName) {
        sceneCache = sceneName;
        loadAsyncOperationCache = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loadAsyncOperationCache.allowSceneActivation = false;
        while (!loadAsyncOperationCache.isDone) {
            yield return loadAsyncOperationCache;
        }
    }

    /// <summary>切换到目标场景 </summary>
    public IEnumerator ConvertSceneAsync(string sceneName, Action completeCallback) {
        bool isCached = !string.IsNullOrEmpty(sceneCache) && sceneName == sceneCache;
        AsyncOperation asyncOperation;
        if (isCached) {
            asyncOperation = loadAsyncOperationCache;
        }
        else {
            asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;
        }
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
        while (!isCached && !asyncOperation.isDone) {
            yield return asyncOperation;
        }
    }

    /// <summary>切换到目标场景 </summary>
    public IEnumerator ConvertSceneAsync(string sceneName) {
        yield return ConvertSceneAsync(sceneName, ()=> {
            //Saver.Instance.Save(); //自动存档
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
        StartCoroutine(ConvertSceneAsync(loadNextScene));
    }

#endif

}
