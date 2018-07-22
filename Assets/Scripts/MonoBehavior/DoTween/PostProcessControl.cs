using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessControl : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public Bloom bloom;

    public float bloomScale;

    private Sequence sequence;

    private void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    public void StartBloomTween(int loopTimes)
    {
         DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 10, 0.3f).SetLoops(loopTimes*2,LoopType.Yoyo);
    }
}
