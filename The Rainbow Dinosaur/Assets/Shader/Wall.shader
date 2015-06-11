﻿Shader "Custom/Wall" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_PlayerPosition ("PlayerPosition", Vector) = (0,0,0,0)
	}
	SubShader {
		pass{

		Blend One OneMinusSrcAlpha
		//ZWrite On
		//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;
		sampler2D _DistortionTex;
		float4 _PlayerPosition;

		struct vertex 
		{
			float4 pos : SV_POSITION;
			float4 posRaw : COLOR;
			half2 uv   : TEXCOORD0;
		};


		vertex vert (appdata_base v) {
			vertex o;

			float4 dis = v.vertex- _PlayerPosition;
			dis *= float4(1,1,0,0);
			float4 disNor = normalize(dis);

			float distance = length(dis) / 17.49285;
			float x = abs(dis.x / 7), y = abs(dis.y / 4.5);
			float ratio = 1-min(1,sqrt(x*x+y*y) );
			//float ratio = max(x,y);
			
			ratio = max(0,min(1, ratio));


			o.pos = mul(UNITY_MATRIX_MVP, v.vertex +disNor*ratio);
			o.posRaw = v.vertex;
			//o.pos = float4(1,1,1,0);
			o.uv = v.texcoord;
			return o;
		}

		fixed4 frag(vertex  i) : COLOR  {
			float4 dis = i.posRaw - _PlayerPosition;
			float distance = length(dis) / 17.49285;
			float x = abs(dis.x / 8), y = abs(dis.y / 5);
			float ratio = ((x*x+y*y)/2) *1.5;
			ratio = 1-ratio;
			ratio = min(1, ratio*2);

			float ratio2 = i.uv.y;

			//ratio = max(ratio,ratio2);

			//return float4(1,0,0,1);
			float4 color = tex2D(_MainTex, i.uv );
			//color.rgb *= y;
			//color.rgb = float3(i.uv.x,i.uv.y,0);
			//color.rgb = float3(0,y,0);
			//color.a = ratio;
			color.rgb = float3(i.uv.x,i.uv.y,0);
			return color;
		}

		ENDCG
		}
	}
	FallBack "Diffuse"
}