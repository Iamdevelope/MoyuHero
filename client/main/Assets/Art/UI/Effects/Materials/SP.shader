// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:2,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:35345,y:32919,varname:node_1,prsc:2|custl-683-OUT,alpha-789-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:34131,y:33126,ptovrint:False,ptlb:maintex,ptin:_maintex,varname:node_9385,prsc:2,tex:0a34fd1b20d81ad47b909a49e4e783b4,ntxv:0,isnm:False|UVIN-98-OUT;n:type:ShaderForge.SFN_Tex2d,id:73,x:33173,y:33057,ptovrint:False,ptlb:offsettex01,ptin:_offsettex01,varname:node_2095,prsc:2,ntxv:0,isnm:False|UVIN-118-OUT;n:type:ShaderForge.SFN_Add,id:98,x:33899,y:33112,varname:node_98,prsc:2|A-99-UVOUT,B-132-OUT;n:type:ShaderForge.SFN_TexCoord,id:99,x:32667,y:32658,varname:node_99,prsc:2,uv:1;n:type:ShaderForge.SFN_Append,id:100,x:33336,y:33074,varname:node_100,prsc:2|A-73-R,B-73-G;n:type:ShaderForge.SFN_Time,id:105,x:32458,y:33078,varname:node_105,prsc:2;n:type:ShaderForge.SFN_Vector1,id:106,x:32640,y:33221,varname:node_106,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:107,x:32809,y:33078,varname:node_107,prsc:2|A-138-OUT,B-106-OUT;n:type:ShaderForge.SFN_Add,id:118,x:32975,y:33058,varname:node_118,prsc:2|A-99-UVOUT,B-107-OUT;n:type:ShaderForge.SFN_Multiply,id:132,x:33556,y:33127,varname:node_132,prsc:2|A-100-OUT,B-2708-OUT;n:type:ShaderForge.SFN_Multiply,id:138,x:32640,y:33078,varname:node_138,prsc:2|A-105-TSL,B-503-OUT;n:type:ShaderForge.SFN_Tex2d,id:238,x:34131,y:33302,ptovrint:False,ptlb:alpha,ptin:_alpha,varname:node_3945,prsc:2,ntxv:0,isnm:False|UVIN-373-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:373,x:33414,y:32698,varname:node_373,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:400,x:33200,y:33340,ptovrint:False,ptlb:offsettex02,ptin:_offsettex02,varname:node_1020,prsc:2,ntxv:0,isnm:False|UVIN-118-OUT;n:type:ShaderForge.SFN_Append,id:401,x:33419,y:33357,varname:node_401,prsc:2|A-400-R,B-400-G;n:type:ShaderForge.SFN_Tex2d,id:407,x:34131,y:33480,ptovrint:False,ptlb:mask1,ptin:_mask1,varname:node_1503,prsc:2,ntxv:0,isnm:False|UVIN-417-OUT;n:type:ShaderForge.SFN_Add,id:417,x:33952,y:33480,varname:node_417,prsc:2|A-99-UVOUT,B-421-OUT,C-418-OUT;n:type:ShaderForge.SFN_Append,id:418,x:33766,y:33761,varname:node_418,prsc:2|A-497-OUT,B-503-OUT;n:type:ShaderForge.SFN_ValueProperty,id:419,x:33039,y:33688,ptovrint:False,ptlb:value unreal,ptin:_valueunreal,varname:node_1172,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:421,x:33780,y:33501,varname:node_421,prsc:2|A-401-OUT,B-2714-OUT;n:type:ShaderForge.SFN_Vector1,id:496,x:33419,y:33611,varname:node_496,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:497,x:33591,y:33649,varname:node_497,prsc:2|A-496-OUT,B-501-OUT;n:type:ShaderForge.SFN_Multiply,id:501,x:33419,y:33671,varname:node_501,prsc:2|A-664-OUT,B-663-OUT;n:type:ShaderForge.SFN_Vector1,id:503,x:32473,y:33794,varname:node_503,prsc:2,v1:-20;n:type:ShaderForge.SFN_Tex2d,id:548,x:34131,y:32765,ptovrint:False,ptlb:maincolor01,ptin:_maincolor01,varname:node_5731,prsc:2,ntxv:0,isnm:False|UVIN-373-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:657,x:34131,y:32679,ptovrint:False,ptlb:strength,ptin:_strength,varname:node_3856,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Vector1,id:662,x:33039,y:33752,varname:node_662,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Add,id:663,x:33244,y:33691,varname:node_663,prsc:2|A-419-OUT,B-662-OUT;n:type:ShaderForge.SFN_Vector1,id:664,x:33244,y:33631,varname:node_664,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Multiply,id:683,x:35124,y:33134,varname:node_683,prsc:2|A-657-OUT,B-2-RGB,C-898-OUT;n:type:ShaderForge.SFN_Multiply,id:789,x:35124,y:33253,varname:node_789,prsc:2|A-238-R,B-407-R,C-896-R,D-931-OUT;n:type:ShaderForge.SFN_VertexColor,id:896,x:34131,y:33635,varname:node_896,prsc:2;n:type:ShaderForge.SFN_Lerp,id:898,x:34749,y:33178,varname:node_898,prsc:2|A-932-RGB,B-548-RGB,T-899-R;n:type:ShaderForge.SFN_Tex2d,id:899,x:34175,y:33975,ptovrint:False,ptlb:mask2,ptin:_mask2,varname:node_2450,prsc:2,ntxv:0,isnm:False|UVIN-911-OUT;n:type:ShaderForge.SFN_Append,id:908,x:33784,y:34007,varname:node_908,prsc:2|A-919-OUT,B-916-OUT;n:type:ShaderForge.SFN_ValueProperty,id:909,x:33042,y:34086,ptovrint:False,ptlb:value real,ptin:_valuereal,varname:node_9457,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Add,id:911,x:33957,y:33975,varname:node_911,prsc:2|A-99-UVOUT,B-908-OUT;n:type:ShaderForge.SFN_Vector1,id:916,x:33600,y:34173,varname:node_916,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:917,x:33227,y:34060,varname:node_917,prsc:2|A-909-OUT,B-662-OUT;n:type:ShaderForge.SFN_Multiply,id:918,x:33413,y:34024,varname:node_918,prsc:2|A-664-OUT,B-917-OUT;n:type:ShaderForge.SFN_Subtract,id:919,x:33600,y:34006,varname:node_919,prsc:2|A-496-OUT,B-918-OUT;n:type:ShaderForge.SFN_Vector1,id:929,x:34444,y:33847,varname:node_929,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:930,x:34444,y:33807,varname:node_930,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Lerp,id:931,x:34701,y:33824,varname:node_931,prsc:2|A-930-OUT,B-929-OUT,T-899-R;n:type:ShaderForge.SFN_Color,id:932,x:34131,y:32959,ptovrint:False,ptlb:maincolor02,ptin:_maincolor02,varname:node_3112,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:2708,x:33336,y:33234,varname:node_2708,prsc:2,v1:-0.07;n:type:ShaderForge.SFN_Vector1,id:2714,x:33579,y:33535,varname:node_2714,prsc:2,v1:0.1;proporder:909-419-548-2-238-73-400-407-899-657-932;pass:END;sub:END;*/

Shader "DreamFaction/Interface/SP" {
    Properties {
        _valuereal ("value real", Float ) = 1
        _valueunreal ("value unreal", Float ) = 1
        _maincolor01 ("maincolor01", 2D) = "white" {}
        _maintex ("maintex", 2D) = "white" {}
        _alpha ("alpha", 2D) = "white" {}
        _offsettex01 ("offsettex01", 2D) = "white" {}
        _offsettex02 ("offsettex02", 2D) = "white" {}
        _mask1 ("mask1", 2D) = "white" {}
        _mask2 ("mask2", 2D) = "white" {}
        _strength ("strength", Float ) = 2
        _maincolor02 ("maincolor02", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
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
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform sampler2D _offsettex01; uniform float4 _offsettex01_ST;
            uniform sampler2D _alpha; uniform float4 _alpha_ST;
            uniform sampler2D _offsettex02; uniform float4 _offsettex02_ST;
            uniform sampler2D _mask1; uniform float4 _mask1_ST;
            uniform float _valueunreal;
            uniform sampler2D _maincolor01; uniform float4 _maincolor01_ST;
            uniform float _strength;
            uniform sampler2D _mask2; uniform float4 _mask2_ST;
            uniform float _valuereal;
            uniform float4 _maincolor02;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
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
                float4 node_105 = _Time + _TimeEditor;
                float node_503 = (-20.0);
                float2 node_118 = (i.uv1+float2((node_105.r*node_503),0.0));
                float4 _offsettex01_var = tex2D(_offsettex01,TRANSFORM_TEX(node_118, _offsettex01));
                float2 node_98 = (i.uv1+(float2(_offsettex01_var.r,_offsettex01_var.g)*(-0.07)));
                float4 _maintex_var = tex2D(_maintex,TRANSFORM_TEX(node_98, _maintex));
                float4 _maincolor01_var = tex2D(_maincolor01,TRANSFORM_TEX(i.uv0, _maincolor01));
                float node_496 = 2.0;
                float node_664 = 1.1;
                float node_662 = 0.3;
                float2 node_911 = (i.uv1+float2((node_496-(node_664*(_valuereal+node_662))),0.0));
                float4 _mask2_var = tex2D(_mask2,TRANSFORM_TEX(node_911, _mask2));
                float3 finalColor = (_strength*_maintex_var.rgb*lerp(_maincolor02.rgb,_maincolor01_var.rgb,_mask2_var.r));
                float4 _alpha_var = tex2D(_alpha,TRANSFORM_TEX(i.uv0, _alpha));
                float4 _offsettex02_var = tex2D(_offsettex02,TRANSFORM_TEX(node_118, _offsettex02));
                float2 node_401 = float2(_offsettex02_var.r,_offsettex02_var.g);
                float2 node_417 = (i.uv1+(node_401*0.1)+float2((node_496-(node_664*(_valueunreal+node_662))),node_503));
                float4 _mask1_var = tex2D(_mask1,TRANSFORM_TEX(node_417, _mask1));
                return fixed4(finalColor,(_alpha_var.r*_mask1_var.r*i.vertexColor.r*lerp(0.5,1.0,_mask2_var.r)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
