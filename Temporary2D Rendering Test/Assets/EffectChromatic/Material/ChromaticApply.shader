Shader "Custom/ChromaticApply" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		pass{

		Blend One OneMinusSrcAlpha
		//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;
		sampler2D _DistortionTex;




		fixed4 frag(v2f_img  i) : COLOR  {
			//return float4(1,0,0,1);
			float4 color = tex2D(_MainTex, i.uv  );
			//color.rgb = float3(color.a,0,0);
			return color;
		}

		ENDCG
		}
	}
	FallBack "Diffuse"
}
