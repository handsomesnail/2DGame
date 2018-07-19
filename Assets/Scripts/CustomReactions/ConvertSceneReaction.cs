using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertSceneReaction : Reaction {

    public string sceneName;

    protected override void ImmediateReaction() {
        SceneManager.Instance.ConvertScene(sceneName);
    }
}

