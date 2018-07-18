using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileBloom : PostEffectBase
{
    public Shader mobileBloomShader;
    private Material mobileBloomgaussianMaterial;

    public Vector4 parameter;
    public Vector4 offsetA;
    public Vector4 offsetB;

    public Material material
    {
        get
        {
            mobileBloomgaussianMaterial = CheckShaderAndCreateMaterial(mobileBloomShader, mobileBloomgaussianMaterial);
            return mobileBloomgaussianMaterial;
        }
    }


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetVector("_Parameter", parameter);
            material.SetVector("_OffsetsA", offsetA);
            material.SetVector("_OffsetsB", offsetB);
            Graphics.Blit(source, destination, material);
        }
        else
            Graphics.Blit(source, destination);
    }
}
