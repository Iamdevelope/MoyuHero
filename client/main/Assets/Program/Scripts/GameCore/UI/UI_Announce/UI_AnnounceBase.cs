using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_AnnounceBase : BaseUI
{
	protected Text m_TitleText;
	protected Button m_CloseBtn;
	protected Text m_CloseBtnText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TitleText = selfTransform.FindChild("TitleObj/TitleText").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(OnClickCloseBtn);
		m_CloseBtnText = selfTransform.FindChild("CloseBtn/CloseBtnText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

}
