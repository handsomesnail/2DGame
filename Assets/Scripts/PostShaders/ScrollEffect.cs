using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
[PostProcess(typeof(ScrollEffectRenderer),PostProcessEvent.AfterStack,"Custom/ScrollEffect")]
public sealed class ScrollEffect : PostProcessEffectSettings
{
    public FloatParameter scrollSpeed = new FloatParameter { value = 0.5f };
}

public sealed class ScrollEffectRenderer:PostProcessEffectRenderer<ScrollEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/ScrollEffect"));
        sheet.properties.SetFloat("_ScrollSpeed", settings.scrollSpeed);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}