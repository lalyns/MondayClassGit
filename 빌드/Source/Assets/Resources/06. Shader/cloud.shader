﻿Shader "Custom/cloud"
{
    Properties
    {
		_rampTex ("Ramp", 2D ) = "" {}
        _Color ("Color", Color) = (1,1,1,1)
        _shadowColor ("Shadow Color", Color) = (1,1,1,1)
        _rimColor ("Rim Color", Color) = (1,1,1,1)
        _rimPow ("Rim Pow", float) = 0
		_smoothMin ("smoothMin" , Range(0,1)) = 0
		_smoothMax ("smoothMax" , Range(0,1)) = 1
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
       
        #pragma surface surf cloud noshadow noambient

        sampler2D _rampTex;
		float4 _shadowColor, _Color, _rimColor;
		float _smoothMin, _smoothMax, _rimPow;

        struct Input
        {
            float2 uv_MainTex;
			
        };
    
        

        void surf (Input IN, inout SurfaceOutput o)
        {
			
			o.Albedo = 1;
        }



		float4 Lightingcloud(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {

			float ndotL = dot(s.Normal, lightDir) * 0.8 + 0.2;
			float ndotV = dot(s.Normal, viewDir);
			
			float4 ramp = tex2D(_rampTex, float2(ndotL, 0.5));

			float3 rim;
			rim = saturate(pow(1 - ndotV, _rimPow) * _rimColor);

			
			float3 shadowCol;
			shadowCol = _shadowColor;


			float3 shadow;
			shadow = smoothstep(_smoothMin, _smoothMax, ndotL) ;
			

			float3 finalCol;
			finalCol = lerp(_Color, shadowCol, shadow) ;
			//finalCol = lerp(_Color, ramp, shadow);
			

			float4 final;

			final.rgb = (finalCol * _LightColor0.rgb  * atten) + rim ;
			final.a = s.Alpha;

			return final;
		}

        ENDCG
    }
    FallBack "Diffuse"
}
