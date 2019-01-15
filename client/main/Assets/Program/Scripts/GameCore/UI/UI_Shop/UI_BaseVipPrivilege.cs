using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseVipPrivilege : BaseUI
{
	protected Text m_OriginalTipsText;
	protected Text m_TopTittleText;
	protected Button m_CloseBtn;
	protected Text m_TopDescriptionText;
	protected Text m_VipExpText;
    protected Text m_CurrentExpText;
	protected Button m_PayButton;
	protected Text m_PayButtonText;
	protected Text m_RightPanelTittleText;
	protected Button m_TurnRightButton;
	protected Button m_TurnLeftButton;
	protected Text m_RightPanelBottomTipsText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_OriginalTipsText = selfTransform.FindChild("OriginalTipsPanel/OriginalTipsText").GetComponent<Text>();
		m_TopTittleText = selfTransform.FindChild("TopPanel/TittleImage/TopTittleText").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("TopPanel/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_TopDescriptionText = selfTransform.FindChild("LeftPanel/TopDescriptionBackground/TopDescriptionText").GetComponent<Text>();
		m_VipExpText = selfTransform.FindChild("LeftPanel/VipExpProgressBar/VipExpText/").GetComponent<Text>();
        m_CurrentExpText = selfTransform.FindChild("LeftPanel/VipExpProgressBar/VipExpText/CurrentExpText").GetComponent<Text>();
		m_PayButton = selfTransform.FindChild("LeftPanel/PayButton").GetComponent<Button>();
		m_PayButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPayButton));
		m_PayButtonText = selfTransform.FindChild("LeftPanel/PayButton/PayButtonText").GetComponent<Text>();
		m_RightPanelTittleText = selfTransform.FindChild("RightPanel/RightPanelTittleText").GetComponent<Text>();
		m_TurnRightButton = selfTransform.FindChild("RightPanel/RightPanelTittleText/TurnRightButton").GetComponent<Button>();
		m_TurnRightButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickTurnRightButton));
		m_TurnLeftButton = selfTransform.FindChild("RightPanel/RightPanelTittleText/TurnLeftButton").GetComponent<Button>();
		m_TurnLeftButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickTurnLeftButton));
        m_RightPanelBottomTipsText = selfTransform.FindChild("RightPanel/RightPanelBottomTips/Bottom/RightPanelBottomTipsText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickPayButton()
	{
	}

	protected virtual void OnClickTurnRightButton()
	{
	}

	protected virtual void OnClickTurnLeftButton()
	{
	}

}
