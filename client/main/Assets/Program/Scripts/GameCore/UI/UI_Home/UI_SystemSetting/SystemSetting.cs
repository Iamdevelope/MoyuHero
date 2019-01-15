using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DG.Tweening;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    /// <summary>
    /// 系统设置界面，继承自BaseUI
    /// </summary>
    public class SystemSetting : BaseUI
    {
        //public static SystemSetting _inst;
        public static string UI_ResPath = "SystemSetting/UI_SystemSetting_2_1";

        private Button m_backBtn;
        private Button m_SettingBtn;
        private Button m_OptionsBtn;
        private GameObject m_Setting;
        private GameObject m_Options;
        private OutLineGlow m_SettingOutline;
        private OutLineGlow m_OptionsOutline;

        private Text m_SystemSetting;
        private Text m_SystemOption;

        public override void InitUIData()
        {
            base.InitUIData();
            //_inst = this;
            ObjectSelf.GetInstance().GetSettingData().GetLocalSettingData();//获取Config表的配置信息

            m_backBtn = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
            m_backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));

            m_Setting = selfTransform.FindChild("Setting").gameObject;
            m_Options = selfTransform.FindChild("Options").gameObject;

            m_SettingBtn = selfTransform.FindChild("UI_BG_Top/UI_Btn_Setting").GetComponent<Button>();
            m_SettingBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSettingBtn));

            m_OptionsBtn = selfTransform.FindChild("UI_BG_Top/UI_Btn_Options").GetComponent<Button>();
            m_OptionsBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickOptionsBtn));

            m_SettingOutline = selfTransform.FindChild("UI_BG_Top/UI_Btn_Setting/Text").GetComponent<OutLineGlow>();
            m_OptionsOutline = selfTransform.FindChild("UI_BG_Top/UI_Btn_Options/Text").GetComponent<OutLineGlow>();

            m_SystemSetting = selfTransform.FindChild("UI_BG_Top/UI_Btn_Setting/Text").GetComponent<Text>();
            m_SystemOption = selfTransform.FindChild("UI_BG_Top/UI_Btn_Options/Text").GetComponent<Text>();
        }

            // 2：初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();
            m_SystemSetting.text = GameUtils.getString("System_setting_content1");
            m_SystemOption.text = GameUtils.getString("System_setting_content2");
        }
        private void OnClickSettingBtn()
        {
            m_Setting.SetActive(true);
            m_Options.SetActive(false);
            m_SettingOutline.enabled = true;
            m_OptionsOutline.enabled = false;
            m_SettingBtn.gameObject.GetComponent<Image>().enabled = true;
            m_OptionsBtn.gameObject.GetComponent<Image>().enabled = false;
            m_SystemSetting.color = new Color(1f,1f,1f);
            m_SystemOption.color = new Color(0.72f, 0.72f, 0.74f); 
        }

        private void OnClickOptionsBtn()
        {
            m_Setting.SetActive(false);
            m_Options.SetActive(true);
            m_SettingOutline.enabled = false;
            m_OptionsOutline.enabled = true;
            m_SettingBtn.gameObject.GetComponent<Image>().enabled = false;
            m_OptionsBtn.gameObject.GetComponent<Image>().enabled = true;
            m_SystemSetting.color = new Color(0.72f, 0.72f, 0.74f);
            m_SystemOption.color = new Color(1f, 1f, 1f);
        }

        private void OnClickBackBtn()
        {
            UI_HomeControler.Inst.ReMoveUI(gameObject);
        }
    }
}
