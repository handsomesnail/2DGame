using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : PostEffectBase {
    public Shader testAutoShader;

    public float x = 5;
    public float y = 5;

    public Texture noiseTexture;

    private Material briSatConMaterial;

    public Material material
    {
        get
        {
            briSatConMaterial = CheckShaderAndCreateMaterial(testAutoShader, briSatConMaterial);
            return briSatConMaterial;
        }
    }   

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetTexture("_NoiseTex", noiseTexture);
            material.SetFloat("_ScrollXSpeed", x);
            material.SetFloat("_ScrollYSpeed", y);

            Graphics.Blit(source, destination, material);
        }
        else
            Graphics.Blit(source, destination);
    }
}
