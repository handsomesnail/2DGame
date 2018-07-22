using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TimeLineTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset playableAsset;
    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorStop;    
    
    public void  TriggerTimeline()
    {
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
