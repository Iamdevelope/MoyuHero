// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:False,aust:False,igpj:True,qofs:300,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5949,x:32719,y:32712,varname:node_5949,prsc:2|custl-3254-RGB,alpha-3254-A;n:type:ShaderForge.SFN_Tex2d,id:3254,x:32526,y:32957,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:0,ntxv:0,isnm:False|UVIN-2805-OUT;n:type:ShaderForge.SFN_Tex2d,id:5006,x:31827,y:32922,ptovrint:False,ptlb:FlowMap,ptin:_FlowMap,varname:_FlowMap,prsc:2,ntxv:0,isnm:False|UVIN-9068-OUT;n:type:ShaderForge.SFN_Append,id:1593,x:32004,y:32969,varname:node_1593,prsc:2|A-5006-R,B-5006-G;n:type:ShaderForge.SFN_Multiply,id:8870,x:32180,y:32976,varname:node_8870,prsc:2|A-1593-OUT,B-7158-R,C-7365-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7365,x:32004,y:33139,ptovrint:False,ptlb:FlowStr,ptin:_FlowStr,varname:_FlowStr,prsc:0,glob:False,v1:0.1;n:type:ShaderForge.SFN_Add,id:2805,x:32356,y:32957,varname:node_2805,prsc:2|A-8645-UVOUT,B-8870-OUT;n:type:ShaderForge.SFN_TexCoord,id:8645,x:31454,y:32701,varname:node_8645,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:4916,x:31089,y:32905,varname:node_4916,prsc:0;n:type:ShaderForge.SFN_Multiply,id:1884,x:31272,y:32913,varname:node_1884,prsc:2|A-4916-T,B-7833-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7833,x:31089,y:33114,ptovrint:False,ptlb:FlowSpeed,ptin:_FlowSpeed,varname:_FlowSpeed,prsc:0,glob:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:9707,x:31454,y:32913,varname:node_9707,prsc:2|A-1884-OUT,B-5199-OUT;n:type:ShaderForge.SFN_Vector1,id:5199,x:31272,y:33066,varname:node_5199,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:9068,x:31656,y:32891,varname:node_9068,prsc:2|A-8645-UVOUT,B-9707-OUT;n:type:ShaderForge.SFN_Tex2d,id:7158,x:31841,y:33281,ptovrint:False,ptlb:FlowMask,ptin:_FlowMask,varname:_FlowMask,prsc:2,ntxv:0,isnm:False|UVIN-8645-UVOUT;proporder:3254-5006-7365-7833-7158;pass:END;sub:END;*/

Shader "Custom/DynamicFlagPlane01" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _FlowMap ("FlowMap", 2D) = "white" {}
        _FlowStr ("FlowStr", Float ) = 0.1
        _FlowSpeed ("FlowSpeed", Float ) = 0.1
        _FlowMask ("FlowMask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+300"
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
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform fixed _FlowStr;
            uniform fixed _FlowSpeed;
            uniform sampler2D _FlowMask; uniform float4 _FlowMask_ST;
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
                fixed4 node_4916 = _Time + _TimeEditor;
                float2 node_9068 = (i.uv0+float2((node_4916.g*_FlowSpeed),0.0));
                float4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_9068, _FlowMap));
                float4 _FlowMask_var = tex2D(_FlowMask,TRANSFORM_TEX(i.uv0, _FlowMask));
                float2 node_2805 = (i.uv0+(float2(_FlowMap_var.r,_FlowMap_var.g)*_FlowMask_var.r*_FlowStr));
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2805, _MainTex));
                float3 finalColor = _MainTex_var.rgb;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
