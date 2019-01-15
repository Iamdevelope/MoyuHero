// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:696,x:33514,y:32870,varname:node_696,prsc:2|custl-5071-OUT;n:type:ShaderForge.SFN_Tex2d,id:4944,x:32650,y:32893,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4944,prsc:2,ntxv:0,isnm:False|UVIN-8029-OUT;n:type:ShaderForge.SFN_Time,id:7241,x:31864,y:32871,varname:node_7241,prsc:2;n:type:ShaderForge.SFN_Append,id:794,x:32308,y:32893,varname:node_794,prsc:2|A-2431-OUT,B-1105-OUT;n:type:ShaderForge.SFN_Multiply,id:2431,x:32088,y:32832,varname:node_2431,prsc:2|A-2175-OUT,B-7241-T;n:type:ShaderForge.SFN_ValueProperty,id:2175,x:31864,y:32813,ptovrint:False,ptlb:Uspeed1,ptin:_Uspeed1,varname:node_2175,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1105,x:32088,y:32972,varname:node_1105,prsc:2|A-7241-T,B-2868-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2868,x:31864,y:33016,ptovrint:False,ptlb:Vspeed1,ptin:_Vspeed1,varname:node_2868,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:2598,x:31615,y:33180,varname:node_2598,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:8029,x:32478,y:32893,varname:node_8029,prsc:2|A-794-OUT,B-2598-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4182,x:32652,y:33180,ptovrint:False,ptlb:MainTex2,ptin:_MainTex2,varname:node_4182,prsc:2,ntxv:0,isnm:False|UVIN-1400-OUT;n:type:ShaderForge.SFN_Add,id:1400,x:32468,y:33180,varname:node_1400,prsc:2|A-2598-UVOUT,B-5310-OUT;n:type:ShaderForge.SFN_Tex2d,id:5430,x:32672,y:33499,ptovrint:False,ptlb:MainTex3,ptin:_MainTex3,varname:node_5430,prsc:2,ntxv:0,isnm:False|UVIN-244-OUT;n:type:ShaderForge.SFN_Add,id:244,x:32487,y:33499,varname:node_244,prsc:2|A-2598-UVOUT,B-853-OUT;n:type:ShaderForge.SFN_Time,id:2773,x:31863,y:33180,varname:node_2773,prsc:2;n:type:ShaderForge.SFN_Append,id:5310,x:32307,y:33202,varname:node_5310,prsc:2|A-2283-OUT,B-6137-OUT;n:type:ShaderForge.SFN_Multiply,id:2283,x:32087,y:33141,varname:node_2283,prsc:2|A-1997-OUT,B-2773-T;n:type:ShaderForge.SFN_Multiply,id:6137,x:32087,y:33281,varname:node_6137,prsc:2|A-2773-T,B-4212-OUT;n:type:ShaderForge.SFN_Time,id:462,x:31869,y:33501,varname:node_462,prsc:2;n:type:ShaderForge.SFN_Append,id:853,x:32313,y:33523,varname:node_853,prsc:2|A-4647-OUT,B-1192-OUT;n:type:ShaderForge.SFN_Multiply,id:4647,x:32093,y:33462,varname:node_4647,prsc:2|A-1710-OUT,B-462-T;n:type:ShaderForge.SFN_Multiply,id:1192,x:32093,y:33602,varname:node_1192,prsc:2|A-462-T,B-3726-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1997,x:31863,y:33116,ptovrint:False,ptlb:Uspeed2,ptin:_Uspeed2,varname:node_1997,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4212,x:31866,y:33331,ptovrint:False,ptlb:Vspeed2,ptin:_Vspeed2,varname:node_4212,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1710,x:31868,y:33433,ptovrint:False,ptlb:Uspeed3,ptin:_Uspeed3,varname:node_1710,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3726,x:31868,y:33677,ptovrint:False,ptlb:Vspeed3,ptin:_Vspeed3,varname:node_3726,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_If,id:5071,x:33342,y:33112,varname:node_5071,prsc:2|A-8153-OUT,B-1811-OUT,GT-2620-OUT,EQ-5366-OUT,LT-4944-RGB;n:type:ShaderForge.SFN_Vector1,id:1811,x:33104,y:33146,varname:node_1811,prsc:2,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:8153,x:33104,y:33095,ptovrint:False,ptlb:Num,ptin:_Num,varname:node_8153,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Add,id:2620,x:32919,y:33047,varname:node_2620,prsc:2|A-4944-RGB,B-4182-RGB,C-5430-RGB;n:type:ShaderForge.SFN_Add,id:5366,x:32909,y:33339,varname:node_5366,prsc:2|A-4944-RGB,B-4182-RGB;proporder:8153-4944-2175-2868-4182-1997-4212-5430-1710-3726;pass:END;sub:END;*/

Shader "DreamFaction/Effects/UVoffsetAddtive011" {
    Properties {
        _Num ("Num", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _Uspeed1 ("Uspeed1", Float ) = 1
        _Vspeed1 ("Vspeed1", Float ) = 1
        _MainTex2 ("MainTex2", 2D) = "white" {}
        _Uspeed2 ("Uspeed2", Float ) = 1
        _Vspeed2 ("Vspeed2", Float ) = 1
        _MainTex3 ("MainTex3", 2D) = "white" {}
        _Uspeed3 ("Uspeed3", Float ) = 1
        _Vspeed3 ("Vspeed3", Float ) = 1
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
            uniform float _Uspeed1;
            uniform float _Vspeed1;
            uniform sampler2D _MainTex2; uniform float4 _MainTex2_ST;
            uniform sampler2D _MainTex3; uniform float4 _MainTex3_ST;
            uniform float _Uspeed2;
            uniform float _Vspeed2;
            uniform float _Uspeed3;
            uniform float _Vspeed3;
            uniform float _Num;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                fixed4 color : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                fixed4 color : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                o.color = v.color;
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
                float node_5071_if_leA = step(_Num,2.0);
                float node_5071_if_leB = step(2.0,_Num);
                float4 node_7241 = _Time + _TimeEditor;
                float2 node_8029 = (float2((_Uspeed1*node_7241.g),(node_7241.g*_Vspeed1))+i.uv0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_8029, _MainTex));
                float4 node_2773 = _Time + _TimeEditor;
                float2 node_1400 = (i.uv0+float2((_Uspeed2*node_2773.g),(node_2773.g*_Vspeed2)));
                float4 _MainTex2_var = tex2D(_MainTex2,TRANSFORM_TEX(node_1400, _MainTex2));
                float4 node_462 = _Time + _TimeEditor;
                float2 node_244 = (i.uv0+float2((_Uspeed3*node_462.g),(node_462.g*_Vspeed3)));
                float4 _MainTex3_var = tex2D(_MainTex3,TRANSFORM_TEX(node_244, _MainTex3));
                float3 finalColor = lerp((node_5071_if_leA*_MainTex_var.rgb* i.color.rgb)+(node_5071_if_leB*((_MainTex_var.rgb+_MainTex2_var.rgb+_MainTex3_var.rgb)* i.color.rgb)),(_MainTex_var.rgb+_MainTex2_var.rgb* i.color.rgb),node_5071_if_leA*node_5071_if_leB);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
