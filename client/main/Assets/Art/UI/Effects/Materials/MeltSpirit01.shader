// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:False,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:2,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9760,x:32805,y:32570,varname:node_9760,prsc:2|custl-2869-OUT,alpha-3563-OUT,voffset-7705-OUT;n:type:ShaderForge.SFN_Tex2d,id:8428,x:32264,y:32619,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8428,prsc:2,ntxv:0,isnm:False|UVIN-3778-OUT;n:type:ShaderForge.SFN_ScreenPos,id:5590,x:31031,y:32363,varname:node_5590,prsc:2,sctp:1;n:type:ShaderForge.SFN_Add,id:3778,x:32096,y:32619,varname:node_3778,prsc:2|A-8837-OUT,B-4885-OUT;n:type:ShaderForge.SFN_Time,id:6205,x:30852,y:32892,varname:node_6205,prsc:2;n:type:ShaderForge.SFN_Append,id:2860,x:31031,y:32595,varname:node_2860,prsc:2|A-6523-OUT,B-6205-T;n:type:ShaderForge.SFN_Tex2d,id:8762,x:31585,y:32657,ptovrint:False,ptlb:OffsetTex,ptin:_OffsetTex,varname:node_8762,prsc:2,tex:105509fc2446cae4f9f9971158efab6f,ntxv:0,isnm:False|UVIN-1281-OUT;n:type:ShaderForge.SFN_Append,id:1618,x:31753,y:32674,varname:node_1618,prsc:2|A-8762-R,B-8762-R;n:type:ShaderForge.SFN_Multiply,id:4885,x:31920,y:32674,varname:node_4885,prsc:2|A-1618-OUT,B-1404-OUT;n:type:ShaderForge.SFN_Add,id:1281,x:31428,y:32657,varname:node_1281,prsc:2|A-5590-UVOUT,B-8585-OUT;n:type:ShaderForge.SFN_Multiply,id:8585,x:31231,y:32595,varname:node_8585,prsc:2|A-2860-OUT,B-2310-OUT;n:type:ShaderForge.SFN_NormalVector,id:3538,x:31920,y:32831,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:8498,x:32166,y:32927,varname:node_8498,prsc:2|NRM-3538-OUT,EXP-5049-OUT;n:type:ShaderForge.SFN_Add,id:2869,x:32481,y:32608,varname:node_2869,prsc:2|A-8428-RGB,B-8428-RGB,C-1504-RGB;n:type:ShaderForge.SFN_Vector1,id:6523,x:30849,y:32595,varname:node_6523,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:1849,x:32329,y:32927,varname:node_1849,prsc:2|IN-8498-OUT;n:type:ShaderForge.SFN_Add,id:8837,x:31794,y:32369,varname:node_8837,prsc:2|A-5590-UVOUT,B-197-OUT;n:type:ShaderForge.SFN_Negate,id:6661,x:31428,y:32431,varname:node_6661,prsc:2|IN-8585-OUT;n:type:ShaderForge.SFN_Multiply,id:197,x:31585,y:32431,varname:node_197,prsc:2|A-6661-OUT,B-8795-OUT;n:type:ShaderForge.SFN_Vector1,id:8795,x:31428,y:32584,varname:node_8795,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3563,x:32506,y:32927,varname:node_3563,prsc:2|A-1849-OUT,B-1849-OUT,C-4408-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:4681,x:31254,y:33139,varname:node_4681,prsc:2;n:type:ShaderForge.SFN_Add,id:877,x:32154,y:33079,varname:node_877,prsc:2|A-4681-Y,B-2031-OUT;n:type:ShaderForge.SFN_Negate,id:4408,x:32329,y:33079,varname:node_4408,prsc:2|IN-877-OUT;n:type:ShaderForge.SFN_Sin,id:5525,x:32154,y:33258,varname:node_5525,prsc:2|IN-5718-OUT;n:type:ShaderForge.SFN_Multiply,id:7705,x:32350,y:33258,varname:node_7705,prsc:2|A-5525-OUT,B-9973-OUT,C-4681-Y;n:type:ShaderForge.SFN_Multiply,id:9603,x:31751,y:33280,varname:node_9603,prsc:2|A-641-OUT,B-6797-OUT;n:type:ShaderForge.SFN_Add,id:5718,x:31943,y:33258,varname:node_5718,prsc:2|A-6205-T,B-9603-OUT;n:type:ShaderForge.SFN_Add,id:641,x:31552,y:33280,varname:node_641,prsc:2|A-4681-X,B-4681-Y,C-4681-Z;n:type:ShaderForge.SFN_Vector1,id:2310,x:31031,y:32744,varname:node_2310,prsc:2,v1:-0.1;n:type:ShaderForge.SFN_Vector1,id:1404,x:31753,y:32813,varname:node_1404,prsc:2,v1:-0.2;n:type:ShaderForge.SFN_Vector1,id:5049,x:31920,y:32982,varname:node_5049,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:9973,x:32154,y:33381,varname:node_9973,prsc:2,v1:0.015;n:type:ShaderForge.SFN_Vector1,id:6797,x:31552,y:33423,varname:node_6797,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:2031,x:31920,y:33117,varname:node_2031,prsc:2|A-2456-OUT,B-9886-OUT;n:type:ShaderForge.SFN_Vector1,id:9886,x:31674,y:33193,varname:node_9886,prsc:2,v1:-2.5;n:type:ShaderForge.SFN_Slider,id:2456,x:31552,y:33100,ptovrint:False,ptlb:AlphaOffset,ptin:_AlphaOffset,varname:node_2456,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:1504,x:32268,y:32443,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_1504,prsc:2,glob:False,c1:0.5073529,c2:0,c3:0,c4:1;proporder:1504-8428-8762-2456;pass:END;sub:END;*/

Shader "Custom/Fire" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5073529,0,0,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _OffsetTex ("OffsetTex", 2D) = "white" {}
        _AlphaOffset ("AlphaOffset", Range(0, 1)) = 0
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
            Cull Off
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
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _OffsetTex; uniform float4 _OffsetTex_ST;
            uniform float _AlphaOffset;
            uniform float4 _MainColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_6205 = _Time + _TimeEditor;
                float node_7705 = (sin((node_6205.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*0.015*mul(_Object2World, v.vertex).g);
                v.vertex.xyz += float3(node_7705,node_7705,node_7705);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = float4( o.pos.xy / o.pos.w, 0, 0 );
                o.screenPos.y *= _ProjectionParams.x;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
                float4 node_6205 = _Time + _TimeEditor;
                float2 node_8585 = (float2(0.0,node_6205.g)*(-0.1));
                float2 node_1281 = (float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+node_8585);
                float4 _OffsetTex_var = tex2D(_OffsetTex,TRANSFORM_TEX(node_1281, _OffsetTex));
                float2 node_3778 = ((float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+((-1*node_8585)*0.5))+(float2(_OffsetTex_var.r,_OffsetTex_var.r)*(-0.2)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3778, _MainTex));
                float3 finalColor = (_MainTex_var.rgb+_MainTex_var.rgb+_MainColor.rgb);
                float node_1849 = (1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),1.0));
                return fixed4(finalColor,(node_1849*node_1849*(-1*(i.posWorld.g+(_AlphaOffset*(-2.5))))));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
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
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 posWorld : TEXCOORD5;
                float4 screenPos : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 node_6205 = _Time + _TimeEditor;
                float node_7705 = (sin((node_6205.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*0.015*mul(_Object2World, v.vertex).g);
                v.vertex.xyz += float3(node_7705,node_7705,node_7705);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = float4( o.pos.xy / o.pos.w, 0, 0 );
                o.screenPos.y *= _ProjectionParams.x;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
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
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float4 node_6205 = _Time + _TimeEditor;
                float node_7705 = (sin((node_6205.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*0.015*mul(_Object2World, v.vertex).g);
                v.vertex.xyz += float3(node_7705,node_7705,node_7705);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = float4( o.pos.xy / o.pos.w, 0, 0 );
                o.screenPos.y *= _ProjectionParams.x;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
