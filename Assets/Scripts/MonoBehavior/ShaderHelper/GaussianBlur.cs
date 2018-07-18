using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBlur : PostEffectBase
{
    public Shader gaussianShader;
    private Material gaussianMaterial;

    public Material material
    {
        get
        {
            gaussianMaterial = CheckShaderAndCreateMaterial(gaussianShader, gaussianMaterial);
            return gaussianMaterial;
        }
    }

    [Range(0.0f, 10f)]
    public float blurSize = 1.0f;


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetFloat("_BlurSize", blurSize);

            Graphics.Blit(source, destination, material);
        }
        else
            Graphics.Blit(source, destination);
    }
}
