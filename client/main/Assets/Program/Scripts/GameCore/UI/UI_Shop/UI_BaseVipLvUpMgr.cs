using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseVipLvUpMgr : BaseUI
{
	protected Text m_OriginalTipsText;
	protected Button m_CloseButton;
	protected Text m_CloseButtonText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_OriginalTipsText = selfTransform.FindChild("OriginalTipsPanel/OriginalTips/TipsImage/OriginalTipsText").GetComponent<Text>();
		m_CloseButton = selfTransform.FindChild("CloseButton").GetComponent<Button>();
		m_CloseButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseButton));
		m_CloseButtonText = selfTransform.FindChild("CloseButton/CloseButtonText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseButton()
	{
	}

}
