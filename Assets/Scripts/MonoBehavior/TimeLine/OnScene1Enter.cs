using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScene1Enter : MonoBehaviour
{
    public TimeLineTrigger timeLine;

    private void Start()
    {
        timeLine.TriggerTimeline();
    }
}
