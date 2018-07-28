using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeTest : MonoBehaviour
{
    public TimeLineTrigger GravityChangeTrigger;

    private bool ifCanChange = false;

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
}
