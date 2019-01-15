using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;

namespace DreamFaction.UI
{
    /// <summary>
    /// 走马灯界面，继承自BaseUI
    /// </summary>
    public class UI_ZouMaDeng : BaseUI
    {
        public static string UI_ResPath = "UI_Home/UI_ZouMaDeng_1_0";
        private Text msgTxt; // 
        private Animation anim;

        // ===================== 继承 ==================
        // 1: 初始化UI数据操作
        public override void InitUIData()
        {
            base.InitUIData();
            msgTxt = selfTransform.Find("Mask/Text").GetComponent<Text>();
            anim = msgTxt.gameObject.GetComponent<Animation>();
            //AnimationEvent animEvent = new AnimationEvent();
            //animEvent.functionName = "OnPlayAnimOver";
            //animEvent.time = anim.clip.length;
            //anim.clip.AddEvent(animEvent);
        }

        // 2：初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();
        }

        public void OnPlayAnimOver()
        {
            //Debug.Log("OnPlayAnimOver ...................");
        }

    }
}
