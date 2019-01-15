using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
/// <summary>
/// 已修改 （请勿生成）
/// </summary>
public class UI_HeroCloneManagerBase : BaseUI
{
	protected Button m_backBtn;
	protected Text m_Text;
	protected Text m_ConNUmTxt;
    protected Transform M_CapPos;
	public override void InitUIData()
	{
		base.InitUIData();
		m_backBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
		m_backBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickbackBtn));
        m_Text = selfTransform.FindChild("TopPanel/BtnGroup/Btn_Name1/Selected/Text").GetComponent<Text>();
		m_ConNUmTxt = selfTransform.FindChild("TopPanel/ConImg/ConNUmTxt").GetComponent<Text>();
        M_CapPos = selfTransform.FindChild("pos");
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickbackBtn()
	{
	}

}
