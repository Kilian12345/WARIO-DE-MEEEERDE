Shader "CustomDeMerde/ShaderDeMerde"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Vertex ("_Vertex", Range(0.8 , 1.25)) = 1
        _Color ("_Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Cull off

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

			fixed4 _Color;
			float _Vertex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

				float3 viewPos = UnityObjectToViewPos(v.vertex);

				if (o.vertex.x <= viewPos.x)
				{
					o.vertex.y *= _Vertex;
				}
				else
				{
					o.vertex.y /= _Vertex;
				}

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * _Color;
            }
            ENDCG
        }
    }
}
