Shader "Hidden/Custom/TextureSlice"
{
	HLSLINCLUDE

#include "PostProcessing/Shaders/StdLib.hlsl"
	
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

	TEXTURE2D_SAMPLER2D(_NoiseTex, sampler_NoiseTex);
	float4 _NoiseTex_ST;

	float _CullScale;
	float _ScrollSpeed;	
	float _SampleScale;

	float4 frag(VaryingsDefault i) : SV_Target
	{
		float2 noiseTexcoord = float2(i.texcoord.y ,frac(i.texcoord.x+ _ScrollSpeed*_Time.x));
		
		float4 mainColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
		float4 noiseColor = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, noiseTexcoord*_SampleScale);
				
		return lerp(mainColor,noiseColor,_CullScale);
	}

	ENDHLSL

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

#pragma vertex VertDefault
#pragma fragment frag

			ENDHLSL
		}
	}
}