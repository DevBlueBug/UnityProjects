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
			float4 color0 = tex2D(_MainTex, i.uv + float2(.01,0)  );
			float4 color1 = tex2D(_MainTex, i.uv + float2(-.01,.0) );
			float4 color2 = tex2D(_MainTex, i.uv + float2(.0,-.01) );
			float4 color3 = tex2D(_MainTex, i.uv + float2(.0,.01) );
			float4 color4 = tex2D(_MainTex, i.uv + float2(.01,.01) );
			float4 color5 = tex2D(_MainTex, i.uv + float2(-.01,.01) );
			float4 color6 = tex2D(_MainTex, i.uv + float2(.01,-.01) );
			float4 color7 = tex2D(_MainTex, i.uv + float2(-.01,-.01) );
			//color *= 1-(1-length(color.rgb+color0.rgb+ color1.rgb+ color2.rgb+ color3.rgb)/7.2) * .3f;
			//return color;

			color.a -=.1;
			float ratio = 1-(1-color.a)*.15;
			color *= ratio;//.99;
			

			//color *= ratio;
			//color.rgb +=  float3(1,1,1) * -.05;
			//color.rgb *= ratio*ratio;//.99;
			

			return color;
		}

		ENDCG
		}
	}
}
