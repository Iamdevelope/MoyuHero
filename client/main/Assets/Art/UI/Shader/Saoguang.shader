Shader "DreamFaction/Effects/Flash" 
{  
Properties {   
        _image ("贴图", 2D) = "white" {}   
        _widthRatio ("宽度收缩", Range(0, 20)) = 1 //光线宽度系数，值越大效果越细  
        _angle ("旋转角度", Range(0, 180)) = 45
        _percent ("滑动进度", Range(-1, 2)) = -1 

    } 
       
    SubShader {      
        Tags {"Queue" = "Transparent"}        
        ZWrite Off      
        // 源rgb * a + 背景rgb * (1-源a)    
        Blend SrcAlpha OneMinusSrcAlpha  
  
        Pass {       
            CGPROGRAM       
            #pragma vertex vert       
            #pragma fragment frag       
            #include "UnityCG.cginc"  
            sampler2D _image;   
            float _widthRatio;
            float _angle;  
            float _percent;   
           
            struct v2f {       
                float4 pos:SV_POSITION;       
                float2 uv : TEXCOORD0;      
            };     
     
            v2f vert(appdata_base v) {     
                v2f o;     
                o.pos = mul (UNITY_MATRIX_MVP, v.vertex);     
                o.uv = v.texcoord.xy;     
                return o;     
            }     
     
            fixed4 frag(v2f i) : COLOR0 {    
                // 对图片进行采样  
                fixed4 k = tex2D(_image, i.uv);  

                // 正常情况UV值变大，当前点位置颜色会增强  
                // 下面用了lerp(1,0,f)，那么这里UV增大，当前点位置颜色会减弱  
                fixed2 blink_uv = (i.uv + fixed2(_percent,_percent)) * _widthRatio - _widthRatio;  

                // 旋转矩阵，旋转_angle度，可以自己修改方向  
                fixed2x2 rotMat = fixed2x2(cos(_angle),sin(_angle),-sin(_angle),cos(_angle));   //fixed2x2(0.866,0.5,0.5,0.866);  30度

                // 乘以旋转矩阵  
                blink_uv = mul(rotMat, blink_uv);  
                // 当y越靠近原点时，RGB值越大  
                // (1-f) * a + b * f;  
                // 起点f=0，颜色最亮；起点f=1，颜色最暗  
                fixed rgba = lerp(1,0,abs(blink_uv.y)); 
                if(k.a > 0.05)  
                {  
                    // 叠加RGB值，可以自己修改颜色  
                    k +=  fixed4(saturate(rgba), saturate(rgba), saturate(rgba), saturate(rgba));  
                }  
                return k;  
            }    
            ENDCG    
        }  
    }  
    FallBack Off     
} 