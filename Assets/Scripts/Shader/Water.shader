Shader "Hidden/Custom/Water"
{
	HLSLINCLUDE

	#include "PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

	float4  _Color;
	float	_Magnitude;
	float	_Frequency;
	float	_InvWaveLength;
	float	_Speed;

	float4 frag(VaryingsDefault i) : SV_Target
	{
		float2 offset;
		offset.x = sin(_Frequency *_Time.y*_Speed + i.texcoord.x*_InvWaveLength + i.texcoord.y*_InvWaveLength)*_Magnitude;
		offset.y = offset.x;

		float2 newTexcoord = frac(i.texcoord + offset);
		
		return _Color+SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, newTexcoord);
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