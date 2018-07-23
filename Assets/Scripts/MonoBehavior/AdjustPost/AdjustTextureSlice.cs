using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AdjustTextureSlice : MonoBehaviour
{
    [Range(0.0f,1.0f)]
    public float cullScale;

    private TextureSlice textureSlice;
    private PostProcessVolume processVolume;

    private void Awake()
    {
        processVolume = GetComponent<PostProcessVolume>();
        processVolume.profile.TryGetSettings(out textureSlice);
    }

    private void Update()
    {
        textureSlice.cullScale.value = cullScale;
    }
}
