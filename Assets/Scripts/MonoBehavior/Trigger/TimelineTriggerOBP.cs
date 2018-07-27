using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TimelineTriggerOBP : InteractOnButtonPress
{
    public TimeLineTrigger timeLine;

    public override void SpecificInteract()
    {
        timeLine.TriggerTimeline();
    }
}
