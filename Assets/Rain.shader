// 参考URL
// https://docs.google.com/presentation/d/1_JHp-k4bDc9WhcMlC9hAPSGj_0UaJh5DBpSlLkwI74M/mobilepresent?slide=id.g5dea027fa_0427
Shader "Custom/Rain" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (0,0,0,1.0)
        _Speed ("Speed", Float) = 6.0
        _Scale ("Scale", Float) = 4.0
    }
    SubShader {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        CGPROGRAM
        #pragma surface surf Lambert alpha
        #pragma vertex vert

        sampler2D _MainTex;

        // [0;1]のランダムな値を返す
        float nhash11(float n){
            return frac(sin(n) * 43758.5453);
        }

        // 値域[a;b] を 値域[0;1]へ変換する
        float remap(float t, float a, float b){
            return clamp((t-a)/(b-a), 0, 1);
        }

        fixed4 _Color;
        half _Speed;
        half _Scale;

        void vert(inout appdata_full v) {
            fixed rnd = nhash11(fmod(v.vertex.z, 512.0));
            float timer = _Time.w * _Speed * remap(0.7, 1.0, rnd);
            v.vertex.y -= fmod(-v.vertex.y + timer, _Scale) + v.vertex.y - _Scale * 0.5;
        }

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;
            o.Alpha  = _Color.a;
        }
        ENDCG
    }
}