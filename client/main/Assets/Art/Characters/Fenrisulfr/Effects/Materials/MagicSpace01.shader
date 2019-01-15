// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.02352941,fgcg:0.254902,fgcb:0.4784314,fgca:1,fgde:0.04,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32481,y:32700|custl-2-RGB,alpha-4-R;n:type:ShaderForge.SFN_Tex2d,id:2,x:32759,y:32896,ptlb:maintex,ptin:_maintex,tex:02dd0afa7eacc8a4097f3af39f3c92ac,ntxv:0,isnm:False|UVIN-55-OUT;n:type:ShaderForge.SFN_Tex2d,id:4,x:32734,y:33357,ptlb:alpha,ptin:_alpha,tex:6ebe2b122a48b9e449210f8f2345d6f3,ntxv:0,isnm:False|UVIN-24-OUT;n:type:ShaderForge.SFN_TexCoord,id:16,x:33289,y:33291,uv:0;n:type:ShaderForge.SFN_Append,id:18,x:33289,y:33432|A-31-OUT,B-31-OUT;n:type:ShaderForge.SFN_Multiply,id:19,x:33083,y:33387|A-16-UVOUT,B-18-OUT;n:type:ShaderForge.SFN_Add,id:24,x:32902,y:33357|A-26-OUT,B-19-OUT;n:type:ShaderForge.SFN_Append,id:26,x:33289,y:33140|A-29-OUT,B-29-OUT;n:type:ShaderForge.SFN_Subtract,id:27,x:33694,y:33091|A-31-OUT,B-28-OUT;n:type:ShaderForge.SFN_Vector1,id:28,x:33892,y:33113,v1:1;n:type:ShaderForge.SFN_Multiply,id:29,x:33469,y:33140|A-27-OUT,B-30-OUT;n:type:ShaderForge.SFN_Vector1,id:30,x:33694,y:33244,v1:-0.5;n:type:ShaderForge.SFN_Add,id:31,x:33892,y:33303|A-35-OUT,B-32-OUT;n:type:ShaderForge.SFN_Vector1,id:32,x:34073,y:33347,v1:-5;n:type:ShaderForge.SFN_ValueProperty,id:35,x:34073,y:33281,ptlb:scale,ptin:_scale,glob:False,v1:4;n:type:ShaderForge.SFN_ScreenPos,id:47,x:33976,y:32749,sctp:0;n:type:ShaderForge.SFN_Tex2d,id:48,x:33459,y:32915,ptlb:offsettex,ptin:_offsettex,ntxv:0,isnm:False|UVIN-51-OUT;n:type:ShaderForge.SFN_Time,id:49,x:34165,y:32918;n:type:ShaderForge.SFN_Append,id:50,x:33791,y:32935|A-56-OUT,B-56-OUT;n:type:ShaderForge.SFN_Add,id:51,x:33623,y:32915|A-47-UVOUT,B-50-OUT;n:type:ShaderForge.SFN_Append,id:52,x:33292,y:32915|A-48-R,B-48-R;n:type:ShaderForge.SFN_Multiply,id:53,x:33112,y:32915|A-52-OUT,B-54-OUT;n:type:ShaderForge.SFN_ValueProperty,id:54,x:33292,y:33074,ptlb:strength,ptin:_strength,glob:False,v1:0.1;n:type:ShaderForge.SFN_Add,id:55,x:32932,y:32896|A-47-UVOUT,B-53-OUT;n:type:ShaderForge.SFN_Multiply,id:56,x:33976,y:32935|A-49-T,B-57-OUT;n:type:ShaderForge.SFN_ValueProperty,id:57,x:34165,y:33068,ptlb:speed,ptin:_speed,glob:False,v1:0.5;proporder:35-2-4-48-54-57;pass:END;sub:END;*/

Shader "DreamFaction/Effects/MagicSpace01" {
    Properties {
        _scale ("scale", Float ) = 4
        _maintex ("maintex", 2D) = "white" {}
        _alpha ("alpha", 2D) = "white" {}
        _offsettex ("offsettex", 2D) = "white" {}
        _strength ("strength", Float ) = 0.1
        _speed ("speed", Float ) = 0.5
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
            uniform float4 _TimeEditor;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform sampler2D _alpha; uniform float4 _alpha_ST;
            uniform float _scale;
            uniform sampler2D _offsettex; uniform float4 _offsettex_ST;
            uniform float _strength;
            uniform float _speed;
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
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
////// Lighting:
                float2 node_47 = i.screenPos;
                float4 node_49 = _Time + _TimeEditor;
                float node_56 = (node_49.g*_speed);
                float2 node_51 = (node_47.rg+float2(node_56,node_56));
                float4 node_48 = tex2D(_offsettex,TRANSFORM_TEX(node_51, _offsettex));
                float2 node_55 = (node_47.rg+(float2(node_48.r,node_48.r)*_strength));
                float3 finalColor = tex2D(_maintex,TRANSFORM_TEX(node_55, _maintex)).rgb;
                float node_31 = (_scale+(-5.0));
                float node_29 = ((node_31-1.0)*(-0.5));
                float2 node_24 = (float2(node_29,node_29)+(i.uv0.rg*float2(node_31,node_31)));
/// Final Color:
                return fixed4(finalColor,tex2D(_alpha,TRANSFORM_TEX(node_24, _alpha)).r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
