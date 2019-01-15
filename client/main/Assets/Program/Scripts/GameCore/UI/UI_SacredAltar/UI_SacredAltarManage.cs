using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_SacredAltarManage : BaseUI
{
	protected Button m_BackBtn;
	protected Text m_Name;
	protected Text m_Des;
	protected Text m_SacredAltarNum;
	protected Button m_SacredAltarBtn;
	protected Text m_SacredAltarText;
	protected Text m_Text;
    protected Transform MsgBoxGroup;
    protected Transform M_CapPos;
	public override void InitUIData()
	{
		base.InitUIData();
		m_BackBtn = selfTransform.FindChild("UI_Top/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		m_Name = selfTransform.FindChild("UI_Top/Name").GetComponent<Text>();
		m_Des = selfTransform.FindChild("UI_Main/Des").GetComponent<Text>();
		m_SacredAltarNum = selfTransform.FindChild("UI_Main/SacredAltarNum").GetComponent<Text>();
		m_SacredAltarBtn = selfTransform.FindChild("UI_Main/SacredAltarBtn").GetComponent<Button>();
		m_SacredAltarBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSacredAltarBtn));
		m_SacredAltarText = selfTransform.FindChild("UI_Main/SacredAltarBtn/SacredAltarText").GetComponent<Text>();
        m_Text = selfTransform.FindChild("UI_Main/HintObj/Bottom/Text").GetComponent<Text>();
        MsgBoxGroup = selfTransform.FindChild("MsgBoxGroup");
        M_CapPos = selfTransform.FindChild("pos");
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickSacredAltarBtn()
	{
	}

}
