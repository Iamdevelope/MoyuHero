using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using GNET;

namespace DreamFaction.UI
{
    /// <summary>
    /// GM命令输入框
    /// </summary>
    public class UI_GM_InputTxt : BaseUI
    {
        public static string UI_ResPath = "Public_Com/UI_GMInputTxt_0_1";
        private InputField inputTxt; // 输入框
        private Button submitBtn;// 提交按钮

        // ===================== 继承 ==================
        // 0：初始化数据
        public override void InitUIData()
        {
            base.InitUIData();
            inputTxt = GetComponent<InputField>();
            submitBtn = selfTransform.FindChild("Button").GetComponent<Button>();
            submitBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSubmit));
            inputTxt.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>(OnEndEdit));
        }
        // 文本框结束输入
        void OnEndEdit(string endstr)
        {
            //Debug.Log(endstr);
        }

        // 提交按钮
        void OnSubmit()
        {
            if(inputTxt.text != string.Empty)
            {
                // 把GM命令发送给服务器
                CSendCommand command = new CSendCommand();
                command.cmd = inputTxt.text;
                IOControler.GetInstance().SendProtocol(command);
            }
            // 重置UI状态，准备关闭UI
            UIState = UIStateEnum.PlayingExitAnimation;
        }

        // 1: 播放进场动画
        public override void OnPlayingEnterAnimation()
        {
            base.OnPlayingEnterAnimation();
            animation.Play("Enter_GM_InputTextAnim");
        }

         // 2：播放退出动画 默认UI状态是 PlayingEnterAnimation 
        public override void OnPlayingExitAnimation()
        {
            base.OnPlayingExitAnimation();
            animation.Play("Exit_GM_InputTextAnim");
            StartCoroutine(closeUI());
        }

        // 3: 关闭UI
        public override void OnReadyForClose()
        {
            base.OnReadyForClose();
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_GM_InputTxt.UI_ResPath);
        }

        // ============= 私有接口 ===========
        IEnumerator closeUI()
        {
            yield return new WaitForSeconds(1.0f);
            UIState = UIStateEnum.ReadyForClose;
        }

        // =================== 回调事件 ==================

    }
}
