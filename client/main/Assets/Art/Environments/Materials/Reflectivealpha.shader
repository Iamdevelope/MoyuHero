// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0.1050709,fgcb:0.4117647,fgca:1,fgde:0.01,fgrn:10,fgrf:100,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8889,x:32719,y:32712,varname:node_8889,prsc:2|custl-2853-OUT,alpha-3982-OUT;n:type:ShaderForge.SFN_Tex2d,id:5745,x:32204,y:32904,ptovrint:False,ptlb:maintex,ptin:_maintex,varname:node_5745,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Cubemap,id:6761,x:32031,y:33063,ptovrint:False,ptlb:reflecube,ptin:_reflecube,varname:node_6761,prsc:2;n:type:ShaderForge.SFN_Add,id:2853,x:32455,y:32998,varname:node_2853,prsc:2|A-5745-RGB,B-3333-OUT;n:type:ShaderForge.SFN_Multiply,id:3333,x:32204,y:33063,varname:node_3333,prsc:2|A-6761-RGB,B-2857-OUT,C-7646-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2857,x:32031,y:33243,ptovrint:False,ptlb:reflestr,ptin:_reflestr,varname:node_2857,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3982,x:32455,y:33143,ptovrint:False,ptlb:alpha,ptin:_alpha,varname:node_3982,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Fresnel,id:7646,x:32042,y:33370,varname:node_7646,prsc:2|NRM-2101-OUT,EXP-6980-OUT;n:type:ShaderForge.SFN_NormalVector,id:2101,x:31837,y:33341,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:6980,x:31846,y:33556,ptovrint:False,ptlb:freexp,ptin:_freexp,varname:node_6980,prsc:2,glob:False,v1:1;proporder:5745-6761-2857-3982-6980;pass:END;sub:END;*/

Shader "DreamFaction/Environment/Reflectivealpha" {
    Properties {
        _maintex ("maintex", 2D) = "white" {}
        _reflecube ("reflecube", Cube) = "_Skybox" {}
        _reflestr ("reflestr", Float ) = 1
        _alpha ("alpha", Float ) = 1
        _freexp ("freexp", Float ) = 1
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
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform samplerCUBE _reflecube;
            uniform float _reflestr;
            uniform float _alpha;
            uniform float _freexp;
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
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float4 _maintex_var = tex2D(_maintex,TRANSFORM_TEX(i.uv0, _maintex));
                float3 finalColor = (_maintex_var.rgb+(texCUBE(_reflecube,viewReflectDirection).rgb*_reflestr*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_freexp)));
                return fixed4(finalColor,_alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
