// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:236,x:33379,y:32814,varname:node_236,prsc:2|custl-7290-OUT;n:type:ShaderForge.SFN_Tex2d,id:8373,x:32537,y:32935,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8373,prsc:2,ntxv:0,isnm:False|UVIN-766-OUT;n:type:ShaderForge.SFN_Tex2d,id:8282,x:32537,y:33135,ptovrint:False,ptlb:MainTex1,ptin:_MainTex1,varname:node_8282,prsc:2,ntxv:0,isnm:False|UVIN-8986-OUT;n:type:ShaderForge.SFN_TexCoord,id:5543,x:31803,y:32790,varname:node_5543,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:7623,x:31154,y:33045,varname:node_7623,prsc:2;n:type:ShaderForge.SFN_Append,id:366,x:31571,y:33127,varname:node_366,prsc:2|A-7623-T,B-8148-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:9791,x:31783,y:33036,ptovrint:False,ptlb:XY,ptin:_XY,varname:node_9791,prsc:2,on:True|A-5810-OUT,B-366-OUT;n:type:ShaderForge.SFN_Vector1,id:8148,x:31317,y:33059,varname:node_8148,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:5810,x:31571,y:32971,varname:node_5810,prsc:2|A-8148-OUT,B-7623-T;n:type:ShaderForge.SFN_Multiply,id:1798,x:31963,y:33036,varname:node_1798,prsc:2|A-9791-OUT,B-6928-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6928,x:31783,y:33190,ptovrint:False,ptlb:TexSpeed,ptin:_TexSpeed,varname:node_6928,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Add,id:766,x:32367,y:32935,varname:node_766,prsc:2|A-5543-UVOUT,B-1798-OUT;n:type:ShaderForge.SFN_Multiply,id:1476,x:32186,y:33242,varname:node_1476,prsc:2|A-1798-OUT,B-4533-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4533,x:32001,y:33263,ptovrint:False,ptlb:Tex1Speed,ptin:_Tex1Speed,varname:node_4533,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:8986,x:32370,y:33135,varname:node_8986,prsc:2|A-5543-UVOUT,B-1476-OUT;n:type:ShaderForge.SFN_Tex2d,id:8230,x:32541,y:33598,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_8230,prsc:2,ntxv:0,isnm:False|UVIN-5543-UVOUT;n:type:ShaderForge.SFN_Color,id:8505,x:32537,y:32763,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_8505,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:7232,x:32917,y:33054,varname:node_7232,prsc:2|A-5820-OUT,B-8339-OUT;n:type:ShaderForge.SFN_Multiply,id:5820,x:32728,y:32890,varname:node_5820,prsc:2|A-8505-RGB,B-8373-RGB,C-9777-OUT;n:type:ShaderForge.SFN_Multiply,id:8339,x:32746,y:33267,varname:node_8339,prsc:2|A-8282-RGB,B-781-RGB,C-7378-OUT;n:type:ShaderForge.SFN_Color,id:781,x:32537,y:33321,ptovrint:False,ptlb:MainColor1,ptin:_MainColor1,varname:node_781,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:7290,x:33138,y:33054,varname:node_7290,prsc:2|A-7232-OUT,B-8230-R,C-8373-A,D-8282-A;n:type:ShaderForge.SFN_ValueProperty,id:9777,x:32537,y:32682,ptovrint:False,ptlb:Str,ptin:_Str,varname:node_9777,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7378,x:32537,y:33501,ptovrint:False,ptlb:Str1,ptin:_Str1,varname:node_7378,prsc:2,glob:False,v1:1;proporder:8505-8373-9777-6928-781-8282-7378-4533-8230-9791;pass:END;sub:END;*/

Shader "DreamFaction/Effects/Surf01" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _Str ("Str", Float ) = 1
        _TexSpeed ("TexSpeed", Float ) = 1
        _MainColor1 ("MainColor1", Color) = (0.5,0.5,0.5,1)
        _MainTex1 ("MainTex1", 2D) = "white" {}
        _Str1 ("Str1", Float ) = 1
        _Tex1Speed ("Tex1Speed", Float ) = 0.5
        _Mask ("Mask", 2D) = "white" {}
        [MaterialToggle] _XY ("XY", Float ) = 0
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
            uniform sampler2D _MainTex1; uniform float4 _MainTex1_ST;
            uniform fixed _XY;
            uniform float _TexSpeed;
            uniform float _Tex1Speed;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _MainColor;
            uniform float4 _MainColor1;
            uniform float _Str;
            uniform float _Str1;
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
                float node_8148 = 0.0;
                float4 node_7623 = _Time + _TimeEditor;
                float2 node_1798 = (lerp( float2(node_8148,node_7623.g), float2(node_7623.g,node_8148), _XY )*_TexSpeed);
                float2 node_766 = (i.uv0+node_1798);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_766, _MainTex));
                float2 node_8986 = (i.uv0+(node_1798*_Tex1Speed));
                float4 _MainTex1_var = tex2D(_MainTex1,TRANSFORM_TEX(node_8986, _MainTex1));
                float3 node_7232 = ((_MainColor.rgb*_MainTex_var.rgb*_Str)+(_MainTex1_var.rgb*_MainColor1.rgb*_Str1));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float3 finalColor = (node_7232*_Mask_var.r*_MainTex_var.a*_MainTex1_var.a);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
