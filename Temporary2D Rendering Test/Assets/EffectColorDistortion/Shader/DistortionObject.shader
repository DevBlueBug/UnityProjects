Shader "ColorDistortionClear/DistortionObject" {
	Properties {
		[PerRendererData]_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
	    pass{
	   		Blend One One
	    	ZTest Always Cull Off ZWrite On Fog { Mode Off } //Rendering settings

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
				float4 center = mul(UNITY_MATRIX_MV, float4(0,0,0,1));
				float4 scale = mul(UNITY_MATRIX_MV, float4(1.0	,0,0,1));
				float4 viewedAs = mul(UNITY_MATRIX_MV, v.vertex);
				float4 shouldBe = v.vertex * scale;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

				o.posRaw = v.vertex;
				o.color = v.color;//*_Color;
				o.uv = v.texcoord;
				o.scale = float4((viewedAs.x -center.x) ,(viewedAs.y -center.y) ,0,1);
				//o.scale = float4((o.pos.x-center.x)/scale.x ,0,0,1);
				return o;
			}


			fixed4 frag(vertex  i) : COLOR  {
				float4 color = tex2D(_MainTex,i.uv)* i.color;
				color.rgb -= float3(.5,.5,0);
				color.rgb *= color.a;
				return color ;

				float3 distance = i.color - float4(.5,.5,0,0);
				//distance.r = (round(distance.r / .115) * .115) /.5;
				//distance.g = (round(distance.g / .115) * .115 )/.5;	

				//distance.rg *= .5;
				
				//color.rgb *= distance;



				float ratio = 1-length(i.uv - float2(.5,.5) );//(i.uv.y  + (i.uv.x) )/ 2;//length(i.uv- float2(.5,.5) ) /.6 ;
				float r = round(color.r / .005) * .005 *ratio;
				float g = round(color.g / .005) * .005 *ratio;
				return float4( r,g,0,0) * color.a;
				//return color;
				return float4(abs(i.scale.x /.5),abs(i.scale.y/.5),0, 1);
				//return float4(abs(i.scale.x),abs(i.scale.y),0, 1);
				//return float4(r.r,g.g,b.b,max(b.a,max(r.a,g.a)) );
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
