Shader "Custom/ChromaticClear" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		pass{

		//Blend one OneMinusSrcAlpha
		//Blend SrcColor OneMinusSrcAlpha
		//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;
		sampler2D _DistortionTex;


float rand(float2 co){
	return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453f);
}

float rand(float x ,float y){
	return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * 43758.5453f);
}

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
			//color *= 1-(1-length(color.rgb+color0.rgb+ color1.rgb+ color2.rgb+ color3.rgb)/5) * .8f;
			//return color;

			
			float ratio = length(color.rgb + color0.rgb+color1.rgb+color2.rgb+color3.rgb)/5;
			float randomRatio = min(1,rand(ratio,0));
			randomRatio *= randomRatio;
			float3 ratioNew = float3(rand(color.r,color.a),rand(color.g,color.a),rand(color.b,color.a) );
			ratioNew = float3(max(.99,ratioNew.x),max(.99,ratioNew.y),max(.99,ratioNew.z));
			
			//color *= (1-ratio*.1);
			//color *= (1-randomRatio*.5);

			color -= .01;
			//color.rgb -= float3(1,1,1)*.05;
			float alphaDecrease = (1- min(1, length(color.rgb)/.1  ) ) ;
			//color.rgb = float3(1,0,0);
			//color.a -=alphaDecrease;


			//color *= ratio;
			//color.rgb +=  float3(1,1,1) * -.05;
			//color.rgb *= ratio*ratio;//.99;
			

			return float4(color.r,color.g,color.b,color.a)*.8;
		}

		ENDCG
		}
	}
}
