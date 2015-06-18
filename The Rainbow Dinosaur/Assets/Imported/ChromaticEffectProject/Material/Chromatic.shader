Shader "Custom/Chromatic" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

	}
	SubShader {
		pass{

		Blend One  One
		
		//ZTest Always Cull Off 
		ZWrite Off// Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"

		float4 _Color;
		sampler2D _MainTex;
		sampler2D _DistortionTexRed;
		sampler2D _DistortionTexGreen;
		sampler2D _DistortionTexBlue;




		fixed4 frag(v2f_img  i) : COLOR  {
			float4 cTex = tex2D(_MainTex, i.uv);
			float dotV = dot(cTex.rgb,  _Color.rgb);
			float4 color = (_Color * dotV )* cTex.a;
			//color.a = cTex.a*1.0*    min(1,dotV);
			return color;
			
		}

		ENDCG
		}
	}
}
