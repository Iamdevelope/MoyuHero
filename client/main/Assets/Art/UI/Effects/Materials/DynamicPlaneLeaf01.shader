// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5818,x:32847,y:33044,varname:node_5818,prsc:2|custl-7578-RGB,clip-7578-A,voffset-1950-OUT;n:type:ShaderForge.SFN_Tex2d,id:7578,x:32519,y:32971,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:0,ntxv:0,isnm:False|UVIN-7134-OUT;n:type:ShaderForge.SFN_Tex2d,id:5980,x:31818,y:33011,ptovrint:False,ptlb:FlowMap,ptin:_FlowMap,varname:_FlowMap,prsc:0,tex:1de99c793f5dc6349b3ba9b6dec7bdd9,ntxv:0,isnm:False|UVIN-2605-OUT;n:type:ShaderForge.SFN_Append,id:7289,x:31975,y:33031,varname:node_7289,prsc:2|A-5980-R,B-5980-G;n:type:ShaderForge.SFN_Multiply,id:9380,x:32143,y:33004,varname:node_9380,prsc:2|A-5097-OUT,B-7289-OUT,C-7474-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7474,x:31975,y:33200,ptovrint:False,ptlb:FlowStr,ptin:_FlowStr,varname:_FlowStr,prsc:0,glob:False,v1:0.1;n:type:ShaderForge.SFN_Add,id:7134,x:32330,y:32971,varname:node_7134,prsc:2|A-3916-UVOUT,B-9380-OUT,C-9245-OUT;n:type:ShaderForge.SFN_Time,id:9063,x:30986,y:33637,varname:node_9063,prsc:0;n:type:ShaderForge.SFN_Multiply,id:5822,x:31187,y:33126,varname:node_5822,prsc:2|A-6209-OUT,B-9063-TSL;n:type:ShaderForge.SFN_ValueProperty,id:6209,x:31018,y:33126,ptovrint:False,ptlb:FlowSpeed,ptin:_FlowSpeed,varname:_FlowSpeed,prsc:0,glob:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:1844,x:31360,y:33126,varname:node_1844,prsc:2|A-5822-OUT,B-7143-OUT;n:type:ShaderForge.SFN_Vector1,id:7143,x:31187,y:33274,varname:node_7143,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:2605,x:31615,y:33011,varname:node_2605,prsc:0|A-3916-UVOUT,B-1844-OUT;n:type:ShaderForge.SFN_Multiply,id:9245,x:32154,y:33200,varname:node_9245,prsc:2|A-7474-OUT,B-898-OUT;n:type:ShaderForge.SFN_Vector2,id:4485,x:31360,y:32844,varname:node_4485,prsc:0,v1:0.5,v2:0.25;n:type:ShaderForge.SFN_Distance,id:5097,x:31615,y:32844,varname:node_5097,prsc:2|A-4485-OUT,B-3916-UVOUT;n:type:ShaderForge.SFN_Vector1,id:898,x:31975,y:33316,varname:node_898,prsc:2,v1:-0.15;n:type:ShaderForge.SFN_FragmentPosition,id:5743,x:31422,y:33743,varname:node_5743,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:3916,x:31360,y:32967,varname:node_3916,prsc:2,uv:0;n:type:ShaderForge.SFN_Sin,id:9006,x:32009,y:33720,varname:node_9006,prsc:2|IN-9294-OUT;n:type:ShaderForge.SFN_Cos,id:7964,x:32009,y:33859,varname:node_7964,prsc:2|IN-431-OUT;n:type:ShaderForge.SFN_Append,id:7296,x:32361,y:33761,varname:node_7296,prsc:2|A-7054-OUT,B-9257-OUT;n:type:ShaderForge.SFN_Multiply,id:8591,x:31593,y:33616,varname:node_8591,prsc:2|A-4451-OUT,B-9063-T;n:type:ShaderForge.SFN_Multiply,id:5688,x:31593,y:33937,varname:node_5688,prsc:2|A-9063-T,B-2078-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4451,x:31408,y:33616,ptovrint:False,ptlb:TimeX,ptin:_TimeX,varname:_TimeX,prsc:0,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2078,x:31408,y:33981,ptovrint:False,ptlb:TimeY,ptin:_TimeY,varname:_TimeY,prsc:0,glob:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:7054,x:32178,y:33699,varname:node_7054,prsc:2|A-4195-OUT,B-9006-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4195,x:32009,y:33613,ptovrint:False,ptlb:sin,ptin:_sin,varname:_sin,prsc:0,glob:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:9257,x:32178,y:33859,varname:node_9257,prsc:2|A-7964-OUT,B-5692-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5692,x:32009,y:34016,ptovrint:False,ptlb:step,ptin:_step,varname:_step,prsc:0,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1950,x:32543,y:33599,varname:node_1950,prsc:2|A-2189-OUT,B-7296-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6058,x:32009,y:33419,ptovrint:False,ptlb:OffsetStr,ptin:_OffsetStr,varname:_OffsetStr,prsc:0,glob:False,v1:1;n:type:ShaderForge.SFN_Append,id:2189,x:32357,y:33469,varname:node_2189,prsc:2|A-6058-OUT,B-5502-OUT;n:type:ShaderForge.SFN_Vector1,id:7377,x:32009,y:33520,varname:node_7377,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:5502,x:32178,y:33492,varname:node_5502,prsc:2|A-6058-OUT,B-7377-OUT;n:type:ShaderForge.SFN_Add,id:9294,x:31821,y:33720,varname:node_9294,prsc:2|A-8591-OUT,B-914-OUT;n:type:ShaderForge.SFN_Add,id:431,x:31821,y:33859,varname:node_431,prsc:2|A-914-OUT,B-5688-OUT;n:type:ShaderForge.SFN_Add,id:914,x:31593,y:33762,varname:node_914,prsc:0|A-5743-X,B-5743-Y,C-5743-Z;proporder:7578-5980-7474-6209-4451-2078-4195-5692-6058;pass:END;sub:END;*/

Shader "DreamFaction/Environment/DynamicPlaneLeaf01" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _FlowMap ("FlowMap", 2D) = "white" {}
        _FlowStr ("FlowStr", Float ) = 0.1
        _FlowSpeed ("FlowSpeed", Float ) = 0.1
        _TimeX ("TimeX", Float ) = 1
        _TimeY ("TimeY", Float ) = 2
        _sin ("sin", Float ) = 2
        _step ("step", Float ) = 1
        _OffsetStr ("OffsetStr", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
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
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform fixed _FlowStr;
            uniform fixed _FlowSpeed;
            uniform fixed _TimeX;
            uniform fixed _TimeY;
            uniform fixed _sin;
            uniform fixed _step;
            uniform fixed _OffsetStr;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed node_914 = (mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b);
                v.vertex.xyz += float3((float2(_OffsetStr,(_OffsetStr*0.5))*float2((_sin*sin(((_TimeX*node_9063.g)+node_914))),(cos((node_914+(node_9063.g*_TimeY)))*_step))),0.0);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed2 node_2605 = (i.uv0+float2((_FlowSpeed*node_9063.r),0.0));
                fixed4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_2605, _FlowMap));
                float2 node_7134 = (i.uv0+(distance(fixed2(0.5,0.25),i.uv0)*float2(_FlowMap_var.r,_FlowMap_var.g)*_FlowStr)+(_FlowStr*(-0.15)));
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7134, _MainTex));
                clip( BinaryDither3x3(_MainTex_var.a - 1.5, sceneUVs) );
////// Lighting:
                float3 finalColor = _MainTex_var.rgb;
                return fixed4(finalColor,1);
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
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform fixed _FlowStr;
            uniform fixed _FlowSpeed;
            uniform fixed _TimeX;
            uniform fixed _TimeY;
            uniform fixed _sin;
            uniform fixed _step;
            uniform fixed _OffsetStr;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float4 posWorld : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed node_914 = (mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b);
                v.vertex.xyz += float3((float2(_OffsetStr,(_OffsetStr*0.5))*float2((_sin*sin(((_TimeX*node_9063.g)+node_914))),(cos((node_914+(node_9063.g*_TimeY)))*_step))),0.0);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed2 node_2605 = (i.uv0+float2((_FlowSpeed*node_9063.r),0.0));
                fixed4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_2605, _FlowMap));
                float2 node_7134 = (i.uv0+(distance(fixed2(0.5,0.25),i.uv0)*float2(_FlowMap_var.r,_FlowMap_var.g)*_FlowStr)+(_FlowStr*(-0.15)));
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7134, _MainTex));
                clip( BinaryDither3x3(_MainTex_var.a - 1.5, sceneUVs) );
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform fixed _FlowStr;
            uniform fixed _FlowSpeed;
            uniform fixed _TimeX;
            uniform fixed _TimeY;
            uniform fixed _sin;
            uniform fixed _step;
            uniform fixed _OffsetStr;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed node_914 = (mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b);
                v.vertex.xyz += float3((float2(_OffsetStr,(_OffsetStr*0.5))*float2((_sin*sin(((_TimeX*node_9063.g)+node_914))),(cos((node_914+(node_9063.g*_TimeY)))*_step))),0.0);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                fixed4 node_9063 = _Time + _TimeEditor;
                fixed2 node_2605 = (i.uv0+float2((_FlowSpeed*node_9063.r),0.0));
                fixed4 _FlowMap_var = tex2D(_FlowMap,TRANSFORM_TEX(node_2605, _FlowMap));
                float2 node_7134 = (i.uv0+(distance(fixed2(0.5,0.25),i.uv0)*float2(_FlowMap_var.r,_FlowMap_var.g)*_FlowStr)+(_FlowStr*(-0.15)));
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7134, _MainTex));
                clip( BinaryDither3x3(_MainTex_var.a - 1.5, sceneUVs) );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
