// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:False,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.4854563,fgcg:0.5056818,fgcb:0.9044118,fgca:1,fgde:0.01,fgrn:10,fgrf:100,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4928,x:32656,y:32790,varname:node_4928,prsc:2|custl-3519-OUT,alpha-4736-A;n:type:ShaderForge.SFN_Color,id:4736,x:32159,y:32881,ptovrint:False,ptlb:maincolor,ptin:_maincolor,varname:node_4736,prsc:2,glob:False,c1:0.3896551,c2:0,c3:0.5,c4:0.4352941;n:type:ShaderForge.SFN_Cubemap,id:2513,x:31929,y:33060,ptovrint:False,ptlb:cubemap,ptin:_cubemap,varname:node_2513,prsc:2,cube:995866700ad069a4ba5918ba0a0faf43,pvfc:0;n:type:ShaderForge.SFN_Add,id:3519,x:32364,y:33015,varname:node_3519,prsc:2|A-4736-RGB,B-3050-OUT,C-3050-OUT;n:type:ShaderForge.SFN_Fresnel,id:3474,x:32013,y:33227,varname:node_3474,prsc:2|NRM-7864-OUT,EXP-2945-OUT;n:type:ShaderForge.SFN_Vector1,id:2945,x:31821,y:33387,varname:node_2945,prsc:2,v1:1.5;n:type:ShaderForge.SFN_NormalVector,id:7864,x:31848,y:33227,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:3050,x:32195,y:33138,varname:node_3050,prsc:2|A-2513-RGB,B-3474-OUT;proporder:4736-2513;pass:END;sub:END;*/

Shader "DreamFaction/Environment/ColorAddCubemap" {
    Properties {
        _maincolor ("maincolor", Color) = (0.3896551,0,0.5,0.4352941)
        _cubemap ("cubemap", Cube) = "_Skybox" {}
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
            uniform float4 _maincolor;
            uniform samplerCUBE _cubemap;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
                float3 node_3050 = (texCUBE(_cubemap,viewReflectDirection).rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),1.5));
                float3 finalColor = (_maincolor.rgb+node_3050+node_3050);
                return fixed4(finalColor,_maincolor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
