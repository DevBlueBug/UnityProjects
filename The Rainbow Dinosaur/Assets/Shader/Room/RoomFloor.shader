Shader "Room/RoomFloor" {
	Properties {
		_FloorReference("RoomFloor", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
		_Power ("power", Float) = .3
	}
	SubShader {
		pass{
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


		CGPROGRAM
		#pragma vertex vert_img
        #pragma fragment frag
        #pragma target 3.0

		#include "UnityCG.cginc"


		sampler2D _MainTex;
		sampler2D _FloorReference;
		float4 _Color;
		float _Power;

		fixed4 frag(v2f_img  i) : COLOR  {
		
			float shade = tex2D(_FloorReference, i.uv).x;
			float shadow = (1-( length(i.uv-.5) / .6 ) );
			shadow = min(1,.80 + pow(shadow,2)	 );
			float4 cTex =  _Color * (1 -(1-shade) * _Power);
			cTex.a=1;
			//cTex.rgb *= shadow  ;
			return cTex;
		}

		ENDCG
		}
	}
}
