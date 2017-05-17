Shader "Custom/Wave"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Wave("Wave Distortion",Float) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Wave;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv + float2(0,sin(i.vertex.y/100 + _Time[0]  * _Wave) /10));
//				fixed4 col = tex2D(_MainTex, i.uv + float2(0,i.vertex.y);

//				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
//				col = 1 - col;
				
				return col;
			}
			ENDCG
		}
	}
}
