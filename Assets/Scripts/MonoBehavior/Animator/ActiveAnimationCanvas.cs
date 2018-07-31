using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAnimationCanvas : MonoBehaviour
{
    public Animator canvasAnimator;

    public readonly string animatorKey = "Active";
       

    public void ActiveCanvas()
    {
        canvasAnimator.SetBool(animatorKey, true);
    }

    public void DeactiveCanvas()
    {
        canvasAnimator.SetBool(animatorKey, false);
    }
}
