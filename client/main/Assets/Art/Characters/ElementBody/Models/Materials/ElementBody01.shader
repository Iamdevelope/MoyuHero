// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:2,dpts:2,wrdp:True,dith:2,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:2406,x:33925,y:32697,varname:node_2406,prsc:2|custl-4014-OUT,voffset-5941-OUT;n:type:ShaderForge.SFN_Tex2d,id:8284,x:32775,y:32901,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:0,tex:259615b3905b2b041b48b8897623ac9c,ntxv:0,isnm:False|UVIN-8860-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8860,x:32594,y:32901,varname:node_8860,prsc:2,uv:0;n:type:ShaderForge.SFN_NormalVector,id:3652,x:32031,y:32670,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:7959,x:32785,y:33287,varname:node_7959,prsc:2,dt:0|A-3652-OUT,B-6615-OUT;n:type:ShaderForge.SFN_Power,id:7412,x:32961,y:33287,varname:node_7412,prsc:2|VAL-7959-OUT,EXP-8990-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8990,x:32591,y:33462,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:0,glob:False,v1:20;n:type:ShaderForge.SFN_Fresnel,id:1372,x:32775,y:32666,varname:node_1372,prsc:2|NRM-3652-OUT,EXP-6998-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6998,x:32589,y:32688,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:_FresnelExp,prsc:0,glob:False,v1:10;n:type:ShaderForge.SFN_ViewVector,id:6615,x:32591,y:33308,varname:node_6615,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7318,x:32947,y:32666,varname:node_7318,prsc:2|A-1372-OUT,B-9045-OUT,C-3436-RGB;n:type:ShaderForge.SFN_ValueProperty,id:9045,x:32775,y:32814,ptovrint:False,ptlb:FresnelStr,ptin:_FresnelStr,varname:_FresnelStr,prsc:0,glob:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:5342,x:33149,y:33265,cmnt:高光,varname:node_5342,prsc:2|A-8284-R,B-7412-OUT,C-3248-RGB,D-2420-OUT;n:type:ShaderForge.SFN_Tex2d,id:609,x:33119,y:32318,ptovrint:False,ptlb:BackGround,ptin:_BackGround,varname:_BackGround,prsc:0,tex:0813fcaca3dbac5448b0882b15e3090c,ntxv:0,isnm:False|UVIN-2751-OUT;n:type:ShaderForge.SFN_ScreenPos,id:5409,x:32211,y:32072,varname:node_5409,prsc:2,sctp:2;n:type:ShaderForge.SFN_Lerp,id:2939,x:33484,y:32446,varname:node_2939,prsc:2|A-9125-OUT,B-7961-OUT,T-6961-OUT;n:type:ShaderForge.SFN_Fresnel,id:3117,x:32947,y:32487,varname:node_3117,prsc:2|NRM-3652-OUT,EXP-2074-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2074,x:32774,y:32509,ptovrint:False,ptlb:BlendExp,ptin:_BlendExp,varname:_BlendExp,prsc:0,glob:False,v1:0.15;n:type:ShaderForge.SFN_Power,id:6961,x:33119,y:32487,varname:node_6961,prsc:2|VAL-3117-OUT,EXP-7250-OUT;n:type:ShaderForge.SFN_Vector1,id:7250,x:32947,y:32615,varname:node_7250,prsc:2,v1:5;n:type:ShaderForge.SFN_Add,id:2751,x:32947,y:32318,varname:node_2751,prsc:0|A-1539-OUT,B-649-OUT;n:type:ShaderForge.SFN_Append,id:649,x:32774,y:32340,varname:node_649,prsc:2|A-4938-OUT,B-4938-OUT;n:type:ShaderForge.SFN_Fresnel,id:4938,x:32578,y:32340,varname:node_4938,prsc:0|NRM-3652-OUT,EXP-4203-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4203,x:32410,y:32360,ptovrint:False,ptlb:RefraExp,ptin:_RefraExp,varname:_RefraExp,prsc:0,glob:False,v1:3;n:type:ShaderForge.SFN_Add,id:4014,x:33748,y:32938,varname:node_4014,prsc:2|A-2939-OUT,B-5342-OUT;n:type:ShaderForge.SFN_Add,id:7961,x:33119,y:32666,varname:node_7961,prsc:2|A-7318-OUT,B-5785-OUT;n:type:ShaderForge.SFN_Multiply,id:5785,x:32947,y:32901,varname:node_5785,prsc:2|A-8284-RGB,B-4104-RGB;n:type:ShaderForge.SFN_Color,id:4104,x:32768,y:33089,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:_MainColor,prsc:0,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:3248,x:32961,y:33428,ptovrint:False,ptlb:SpecularColor,ptin:_SpecularColor,varname:_SpecularColor,prsc:0,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:2420,x:32961,y:33591,varname:node_2420,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:3436,x:32394,y:32706,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:_FresnelColor,prsc:0,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:1539,x:32410,y:32212,varname:node_1539,prsc:2|A-5409-UVOUT,B-25-OUT;n:type:ShaderForge.SFN_Tex2d,id:9788,x:31844,y:32220,ptovrint:False,ptlb:OffsetTex,ptin:_OffsetTex,varname:_OffsetTex,prsc:0,ntxv:0,isnm:False|UVIN-8490-OUT;n:type:ShaderForge.SFN_Append,id:3763,x:32033,y:32233,varname:node_3763,prsc:2|A-9788-R,B-9788-R;n:type:ShaderForge.SFN_Multiply,id:25,x:32211,y:32233,varname:node_25,prsc:0|A-3763-OUT,B-2880-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2880,x:32033,y:32408,ptovrint:False,ptlb:OffsetStr,ptin:_OffsetStr,varname:_OffsetStr,prsc:0,glob:False,v1:0.1;n:type:ShaderForge.SFN_Time,id:5529,x:31128,y:32426,varname:node_5529,prsc:0;n:type:ShaderForge.SFN_Vector1,id:8026,x:31278,y:32150,varname:node_8026,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:8957,x:31476,y:32281,varname:node_8957,prsc:0|A-8026-OUT,B-227-OUT;n:type:ShaderForge.SFN_Multiply,id:227,x:31303,y:32302,varname:node_227,prsc:2|A-370-OUT,B-5529-T;n:type:ShaderForge.SFN_ValueProperty,id:370,x:31128,y:32302,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_Speed,prsc:0,glob:False,v1:-0.02;n:type:ShaderForge.SFN_Add,id:8490,x:31682,y:32220,varname:node_8490,prsc:0|A-670-UVOUT,B-8957-OUT;n:type:ShaderForge.SFN_ScreenPos,id:670,x:31476,y:32024,varname:node_670,prsc:2,sctp:1;n:type:ShaderForge.SFN_Sin,id:4497,x:32961,y:33678,varname:node_4497,prsc:2|IN-8157-OUT;n:type:ShaderForge.SFN_Multiply,id:5941,x:33136,y:33678,varname:node_5941,prsc:2|A-4497-OUT,B-3652-OUT,C-4210-OUT;n:type:ShaderForge.SFN_Multiply,id:8391,x:32558,y:33700,varname:node_8391,prsc:2|A-1293-OUT,B-6892-OUT;n:type:ShaderForge.SFN_Add,id:8157,x:32750,y:33678,varname:node_8157,prsc:2|A-5529-T,B-8391-OUT;n:type:ShaderForge.SFN_Add,id:1293,x:32359,y:33700,varname:node_1293,prsc:2|A-3062-X,B-3062-Y,C-3062-Z;n:type:ShaderForge.SFN_Vector1,id:6892,x:32359,y:33843,varname:node_6892,prsc:2,v1:10;n:type:ShaderForge.SFN_FragmentPosition,id:3062,x:32136,y:33681,varname:node_3062,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4210,x:32961,y:33835,ptovrint:False,ptlb:WaveStr,ptin:_WaveStr,varname:_WaveStr,prsc:0,glob:False,v1:0.05;n:type:ShaderForge.SFN_Tex2d,id:2549,x:32892,y:31994,ptovrint:False,ptlb:DecorateTex,ptin:_DecorateTex,varname:_DecorateTex,prsc:0,tex:7e4b9120221965c48a0468dcadd1f114,ntxv:0,isnm:False|UVIN-1416-OUT;n:type:ShaderForge.SFN_Add,id:1416,x:32410,y:31996,varname:node_1416,prsc:0|A-25-OUT,B-670-UVOUT,C-1515-OUT;n:type:ShaderForge.SFN_Add,id:9125,x:33291,y:32297,varname:node_9125,prsc:2|A-2549-RGB,B-609-RGB;n:type:ShaderForge.SFN_Multiply,id:1515,x:31844,y:32055,varname:node_1515,prsc:2|A-2322-OUT,B-8957-OUT;n:type:ShaderForge.SFN_Vector1,id:2322,x:31646,y:31963,varname:node_2322,prsc:2,v1:-0.25;proporder:4104-8284-3248-8990-3436-9045-6998-609-2074-4203-9788-2880-370-4210-2549;pass:END;sub:END;*/

Shader "Custom/ElementBody01" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _SpecularColor ("SpecularColor", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Float ) = 20
        _FresnelColor ("FresnelColor", Color) = (0.5,0.5,0.5,1)
        _FresnelStr ("FresnelStr", Float ) = 3
        _FresnelExp ("FresnelExp", Float ) = 10
        _BackGround ("BackGround", 2D) = "white" {}
        _BlendExp ("BlendExp", Float ) = 0.15
        _RefraExp ("RefraExp", Float ) = 3
        _OffsetTex ("OffsetTex", 2D) = "white" {}
        _OffsetStr ("OffsetStr", Float ) = 0.1
        _Speed ("Speed", Float ) = -0.02
        _WaveStr ("WaveStr", Float ) = 0.05
        _DecorateTex ("DecorateTex", 2D) = "white" {}
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
            Cull Off
            
            
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
            uniform fixed _Gloss;
            uniform fixed _FresnelExp;
            uniform fixed _FresnelStr;
            uniform sampler2D _BackGround; uniform float4 _BackGround_ST;
            uniform fixed _BlendExp;
            uniform fixed _RefraExp;
            uniform fixed4 _MainColor;
            uniform fixed4 _SpecularColor;
            uniform fixed4 _FresnelColor;
            uniform sampler2D _OffsetTex; uniform float4 _OffsetTex_ST;
            uniform fixed _OffsetStr;
            uniform fixed _Speed;
            uniform fixed _WaveStr;
            uniform sampler2D _DecorateTex; uniform float4 _DecorateTex_ST;
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
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                fixed4 node_5529 = _Time + _TimeEditor;
                v.vertex.xyz += (sin((node_5529.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*v.normal*_WaveStr);
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
                fixed4 node_5529 = _Time + _TimeEditor;
                fixed2 node_8957 = float2(0.0,(_Speed*node_5529.g));
                fixed2 node_8490 = (float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+node_8957);
                fixed4 _OffsetTex_var = tex2D(_OffsetTex,TRANSFORM_TEX(node_8490, _OffsetTex));
                fixed2 node_25 = (float2(_OffsetTex_var.r,_OffsetTex_var.r)*_OffsetStr);
                fixed2 node_1416 = (node_25+float2(i.screenPos.x*(_ScreenParams.r/_ScreenParams.g), i.screenPos.y).rg+((-0.25)*node_8957));
                fixed4 _DecorateTex_var = tex2D(_DecorateTex,TRANSFORM_TEX(node_1416, _DecorateTex));
                fixed node_4938 = pow(1.0-max(0,dot(i.normalDir, viewDirection)),_RefraExp);
                fixed2 node_2751 = ((sceneUVs.rg+node_25)+float2(node_4938,node_4938));
                fixed4 _BackGround_var = tex2D(_BackGround,TRANSFORM_TEX(node_2751, _BackGround));
                fixed4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 finalColor = (lerp((_DecorateTex_var.rgb+_BackGround_var.rgb),((pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelExp)*_FresnelStr*_FresnelColor.rgb)+(_MainTex_var.rgb*_MainColor.rgb)),pow(pow(1.0-max(0,dot(i.normalDir, viewDirection)),_BlendExp),5.0))+(_MainTex_var.r*pow(dot(i.normalDir,viewDirection),_Gloss)*_SpecularColor.rgb*2.0));
                return fixed4(finalColor,1);
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
            uniform fixed _WaveStr;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 posWorld : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                fixed4 node_5529 = _Time + _TimeEditor;
                v.vertex.xyz += (sin((node_5529.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*v.normal*_WaveStr);
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
            uniform fixed _WaveStr;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                fixed4 node_5529 = _Time + _TimeEditor;
                v.vertex.xyz += (sin((node_5529.g+((mul(_Object2World, v.vertex).r+mul(_Object2World, v.vertex).g+mul(_Object2World, v.vertex).b)*10.0)))*v.normal*_WaveStr);
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
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
