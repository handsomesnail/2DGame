using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class CameraRenderControl : MonoBehaviour
{
    public PostProcessVolume processVolume;

    private Bloom bloom;
    private ColorGrading colorGrading;

    private void Awake()
    {
        processVolume.profile.TryGetSettings(out bloom);
    }

    public void StartBloom(float  intensity, float duration, float times)
    {

    }

}
