using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public enum TriggerType
{
    Once,
    Always
}

public class TimeLineTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset playableAsset;

    public TriggerType triggerType = TriggerType.Once;

    private bool _alreadyTrigger = false;

    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorStop;    
       
    public void  TriggerTimeline()
    {
        if (triggerType == TriggerType.Once && _alreadyTrigger)
            return;

        _alreadyTrigger = true;

        DirectorPlay();
        DirectorStop();
    }

    protected void DirectorPlay()
    {
        playableDirector.playableAsset = playableAsset;
        playableDirector.Play();
        OnDirectorPlay.Invoke();
    }

    protected void FinishDirector()
    {
        OnDirectorStop.Invoke();
    }

    protected void DirectorStop()
    {
        Invoke("FinishDirector",(float)playableDirector.duration);
    }
}
