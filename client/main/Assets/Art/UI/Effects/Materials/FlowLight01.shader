// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:2,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:438,x:32866,y:32620,varname:node_438,prsc:2|custl-4107-OUT;n:type:ShaderForge.SFN_Tex2d,id:3005,x:32474,y:32933,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3005,prsc:2,tex:b2a0741f9ea83f443b135a0c97536bff,ntxv:0,isnm:False|UVIN-2758-OUT;n:type:ShaderForge.SFN_TexCoord,id:8347,x:31943,y:32930,varname:node_8347,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:2758,x:32198,y:32964,varname:node_2758,prsc:2|A-8347-UVOUT,B-5098-OUT;n:type:ShaderForge.SFN_Time,id:3366,x:31548,y:33006,varname:node_3366,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1628,x:31764,y:33169,varname:node_1628,prsc:2|A-3366-T,B-3646-OUT;n:type:ShaderForge.SFN_Multiply,id:3409,x:31764,y:33018,varname:node_3409,prsc:2|A-3366-T,B-6195-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6195,x:31548,y:33169,ptovrint:False,ptlb:XSpeed,ptin:_XSpeed,varname:node_6195,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3646,x:31548,y:33253,ptovrint:False,ptlb:YSpeed,ptin:_YSpeed,varname:node_3646,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_Append,id:5098,x:31936,y:33100,varname:node_5098,prsc:2|A-3409-OUT,B-1628-OUT;n:type:ShaderForge.SFN_Color,id:7023,x:32469,y:32754,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_7023,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4107,x:32673,y:32863,varname:node_4107,prsc:2|A-7023-RGB,B-3005-RGB;proporder:7023-3005-6195-3646;pass:END;sub:END;*/

Shader "DreamFaction/Effects/FlowLight01" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _XSpeed ("XSpeed", Float ) = 1
        _YSpeed ("YSpeed", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _XSpeed;
            uniform float _YSpeed;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
////// Lighting:
                float4 node_3366 = _Time + _TimeEditor;
                float2 node_2758 = (i.uv0+float2((node_3366.g*_XSpeed),(node_3366.g*_YSpeed)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2758, _MainTex));
                float3 finalColor = (_MainColor.rgb*_MainTex_var.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
