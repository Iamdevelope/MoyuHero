using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_Loading :BaseUI
    {
        public static UI_Loading Inst;
        public static string UI_ResPath = "UI_Loding/UI_Loading_3_0";  
//         private Image BackImg; // 背景图
//         private Image FillImage;// 填充用的图
        private Slider m_LoadSlider = null;

        // ===================== 继承 ==================
        public override void InitUIData()
        {
            base.InitUIData();
            Inst = this;
//             BackImg = selfTransform.FindChild("BackImg").GetComponent<Image>();
//             FillImage = selfTransform.FindChild("Fill").GetComponent<Image>();
//             FillImage.fillAmount = 0;
//             if(Random.Range(0,100) %2 == 0)
//                 BackImg.sprite = Resources.Load<Sprite>(common.defaultPath + "LoadingImg_0");
//             else
//                 BackImg.sprite = Resources.Load<Sprite>(common.defaultPath + "LoadingImg_0");
                //BackImg.sprite = Resources.Load<Sprite>("UI/Sprites/LoadingImg_1");

            m_LoadSlider = selfTransform.FindChild("LoadingSlider").GetComponent<Slider>();
        }

        void OnDestroy()
        {
            Inst = null;
        }
        

        // ==================== 公共接口 ==============
        // 设置当前加载进度
        public void SetValue(float value)
        {
            //FillImage.fillAmount = value;
            m_LoadSlider.value = value;
        }



    }
}
