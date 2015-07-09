Shader "Custom/CBTScreen" {
	Properties {
		_MainTex ("_MainTex", 2D) = "white" {}
		_LightMap ("Shadow", 2D) = "white" {}
		_LightBase("LightBase", Float) = .1
		_LightMax("LightBase", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
	    pass{
	    	//Blend one one
	   		//Blend OneMinusDstColor OneMinusSrcColor
	   		//Blend Zero OneMinusSrcColor
	   		///Blend one OneMinusSrcAlpha
	    	//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _LightMap;
			float _LightBase;
			float _LightMax;

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
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
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
				//return tex2D(_LightMap, i.uv);
				float4 light =  (1+(tex2D(_LightMap, i.uv) - .5 ) /.5 )* _LightMax;
				light = float4(max(_LightBase,light.x),max(_LightBase,light.y),max(_LightBase,light.z),light.a); 
				return tex2D(_MainTex, i.uv  ) * light;
				float distortPower = .48f;
				i.uv *= 1-distortPower;
				i.uv += float2(distortPower,distortPower)/2;
				
				float2 uvDis = (i.uv - float2(.5,.5))/ .5f;
					float ratio= length(uvDis) / .5;
				//float ratio = max(abs( uvDis.x),abs(uvDis.y)) /.5;
				float ratioMax = max(abs(uvDis.x), abs(uvDis.y) );
				ratio = ratio + (ratio*ratio - ratio) * .4f;
				//ratio *= ratio;

				float2 dir = normalize(uvDis);
				//+ dir* ratio * .3
				float4 color = tex2D(_MainTex, i.uv  + dir* ratio * .2);
				return color;
				//return float4(uvDis.x, uvDis.y,0,1);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
