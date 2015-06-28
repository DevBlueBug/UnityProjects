Shader "Rorschach/Blur" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SubTex ("_SubTex", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
	    pass{
	    	//Blend One One
	    	//Blend SrcAlpha OneMinusSrcAlpha
	    	//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _SubTex;
			float4 _PlayerPosition;

			struct appdata_me{
				float4 vertex : POSITION;
				float4 color : COLOR;
				half2 texcoord   : TEXCOORD0;
			};

			struct vertex {
				float4 pos : SV_POSITION;
				float4 posRaw : VECTOR;
				float4 color : COLOR;
				half2 uv   : TEXCOORD0;
			};


			float rand(float x ,float y){
				return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * 99999.453f);
				//return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * 43758.5453f);
			}
			
			float rand(float x ,float y, float frequency){
				return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * frequency);
				//return frac(sin(dot(float2(x,y) ,float2(12.9898,78.233))) * 43758.5453f);
			}

            fixed4 _Color;

			vertex vert (appdata_me v) {
				vertex o;


				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.posRaw = v.vertex;
				o.color = v.color;//*_Color;
				o.uv = v.texcoord;
				return o;
			}

			fixed3 GetDir(float3 color, float3 dir){
				float d = dot(color,float3(	dir.x,dir.y,0));
				return dir*d;
				//return dir*d;

			}
			fixed4 GetLeftOver(float2 uv, float2 uvChange){
				float4 color = tex2D(_MainTex,float2(uv.x+uvChange.x,  uv.y+uvChange.y));
				color.rgb = color.rgb-float3(.5,.5,0);
				float3 dir = normalize(float3(uvChange.x,uvChange.y,0));
				float3 colorDir = GetDir(color.rgb, -dir );
				float3 colorOriginal = color.rgb- colorDir.rgb;
				//colorDir *= 2.0f;

				float colorDirMag = length(colorDir);
				color.rgb =  float3(.5,.5,0) + (colorOriginal)* ( .9) + colorDir * 1.0;
				//color.a = max(0, color.a - .05);
				return color;
			}

			fixed4 frag(vertex  i) : COLOR  {
				//dreturn float4(1,0,0,1);
				float r =  tex2D(_MainTex,float2(i.uv.x,i.uv.y));
				float r0 = tex2D(_MainTex,float2(i.uv.x+.001,i.uv.y));
				float r1 = tex2D(_MainTex,float2(i.uv.x-.001,i.uv.y));
				float r2 = tex2D(_MainTex,float2(i.uv.x,i.uv.y+.001));
				float r3 = tex2D(_MainTex,float2(i.uv.x,i.uv.y-.001));
				float average = ( r+ r0 + r1+ r2+ r3 )/5.0f;
				//if(average > .5f) average =1.0f;
			
				average = pow(average,1);
				return float4(average,average,average,1);
				//average*= average;
				
				//average = min(1, average*5);
				
				//float4 color0 = GetLeftOver (i.uv);
				//float2 dir =normalize( color0.rg - float2(.5,.5) );
				//float4 colorDir = GetLeftOver(i.uv + dir * .01);

				float4 color1 = GetLeftOver (i.uv,float2(0,.01));
				float4 color2 = GetLeftOver (i.uv,float2(0,-.01));
				float4 color3 = GetLeftOver (i.uv,float2(0.01,.0));
				float4 color4 = GetLeftOver (i.uv,float2(-0.01,.0));

				float3 colorRGB = tex2D(_SubTex,float2(i.uv.x,1-i.uv.y)) ;

				float4 color = ((color1 + color2+ color3 + color4)/4 ) ;
				return color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
