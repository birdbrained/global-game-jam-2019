// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/demo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		//Vanishing Line effects
		_XPos("X Position", Range(0,1)) = 0.5
		_YPos("Y Position", Range(-10, 10)) = 0.5
		_Speed("Speed", Range(-100,100)) = 60.0
		_Circles("How many circles do you need?", Range(0, 1000)) = 0.0

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
			#pragma target 3.0

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
			float4 _Color;
			float _XPos;
			float _YPos;
			float _Speed;
			float _Circles;
		



            v2f vert (appdata v)
            {
                v2f o;
       
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);


                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               
                fixed4 col = tex2D(_MainTex, i.uv);
				
				float distanceToCenter;
				float time = _Time.x * _Speed;

				float xDistance = _XPos - i.uv.x;
				float yDistance = _YPos - i.uv.y;

				distanceToCenter = (xDistance * xDistance + yDistance * yDistance) * _Circles;

				col = sin(atan2(xDistance, yDistance) * _Circles + time);
				col *= _Color;
      
                return col;
            }
            ENDCG
        }
    }
}
