using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AdjustPostEffect : MonoBehaviour
{
    [Range(0.0f,1.0f)]
    public float cullScale;

    [Range(0.0f, 20.0f)]
    public float bloomIntensity;
    [Range(0.0f,1.0f)]
    public float bloomScale;

    private TextureSlice textureSlice;
    private Bloom bloom;
    public PostProcessVolume processVolume;


    private void Awake()
    {
        processVolume.profile.TryGetSettings(out textureSlice);
        processVolume.profile.TryGetSettings(out bloom);
    }

    private void Update()
    {
        textureSlice.cullScale.value = cullScale;
        bloom.intensity.value = bloomIntensity;
        bloom.threshold.value = bloomScale;
    }
}
