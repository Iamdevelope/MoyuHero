// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:2,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.6102941,fgcg:0.7059274,fgcb:1,fgca:1,fgde:0.01,fgrn:42,fgrf:150,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32133,y:32763|custl-939-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:32951,y:32840,ptlb:maintex,ptin:_maintex,ntxv:0,isnm:False|UVIN-726-UVOUT;n:type:ShaderForge.SFN_NormalVector,id:7,x:33521,y:33186,pt:False;n:type:ShaderForge.SFN_TexCoord,id:726,x:33151,y:32840,uv:0;n:type:ShaderForge.SFN_Fresnel,id:928,x:32951,y:33177|NRM-7-OUT,EXP-3390-OUT;n:type:ShaderForge.SFN_Color,id:931,x:32951,y:33035,ptlb:buffcolor,ptin:_buffcolor,glob:False,c1:0,c2:0.9586205,c3:1,c4:1;n:type:ShaderForge.SFN_ScreenPos,id:936,x:33521,y:33375,sctp:0;n:type:ShaderForge.SFN_Tex2d,id:937,x:33151,y:33375,ptlb:noise,ptin:_noise,tex:f4b89672ccd6fc546a6a782678656c05,ntxv:0,isnm:False|UVIN-941-OUT;n:type:ShaderForge.SFN_Multiply,id:938,x:32723,y:32991|A-931-RGB,B-928-OUT,C-940-OUT,D-945-OUT;n:type:ShaderForge.SFN_Add,id:939,x:32441,y:32965|A-2-RGB,B-938-OUT,C-3393-OUT;n:type:ShaderForge.SFN_ValueProperty,id:940,x:32951,y:33317,ptlb:buffstrength,ptin:_buffstrength,glob:False,v1:0.1;n:type:ShaderForge.SFN_Add,id:941,x:33327,y:33375|A-936-UVOUT,B-943-OUT;n:type:ShaderForge.SFN_Time,id:942,x:34160,y:33605;n:type:ShaderForge.SFN_Append,id:943,x:33521,y:33524|A-949-OUT,B-951-OUT;n:type:ShaderForge.SFN_Power,id:945,x:32951,y:33375|VAL-937-RGB,EXP-947-OUT;n:type:ShaderForge.SFN_ValueProperty,id:947,x:33151,y:33561,ptlb:noiseexp,ptin:_noiseexp,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:949,x:33700,y:33524|A-958-OUT,B-950-OUT;n:type:ShaderForge.SFN_ValueProperty,id:950,x:33882,y:33545,ptlb:noisexspeed,ptin:_noisexspeed,glob:False,v1:5;n:type:ShaderForge.SFN_Multiply,id:951,x:33700,y:33673|A-958-OUT,B-952-OUT;n:type:ShaderForge.SFN_ValueProperty,id:952,x:33871,y:33693,ptlb:noiseyspeed,ptin:_noiseyspeed,glob:False,v1:10;n:type:ShaderForge.SFN_Negate,id:958,x:33995,y:33605|IN-942-TSL;n:type:ShaderForge.SFN_Cubemap,id:3385,x:33295,y:33607,ptlb:cubemap,ptin:_cubemap,cube:b995d4bd9d11078d11005b9844295342,pvfc:0;n:type:ShaderForge.SFN_Vector1,id:3390,x:33255,y:33201,v1:-2.5;n:type:ShaderForge.SFN_Fresnel,id:3392,x:33295,y:33765|NRM-7-OUT,EXP-3394-OUT;n:type:ShaderForge.SFN_Multiply,id:3393,x:32951,y:33607|A-3385-RGB,B-3392-OUT,C-3395-RGB,D-3396-OUT;n:type:ShaderForge.SFN_Vector1,id:3394,x:33521,y:33787,v1:1;n:type:ShaderForge.SFN_Color,id:3395,x:33295,y:33931,ptlb:color,ptin:_color,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3396,x:33295,y:34114,ptlb:strength,ptin:_strength,glob:False,v1:2;proporder:2-931-940-937-947-950-952-3385-3395-3396;pass:END;sub:END;*/

Shader "DreamFaction/Characters/ToonBuff" {
    Properties {
        _maintex ("maintex", 2D) = "white" {}
        _buffcolor ("buffcolor", Color) = (0,0.9586205,1,1)
        _buffstrength ("buffstrength", Float ) = 0.1
        _noise ("noise", 2D) = "white" {}
        _noiseexp ("noiseexp", Float ) = 1
        _noisexspeed ("noisexspeed", Float ) = 5
        _noiseyspeed ("noiseyspeed", Float ) = 10
        _cubemap ("cubemap", Cube) = "_Skybox" {}
        _color ("color", Color) = (1,1,1,1)
        _strength ("strength", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float4 _buffcolor;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _buffstrength;
            uniform float _noiseexp;
            uniform float _noisexspeed;
            uniform float _noiseyspeed;
            uniform samplerCUBE _cubemap;
            uniform float4 _color;
            uniform float _strength;
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
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float3 node_7 = i.normalDir;
                float node_928 = pow(1.0-max(0,dot(node_7, viewDirection)),(-2.5));
                float4 node_942 = _Time + _TimeEditor;
                float node_958 = (-1*node_942.r);
                float2 node_941 = (i.screenPos.rg+float2((node_958*_noisexspeed),(node_958*_noiseyspeed)));
                float4 node_3385 = texCUBE(_cubemap,viewReflectDirection);
                float node_3392 = pow(1.0-max(0,dot(node_7, viewDirection)),1.0);
                float3 node_939 = (node_2.rgb+(_buffcolor.rgb*node_928*_buffstrength*pow(tex2D(_noise,TRANSFORM_TEX(node_941, _noise)).rgb,_noiseexp))+(node_3385.rgb*node_3392*_color.rgb*_strength));
                float3 finalColor = node_939;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
