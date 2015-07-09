Shader "Rorschach/Noise" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Speed("Speed", Float) = 1
		_Frequency("Frequency", Float) = 2
		_Seed("Seed", Float) = 0
		_Resolution("Resolution",Float) = 1.5
	}  
	SubShader {
		Tags { "RenderType"="Opaque" }
	    pass{ 
	    	//Blend One One
	    	Blend One SrcAlpha
	    	//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0 

			#include "UnityCG.cginc"
			#include "Assets/Shader/ShaderUtility.cginc"

			sampler2D _MainTex;
			sampler2D _SubTex;
			float4 _PlayerPosition;

			float4 _Color;

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
			vertex vert (appdata_me v) {
				vertex o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.posRaw = v.vertex;
				o.color = v.color;//*_Color;
				o.uv = v.texcoord;
				return o;
			}
			float _Speed;
			float _Resolution;
			float _Frequency;
			float _Seed;
			static float2 gradients[8] = {
					float2(1,0),float2(-1,0),float2(0,1),float2(0,-1),
					float2(.7,.7),float2(-.7,.7),float2(.7,-.7),float2(-.7,-.7) };
			static int gradientsCount = 8;

			float2 getGradient(float number){
				number += _Time.y*_Speed;
				return float2(cos(number),sin(number) );
				
				//return gradients[ int(number ) &	 (gradientsCount -1)];
			}
			float2 getColor(float2 uv, float2 uvInt ){
				float2 color = float2(rand(uvInt.x,uvInt.y), rand(uvInt.x+1,uvInt.y) );
				float2 gradient00 = getGradient(rand( uvInt.x ,uvInt.y) * 6.3) ;
				float2 gradient01 = getGradient(rand( uvInt.x +1 ,uvInt.y) *6.3);
				float2 dis00 = (uvInt-uv  );
				float2 dis01 =  (uvInt + float2(1,0) ) - uv ;
				return (.5f+ float2(dot(gradient00,dis00),dot(gradient01,dis00+float2(1,0) ) ) *.5 ) ;
			}
			float getNoise(float2 uv, float frequency){
				uv *= frequency;
				float2 uvInt = float2(int(uv.x),int(uv.y));
				float2 uvLeft = uv - uvInt;
				float2 ratio = float2(smooth(uvLeft.x),smooth(uvLeft.y));
				
				float2 interval00 = getColor(uv,uvInt );
				float2 interval01 = getColor(uv,uvInt + float2(0,1) );
				
				float noise00 = lerp(interval00.x, interval00.y, ratio.x );
				float noise01 = lerp(interval01.x, interval01.y, ratio.x);
				return lerp(noise00,noise01, ratio.y) ;
			}  


			fixed4 frag(vertex  i) : COLOR  {
				float whiteIntensity = 1.8 + 0.52 * pow((length(i.uv - .5 ) /.5),2);
				
				i.uv = float2(abs( i.uv.x-.5f) *_Resolution ,i.uv.y + _Seed);
				float frequency = _Frequency ;
				float noise = getNoise(i.uv,_Frequency);
				float amplitude = 1.0f;
				float frequencyAmplitdue = 2.0f;
				float maxNoise = 1.0f;
				float persistence = .5f;
				for(int n = 1; n < 5; n++){
					frequency *= frequencyAmplitdue;	
					amplitude *= persistence;
					maxNoise += amplitude;
					noise += getNoise(i.uv,frequency) * amplitude;
					
				}
				noise /= maxNoise;
				//noise *= noise;
				noise *= whiteIntensity;
				noise = pow(noise,35);
				return float4(noise,noise,noise,.2f);
				
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
