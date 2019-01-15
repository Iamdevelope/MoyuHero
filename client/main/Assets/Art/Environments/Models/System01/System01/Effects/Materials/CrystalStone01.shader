// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1,fgcg:0.9340771,fgcb:0.8161765,fgca:1,fgde:0.01,fgrn:0,fgrf:200,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:31784,y:32749|custl-7-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32515,y:32765,ptlb:maintex,ptin:_maintex,tex:cde0c3c7daef2854c9753b7022ceac88,ntxv:0,isnm:False|UVIN-3-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3,x:32724,y:32743,uv:0;n:type:ShaderForge.SFN_Add,id:7,x:32035,y:32951|A-2-RGB,B-37-OUT,C-43-OUT;n:type:ShaderForge.SFN_Fresnel,id:25,x:32515,y:33189|NRM-26-OUT,EXP-27-OUT;n:type:ShaderForge.SFN_NormalVector,id:26,x:32724,y:33179,pt:False;n:type:ShaderForge.SFN_Vector1,id:27,x:32724,y:33324,v1:1;n:type:ShaderForge.SFN_Cubemap,id:31,x:32515,y:32981,ptlb:node_31,ptin:_node_31,cube:78e87b825fa2e144181c36798ae5d833,pvfc:0;n:type:ShaderForge.SFN_Multiply,id:37,x:32275,y:33080|A-2-RGB,B-31-RGB,C-25-OUT,D-38-OUT;n:type:ShaderForge.SFN_ValueProperty,id:38,x:32503,y:33360,ptlb:fresnelstr,ptin:_fresnelstr,glob:False,v1:2;n:type:ShaderForge.SFN_OneMinus,id:39,x:32308,y:33337|IN-25-OUT;n:type:ShaderForge.SFN_Power,id:40,x:32181,y:33575|VAL-39-OUT,EXP-41-OUT;n:type:ShaderForge.SFN_Vector1,id:41,x:32422,y:33675,v1:40;n:type:ShaderForge.SFN_Multiply,id:43,x:32052,y:33299|A-40-OUT,B-44-RGB;n:type:ShaderForge.SFN_Color,id:44,x:32489,y:33518,ptlb:glowcolor,ptin:_glowcolor,glob:False,c1:1,c2:0.5294118,c3:0.9286003,c4:1;proporder:2-31-38-44;pass:END;sub:END;*/

Shader "Custom/CrystalStone01" {
    Properties {
        _maintex ("maintex", 2D) = "white" {}
        _node_31 ("node_31", Cube) = "_Skybox" {}
        _fresnelstr ("fresnelstr", Float ) = 2
        _glowcolor ("glowcolor", Color) = (1,0.5294118,0.9286003,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform samplerCUBE _node_31;
            uniform float _fresnelstr;
            uniform float4 _glowcolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
                float2 node_3 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_3.rg, _maintex));
                float node_25 = pow(1.0-max(0,dot(i.normalDir, viewDirection)),1.0);
                float node_40 = pow((1.0 - node_25),40.0);
                float3 finalColor = (node_2.rgb+(node_2.rgb*texCUBE(_node_31,viewReflectDirection).rgb*node_25*_fresnelstr)+(node_40*_glowcolor.rgb));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
