using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneReaction : Reaction {

    public string sceneName;

    protected override void ImmediateReaction(MonoBehaviour monoBehaviour) {
        monoBehaviour.StartCoroutine(SceneManager.Instance.LoadSceneAsync(sceneName));
    }

    protected override void ImmediateReaction() {

    }

}
