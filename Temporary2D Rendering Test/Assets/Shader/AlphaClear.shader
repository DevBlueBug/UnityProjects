Shader "Custom/AlphaClear" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		pass{

		//Blend SrcAlpha OneMinusSrcAlpha 
		//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;




		fixed4 frag(v2f_img  i) : COLOR  {
			float4 cTex = tex2D(_MainTex, i.uv);

			return float4(cTex.r,cTex.g,cTex.b,0);
		}

		ENDCG
		}
	}
}
