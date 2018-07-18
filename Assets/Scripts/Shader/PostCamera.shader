Shader "Custom/PostCamera" 
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ScanLinesTex("ScanLinesTexture",2D) = "white"{}
		_ScanLineAmount("ScanLinesAmount",Float) = 4.0
		_ScrollY("Scroll y Speed",Float) = 4.0

		_TintColor("Tint Color",Color) = (1,1,1,1)

		_VignetteTex("Vignette Texture",2D) = "white"{}
		_VignetteXScale("Vignette X Scale",Float) = 4.0
		_VignetteYScale("Vignette Y Scale",Float) = 4.0
		_VignetteXOffset("Vignette X Offset",Float) = 4.0
		_VignetteYOffset("Vignette Y Offset",Float) = 4.0

		_DistortionScale("Distortion Scale",Float) = 4.0
		_Distortion("Distortion",Float) = 4.0

		
	}
		SubShader
		{
			Pass
			{


			ZTest Always Cull Off ZWrite Off

			CGPROGRAM

			sampler2D _MainTex;
			sampler2D _ScanLinesTex;
			float4 _MainTex_ST;
			float4 _ScanLinesTex_ST;
			fixed _ScanLineAmount;
			fixed _ScrollY;


			fixed4 _TintColor;

			sampler2D _VignetteTex;
			fixed _VignetteXOffset;
			fixed _VignetteYOffset;
			fixed _VignetteXScale;
			fixed _VignetteYScale;

			fixed _Distortion;
			fixed _DistortionScale;

			float2 barrelDistortion(float2 coord) 
			{
				// Lens distortion algorithm
				// See http://www.ssontech.com/content/lensalg.htm

				float2 h = coord.xy - float2(0.5, 0.5);
				float r2 = h.x * h.x + h.y * h.y;
				float f = 1.0 + r2 * (_Distortion * sqrt(r2));

				return f * _DistortionScale * h + 0.5;
			}


	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos:SV_POSITION;
				float4 uv:TEXCOORD0;
			};

			v2f vert(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord, _ScanLinesTex) + frac(float2(0.0, _ScrollY)*_Time.x);
				return o;
			}

			fixed4 frag(v2f i) :SV_Target
			{				
				half2 scanLinesUV = half2(i.uv.x*_ScanLineAmount,i.uv.y*_ScanLineAmount);
				fixed4 scanLineTex = tex2D(_ScanLinesTex, scanLinesUV+i.uv.zw);

				half2 distortedUV = barrelDistortion(i.uv);
				fixed4 renderDistortedTex = tex2D(_MainTex, distortedUV);

				half2 vignetteUV = half2(i.uv.x*_VignetteXScale+_VignetteXOffset, i.uv.y*_VignetteYScale + _VignetteYOffset);
				fixed4 vignetteTex = tex2D(_VignetteTex, i.uv+vignetteUV);

				return  vignetteTex*scanLineTex*renderDistortedTex+_TintColor;
			}
			ENDCG
		}
		}

	FallBack Off
}
