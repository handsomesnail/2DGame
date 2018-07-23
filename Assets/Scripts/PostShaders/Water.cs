using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

[Serializable]
[PostProcess(typeof(WaterRenderer), PostProcessEvent.AfterStack, "Custom/Water")]
public sealed class Water : PostProcessEffectSettings
{
    public ColorParameter tintColor = new ColorParameter { value = new Vector4(1, 1, 1, 1) };

    public FloatParameter magnitude = new FloatParameter { value = 0.5f };

    public FloatParameter frequency = new FloatParameter { value = 1f };

    public FloatParameter invWaveLength = new FloatParameter { value = 1f };

    public FloatParameter speed = new FloatParameter { value = 1f };
}

public sealed class WaterRenderer : PostProcessEffectRenderer<Water>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Water"));
        sheet.properties.SetFloat("_Magnitude", settings.magnitude);
        sheet.properties.SetFloat("_Frequency", settings.frequency);
        sheet.properties.SetFloat("_InvWaveLength", settings.invWaveLength);
        sheet.properties.SetFloat("_Speed", settings.speed);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}