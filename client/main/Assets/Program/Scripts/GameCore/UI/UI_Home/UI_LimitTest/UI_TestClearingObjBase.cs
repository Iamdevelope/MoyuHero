using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_TestClearingObjBase : BaseUI
{
	protected Text m_TiliteTxt;
	protected Button m_CloseBtn;
	protected Text m_Text;
	protected RichText m_FightGetTxt;
	protected Text m_PowerTxt;
	protected Text m_DesTxt;
	protected Text m_TestRewaedTxt;


	public override void InitUIData()
	{
		base.InitUIData();
		m_TiliteTxt = selfTransform.FindChild("ClearingWindow/Image/TiliteTxt").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("ClearingWindow/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_Text = selfTransform.FindChild("ClearingWindow/CloseBtn/Text").GetComponent<Text>();
		m_FightGetTxt = selfTransform.FindChild("ClearingWindow/FightGetTxt").GetComponent<RichText>();
		m_PowerTxt = selfTransform.FindChild("ClearingWindow/PowerTxt").GetComponent<Text>();
		m_DesTxt = selfTransform.FindChild("ClearingWindow/DesTxt").GetComponent<Text>();
		m_TestRewaedTxt = selfTransform.FindChild("ClearingWindow/TestRewaedTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

}
