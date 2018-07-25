using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeTest : MonoBehaviour
{
    public TimeLineTrigger GravityChangeTrigger;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GravityChangeTrigger.TriggerTimeline();
        }
    }
}
