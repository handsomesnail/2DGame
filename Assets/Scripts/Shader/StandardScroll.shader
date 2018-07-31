Shader "Custom/StandardScroll" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ScrollXSpeed("X Scroll Speed",Float) = 0.01
		_ScrollYSpeed("Y Scroll Speed",Float) = 0.01
	}
	SubShader
	{
		CGINCLUDE

#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _MainTex_ST;
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
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex)+frac(float2(_ScrollXSpeed, _ScrollYSpeed)*_Time.x);
			return o;
		}

		fixed4 frag(v2f i):SV_Target
		{
			return tex2D(_MainTex, i.uv.xy);
		}
		ENDCG


		Pass
		{
			CGPROGRAM
#pragma vertex vert
#pragma	fragment frag
			
			ENDCG
		}

	}
	FallBack "Diffuse"
}
