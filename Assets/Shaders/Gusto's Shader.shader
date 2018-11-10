Shader "Custom/Gusto's Shader" {
	Properties 
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)

		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		_CelShadingLevels ("Cel Shading Levels", Range(0,15)) = 2

        _Ambient ("Ambient Intensity", Range(0., 0.5)) = 0.2
	}


	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Instead of Standard, point to a function called LightingToon()
		#pragma surface surf Cel

		float _CelShadingLevels;
        half _Ambient;

		#pragma lighting Cel exclude_path:prepass
		inline half4 LightingCel (SurfaceOutput s, half3 lightDir, half atten)
		{
			
			
			half NdotL =  max(0.0, dot(normalize(s.Normal), lightDir)  * atten);
			// Snap the lighting level
			half cel = floor(NdotL * _CelShadingLevels) / (_CelShadingLevels - 0.5);

			half4 c;
			c.rgb = s.Albedo;
			c.rgb *= saturate(cel + _Ambient) * _LightColor0.rgb;
			c.a = s.Alpha;

			return c;
		}


		sampler2D _MainTex;
		float4 _Color;

		struct Input 
		{
			float2 uv_MainTex : TEXCOORD0;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
