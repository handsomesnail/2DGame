using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineIOBP : InteractOnButtonPressTrigger
{
    public TimeLineTrigger timeLineTrigger;
    
    protected override void SpecificTrigger()
    {
        timeLineTrigger.TriggerTimeline();
    }
}
