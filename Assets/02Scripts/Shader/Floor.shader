Shader "Custom/Floor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("MainTexture", 2D) = "white" {}
        _MainTex2("MainTexture2", 2D) = "white" {}
        _MainTex3("MainTexture3", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Interpolation1("interpolation1", Range(0,1)) = 0.0
        _Interpolation2("interpolation2", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MainTex2;
        sampler2D _MainTex3;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
            float2 uv_MainTex3;
            float4 color:COLOR;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed _interpolation1;
        fixed _Interpolation2;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 d = tex2D(_MainTex2, IN.uv_MainTex2) * _Color;
            fixed4 e = tex2D(_MainTex3, IN.uv_MainTex3) * _Color;
            //fixed3 tmp = lerp(c,d, _interpolation1);
            //fixed tmp2 = lerp(tmp, e, _Interpolation2); //합치기
            fixed3 tmp = lerp(c.rgb, d.rgb, IN.color.r);
            o.Albedo = lerp(tmp.rgb, e.rgb, IN.color.b); //r값부분에 비춰지게
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
