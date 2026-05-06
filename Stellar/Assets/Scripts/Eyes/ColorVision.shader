Shader "Custom/ColorVision"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShiftEnabled ("Shift Enabled", Float) = 0
    }

    SubShader
    {
        ZWrite Off ZTest Always Cull Off

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
            float _ShiftEnabled;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 ShiftColor(float3 c)
            {
                return float3(1.0 - c.r, 1.0 - c.g, 1.0 - c.b);
            } 


            fixed4 frag(v2f i) : SV_Target
            {
              fixed4 col = tex2D(_MainTex, i.uv);
                 if (_ShiftEnabled > 0.5)
                 col.rgb = ShiftColor(col.rgb);
                 return col;
            }
            ENDCG
        }
    }
}