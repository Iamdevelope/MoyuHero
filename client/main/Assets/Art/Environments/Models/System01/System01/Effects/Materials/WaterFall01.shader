// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:0.9340771,fgcb:0.8161765,fgca:1,fgde:0.01,fgrn:0,fgrf:200,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32047,y:32715|custl-17-RGB,alpha-12-OUT,voffset-33-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32647,y:32974,ptlb:maintex,ptin:_maintex,tex:4fea5088624437e4d959dbc3dbd6c31e,ntxv:0,isnm:False|UVIN-26-OUT;n:type:ShaderForge.SFN_VertexColor,id:7,x:32657,y:33132;n:type:ShaderForge.SFN_Multiply,id:12,x:32317,y:32993|A-2-R,B-7-R,C-17-A,D-36-OUT;n:type:ShaderForge.SFN_Color,id:17,x:32647,y:32806,ptlb:maincolor,ptin:_maincolor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:22,x:33126,y:32970,uv:0;n:type:ShaderForge.SFN_Time,id:23,x:33763,y:33169;n:type:ShaderForge.SFN_Append,id:24,x:33126,y:33140|A-27-OUT,B-25-OUT;n:type:ShaderForge.SFN_Vector1,id:25,x:33329,y:33285,v1:0;n:type:ShaderForge.SFN_Add,id:26,x:32808,y:32974|A-22-UVOUT,B-24-OUT;n:type:ShaderForge.SFN_Negate,id:27,x:33329,y:33140|IN-38-OUT;n:type:ShaderForge.SFN_Sin,id:28,x:32997,y:33549|IN-30-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:29,x:33427,y:33593;n:type:ShaderForge.SFN_Multiply,id:30,x:33165,y:33549|A-23-T,B-29-Y;n:type:ShaderForge.SFN_Append,id:31,x:32500,y:33550|A-34-OUT,B-25-OUT;n:type:ShaderForge.SFN_Append,id:33,x:32327,y:33550|A-31-OUT,B-25-OUT;n:type:ShaderForge.SFN_Multiply,id:34,x:32709,y:33550|A-28-OUT,B-35-OUT;n:type:ShaderForge.SFN_ValueProperty,id:35,x:32997,y:33716,ptlb:wave,ptin:_wave,glob:False,v1:0.03;n:type:ShaderForge.SFN_Vector1,id:36,x:32647,y:33274,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:37,x:33763,y:33317,ptlb:speed,ptin:_speed,glob:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:38,x:33521,y:33159|A-23-T,B-37-OUT;proporder:17-2-35-37;pass:END;sub:END;*/

Shader "Custom/WaterFall01" {
    Properties {
        _maincolor ("maincolor", Color) = (1,1,1,1)
        _maintex ("maintex", 2D) = "white" {}
        _wave ("wave", Float ) = 0.03
        _speed ("speed", Float ) = 0.2
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
            uniform float4 _TimeEditor;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float4 _maincolor;
            uniform float _wave;
            uniform float _speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_23 = _Time + _TimeEditor;
                float node_25 = 0.0;
                v.vertex.xyz += float3(float2((sin((node_23.g*mul(_Object2World, v.vertex).g))*_wave),node_25),node_25);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float3 finalColor = _maincolor.rgb;
                float4 node_23 = _Time + _TimeEditor;
                float node_25 = 0.0;
                float2 node_26 = (i.uv0.rg+float2((-1*(node_23.g*_speed)),node_25));
/// Final Color:
                return fixed4(finalColor,(tex2D(_maintex,TRANSFORM_TEX(node_26, _maintex)).r*i.vertexColor.r*_maincolor.a*3.0));
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
            uniform float4 _TimeEditor;
            uniform float _wave;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 posWorld : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                float4 node_23 = _Time + _TimeEditor;
                float node_25 = 0.0;
                v.vertex.xyz += float3(float2((sin((node_23.g*mul(_Object2World, v.vertex).g))*_wave),node_25),node_25);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
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
            uniform float4 _TimeEditor;
            uniform float _wave;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                float4 node_23 = _Time + _TimeEditor;
                float node_25 = 0.0;
                v.vertex.xyz += float3(float2((sin((node_23.g*mul(_Object2World, v.vertex).g))*_wave),node_25),node_25);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
