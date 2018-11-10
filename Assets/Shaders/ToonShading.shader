Shader "Custom/ToonShading" {
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_CelShadingLevels ("Cel Shading Levels", Range(0,15)) = 2

        _Ambient ("Ambient intensity", Range(0., 0.5)) = 0.15
	}


	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Instead of Standard, point to a function called LightingToon()
		#pragma surface surf Toon

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RampTex;
		float _CelShadingLevels;
        half _Ambient;

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}



		half4 LightingToon (SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL =  max(0.0, dot(normalize(s.Normal), normalize(lightDir)));
			// Snap the lighting level
			half cel = floor(NdotL * _CelShadingLevels) / (_CelShadingLevels - 0.5);

			half4 c;
			c.rgb = s.Albedo;
			c.rgb *= saturate(cel + _Ambient) * _LightColor0.rgb;
			c.a = s.Alpha;

			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}