Shader "Unlit/Gradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Min("Min", Range(0,1)) = 0.3
		_Max("Max", Range(0,1)) = 0.5
		_GradientStregth("Gradient Strength", Range(0,1)) = 0.75
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				half4 color : COLOR;
            };

            struct v2f
            {
				half4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Min;
			float _Max;
			float _GradientStregth;

            v2f vert (appdata v)
            {
                v2f o;
				o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
				 
                return col * clamp(smoothstep(_Min, _Max, i.uv.g),_GradientStregth,1);
            }
            ENDCG
        }
    }
}
