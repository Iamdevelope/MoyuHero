// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.02352941,fgcg:0.254902,fgcb:0.4784314,fgca:1,fgde:0.04,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|custl-3-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33196,y:32836,ptlb:maintex,ptin:_maintex,tex:6eb0c7ad448379b419672f5f0b9add28,ntxv:0,isnm:False|UVIN-16-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3,x:32969,y:32911|A-2-RGB,B-4-RGB,C-37-OUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:33196,y:33014,ptlb:Alpha,ptin:_Alpha,tex:c8975f24fa0bc7f46a9ed010c10e3230,ntxv:0,isnm:False|UVIN-24-OUT;n:type:ShaderForge.SFN_TexCoord,id:16,x:33745,y:32939,uv:0;n:type:ShaderForge.SFN_Append,id:18,x:33745,y:33080|A-31-OUT,B-31-OUT;n:type:ShaderForge.SFN_Multiply,id:19,x:33539,y:33035|A-16-UVOUT,B-18-OUT;n:type:ShaderForge.SFN_Add,id:24,x:33354,y:33014|A-26-OUT,B-19-OUT;n:type:ShaderForge.SFN_Append,id:26,x:33745,y:32788|A-29-OUT,B-29-OUT;n:type:ShaderForge.SFN_Subtract,id:27,x:34150,y:32739|A-31-OUT,B-28-OUT;n:type:ShaderForge.SFN_Vector1,id:28,x:34348,y:32761,v1:1;n:type:ShaderForge.SFN_Multiply,id:29,x:33925,y:32788|A-27-OUT,B-30-OUT;n:type:ShaderForge.SFN_Vector1,id:30,x:34150,y:32892,v1:-0.5;n:type:ShaderForge.SFN_Add,id:31,x:34348,y:32951|A-35-OUT,B-32-OUT;n:type:ShaderForge.SFN_Vector1,id:32,x:34529,y:32995,v1:-5;n:type:ShaderForge.SFN_ValueProperty,id:35,x:34529,y:32929,ptlb:scale,ptin:_scale,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:37,x:33196,y:33218,ptlb:strength,ptin:_strength,glob:False,v1:1;proporder:35-37-2-4;pass:END;sub:END;*/

Shader "DreamFaction/Effects/Equake01" {
    Properties {
        _scale ("scale", Float ) = 1
        _strength ("strength", Float ) = 1
        _maintex ("maintex", 2D) = "white" {}
        _Alpha ("Alpha", 2D) = "white" {}
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
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform float _scale;
            uniform float _strength;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float2 node_16 = i.uv0;
                float node_31 = (_scale+(-5.0));
                float node_29 = ((node_31-1.0)*(-0.5));
                float2 node_24 = (float2(node_29,node_29)+(node_16.rg*float2(node_31,node_31)));
                float3 finalColor = (tex2D(_maintex,TRANSFORM_TEX(node_16.rg, _maintex)).rgb*tex2D(_Alpha,TRANSFORM_TEX(node_24, _Alpha)).rgb*_strength);
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
