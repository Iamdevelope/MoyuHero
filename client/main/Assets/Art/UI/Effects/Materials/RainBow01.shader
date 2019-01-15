// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:6,wrdp:False,dith:6,ufog:False,aust:False,igpj:True,qofs:0,qpre:4,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:678,x:32907,y:32902,varname:node_678,prsc:2|custl-6520-OUT;n:type:ShaderForge.SFN_Tex2d,id:3328,x:32538,y:32949,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:0,tex:abcb0f95e1d327e40b972872f08f4be4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:2496,x:31787,y:33138,varname:node_2496,prsc:0;n:type:ShaderForge.SFN_Sin,id:9931,x:32141,y:33155,varname:node_9931,prsc:0|IN-3598-OUT;n:type:ShaderForge.SFN_Multiply,id:6520,x:32733,y:33142,varname:node_6520,prsc:2|A-3328-RGB,B-6209-OUT,C-6409-OUT;n:type:ShaderForge.SFN_Add,id:5592,x:32323,y:33162,varname:node_5592,prsc:2|A-9931-OUT,B-6282-OUT;n:type:ShaderForge.SFN_Vector1,id:6282,x:32141,y:33322,varname:node_6282,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Clamp01,id:6209,x:32526,y:33163,varname:node_6209,prsc:2|IN-5592-OUT;n:type:ShaderForge.SFN_Multiply,id:3598,x:31973,y:33190,varname:node_3598,prsc:2|A-2496-T,B-2055-OUT;n:type:ShaderForge.SFN_Vector1,id:2055,x:31797,y:33332,varname:node_2055,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:6409,x:32537,y:33312,varname:node_6409,prsc:2,v1:0.5;proporder:3328;pass:END;sub:END;*/

Shader "Custom/RainBow01" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZTest Always
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                fixed4 node_2496 = _Time + _TimeEditor;
                float3 finalColor = (_MainTex_var.rgb*saturate((sin((node_2496.g*0.5))+1.5))*0.5);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
