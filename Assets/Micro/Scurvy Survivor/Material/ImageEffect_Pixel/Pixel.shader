Shader "Custom/Pixel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Width ("_Width", Float) = 64
		_Height ("_Height", Float) = 64
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
			Cull Off
			ZWrite Off
			ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _Width;
			float _Height;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;
				uv.x *= _Width;
				uv.y *= _Height;
				uv.x = round(uv.x);
				uv.y = round(uv.y);
				uv.x /= _Width;
				uv.y /= _Height;

                // sample the texture
                fixed4 col = tex2D(_MainTex, uv);               
                return col;
            }
            ENDCG
        }
    }
}
