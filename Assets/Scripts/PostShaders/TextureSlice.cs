using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

[Serializable]
[PostProcess(typeof(TextureSliceRenderer),PostProcessEvent.AfterStack,"Custom/TextureSlice")]
public sealed class TextureSlice : PostProcessEffectSettings
{
    public TextureParameter noiseTexture = new TextureParameter { value = null };

    [Range(0.0f,1.0f)]
    public FloatParameter cullScale = new FloatParameter { value = 0.5f };

    public FloatParameter scrollSpeed = new FloatParameter { value = 1f };

    public FloatParameter sampleScale = new FloatParameter { value = 1f };
}

public sealed class TextureSliceRenderer:PostProcessEffectRenderer<TextureSlice>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/TextureSlice"));
        sheet.properties.SetTexture("_NoiseTex", settings.noiseTexture);
        sheet.properties.SetFloat("_SampleScale", settings.sampleScale);
        sheet.properties.SetFloat("_CullScale", settings.cullScale);
        sheet.properties.SetFloat("_ScrollSpeed", settings.scrollSpeed);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}