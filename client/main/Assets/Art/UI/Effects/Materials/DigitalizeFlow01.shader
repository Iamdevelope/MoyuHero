// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:2,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7638,x:33659,y:32953,varname:node_7638,prsc:2|custl-2061-OUT,alpha-4912-OUT;n:type:ShaderForge.SFN_TexCoord,id:3649,x:31947,y:32799,varname:node_3649,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:9808,x:32268,y:33814,varname:node_9808,prsc:2|A-3649-V,B-4259-OUT;n:type:ShaderForge.SFN_Vector1,id:4259,x:32108,y:33885,varname:node_4259,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Abs,id:3242,x:32599,y:33814,varname:node_3242,prsc:2|IN-5408-OUT;n:type:ShaderForge.SFN_OneMinus,id:6128,x:32761,y:33814,varname:node_6128,prsc:2|IN-3242-OUT;n:type:ShaderForge.SFN_Tex2d,id:226,x:32773,y:33019,varname:node_226,prsc:2,tex:d7d730d3faf252447a1f9fa99f7149a2,ntxv:0,isnm:False|UVIN-5600-OUT,TEX-7708-TEX;n:type:ShaderForge.SFN_Add,id:5600,x:32515,y:33020,varname:node_5600,prsc:2|A-3649-UVOUT,B-4411-OUT;n:type:ShaderForge.SFN_Time,id:9086,x:31743,y:33019,varname:node_9086,prsc:2;n:type:ShaderForge.SFN_Vector1,id:9778,x:31947,y:33189,varname:node_9778,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:4411,x:32127,y:33040,varname:node_4411,prsc:2|A-8067-OUT,B-9778-OUT;n:type:ShaderForge.SFN_Multiply,id:8067,x:31947,y:33040,varname:node_8067,prsc:2|A-9086-T,B-8084-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8084,x:31743,y:33163,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_8084,prsc:2,glob:False,v1:-0.02;n:type:ShaderForge.SFN_Multiply,id:2061,x:33378,y:33105,varname:node_2061,prsc:2|A-4221-OUT,B-6217-OUT,C-3649-U,D-6128-OUT,E-4758-OUT;n:type:ShaderForge.SFN_Lerp,id:4221,x:32773,y:32854,varname:node_4221,prsc:2|A-887-RGB,B-206-RGB,T-3649-U;n:type:ShaderForge.SFN_Color,id:887,x:32515,y:32700,ptovrint:False,ptlb:StarColor,ptin:_StarColor,varname:node_887,prsc:2,glob:False,c1:0.5310345,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:206,x:32515,y:32874,ptovrint:False,ptlb:EndColor,ptin:_EndColor,varname:node_206,prsc:2,glob:False,c1:0,c2:0.6275864,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:4758,x:32761,y:33960,ptovrint:False,ptlb:ColorStrength,ptin:_ColorStrength,varname:node_4758,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:7708,x:32515,y:33175,ptovrint:False,ptlb:MainTex1,ptin:_MainTex1,varname:node_7708,tex:d7d730d3faf252447a1f9fa99f7149a2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8894,x:32773,y:33176,varname:node_8894,prsc:2,tex:d7d730d3faf252447a1f9fa99f7149a2,ntxv:0,isnm:False|UVIN-1129-OUT,TEX-7708-TEX;n:type:ShaderForge.SFN_Add,id:6217,x:33025,y:33176,varname:node_6217,prsc:2|A-226-A,B-8894-A,C-2857-A;n:type:ShaderForge.SFN_Multiply,id:5119,x:32328,y:33326,varname:node_5119,prsc:2|A-4411-OUT,B-1882-OUT;n:type:ShaderForge.SFN_Vector1,id:1882,x:32123,y:33341,varname:node_1882,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Add,id:1129,x:32515,y:33326,varname:node_1129,prsc:2|A-5119-OUT,B-3649-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2857,x:32773,y:33346,ptovrint:False,ptlb:MainTex2,ptin:_MainTex2,varname:node_2857,prsc:2,tex:bcb7f7d1a1f447646aec11338fe4e790,ntxv:0,isnm:False|UVIN-5600-OUT;n:type:ShaderForge.SFN_Add,id:6617,x:33031,y:33393,varname:node_6617,prsc:2|A-226-A,B-8894-A,C-2857-A;n:type:ShaderForge.SFN_Multiply,id:4912,x:33297,y:33398,varname:node_4912,prsc:2|A-6617-OUT,B-6128-OUT;n:type:ShaderForge.SFN_Add,id:5408,x:32425,y:33814,varname:node_5408,prsc:2|A-9808-OUT,B-9808-OUT;proporder:887-206-4758-8084-7708-2857;pass:END;sub:END;*/

Shader "DreamFaction/UI/DigitalizeFlow01" {
    Properties {
        _StarColor ("StarColor", Color) = (0.5310345,0,1,1)
        _EndColor ("EndColor", Color) = (0,0.6275864,1,1)
        _ColorStrength ("ColorStrength", Float ) = 2
        _Speed ("Speed", Float ) = -0.02
        _MainTex1 ("MainTex1", 2D) = "white" {}
        _MainTex2 ("MainTex2", 2D) = "white" {}
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
            uniform float4 _TimeEditor;
            uniform float _Speed;
            uniform float4 _StarColor;
            uniform float4 _EndColor;
            uniform float _ColorStrength;
            uniform sampler2D _MainTex1; uniform float4 _MainTex1_ST;
            uniform sampler2D _MainTex2; uniform float4 _MainTex2_ST;
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
                float4 node_9086 = _Time + _TimeEditor;
                float2 node_4411 = float2((node_9086.g*_Speed),0.0);
                float2 node_5600 = (i.uv0+node_4411);
                float4 node_226 = tex2D(_MainTex1,TRANSFORM_TEX(node_5600, _MainTex1));
                float2 node_1129 = ((node_4411*0.8)+i.uv0);
                float4 node_8894 = tex2D(_MainTex1,TRANSFORM_TEX(node_1129, _MainTex1));
                float4 _MainTex2_var = tex2D(_MainTex2,TRANSFORM_TEX(node_5600, _MainTex2));
                float node_9808 = (i.uv0.g+(-0.5));
                float node_6128 = (1.0 - abs((node_9808+node_9808)));
                float3 finalColor = (lerp(_StarColor.rgb,_EndColor.rgb,i.uv0.r)*(node_226.a+node_8894.a+_MainTex2_var.a)*i.uv0.r*node_6128*_ColorStrength);
                return fixed4(finalColor,((node_226.a+node_8894.a+_MainTex2_var.a)*node_6128));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
