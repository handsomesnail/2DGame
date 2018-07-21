using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class TimeLineControlTest : MonoBehaviour
{
    public PlayableDirector director; 
    public List<PlayableAsset> playableAssetsList = new List<PlayableAsset>();

    public int index;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayClip();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Pre();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Next();
    }

    private void PlayClip()
    {
        director.Play();
    }

    private void Next()
    {
        if (index == playableAssetsList.Count - 1)
        {
            index = 0;
            director.playableAsset = playableAssetsList[index];
        }
        else
        {
            index++;
            director.playableAsset = playableAssetsList[index];
        }
    }

    private void Pre()
    {
        if (index == 0)
            index = playableAssetsList.Count - 1;
        else
            index--;
        director.playableAsset = playableAssetsList[index];
    }
}
