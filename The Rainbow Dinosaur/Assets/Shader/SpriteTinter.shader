Shader "Custom/SpriteTinter" {
	Properties {
		_Color0 ("Color0", Color) = (1,1,1,1)
		_Color1 ("Color1", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		pass{

		Blend SrcAlpha OneMinusSrcAlpha 


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;
		fixed4 _Color0;
		fixed4 _Color1;


		fixed4 frag(v2f_img  i) : COLOR  {
			float4 cTex = tex2D(_MainTex, i.uv);
			float4 color1 = float4(cTex.r,cTex.r,cTex.r,cTex.a) *_Color0;
			float4 color2 = float4(cTex.b,cTex.b,cTex.b,cTex.a) *_Color1; 
			float4 color = color1 + color2;
			color.a = cTex.a;
			//float4 cTex = tex2D(_MainTex, i.uv);
			//color.a = 0;
			//color.a = i.a;
			//return color;
			return color;
		}

		ENDCG
		}
	}
}
