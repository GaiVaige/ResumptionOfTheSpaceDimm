Shader "Hidden/SH_DitherPass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        [Header(Dots)]
        _CellSize("Cell Size", float) = 6
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _CellSize;

            fixed4 frag (v2f i) : SV_Target
            {
                //Create Cells for the effect
                float cellWidth = _CellSize / _ScreenParams.x;
                float cellHeight = _CellSize / _ScreenParams.y;
                fixed2 roundedUV;
                roundedUV.x = round(i.uv.x / cellWidth) * cellWidth;
                roundedUV.y = round(i.uv.y / cellHeight) *cellHeight;

                fixed4 roundedCol = tex2D(_MainTex, roundedUV);
                return roundedCol;
            }
            ENDCG
        }
    }
}
