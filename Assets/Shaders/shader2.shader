// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/shader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Rotation("Rotation", Range(0,360)) = 0.0
		_Speed("Speed", range(-100,100)) = 60.0
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
			float _Rotation;
			float _Speed;
			float4 _Color;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				// rotating UV
				const float Deg2Rad = (UNITY_PI * 2.0) / 360.0;

				float rotationRadians = _Rotation * Deg2Rad; // convert degrees to radians
				float s = sin(rotationRadians); // sin and cos take radians, not degrees
				float c = cos(rotationRadians);

				float2x2 rotationMatrix = float2x2(c, -s, s, c); // construct simple rotation matrix

				v.uv -= 0.5; // offset UV so we rotate around 0.5 and not 0.0
				v.uv = mul(rotationMatrix, v.uv); // apply rotation matrix
				v.uv += 0.5; // offset UV again so UVs are in the correct location

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv += _Rotation * _Time.x;
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float time = _Time.x * _Speed;
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
				
                return col;
            }
            ENDCG
        }
    }
}
