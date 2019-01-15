using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameCore;
using DreamFaction.Utils;
using Platform;
using DreamFaction.GameEventSystem;

namespace DreamFaction.UI  
{ 
    public class UI_RegistUser : BaseUI
    {
        public static string UI_ResPath = "UI_Login/UI_RegistUser_2_3";
        private Button RegistBtn; //注册按钮
        private Button BackBtn; // 返回按钮
        private InputField userName_InTxt; // 用户名输入框
        private InputField userPass_InTxt; // 密码输入框
        private InputField userRePass_InTxt; // 重新输入的密码输入框
        public string m_account = "";
        public string m_password = "";

        // ===================== 继承 ==================
        public override void InitUIData()
        {
            base.InitUIData();
            RegistBtn = selfTransform.FindChild("RegistBtn").GetComponent<Button>();
            BackBtn = selfTransform.FindChild("BackBtn").GetComponent<Button>();
            RegistBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRegistBtn));
            BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnBackBtn));
            userName_InTxt = selfTransform.FindChild("UserName/InputField").GetComponent<InputField>();
            userPass_InTxt = selfTransform.FindChild("PassWord/InputField").GetComponent<InputField>();
            userRePass_InTxt = selfTransform.FindChild("RePassWord/InputField").GetComponent<InputField>();
            userName_InTxt.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(OnUserNameChanged));
        }


        // ===================== 按钮回调 =================
        // 用户名文本框发生变化后的回调
        public void OnUserNameChanged(string username)
        {
            
        }
        
        // 注册按钮
        private void OnClickRegistBtn()
        {
           
        }
        // 返回按钮
        private void OnBackBtn()
        {
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_CloseUI, UI_RegistUser.UI_ResPath);
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, UI_SelectLoginServer.UI_ResPath);
        }

    }
}
