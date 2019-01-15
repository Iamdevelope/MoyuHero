// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|diff-18-OUT,diffpow-18-OUT,emission-18-OUT,alpha-246-A;n:type:ShaderForge.SFN_Tex2d,id:2,x:34099,y:33001,ptlb:node_2,ptin:_node_2,tex:1338c301c6719b141b52b7a238d50538,ntxv:0,isnm:False|UVIN-4-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:34099,y:33184,ptlb:node_3,ptin:_node_3,tex:5832f18ff1d88a54b94f58da4cf4aa33,ntxv:0,isnm:False|UVIN-5-UVOUT;n:type:ShaderForge.SFN_Panner,id:4,x:34544,y:33008,spu:0.2,spv:0.3;n:type:ShaderForge.SFN_Panner,id:5,x:34469,y:33187,spu:-0.2213,spv:-0.1245;n:type:ShaderForge.SFN_Multiply,id:6,x:34007,y:32839|A-2-R,B-3-R;n:type:ShaderForge.SFN_Panner,id:7,x:33857,y:32779,spu:0.2,spv:0.3|DIST-6-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:33553,y:32914,ptlb:node_8,ptin:_node_8,tex:e9e92c8d46504a7488e57e902f547185,ntxv:0,isnm:False|UVIN-7-UVOUT;n:type:ShaderForge.SFN_Vector3,id:17,x:33856,y:33299,v1:0.6,v2:0.2,v3:0.8;n:type:ShaderForge.SFN_Multiply,id:18,x:33131,y:33027|A-188-OUT,B-83-OUT,C-246-RGB;n:type:ShaderForge.SFN_Vector1,id:83,x:33386,y:33307,v1:1.5;n:type:ShaderForge.SFN_Tex2d,id:146,x:33673,y:33414,ptlb:node_146,ptin:_node_146,tex:c92e5c1caf27c5e4c98acd88dad92164,ntxv:0,isnm:False|UVIN-7-UVOUT;n:type:ShaderForge.SFN_Multiply,id:155,x:33525,y:33273|A-146-RGB,B-17-OUT;n:type:ShaderForge.SFN_Add,id:188,x:33339,y:32914|A-8-RGB,B-155-OUT;n:type:ShaderForge.SFN_Panner,id:229,x:34137,y:33409,spu:-0.056,spv:-0.1;n:type:ShaderForge.SFN_VertexColor,id:246,x:33216,y:33198;proporder:2-3-8-146;pass:END;sub:END;*/

Shader "Shader Forge/Jingshencaokongshi01" {
    Properties {
        _node_2 ("node_2", 2D) = "white" {}
        _node_3 ("node_3", 2D) = "white" {}
        _node_8 ("node_8", 2D) = "white" {}
        _node_146 ("node_146", 2D) = "white" {}
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_8; uniform float4 _node_8_ST;
            uniform sampler2D _node_146; uniform float4 _node_146_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float4 node_776 = _Time + _TimeEditor;
                float2 node_775 = i.uv0;
                float2 node_4 = (node_775.rg+node_776.g*float2(0.2,0.3));
                float2 node_5 = (node_775.rg+node_776.g*float2(-0.2213,-0.1245));
                float2 node_7 = (node_775.rg+(tex2D(_node_2,TRANSFORM_TEX(node_4, _node_2)).r*tex2D(_node_3,TRANSFORM_TEX(node_5, _node_3)).r)*float2(0.2,0.3));
                float4 node_246 = i.vertexColor;
                float3 node_18 = ((tex2D(_node_8,TRANSFORM_TEX(node_7, _node_8)).rgb+(tex2D(_node_146,TRANSFORM_TEX(node_7, _node_146)).rgb*float3(0.6,0.2,0.8)))*1.5*node_246.rgb);
                float3 diffuse = pow(max( 0.0, NdotL), node_18) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float3 emissive = node_18;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_18;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,node_246.a);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2; uniform float4 _node_2_ST;
            uniform sampler2D _node_3; uniform float4 _node_3_ST;
            uniform sampler2D _node_8; uniform float4 _node_8_ST;
            uniform sampler2D _node_146; uniform float4 _node_146_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float4 node_778 = _Time + _TimeEditor;
                float2 node_777 = i.uv0;
                float2 node_4 = (node_777.rg+node_778.g*float2(0.2,0.3));
                float2 node_5 = (node_777.rg+node_778.g*float2(-0.2213,-0.1245));
                float2 node_7 = (node_777.rg+(tex2D(_node_2,TRANSFORM_TEX(node_4, _node_2)).r*tex2D(_node_3,TRANSFORM_TEX(node_5, _node_3)).r)*float2(0.2,0.3));
                float4 node_246 = i.vertexColor;
                float3 node_18 = ((tex2D(_node_8,TRANSFORM_TEX(node_7, _node_8)).rgb+(tex2D(_node_146,TRANSFORM_TEX(node_7, _node_146)).rgb*float3(0.6,0.2,0.8)))*1.5*node_246.rgb);
                float3 diffuse = pow(max( 0.0, NdotL), node_18) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_18;
/// Final Color:
                return fixed4(finalColor * node_246.a,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
