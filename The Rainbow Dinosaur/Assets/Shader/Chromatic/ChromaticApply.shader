Shader "Chromatic/Apply" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("_MainTex", 2D) = "white" {}
		_distortionRed ("DistortionRed", 2D) = "white" {}
		_distortionGreen ("DistortionGreen", 2D) = "white" {}
		_distortionBlue ("DistortionBlue", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
	    pass{
	   		Blend SrcAlpha OneMinusSrcAlpha
	    	//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

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
				float4 posRaw : VECTOR;
				float4 posCenter : VECTOR1;
				float4 color : COLOR;
				half2 uv   : TEXCOORD0;
			};

            fixed4 _Color;

			vertex vert (appdata_me v) {
				vertex o;
				o.posRaw = v.vertex;
				o.posCenter = mul(UNITY_MATRIX_MVP, float4(0,0,0,1));
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;//*_Color;
				o.uv = v.texcoord;
				return o;
			}
			
			float rand(float x ,float y){
				return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * 43758.5453f);
			}
			float toSquare(float n){
				return round(n/.005) * .005;
			}
			float randSquareFloat2(float2 uv){
				return rand(toSquare(uv.x)+ _Time ,toSquare(uv.y) );
			}
			

			fixed4 GetColor(sampler2D main, sampler2D src, float2 uv, float ratio){
				float4 color = tex2D(src,float2(uv.x,uv.y));
				float2 coordinate = (color.rg - float2(.5,.5) ) /.5;
				//float r =  min(1, max(0,-.5f+randSquareFloat2(uv)) * 1000.0f);
				float r = max(0 , min(1,(-.05f+randSquareFloat2(uv) ) * 10 ) );
				return tex2D(main,uv + float2(coordinate.x*r,coordinate.y*r)*ratio );
			}


			fixed4 frag(vertex  i) : COLOR  {
				//float ratio = .2+length(i.uv - float2(.5,.5) )*.8;
				float ratio = 1;

				float4 r = GetColor (_MainTex,_distortionRed, i.uv ,ratio);
				float4 g = GetColor (_MainTex,_distortionGreen, i.uv ,ratio);
				float4 b = GetColor (_MainTex,_distortionBlue, i.uv ,ratio);
				float4 color = float4(r.r,g.g,b.b,max(r.a,max(g.a,b.a)) );

				return color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
