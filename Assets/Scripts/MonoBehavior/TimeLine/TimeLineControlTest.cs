using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimeLineControlTest : MonoBehaviour
{
    public PlayableDirector startMove;
    public PlayerInteract playerInteract;

    private void Start()
    {
        startMove.played += DirectorStart;
        startMove.stopped += DirectorEnd;
        startMove.Play();
    }

    private void DirectorStart(PlayableDirector playableDirector)
    {
        Debug.Log("Start");
        playerInteract.StartInteract();
    }

    private void DirectorEnd(PlayableDirector playableDirector)
    {
        Debug.Log("End");
        playerInteract.EndInteract();
    }

}
