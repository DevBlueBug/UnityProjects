Shader "Chromatic/DistortionObject" {
	Properties {
		[PerRendererData]_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
	    pass{
	   		Blend SrcAlpha one
	    	//ZTest Always Cull Off ZWrite On Fog { Mode Off } //Rendering settings

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _distortionRed,_distortionGreen,_distortionBlue;

			struct appdata_me{
				float4 vertex : POSITION;
				float4 color : COLOR;
				half2 texcoord   : TEXCOORD0;
			};

			struct vertex {
				float4 pos : SV_POSITION;
				float4 posRaw : VECTOR0;
				float4 scale : VECTOR1;
				float4 color : COLOR;
				half2 uv   : TEXCOORD0;
			};

            fixed4 _Color;

			vertex vert (appdata_me v) {
				vertex o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;//*_Color;
				o.uv = v.texcoord;
				return o;
			}


			fixed4 frag(vertex  i) : COLOR  {
				float4 color = tex2D(_MainTex,i.uv)* i.color;
				color.rgb -= float3(.5,.5,0);
				return color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
