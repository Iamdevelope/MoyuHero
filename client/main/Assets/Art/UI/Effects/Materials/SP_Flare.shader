// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:6,wrdp:False,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:0,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:526,x:32095,y:32762|custl-532-OUT;n:type:ShaderForge.SFN_Tex2d,id:527,x:32522,y:32963,ptlb:alpha,ptin:_alpha,tex:4a4454f06e354dd4786a804eb5f1e010,ntxv:0,isnm:False|UVIN-538-UVOUT;n:type:ShaderForge.SFN_Multiply,id:532,x:32343,y:32963|A-729-RGB,B-527-R,C-533-R,D-739-R;n:type:ShaderForge.SFN_Tex2d,id:533,x:32522,y:33143,ptlb:light,ptin:_light,tex:2459c8d75a72562449506b413fe2f430,ntxv:0,isnm:False|UVIN-539-OUT;n:type:ShaderForge.SFN_TexCoord,id:538,x:33106,y:33081,uv:0;n:type:ShaderForge.SFN_Add,id:539,x:32698,y:33143|A-553-UVOUT,B-542-OUT;n:type:ShaderForge.SFN_ValueProperty,id:540,x:33428,y:33434,ptlb:value,ptin:_value,glob:False,v1:0;n:type:ShaderForge.SFN_Vector1,id:541,x:33060,y:33525,v1:0;n:type:ShaderForge.SFN_Append,id:542,x:32864,y:33367|A-611-OUT,B-541-OUT;n:type:ShaderForge.SFN_Rotator,id:553,x:32864,y:33143|UVIN-538-UVOUT,ANG-575-OUT;n:type:ShaderForge.SFN_ValueProperty,id:575,x:33106,y:33263,ptlb:rotate,ptin:_rotate,glob:False,v1:0.35;n:type:ShaderForge.SFN_Subtract,id:593,x:33245,y:33367|A-595-OUT,B-540-OUT;n:type:ShaderForge.SFN_Vector1,id:595,x:33428,y:33367,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:605,x:33245,y:33525,v1:1.75;n:type:ShaderForge.SFN_Multiply,id:611,x:33060,y:33367|A-593-OUT,B-605-OUT;n:type:ShaderForge.SFN_Color,id:729,x:32522,y:32800,ptlb:maincolor,ptin:_maincolor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:739,x:32522,y:33354,ptlb:ramp,ptin:_ramp,tex:f110c2736e9b8c0458c48a200b9cc53d,ntxv:0,isnm:False|UVIN-538-UVOUT;proporder:540-729-527-533-739-575;pass:END;sub:END;*/

Shader "DreamFaction/Interface/SPFlare" {
    Properties {
        _value ("value", Float ) = 0
        _maincolor ("maincolor", Color) = (0.5,0.5,0.5,1)
        _alpha ("alpha", 2D) = "white" {}
        _light ("light", 2D) = "white" {}
        _ramp ("ramp", 2D) = "white" {}
        _rotate ("rotate", Float ) = 0.35
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
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
            uniform sampler2D _alpha; uniform float4 _alpha_ST;
            uniform sampler2D _light; uniform float4 _light_ST;
            uniform float _value;
            uniform float _rotate;
            uniform float4 _maincolor;
            uniform sampler2D _ramp; uniform float4 _ramp_ST;
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
                float2 node_538 = i.uv0;
                float node_553_ang = _rotate;
                float node_553_spd = 1.0;
                float node_553_cos = cos(node_553_spd*node_553_ang);
                float node_553_sin = sin(node_553_spd*node_553_ang);
                float2 node_553_piv = float2(0.5,0.5);
                float2 node_553 = (mul(node_538.rg-node_553_piv,float2x2( node_553_cos, -node_553_sin, node_553_sin, node_553_cos))+node_553_piv);
                float2 node_539 = (node_553+float2(((0.5-_value)*1.75),0.0));
                float3 finalColor = (_maincolor.rgb*tex2D(_alpha,TRANSFORM_TEX(node_538.rg, _alpha)).r*tex2D(_light,TRANSFORM_TEX(node_539, _light)).r*tex2D(_ramp,TRANSFORM_TEX(node_538.rg, _ramp)).r);
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
