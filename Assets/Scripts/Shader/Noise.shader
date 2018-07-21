Shader "Custom/Noise" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex("Noise Texture",2D) = "white"{}
		_ScrollXSpeed("Scroll X Speed",Float) = 1.0
		_ScrollYSpeed("Scroll Y Speed", Float) = 1.0
	}
Subshader{
		// Lightweight Pipeline tag is required. If Lightweight pipeline is not set in the graphics settings
		// this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
		// material work with both Lightweight Pipeline and Builtin Unity Pipeline
			Tags{ "RenderType" = "Opaque" "RenderPipeline" = "LightweightPipeline" "IgnoreProjector" = "True" }
			LOD 300

			// ------------------------------------------------------------------
			//  Forward pass. Shades all light in a single pass. GI + emission + Fog
			Pass
		{
			
			Tags{ "LightMode" = "LightweightForward" }


			CGPROGRAM

	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _NoiseTex;

			float4 _MainTex_ST;
			float4 _NoiseTex_ST;

			half _ScrollXSpeed;
			half _ScrollYSpeed;

			struct v2f
			{
				float4 pos:SV_POSITION;
				float4 uv:TEXCOORD0;
			};

			v2f vert(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord, _NoiseTex) + frac(float2(_ScrollXSpeed, _ScrollYSpeed)*_Time.x);
				return o;
			}

			fixed4 frag(v2f i) :SV_Target
			{
				half2 mainTexUV = half2(i.uv.x, i.uv.y);
				fixed4 mainTex = tex2D(_MainTex, mainTexUV);

				half noiseUV = half2(i.uv.z, i.uv.w);
				fixed4 noiseTex = tex2D(_NoiseTex, noiseUV);

				return mainTex * noiseTex;
			}

			ENDCG
		}
		}
	FallBack "Diffuse"
}
