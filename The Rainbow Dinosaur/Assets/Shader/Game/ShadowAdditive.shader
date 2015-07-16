Shader "Game/ShadowAdditive" {
	Properties {
		[PerRendererData]_MainTex("_MainTex", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
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
       	Blend SrcAlpha One		
       // Blend DstColor SrcColor
		
        Cull Off Lighting Off ZWrite Off Fog { Mode Off }
		//ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings


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
		fixed4 _Color;
		v2f vert(appdata_full  IN)
		{
			v2f OUT;
			OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);//+ float4(0,0,mul(UNITY_MATRIX_MVP, float4(0,0,0,1) ).y ,0) );
			//OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex + float4(0,0,mul(UNITY_MATRIX_MVP, float4(0,0,0,1) ).y ,0) );
			OUT.uv = IN.texcoord;
			OUT.color = IN.color * _Color;
			return OUT;
		}
		

		fixed4 frag(v2f  i) : COLOR  {
			float4 c =  tex2D(_MainTex, i.uv)*i.color;
			//c.rgb *= c.a;
			return c;
		}

		ENDCG
		}
	}
}
