using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class TestScripts : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Bloom bloom;

    public float bloomScale;

    private Sequence sequence;

    private Tweener startTweener;
    private Tweener endTweener;
    private void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out bloom);

    }


    
    void Start()
    {
        startTweener = DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 10, 1).SetLoops(10,LoopType.Yoyo);
        
        startTweener.Play();
    }

    private void Update()
    {
          
    }
}
