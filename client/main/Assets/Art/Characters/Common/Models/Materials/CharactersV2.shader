// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3149,x:34202,y:32693,varname:node_3149,prsc:2|custl-3209-OUT;n:type:ShaderForge.SFN_Tex2d,id:3150,x:30689,y:32769,ptovrint:False,ptlb:maintex,ptin:_maintex,varname:node_9618,prsc:2,ntxv:0,isnm:False|UVIN-3151-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3151,x:30536,y:32578,varname:node_3151,prsc:2,uv:0;n:type:ShaderForge.SFN_NormalVector,id:3193,x:33394,y:33597,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:3195,x:33594,y:33653,varname:node_3195,prsc:2,v1:2;n:type:ShaderForge.SFN_Fresnel,id:3197,x:33779,y:33597,varname:node_3197,prsc:2|NRM-3193-OUT,EXP-3195-OUT;n:type:ShaderForge.SFN_Vector1,id:3201,x:33769,y:33903,varname:node_3201,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:3203,x:33984,y:33597,varname:node_3203,prsc:2|A-3197-OUT,B-3260-RGB,C-3201-OUT;n:type:ShaderForge.SFN_Add,id:3205,x:34250,y:33532,varname:node_3205,prsc:2|A-9090-OUT,B-3203-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:3207,x:33330,y:32587,ptovrint:False,ptlb:flare,ptin:_flare,varname:node_4817,prsc:2,on:False;n:type:ShaderForge.SFN_If,id:3209,x:33855,y:32928,varname:node_3209,prsc:2|A-3207-OUT,B-3258-OUT,GT-3205-OUT,EQ-3205-OUT,LT-3258-OUT;n:type:ShaderForge.SFN_Multiply,id:3258,x:33147,y:32723,varname:node_3258,prsc:2|A-9090-OUT,B-3259-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3259,x:32706,y:32453,ptovrint:False,ptlb:mainstrength,ptin:_mainstrength,varname:node_8669,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:3260,x:33779,y:33736,ptovrint:False,ptlb:flarecolor,ptin:_flarecolor,varname:node_4171,prsc:2,glob:False,c1:0.5,c2:0.32,c3:0.11,c4:1;n:type:ShaderForge.SFN_Code,id:6947,x:30760,y:33320,varname:node_6947,prsc:2,code:ZgBsAG8AYQB0ADQAIABrACAAPQAgAGYAbABvAGEAdAA0ACgAMAAuADAALAAgAC0AMQAuADAALwAzAC4AMAAsACAAMgAuADAALwAzAC4AMAAsACAALQAxAC4AMAApADsACgBmAGwAbwBhAHQANAAgAHAAIAA9ACAAUgBHAEIALgBnACAAPAAgAFIARwBCAC4AYgAgAD8AIABmAGwAbwBhAHQANAAoAFIARwBCAC4AYgAsACAAUgBHAEIALgBnACwAIABrAC4AdwAsACAAawAuAHoAKQAgADoAIABmAGwAbwBhAHQANAAoAFIARwBCAC4AZwBiACwAIABrAC4AeAB5ACkAOwAKAGYAbABvAGEAdAA0ACAAcQAgAD0AIABSAEcAQgAuAHIAIAA8ACAAcAAuAHgAIAAgACAAPwAgAGYAbABvAGEAdAA0ACgAcAAuAHgALAAgAHAALgB5ACwAIABwAC4AdwAsACAAUgBHAEIALgByACkAIAAgACAAOgAgAGYAbABvAGEAdAA0ACgAUgBHAEIALgByACAALAAgAHAALgB5AHoAeAApADsACgBmAGwAbwBhAHQAIAAgAGQAIAA9ACAAcQAuAHgAIAAtACAAbQBpAG4AKABxAC4AdwAsACAAcQAuAHkAKQA7AAoAZgBsAG8AYQB0ACAAIABlACAAPQAgADEALgAwAGUALQAxADAAOwAKAHIAZQB0AHUAcgBuACAAZgBsAG8AYQB0ADMAKABhAGIAcwAoAHEALgB6ACAAKwAgACgAcQAuAHcAIAAtACAAcQAuAHkAKQAgAC8AIAAoADYALgAwACAAKgAgAGQAIAArACAAZQApACkALAAgAGQAIAAvACAAKABxAC4AeAAgACsAIABlACkALAAgAHEALgB4ACkAOwANAAoA,output:2,fname:RGB2HSV,width:769,height:276,input:2,input_1_label:RGB|A-3150-RGB;n:type:ShaderForge.SFN_Code,id:9090,x:32786,y:33224,varname:node_9090,prsc:2,code:DQAKAGYAbABvAGEAdAA0ACAAawAgAD0AIABmAGwAbwBhAHQANAAoADEALgAwACwAIAAyAC4AMAAvADMALgAwACwAIAAxAC4AMAAvADMALgAwACwAIAAzAC4AMAApADsACgBmAGwAbwBhAHQAMwAgAHAAIAA9ACAAYQBiAHMAKABmAHIAYQBjACgASABTAFYALgB4AHgAeAAgACsAIABrAC4AeAB5AHoAKQAgACoAIAA2AC4AMAAgAC0AIABrAC4AdwB3AHcAKQA7AAoAcgBlAHQAdQByAG4AIABIAFMAVgAuAHoAIAAqACAAbABlAHIAcAAoAGsALgB4AHgAeAAsACAAYwBsAGEAbQBwACgAcAAgAC0AIABrAC4AeAB4AHgALAAgADAALgAwACwAIAAxAC4AMAApACwAIABIAFMAVgAuAHkAKQA7AA0ACgA=,output:2,fname:HSV2RGB,width:247,height:132,input:2,input_1_label:HSV|A-2764-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3262,x:31799,y:33643,varname:node_3262,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-6947-OUT;n:type:ShaderForge.SFN_Slider,id:91,x:31708,y:33401,ptovrint:False,ptlb:Hue,ptin:_Hue,varname:node_91,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3028,x:31667,y:33895,ptovrint:False,ptlb:Saturation,ptin:_Saturation,varname:node_3028,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3386,x:31747,y:34079,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_3386,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Append,id:4566,x:32371,y:33595,varname:node_4566,prsc:2|A-1026-OUT,B-9422-OUT;n:type:ShaderForge.SFN_Add,id:1026,x:32171,y:33491,varname:node_1026,prsc:2|A-91-OUT,B-3262-R;n:type:ShaderForge.SFN_Add,id:9422,x:32172,y:33760,varname:node_9422,prsc:2|A-3028-OUT,B-3262-G;n:type:ShaderForge.SFN_Add,id:4707,x:32193,y:33985,varname:node_4707,prsc:2|A-3386-OUT,B-3262-B;n:type:ShaderForge.SFN_Append,id:2764,x:32699,y:33728,varname:node_2764,prsc:2|A-4566-OUT,B-4707-OUT;proporder:3207-3260-3259-3150-91-3028-3386;pass:END;sub:END;*/

Shader "DreamFaction/Characters/CharactersV2" {
    Properties {
        [MaterialToggle] _flare ("flare", Float ) = 0
        _flarecolor ("flarecolor", Color) = (0.5,0.32,0.11,1)
        _mainstrength ("mainstrength", Float ) = 1
        _maintex ("maintex", 2D) = "white" {}
        _Hue ("Hue", Range(0, 1)) = 0
        _Saturation ("Saturation", Range(0, 1)) = 0
        _Value ("Value", Range(0, 1)) = 0
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform fixed _flare;
            uniform float _mainstrength;
            uniform float4 _flarecolor;
            float3 RGB2HSV( float3 RGB ){
            float4 k = float4(0.0, -1.0/3.0, 2.0/3.0, -1.0);
            float4 p = RGB.g < RGB.b ? float4(RGB.b, RGB.g, k.w, k.z) : float4(RGB.gb, k.xy);
            float4 q = RGB.r < p.x   ? float4(p.x, p.y, p.w, RGB.r)   : float4(RGB.r , p.yzx);
            float  d = q.x - min(q.w, q.y);
            float  e = 1.0e-10;
            return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            
            }
            
            float3 HSV2RGB( float3 HSV ){
            
            float4 k = float4(1.0, 2.0/3.0, 1.0/3.0, 3.0);
            float3 p = abs(frac(HSV.xxx + k.xyz) * 6.0 - k.www);
            return HSV.z * lerp(k.xxx, clamp(p - k.xxx, 0.0, 1.0), HSV.y);
            
            }
            
            uniform float _Hue;
            uniform float _Saturation;
            uniform float _Value;
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
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
                float4 _maintex_var = tex2D(_maintex,TRANSFORM_TEX(i.uv0, _maintex));
                float3 node_3262 = RGB2HSV( _maintex_var.rgb ).rgb;
                float3 node_9090 = HSV2RGB( float3(float2((_Hue+node_3262.r),(_Saturation+node_3262.g)),(_Value+node_3262.b)) );
                float3 node_3258 = (node_9090*_mainstrength);
                float node_3209_if_leA = step(_flare,node_3258);
                float node_3209_if_leB = step(node_3258,_flare);
                float3 node_3205 = (node_9090+(pow(1.0-max(0,dot(i.normalDir, viewDirection)),2.0)*_flarecolor.rgb*3.0));
                float3 finalColor = lerp((node_3209_if_leA*node_3258)+(node_3209_if_leB*node_3205),node_3205,node_3209_if_leA*node_3209_if_leB);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
