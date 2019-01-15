// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5320,x:32869,y:32674,varname:node_5320,prsc:2|custl-6411-RGB,alpha-5025-OUT;n:type:ShaderForge.SFN_Tex2d,id:5428,x:32209,y:32718,ptovrint:False,ptlb:alpha1,ptin:_alpha1,varname:node_5428,prsc:2,tex:c55ca68b291d4b24fb2611602c203a16,ntxv:0,isnm:False|UVIN-6247-OUT;n:type:ShaderForge.SFN_Tex2d,id:6411,x:32209,y:32915,ptovrint:False,ptlb:maintex,ptin:_maintex,varname:node_6411,prsc:2,tex:579e7d84a4a464a47ac2b31b62887ce8,ntxv:0,isnm:False|UVIN-6585-OUT;n:type:ShaderForge.SFN_Tex2d,id:7113,x:31238,y:33349,ptovrint:False,ptlb:alpha2,ptin:_alpha2,varname:node_7113,prsc:2,tex:1305f4e86b323cd44bb9615008bc033e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:8766,x:32432,y:32943,varname:node_8766,prsc:2|A-5428-R,B-6411-R,T-7113-R;n:type:ShaderForge.SFN_Power,id:5025,x:32660,y:32943,varname:node_5025,prsc:2|VAL-8766-OUT,EXP-7137-OUT;n:type:ShaderForge.SFN_Vector1,id:7137,x:32432,y:33089,varname:node_7137,prsc:2,v1:4;n:type:ShaderForge.SFN_Add,id:6585,x:31992,y:32915,varname:node_6585,prsc:2|A-1360-UVOUT,B-1654-OUT;n:type:ShaderForge.SFN_Append,id:5093,x:31067,y:32919,varname:node_5093,prsc:2|A-8484-R,B-8484-R;n:type:ShaderForge.SFN_Tex2d,id:8484,x:30873,y:32902,ptovrint:False,ptlb:reflatex1,ptin:_reflatex1,varname:node_8484,prsc:2,tex:579e7d84a4a464a47ac2b31b62887ce8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1654,x:31436,y:32921,varname:node_1654,prsc:2|A-5093-OUT,B-4866-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4866,x:31224,y:32986,ptovrint:False,ptlb:reflastr1,ptin:_reflastr1,varname:node_4866,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_TexCoord,id:1360,x:31591,y:32731,varname:node_1360,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:7505,x:31271,y:32538,ptovrint:False,ptlb:reflatex2,ptin:_reflatex2,varname:node_7505,prsc:2,tex:931ee4b043ae55b45bd5d856dc7cef29,ntxv:0,isnm:False|UVIN-9104-OUT;n:type:ShaderForge.SFN_Append,id:8836,x:31443,y:32545,varname:node_8836,prsc:2|A-7505-R,B-7505-R;n:type:ShaderForge.SFN_Add,id:6247,x:31871,y:32542,varname:node_6247,prsc:2|A-8631-OUT,B-1360-UVOUT;n:type:ShaderForge.SFN_Multiply,id:8631,x:31643,y:32542,varname:node_8631,prsc:2|A-8836-OUT,B-306-OUT,C-7113-R;n:type:ShaderForge.SFN_Time,id:6192,x:30392,y:32682,varname:node_6192,prsc:2;n:type:ShaderForge.SFN_Append,id:8568,x:30872,y:32697,varname:node_8568,prsc:2|A-8852-OUT,B-8852-OUT;n:type:ShaderForge.SFN_Add,id:9104,x:31061,y:32538,varname:node_9104,prsc:2|A-6098-UVOUT,B-8568-OUT;n:type:ShaderForge.SFN_TexCoord,id:6098,x:30702,y:32420,varname:node_6098,prsc:2,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:5089,x:30392,y:32840,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_5089,prsc:2,glob:False,v1:-0.4;n:type:ShaderForge.SFN_Multiply,id:8852,x:30644,y:32718,varname:node_8852,prsc:2|A-6192-T,B-5089-OUT;n:type:ShaderForge.SFN_ValueProperty,id:306,x:31210,y:32834,ptovrint:False,ptlb:reflastr2,ptin:_reflastr2,varname:node_306,prsc:2,glob:False,v1:-0.5;proporder:6411-5428-7113-8484-4866-7505-5089-306;pass:END;sub:END;*/

Shader "DreamFaction/UI/StartFight01" {
    Properties {
        _maintex ("maintex", 2D) = "white" {}
        _alpha1 ("alpha1", 2D) = "white" {}
        _alpha2 ("alpha2", 2D) = "white" {}
        _reflatex1 ("reflatex1", 2D) = "white" {}
        _reflastr1 ("reflastr1", Float ) = 0.1
        _reflatex2 ("reflatex2", 2D) = "white" {}
        _speed ("speed", Float ) = -0.4
        _reflastr2 ("reflastr2", Float ) = -0.5
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
            uniform sampler2D _alpha1; uniform float4 _alpha1_ST;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform sampler2D _alpha2; uniform float4 _alpha2_ST;
            uniform sampler2D _reflatex1; uniform float4 _reflatex1_ST;
            uniform float _reflastr1;
            uniform sampler2D _reflatex2; uniform float4 _reflatex2_ST;
            uniform float _speed;
            uniform float _reflastr2;
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
                float4 _reflatex1_var = tex2D(_reflatex1,TRANSFORM_TEX(i.uv0, _reflatex1));
                float2 node_6585 = (i.uv0+(float2(_reflatex1_var.r,_reflatex1_var.r)*_reflastr1));
                float4 _maintex_var = tex2D(_maintex,TRANSFORM_TEX(node_6585, _maintex));
                float3 finalColor = _maintex_var.rgb;
                float4 node_6192 = _Time + _TimeEditor;
                float node_8852 = (node_6192.g*_speed);
                float2 node_9104 = (i.uv0+float2(node_8852,node_8852));
                float4 _reflatex2_var = tex2D(_reflatex2,TRANSFORM_TEX(node_9104, _reflatex2));
                float4 _alpha2_var = tex2D(_alpha2,TRANSFORM_TEX(i.uv0, _alpha2));
                float2 node_6247 = ((float2(_reflatex2_var.r,_reflatex2_var.r)*_reflastr2*_alpha2_var.r)+i.uv0);
                float4 _alpha1_var = tex2D(_alpha1,TRANSFORM_TEX(node_6247, _alpha1));
                return fixed4(finalColor,pow(lerp(_alpha1_var.r,_maintex_var.r,_alpha2_var.r),4.0));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
