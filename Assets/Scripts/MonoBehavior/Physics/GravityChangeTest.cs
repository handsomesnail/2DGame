using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeTest : MonoBehaviour
{
    public TimeLineTrigger GravityChangeTrigger;

    public bool ifCanChange = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)&&ifCanChange)
        {
            GravityChangeTrigger.TriggerTimeline();
        }
    }

    public void SetCanChange()
    {
        ifCanChange = true;
    }

    public void SetCantChange()
    {
        ifCanChange = false;
    }

    public void ChangeGravity()
    {
        if (ifCanChange)
            GravityChangeTrigger.TriggerTimeline();
    }
}
