// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.6102941,fgcg:0.7059274,fgcb:1,fgca:1,fgde:0.01,fgrn:42,fgrf:150,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32268,y:32773|custl-446-OUT,clip-1155-OUT,olwid-1419-OUT,olcol-631-RGB;n:type:ShaderForge.SFN_Tex2d,id:2,x:33510,y:32642,ptlb:maintex,ptin:_maintex,tex:5b6606339c98d64429045cf4242e979b,ntxv:0,isnm:False|UVIN-726-UVOUT;n:type:ShaderForge.SFN_Dot,id:5,x:33613,y:32819,dt:4|A-1429-OUT,B-7-OUT;n:type:ShaderForge.SFN_NormalVector,id:7,x:33870,y:32898,pt:False;n:type:ShaderForge.SFN_Append,id:8,x:33396,y:32819|A-5-OUT,B-5-OUT;n:type:ShaderForge.SFN_Multiply,id:446,x:32742,y:32701|A-859-RGB,B-861-OUT;n:type:ShaderForge.SFN_ValueProperty,id:459,x:32714,y:33060,ptlb:outlinewidth,ptin:_outlinewidth,glob:False,v1:1;n:type:ShaderForge.SFN_Color,id:631,x:32521,y:33230,ptlb:linecolor,ptin:_linecolor,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:726,x:33870,y:32614,uv:0;n:type:ShaderForge.SFN_Color,id:859,x:32962,y:32561,ptlb:maincolor,ptin:_maincolor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:861,x:32962,y:32728|A-862-OUT,B-1438-OUT,T-1431-R;n:type:ShaderForge.SFN_Multiply,id:862,x:33143,y:32363|A-2-RGB,B-2-RGB;n:type:ShaderForge.SFN_ToggleProperty,id:1154,x:32950,y:32913,ptlb:destory,ptin:_destory,on:False;n:type:ShaderForge.SFN_If,id:1155,x:32714,y:32900|A-1154-OUT,B-1156-OUT,GT-1157-OUT,EQ-1157-OUT,LT-2-A;n:type:ShaderForge.SFN_Vector1,id:1156,x:32950,y:32970,v1:1;n:type:ShaderForge.SFN_Multiply,id:1157,x:32967,y:33091|A-2-A,B-1170-OUT,C-1172-OUT;n:type:ShaderForge.SFN_Tex2d,id:1159,x:33429,y:33060,ptlb:destroytex,ptin:_destroytex,tex:0a7c59330f3cd8247bdaa23b4faa5b1d,ntxv:0,isnm:False|UVIN-1260-UVOUT;n:type:ShaderForge.SFN_Power,id:1170,x:33182,y:33116|VAL-1159-R,EXP-1261-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1172,x:33739,y:33295,ptlb:destroyvalue,ptin:_destroyvalue,glob:False,v1:1;n:type:ShaderForge.SFN_ScreenPos,id:1260,x:33645,y:33060,sctp:0;n:type:ShaderForge.SFN_OneMinus,id:1261,x:33429,y:33210|IN-1172-OUT;n:type:ShaderForge.SFN_Divide,id:1419,x:32521,y:33060|A-459-OUT,B-1420-OUT;n:type:ShaderForge.SFN_Vector1,id:1420,x:32714,y:33125,v1:100;n:type:ShaderForge.SFN_LightVector,id:1429,x:33876,y:32761;n:type:ShaderForge.SFN_Tex2d,id:1431,x:33168,y:32819,ptlb:toonramp,ptin:_toonramp,tex:ab32c04e0e597744fa44601c64973df1,ntxv:0,isnm:False|UVIN-8-OUT;n:type:ShaderForge.SFN_LightColor,id:1435,x:33510,y:32489;n:type:ShaderForge.SFN_Add,id:1438,x:33168,y:32645|A-1439-OUT,B-2-RGB;n:type:ShaderForge.SFN_Multiply,id:1439,x:33298,y:32473|A-1435-RGB,B-1435-RGB;proporder:859-2-1431-631-459-1154-1159-1172;pass:END;sub:END;*/

Shader "DreamFaction/Characters/Toon" {
    Properties {
        _maincolor ("maincolor", Color) = (0.5,0.5,0.5,1)
        _maintex ("maintex", 2D) = "white" {}
        _toonramp ("toonramp", 2D) = "white" {}
        _linecolor ("linecolor", Color) = (0,0,0,1)
        _outlinewidth ("outlinewidth", Float ) = 1
        [MaterialToggle] _destory ("destory", Float ) = 0
        _destroytex ("destroytex", 2D) = "white" {}
        _destroyvalue ("destroyvalue", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float _outlinewidth;
            uniform float4 _linecolor;
            uniform fixed _destory;
            uniform sampler2D _destroytex; uniform float4 _destroytex_ST;
            uniform float _destroyvalue;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*(_outlinewidth/100.0),1));
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float node_1155_if_leA = step(_destory,1.0);
                float node_1155_if_leB = step(1.0,_destory);
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float2 node_1260 = i.screenPos;
                float node_1157 = (node_2.a*pow(tex2D(_destroytex,TRANSFORM_TEX(node_1260.rg, _destroytex)).r,(1.0 - _destroyvalue))*_destroyvalue);
                clip(lerp((node_1155_if_leA*node_2.a)+(node_1155_if_leB*node_1157),node_1157,node_1155_if_leA*node_1155_if_leB) - 0.5);
                return fixed4(_linecolor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float4 _maincolor;
            uniform fixed _destory;
            uniform sampler2D _destroytex; uniform float4 _destroytex_ST;
            uniform float _destroyvalue;
            uniform sampler2D _toonramp; uniform float4 _toonramp_ST;
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
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
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
                
                float node_1155_if_leA = step(_destory,1.0);
                float node_1155_if_leB = step(1.0,_destory);
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float2 node_1260 = i.screenPos;
                float node_1157 = (node_2.a*pow(tex2D(_destroytex,TRANSFORM_TEX(node_1260.rg, _destroytex)).r,(1.0 - _destroyvalue))*_destroyvalue);
                clip(lerp((node_1155_if_leA*node_2.a)+(node_1155_if_leB*node_1157),node_1157,node_1155_if_leA*node_1155_if_leB) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float4 node_1435 = _LightColor0;
                float node_5 = 0.5*dot(lightDirection,i.normalDir)+0.5;
                float2 node_8 = float2(node_5,node_5);
                float3 finalColor = (_maincolor.rgb*lerp((node_2.rgb*node_2.rgb),((node_1435.rgb*node_1435.rgb)+node_2.rgb),tex2D(_toonramp,TRANSFORM_TEX(node_8, _toonramp)).r));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform float4 _maincolor;
            uniform fixed _destory;
            uniform sampler2D _destroytex; uniform float4 _destroytex_ST;
            uniform float _destroyvalue;
            uniform sampler2D _toonramp; uniform float4 _toonramp_ST;
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
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
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
                
                float node_1155_if_leA = step(_destory,1.0);
                float node_1155_if_leB = step(1.0,_destory);
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float2 node_1260 = i.screenPos;
                float node_1157 = (node_2.a*pow(tex2D(_destroytex,TRANSFORM_TEX(node_1260.rg, _destroytex)).r,(1.0 - _destroyvalue))*_destroyvalue);
                clip(lerp((node_1155_if_leA*node_2.a)+(node_1155_if_leB*node_1157),node_1157,node_1155_if_leA*node_1155_if_leB) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float4 node_1435 = _LightColor0;
                float node_5 = 0.5*dot(lightDirection,i.normalDir)+0.5;
                float2 node_8 = float2(node_5,node_5);
                float3 finalColor = (_maincolor.rgb*lerp((node_2.rgb*node_2.rgb),((node_1435.rgb*node_1435.rgb)+node_2.rgb),tex2D(_toonramp,TRANSFORM_TEX(node_8, _toonramp)).r));
/// Final Color:
                return fixed4(finalColor * 1,0);
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
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform fixed _destory;
            uniform sampler2D _destroytex; uniform float4 _destroytex_ST;
            uniform float _destroyvalue;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float4 screenPos : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float node_1155_if_leA = step(_destory,1.0);
                float node_1155_if_leB = step(1.0,_destory);
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float2 node_1260 = i.screenPos;
                float node_1157 = (node_2.a*pow(tex2D(_destroytex,TRANSFORM_TEX(node_1260.rg, _destroytex)).r,(1.0 - _destroyvalue))*_destroyvalue);
                clip(lerp((node_1155_if_leA*node_2.a)+(node_1155_if_leB*node_1157),node_1157,node_1155_if_leA*node_1155_if_leB) - 0.5);
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
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _maintex; uniform float4 _maintex_ST;
            uniform fixed _destory;
            uniform sampler2D _destroytex; uniform float4 _destroytex_ST;
            uniform float _destroyvalue;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float node_1155_if_leA = step(_destory,1.0);
                float node_1155_if_leB = step(1.0,_destory);
                float2 node_726 = i.uv0;
                float4 node_2 = tex2D(_maintex,TRANSFORM_TEX(node_726.rg, _maintex));
                float2 node_1260 = i.screenPos;
                float node_1157 = (node_2.a*pow(tex2D(_destroytex,TRANSFORM_TEX(node_1260.rg, _destroytex)).r,(1.0 - _destroyvalue))*_destroyvalue);
                clip(lerp((node_1155_if_leA*node_2.a)+(node_1155_if_leB*node_1157),node_1157,node_1155_if_leA*node_1155_if_leB) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
