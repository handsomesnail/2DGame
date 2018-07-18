using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLinesShaderControl : PostEffectBase
{
    public Shader scanLinesShader;
    private Material scanLinesMaterial;

    [Header("扫描线效果")]
    public Texture2D scanLinesTexture;
    public float scanLinesTexAmount;
    public float scanLinesScrollYSpeed;
    public Color tintColor;

    [Header("影晕效果")]
    public Texture2D vignetteTexture;
    public float vignetteXScale;
    public float vignetteYScale;
    [Range(-1.0f,1.0f)]
    public float vignetteXOffset;

    [Range(-1.0f, 1.0f)]
    public float vignetteYOffset;

    [Header("Distortion")]
    [Range(0.0f,2.0f)]
    public float distortion;
    [Range(0.0f,2.0f)]
    public float distortionScale;

    //[Header("Bloom")]

    


    public Material material
    {
        get
        {
            scanLinesMaterial = CheckShaderAndCreateMaterial(scanLinesShader, scanLinesMaterial);
            return scanLinesMaterial;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetTexture("_ScanLinesTex", scanLinesTexture);
            material.SetFloat("_ScanLineAmount", scanLinesTexAmount);
            material.SetFloat("_ScrollY", scanLinesScrollYSpeed);
            material.SetColor("_TintColor", tintColor);
            material.SetTexture("_VignetteTex", vignetteTexture);
            material.SetFloat("_VignetteXScale", vignetteXScale);
            material.SetFloat("_VignetteYScale", vignetteYScale);
            material.SetFloat("_VignetteXOffset", vignetteXOffset);
            material.SetFloat("_VignetteYOffset", vignetteYOffset);
            material.SetFloat("_Distortion", distortion);
            material.SetFloat("_DistortionScale", distortionScale);

            Graphics.Blit(source, destination, material);
        }
        else
            Graphics.Blit(source, destination);
    }

}
