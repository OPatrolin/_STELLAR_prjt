Shader "Custom/ColorVision"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShiftEnabled ("Shift Enabled", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float _ShiftEnabled;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float3 ShiftColor(float3 col)
            {
                float r = col.r;
                float g = col.g;
                float b = col.b;

                // vert -> rouge, rouge -> vert
                // bleu -> orange (rouge+vert), jaune -> violet (rouge+bleu)
                return float3(g, r, 1.0 - b);
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                if (_ShiftEnabled > 0.5)
                    col.rgb = ShiftColor(col.rgb);
                return col;
            }
            ENDHLSL
        }
    }
}