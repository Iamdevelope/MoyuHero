using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseSpecialTips : BaseUI
{
	protected Button m_OKBtn;
	protected Text m_OKBtnText;
	protected Button m_CancelBtn;
	protected Text m_CancelBtnText;
	protected Text m_TittleText;
	protected Text m_ContentText;
	protected Text m_TipText;


	public override void InitUIData()
	{
		base.InitUIData();
		m_OKBtn = selfTransform.FindChild("OKBtn").GetComponent<Button>();
		m_OKBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickOKBtn));
		m_OKBtnText = selfTransform.FindChild("OKBtn/OKBtnText").GetComponent<Text>();
		m_CancelBtn = selfTransform.FindChild("CancelBtn").GetComponent<Button>();
		m_CancelBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCancelBtn));
		m_CancelBtnText = selfTransform.FindChild("CancelBtn/CancelBtnText").GetComponent<Text>();
		m_TittleText = selfTransform.FindChild("TittleText").GetComponent<Text>();
		m_ContentText = selfTransform.FindChild("ContentText").GetComponent<Text>();
        m_TipText = selfTransform.FindChild("Tip/Bottom/TipText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickOKBtn()
	{
	}

	protected virtual void OnClickCancelBtn()
	{
        
	}

}
