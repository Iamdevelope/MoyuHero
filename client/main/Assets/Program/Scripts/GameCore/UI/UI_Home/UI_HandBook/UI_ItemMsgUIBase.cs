using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
/// <summary>
/// ÒÑÐÞ¸Ä
/// </summary>
public class UI_ItemMsgUIBase : BaseUI
{
	protected Text m_TiliteTxt;
	protected Text m_RuneName;
	protected Text m_PatentHeroTxt;
	protected Text m_DesTxt;
	protected Button m_CloseBtn;
	protected Text m_Text;
	protected Text m_BaseArttbueTxt;
	protected Text m_PhyAttackTxt;
	protected Text m_PhyAttackNumTxt;

	public override void InitUIData()
	{
		base.InitUIData();
        m_TiliteTxt = selfTransform.FindChild("TitlePanel/TitleObj/Text").GetComponent<Text>();
		m_RuneName = selfTransform.FindChild("RuneName").GetComponent<Text>();
		m_PatentHeroTxt = selfTransform.FindChild("PatentHeroTxt").GetComponent<Text>();
		m_DesTxt = selfTransform.FindChild("DesTxt").GetComponent<Text>();
		m_CloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_Text = selfTransform.FindChild("CloseBtn/Text").GetComponent<Text>();
		m_BaseArttbueTxt = selfTransform.FindChild("BaseArttbueTxt").GetComponent<Text>();
		m_PhyAttackTxt = selfTransform.FindChild("PhyAttackTxt").GetComponent<Text>();
		m_PhyAttackNumTxt = selfTransform.FindChild("PhyAttackNumTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

}
