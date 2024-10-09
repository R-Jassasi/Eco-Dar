Shader "Water/Ocean_03" {
    Properties {
        _WaterColor ("Water Color", Color) = (0.4926471, 0.6011156, 1, 1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _FoamTexture ("Foam Texture", 2D) = "white" {}
        _AdditionalTexture ("Additional Texture", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _Highlight ("Highlight", 2D) = "white" {}
        [Space(10)]
        [Space(10)]
        _FoamOpacity ("Foam Opacity", Range(0, 2)) = 1
        _Noiselevel ("Noise level", Range(0, 0.12)) = 0.02
        _WaterOrientation ("Water Orientation", Range(0, 4)) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            
            uniform sampler2D _MainTexture; 
            uniform sampler2D _FoamTexture; 
            uniform float _FoamOpacity;

            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(VertexOutput i) : COLOR {
                float2 uv = i.uv0;

                // Sample the main texture and foam texture
                float4 mainTexColor = tex2D(_MainTexture, uv);
                float4 foamTexColor = tex2D(_FoamTexture, uv);

                // Combine the main color with foam opacity
                float3 finalColor = mainTexColor.rgb * (1.0 - foamTexColor.r * _FoamOpacity);
                
                // Ensure the final color is clamped between 0 and 1
                return fixed4(finalColor, 1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
