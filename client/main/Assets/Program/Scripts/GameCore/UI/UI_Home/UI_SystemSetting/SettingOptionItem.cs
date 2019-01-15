using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DG.Tweening;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using GNET;
using System;
using DreamFaction.UI;
using DreamFaction.GameCore;

public class SettingOptionItem : BaseUI
{
    public static SettingOptionItem instance;
	protected Button m_AccountBindingButton;
    protected Text m_AccountBindingText;
	protected Button m_SpreeCodeButton;
    protected Text m_SpreeCodeText;
	protected Button m_GameRaidersButton;
    protected Text m_GameRaidersText;
	protected Button m_GameFrumButton;
    protected Text m_GameFrumText;
	protected Button m_UserAgreementButton;
    protected Text m_UserAgreementText;
	protected Button m_FeedbackButton;
    protected Text m_FeedbackText;
	protected Button m_TeamListButton;
    protected Text m_TeamListText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_AccountBindingButton = selfTransform.FindChild("AccountBindingButton").GetComponent<Button>();
		m_AccountBindingButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAccountBindingButton));
        m_AccountBindingText = selfTransform.FindChild("AccountBindingButton/Text").GetComponent<Text>();
		m_SpreeCodeButton = selfTransform.FindChild("SpreeCodeButton").GetComponent<Button>();
		m_SpreeCodeButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSpreeCodeButton));
        m_SpreeCodeText = selfTransform.FindChild("SpreeCodeButton/Text").GetComponent<Text>();
		m_GameRaidersButton = selfTransform.FindChild("GameRaidersButton").GetComponent<Button>();
		m_GameRaidersButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGameRaidersButton));
        m_GameRaidersText = selfTransform.FindChild("GameRaidersButton/Text").GetComponent<Text>();
		m_GameFrumButton = selfTransform.FindChild("GameFrumButton").GetComponent<Button>();
		m_GameFrumButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGameFrumButton));
        m_GameFrumText = selfTransform.FindChild("GameFrumButton/Text").GetComponent<Text>();
		m_UserAgreementButton = selfTransform.FindChild("UserAgreementButton").GetComponent<Button>();
		m_UserAgreementButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickUserAgreementButton));
        m_UserAgreementText = selfTransform.FindChild("UserAgreementButton/Text").GetComponent<Text>();
		m_FeedbackButton = selfTransform.FindChild("FeedbackButton").GetComponent<Button>();
		m_FeedbackButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickFeedbackButton));
        m_FeedbackText = selfTransform.FindChild("FeedbackButton/Text").GetComponent<Text>();
		m_TeamListButton = selfTransform.FindChild("TeamListButton").GetComponent<Button>();
		m_TeamListButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickTeamListButton));
        m_TeamListText = selfTransform.FindChild("TeamListButton/Text").GetComponent<Text>();

	}

	public override void InitUIView() 
	{
		base.InitUIView();
        instance = this;
        //if (ConfigsManager.Inst.GetClientConfig(ClientConfigs.Fromaccount) == GameUtils.getString("login_content4")) //"游客"
        {
            m_AccountBindingText.text = GameUtils.getString("System_setting_content1"); //"账号绑定";
        }
        //else
        //{
        //    m_AccountBindingText.text = GameUtils.getString("System_setting_button9");
        //}

        m_SpreeCodeText.text = GameUtils.getString("System_setting_button2");
        m_GameRaidersText.text = GameUtils.getString("System_setting_button3"); 
        m_GameFrumText.text = GameUtils.getString("System_setting_button4"); 
        m_UserAgreementText.text = GameUtils.getString("System_setting_button5"); 
        m_FeedbackText.text = GameUtils.getString("System_setting_button6");
        m_TeamListText.text = GameUtils.getString("System_setting_button7"); 

	}

	protected virtual void OnClickAccountBindingButton()
    {
        //if (ConfigsManager.Inst.GetClientConfig(ClientConfigs.Fromaccount).ToString() == GameUtils.getString("login_content4")) //"游客"
        //{
        //    //弹出账号绑定窗口
        //    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_OpenUI, AccountBinding.UI_ResPath);
        //}
        //else
        //{
        //    //登出账号的提示框
        //    UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

        //    if (box == null)
        //    {
        //        DreamFaction.LogSystem.LogManager.LogError("提示窗is null");
        //        return;
        //    }

        //    box.SetDescription_text(GameUtils.getString("System_setting_content15").Replace("{0}", "<color=#1E90FF>" +  ConfigsManager.Inst.GetClientConfig(ClientConfigs.Account) + "</color>"));
        //    box.SetIsNeedDescription(false);
        //    box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
        //    box.SetLeftClick(OnSignOutBtnClick);

        //    box.SetRightBtn_text(GameUtils.getString("common_button_close"));
        //}
	}
    /// <summary>
    /// 打开礼包兑换码窗口
    /// </summary>
	protected virtual void OnClickSpreeCodeButton()
	{
        UI_HomeControler.Inst.AddUI(SpreeCode.UI_ResPath);
	}

	protected virtual void OnClickGameRaidersButton()
	{
	}

	protected virtual void OnClickGameFrumButton()
	{
	}

	protected virtual void OnClickUserAgreementButton()
	{
	}

	protected virtual void OnClickFeedbackButton()
	{
	}

	protected virtual void OnClickTeamListButton()
	{
	}

    public void OnSignOutBtn()
    {
        // 注册成功显示确定提示框
        UI_GameTips ui = UI_HomeControler.Inst.AddUI(UI_GameTips.UI_ResPath).GetComponent<UI_GameTips>();
        ui.type = UI_GameTips.TipsType.AccountBindingOk;
    }
    public void OnSignOutBtnClick()
    {
        // 切换场景到Login
        if (!SceneManager.Inst.CurScene.Equals(SceneEntry.Login.ToString()))
        {
            GameObject.Destroy(MainGameControler.Inst.gameObject.GetComponent<ObjectSelf>());
            SceneManager.Inst.StartChangeScene(SceneEntry.Login.ToString());
        }
        else
        {
            HideUI();
        }

        IOControler.GetInstance().UnInit();
 
    }

    void HideUI()
    {
        gameObject.SetActive(false);
    }

    // 2: 准备删除UI
    public override void OnReadyForClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }


}
