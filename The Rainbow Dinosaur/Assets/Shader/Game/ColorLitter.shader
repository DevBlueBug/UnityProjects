Shader "Game/ColorLitter" {
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
       	Blend SrcAlpha OneMinusSrcAlpha		
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
			OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
			OUT.uv = IN.texcoord;
			OUT.color = IN.color * _Color;
			return OUT;
		}
		

		fixed4 frag(v2f  i) : COLOR  {
			float4 c =  tex2D(_MainTex, i.uv)*i.color;
			c.rgb = (c.rgb - .5) /.5;
			c.rgb *=c.a;
			//c =  w * (1-c.a);
			return c;
		}

		ENDCG
		}
	}
}
