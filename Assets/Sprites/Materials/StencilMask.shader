Shader "Custom Shader/Sample Clipping Transparent"
{
	Properties
	{
	_Color("2X Color (RGBA Mul)", Color) = (0.5, 0.5, 0.5, 1.0)
	_MainTex("Base Texture (RGBA)", 2D) = "white" {}
	_MaskTex("Mask Texture (A)", 2D) = "white" {}
	_MaskScreenSpaceOffset("Mask Screen Space Offset (XY_Scale)", Vector) = (0, 0, 0, 1)
	}

	SubShader
	{
		Stencil
		{
			Ref 1
			Comp LEqual
		}
		
	Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "PreviewType" = "Plane"}
	Blend SrcAlpha OneMinusSrcAlpha
	LOD 200

	CGPROGRAM

	#pragma surface surf SimpleColor alpha
	#pragma target 3.0
	half4 LightingSimpleColor(SurfaceOutput s, half3 lightDir, half atten)
	{
		half4 c;
		c.rgb = s.Albedo;
		c.a = s.Alpha;
		return c;
	}

	half4 _Color;
	sampler2D _MainTex;
	sampler2D _MaskTex;
	float4 _MaskScreenSpaceOffset;

	struct Input
	{
		float2 uv_MainTex;
		float4 screenPos;
		float4 color : COLOR;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex);

		c.rgb *= _Color.rgb * 2.0f;
		float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

		screenUV -= float2(0.5f, 0.5f);
		screenUV.x *= _MaskScreenSpaceOffset.z;
		screenUV.y *= _MaskScreenSpaceOffset.w;
		screenUV.x += _MaskScreenSpaceOffset.x * _MaskScreenSpaceOffset.z;
		screenUV.y += _MaskScreenSpaceOffset.y * _MaskScreenSpaceOffset.w;
		screenUV += float2(0.5f, 0.5f);
		float mask = tex2D(_MaskTex, screenUV).r;

		c.a *= mask;
		o.Alpha = c.a * _Color.a;
		o.Albedo = c.rgb;
	}
	ENDCG
	}
}