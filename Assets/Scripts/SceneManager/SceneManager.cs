using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

[DisallowMultipleComponent]
public class SceneManager : MonoBehaviour {

    private Dictionary<string, AsyncOperation> sceneAsyncOperations;
   
    public event Action<string> StartConvertScene;
    public event Action<string> EndConvertScene;

    public static SceneManager Instance {
        private set; get;
    }

    private void Awake() {
        Instance = this;
        sceneAsyncOperations = new Dictionary<string, AsyncOperation>();
        StartConvertScene = (sceneName) => { };
        EndConvertScene = (sceneName) => { };
        DontDestroyOnLoad(this);
    }

    /// <summary>预加载场景 </summary>
    public IEnumerator LoadSceneAsync(string sceneName) {
        if (sceneAsyncOperations.ContainsKey(sceneName)) {
            throw new Exception("重复加载场景" + sceneName);
        }
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName/*,LoadSceneMode.Single*/);
        sceneAsyncOperations.Add(sceneName, asyncOperation);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone) {
            //进度asyncOperation.progress
            yield return asyncOperation;
        }
    }

    //(均使用Single模式加载场景所以这个方法应该用不到)
    /// <summary>异步卸载场景 </summary>
    public IEnumerator UnLoadSceneAsync(string sceneName) {
        if (!sceneAsyncOperations.ContainsKey(sceneName)) {
            throw new Exception("未加载该场景"+ sceneName);
        }
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncOperation.isDone) {
            yield return asyncOperation;
        }
        sceneAsyncOperations.Remove(sceneName);
    }

    /// <summary>切换到目标场景 </summary>
    public void ConvertScene(string sceneName, Action completeCallback) {
        AsyncOperation asyncOperation;
        if (!sceneAsyncOperations.TryGetValue(sceneName, out asyncOperation)) {
            throw new Exception("未加载该场景" + sceneName);
        }
        StartConvertScene(sceneName);
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneAsyncOperations.Remove(currentScene.name);
        GameObject ConvertSceneMask = Resources.Load<GameObject>("ConvertSceneMask");
        GameObject mask = Instantiate(ConvertSceneMask);
        DontDestroyOnLoad(mask);
        Image maskImage = mask.transform.Find("Panel").GetComponent<Image>();
        maskImage.DOFade(1, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            asyncOperation.allowSceneActivation = true;
            maskImage.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
                EndConvertScene(sceneName);
                Destroy(mask);
                completeCallback();
            });
        });
    }

    /// <summary>切换到目标场景 </summary>
    public void ConvertScene(string sceneName) {
        ConvertScene(sceneName, ()=> {
            Saver.Instance.Save(); //自动存档
        });
    }

#if UNITY_EDITOR
    public string convertNextScene;
    [ContextMenu("ConvertScene")]
    public void ConvertScene() {
        ConvertScene(convertNextScene);
    }
    public string loadNextScene;
    [ContextMenu("LoadScene")]
    public void LoadScene() {
        StartCoroutine(LoadSceneAsync(loadNextScene));
    }
#endif

}
