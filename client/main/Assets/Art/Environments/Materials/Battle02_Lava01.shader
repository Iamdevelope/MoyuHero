// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0.1031947,fgcb:0.2720588,fgca:1,fgde:0.02,fgrn:8,fgrf:50,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2438,x:34303,y:32738,varname:node_2438,prsc:2|custl-4724-OUT,alpha-1259-R,voffset-2486-OUT;n:type:ShaderForge.SFN_Color,id:2441,x:33794,y:32750,ptovrint:False,ptlb:maincolor,ptin:_maincolor,varname:node_7625,prsc:2,glob:False,c1:1,c2:0.5586207,c3:0,c4:1;n:type:ShaderForge.SFN_NormalVector,id:2446,x:33255,y:33241,prsc:2,pt:False;n:type:ShaderForge.SFN_Tex2d,id:2449,x:33794,y:32925,ptovrint:False,ptlb:maintex,ptin:_maintex,varname:node_2471,prsc:2,tex:7ee69aa98e5ba7d4e99d3d531d23290b,ntxv:0,isnm:False|UVIN-2462-OUT;n:type:ShaderForge.SFN_Add,id:2462,x:33616,y:32925,varname:node_2462,prsc:2|A-2463-OUT,B-2464-OUT;n:type:ShaderForge.SFN_Add,id:2463,x:33390,y:32828,varname:node_2463,prsc:2|A-5961-UVOUT,B-2469-OUT;n:type:ShaderForge.SFN_Multiply,id:2464,x:33390,y:32947,varname:node_2464,prsc:2|A-2466-OUT,B-2465-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2465,x:33210,y:33149,ptovrint:False,ptlb:refrastrength,ptin:_refrastrength,varname:node_7589,prsc:2,glob:False,v1:0.02;n:type:ShaderForge.SFN_Append,id:2466,x:33200,y:32989,varname:node_2466,prsc:2|A-2467-R,B-2467-R;n:type:ShaderForge.SFN_Tex2d,id:2467,x:33033,y:32989,ptovrint:False,ptlb:refratex,ptin:_refratex,varname:node_5131,prsc:2,tex:105509fc2446cae4f9f9971158efab6f,ntxv:0,isnm:False|UVIN-2468-OUT;n:type:ShaderForge.SFN_Add,id:2468,x:32864,y:32989,varname:node_2468,prsc:2|A-5961-UVOUT,B-2472-OUT;n:type:ShaderForge.SFN_Multiply,id:2469,x:33210,y:32856,varname:node_2469,prsc:2|A-2474-OUT,B-2470-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2470,x:33033,y:32915,ptovrint:False,ptlb:maintexspeed,ptin:_maintexspeed,varname:node_7648,prsc:2,glob:False,v1:-0.5;n:type:ShaderForge.SFN_Multiply,id:2472,x:32690,y:33009,varname:node_2472,prsc:2|A-2474-OUT,B-2473-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2473,x:32521,y:33171,ptovrint:False,ptlb:refraspeed,ptin:_refraspeed,varname:node_3784,prsc:2,glob:False,v1:0.2;n:type:ShaderForge.SFN_Append,id:2474,x:32521,y:33009,varname:node_2474,prsc:2|A-2475-TSL,B-2476-OUT;n:type:ShaderForge.SFN_Time,id:2475,x:32351,y:33009,varname:node_2475,prsc:2;n:type:ShaderForge.SFN_Vector1,id:2476,x:32351,y:33136,varname:node_2476,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:2486,x:33832,y:33235,varname:node_2486,prsc:2|A-2446-OUT,B-2488-OUT,C-2487-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2487,x:33639,y:33473,ptovrint:False,ptlb:wavestrength,ptin:_wavestrength,varname:node_6276,prsc:2,glob:False,v1:0.2;n:type:ShaderForge.SFN_Sin,id:2488,x:33639,y:33325,varname:node_2488,prsc:2|IN-2490-OUT;n:type:ShaderForge.SFN_Multiply,id:2490,x:33445,y:33325,varname:node_2490,prsc:2|A-2475-T,B-2491-OUT;n:type:ShaderForge.SFN_Multiply,id:2491,x:33255,y:33389,varname:node_2491,prsc:2|A-2492-X,B-2493-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:2492,x:33041,y:33401,varname:node_2492,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2493,x:33041,y:33551,ptovrint:False,ptlb:waveferq,ptin:_waveferq,varname:node_7481,prsc:2,glob:False,v1:0.05;n:type:ShaderForge.SFN_TexCoord,id:5961,x:32608,y:32704,varname:node_5961,prsc:2,uv:1;n:type:ShaderForge.SFN_Add,id:4724,x:34029,y:32871,varname:node_4724,prsc:2|A-2449-RGB,B-2449-RGB,C-2441-RGB;n:type:ShaderForge.SFN_Tex2d,id:1259,x:34029,y:33061,ptovrint:False,ptlb:alpha,ptin:_alpha,varname:node_1259,prsc:2,ntxv:0,isnm:False|UVIN-4265-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4265,x:33794,y:33080,varname:node_4265,prsc:2,uv:0;proporder:2441-2449-2467-2470-2465-2473-2487-2493-1259;pass:END;sub:END;*/

Shader "DreamFaction/Environment/Lava01" {
    Properties {
        _maincolor ("maincolor", Color) = (1,0.5586207,0,1)
        _maintex ("maintex", 2D) = "white" {}
        _refratex ("refratex", 2D) = "white" {}
        _maintexspeed ("maintexspeed", Float ) = -0.5
        _refrastrength ("refrastrength", Float ) = 0.02
        _refraspeed ("refraspeed", Float ) = 0.2
        _wavestrength ("wavestrength", Float ) = 0.2
        _waveferq ("waveferq", Float ) = 0.05
        _alpha ("alpha", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
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
            uniform float4 _maincolor;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float _refrastrength;
            uniform sampler2D _refratex; uniform float4 _refratex_ST;
            uniform float _maintexspeed;
            uniform float _refraspeed;
            uniform float _wavestrength;
            uniform float _waveferq;
            uniform sampler2D _alpha; uniform float4 _alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 screenPos : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_2475 = _Time + _TimeEditor;
                v.vertex.xyz += (v.normal*sin((node_2475.g*(mul(_Object2World, v.vertex).r*_waveferq)))*_wavestrength);
                o.posWorld = mul(_Object2World, v.vertex);
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
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float3 normalDirection = i.normalDir;
////// Lighting:
                float4 node_2475 = _Time + _TimeEditor;
                float2 node_2474 = float2(node_2475.r,0.0);
                float2 node_2468 = (i.uv1+(node_2474*_refraspeed));
                float4 _refratex_var = tex2D(_refratex,TRANSFORM_TEX(node_2468, _refratex));
                float2 node_2462 = ((i.uv1+(node_2474*_maintexspeed))+(float2(_refratex_var.r,_refratex_var.r)*_refrastrength));
                float4 _maintex_var = tex2D(_maintex,TRANSFORM_TEX(node_2462, _maintex));
                float3 finalColor = (_maintex_var.rgb+_maintex_var.rgb+_maincolor.rgb);
                float4 _alpha_var = tex2D(_alpha,TRANSFORM_TEX(i.uv0, _alpha));
                return fixed4(finalColor,_alpha_var.r);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
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
            uniform float _wavestrength;
            uniform float _waveferq;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 posWorld : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_2475 = _Time + _TimeEditor;
                v.vertex.xyz += (v.normal*sin((node_2475.g*(mul(_Object2World, v.vertex).r*_waveferq)))*_wavestrength);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
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
            uniform float _wavestrength;
            uniform float _waveferq;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_2475 = _Time + _TimeEditor;
                v.vertex.xyz += (v.normal*sin((node_2475.g*(mul(_Object2World, v.vertex).r*_waveferq)))*_wavestrength);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
