
Shader "Wolf/ColorPickerShader"
{
    Properties
    {
        _square("square", Range(0, 1)) = 0.5
        _circleIn("circleIn", Range(0, 1)) = 0.8
        _circleOut("circleOut", Range(0, 1)) = 1
        _hue("hue", Range(0, 1)) = 1
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _Color ("Tint", Color) = (1,1,1,1)
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
	    Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                half4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };


            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _MaskSoftnessX;
            float _MaskSoftnessY;

            float _hue;
            float _square;
            float _circleOut;
            float _circleIn;

            
            float2 PolarCoordinate( float2 uv){
                float2 delta = uv - (0.5, 0.5);
                float radius = length(delta) * 2 * 1;
                float angle = atan2(delta.x, delta.y) * 1.0/6.28 * 1;
                return float2(radius, angle);
            }
            
            float Rectangle(float Size,float2 UV)
            {
                float d = (UV.x * UV.y);
                //float2 d = abs(UV * 2 - 1) - float2(Size, Size);
                //d = saturate(1 - d / fwidth(d));
                return  UV.x < 1 ?
                        UV.x > 0 ?
                        UV.y < 1 ?
                        UV.y > 0 ?
                        1 : 0 : 0 :0 : 0;
            }

            float3 Hue(float3 Col, float Ofs)
            {
                // RGB to HSV
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 P = lerp(float4(Col.bg, K.wz), float4(Col.gb, K.xy), step(Col.b, Col.g));
                float4 Q = lerp(float4(P.xyw, Col.r), float4(Col.r, P.yzx), step(P.x, Col.r));
                float D = Q.x - min(Q.w, Q.y);
                float E = 1e-10;
                float V = (D == 0) ? Q.x : (Q.x + E);
                float3 hsv = float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), V);
        
                float hue = hsv.x + Ofs;
                hsv.x = (hue < 0)
                        ? hue + 1
                        : (hue > 1)
                            ? hue - 1
                            : hue;
        
                // HSV to RGB
                float4 K2 = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 P2 = abs(frac(hsv.xxx + K2.xyz) * 6.0 - K2.www);
                return hsv.z * lerp(K2.xxx, saturate(P2 - K2.xxx), hsv.y);
            }


            v2f vert(appdata_t v)
            {
                v2f OUT;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                float4 vPosition = UnityObjectToClipPos(v.vertex);

                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                
                OUT.texcoord = float4(v.texcoord.x, v.texcoord.y, maskUV.x, maskUV.y);

                OUT.mask = half4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 
                0.25 / (0.25 * half2(_MaskSoftnessX, _MaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half4 color = (tex2D(_MainTex, i.texcoord) + _TextureSampleAdd) * i.color;

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(i.mask.xy)) * i.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                float2 Polar = PolarCoordinate(i.texcoord);
                float PolarMask = Polar < _circleOut ? 
                                    Polar > _circleIn ? 1 : 0
                                                    : 0;
                float3 PolarRaimbow = Hue(float3(1, 0, 0), Polar.g);
                float2 ResizedUV =  saturate((i.texcoord + (1-_square) * -0.5) * (1/_square));
                float SquareMask = Rectangle(_square, ResizedUV);
                float3 SquareColor = lerp(float3(1,1,1), Hue(float3(1, 0, 0), _hue), ResizedUV.x) * ResizedUV.y;
                return fixed4(SquareColor, 1) * SquareMask + fixed4( PolarRaimbow * PolarMask,  1) * PolarMask;
            }
        ENDCG
        }
    }
}