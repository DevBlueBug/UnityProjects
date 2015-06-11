Shader "Custom/ChromaticClear" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		pass{

		//Blend SrcColor one
		//Blend SrcColor OneMinusSrcColor
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
			//return color;

			color.a -=.01;
			float ratio = 1-(1-color.a)*.31;
			color *= ratio*ratio;//.99;
			//color *= ratio;
			//color.rgb +=  float3(1,1,1) * -.005;
			//color.rgb *= ratio*ratio;//.99;
			

			return color;
		}

		ENDCG
		}
	}
}
