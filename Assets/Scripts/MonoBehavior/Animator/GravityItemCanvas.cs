using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityItemCanvas : MonoBehaviour
{
    public Animator canvasAnimator;

    public readonly string animatorKey = "Active";

    public bool already_Taken = false;

    public void ActiveCanvas()
    {
        if (already_Taken)
            return;
        canvasAnimator.SetBool(animatorKey, true);
    }

    public void DeactiveCanvas()
    {
        if (already_Taken)
            return;
        canvasAnimator.SetBool(animatorKey, false);
    }

    public void TakeItem()
    {
        already_Taken = true;
    }
}
