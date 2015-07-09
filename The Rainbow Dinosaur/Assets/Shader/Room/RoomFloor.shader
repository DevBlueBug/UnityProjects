Shader "Room/RoomFloor" {
	Properties {
		_MainTex("_MainTex", 2D) = "white" {}
		_FloorReference("RoomFloor", 2D) = "white" {}
		_Power ("power", Float) = .3
	}
	SubShader {
	 Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
		pass{
		Blend SrcAlpha OneMinusSrcAlpha
        //Cull Off Lighting Off ZWrite Off Fog { Mode Off }
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"
		
		struct v2f{
			float4 vertex   : SV_POSITION;
			fixed4 color    : COLOR;
			half2 uv  : TEXCOORD0;
		};


		sampler2D _MainTex;
		sampler2D _FloorReference;
		float _Power;
		
		
		
		
		v2f vert(appdata_full  IN)
		{
			v2f OUT;
			OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
			OUT.uv = IN.texcoord;
			OUT.color = IN.color;
			return OUT;
		}

		fixed4 frag(v2f  i) : COLOR  {
			//return _Color* (1-tex2D(_FloorReference, i.uv).x);
		
			float shade = tex2D(_FloorReference, i.uv).x;	
			float shadow = (1-( length(i.uv-.5) / .6 ) );
			shadow = min(1,.80 + pow(shadow,2)	 );
			float4 cTex = 0;
			cTex.a = ( (1-shade) * _Power);
			cTex.rgb = i.color;
			//float4 cTex =  _Color * (1 - (1-shade) * _Power);
			
			//cTex.rgb *= shadow  ;
			return cTex;
		}

		ENDCG
		}
	}
}
