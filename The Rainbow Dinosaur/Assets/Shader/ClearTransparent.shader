Shader "Custom/ClearTransparent" {
	Properties {
		_MainTex ("_MainTex", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
	    pass{
	   		//Blend SrcAlpha OneMinusSrcAlpha
	    	//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct appdata_me{
				float4 vertex : POSITION;
				float4 color : COLOR;
				half2 texcoord   : TEXCOORD0;
			};

			struct vertex {
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
			};


			vertex vert (appdata_me v) {
				vertex o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}



			fixed4 frag(vertex  i) : COLOR  {
				return  tex2D(_MainTex,i.uv ) * float4(1,1,1,.00);
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
