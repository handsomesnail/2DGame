using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConvertSceneReaction))]
public class ConvertSceneReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel() {
        return "Convert Scene";
    }

}

[CustomEditor(typeof(LoadSceneReaction))]
public class LoadSceneReactionEditor : ReactionEditor {
    protected override string GetFoldoutLabel() {
        return "Load Scene";
    }

}
