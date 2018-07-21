using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public abstract class TimeLineTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorStop;    

    protected void DirectorPlay()
    {
        playableDirector.Play();
        OnDirectorPlay.Invoke();
    }

    protected void FinishDirector()
    {
        OnDirectorStop.Invoke();
    }

    protected void DirectorStop(PlayableDirector director)
    {
        Invoke("DirectorStop",(float)director.duration);
    }
}
