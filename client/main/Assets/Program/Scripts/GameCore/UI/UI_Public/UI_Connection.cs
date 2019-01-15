using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using UnityEngine.EventSystems;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_Connection : BaseUI
    {
       
        public static string UI_ResPath = "Public_Com/UI_Connection_0_2";
        private Image aroundImag; // 转圈的图片
        private Text infoMsg_txt; // 提示信息文本！ 

        // ===================== 继承 ==================
        // 1: 初始化UI数据操作
        public override void InitUIData()
        {
            base.InitUIData();
        
            aroundImag = selfTransform.FindChild("Image").GetComponent<Image>();
            infoMsg_txt = selfTransform.FindChild("Text").GetComponent<Text>();
            infoMsg_txt.text = GameUtils.getString("loading");

            // 屏蔽除了canvas0层之外图层的UI点击事件！
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_UnBlockCanvasRaycasts, BaseUIControler.UICanvasFlag.Canvas0);
        }

       // 2: 界面关闭后显示UICanvas
       void OnDestroy()
       {
            //CancelInvoke();

            // 开启除了canvas0层之外图层的UI点击事件！
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_BlockCanvasRaycasts, BaseUIControler.UICanvasFlag.Canvas0);
        }

        // 3：更新UI显示
        public override void UpdateUIView()
        {
            base.UpdateUIView();

            switch(UIState)
            {
                case UIStateEnum.PlayingEnterAnimation:
                    //aroundImag.transform.eulerAngles = new Vector3 ( aroundImag.transform.eulerAngles.x, aroundImag.transform.eulerAngles.y, aroundImag.transform.eulerAngles.z - 1.5f );
                    break;
            }
        }

        // =================== 公共接口 ==========================
        public void SetInfoMsg(string msg)
        {
            infoMsg_txt.text = msg;
        }
    }
}