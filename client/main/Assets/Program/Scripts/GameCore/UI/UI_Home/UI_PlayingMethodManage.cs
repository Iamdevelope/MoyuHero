using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_PlayingMethodManage : BaseUI
{
	protected Button m_BackBtn;
	protected Text m_Name;
	protected Button m_GetPowerBtn;
	protected Text m_GetPowerText;
	protected Button m_SacredAltarBtn;
	protected Text m_SacredAltarText;
    protected Button m_ExplorationBtn;
	protected Text m_ExplorationText;
    protected Transform M_CapPos;

	public override void InitUIData()
	{
		base.InitUIData();
        m_BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
        m_Name = selfTransform.FindChild("TopPanel/BtnGroup/Btn_Name1/Selected/Text").GetComponent<Text>();
		m_GetPowerBtn = selfTransform.FindChild("UI_Main/GetPowerBtn").GetComponent<Button>();
		m_GetPowerBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGetPowerBtn));
		m_GetPowerText = selfTransform.FindChild("UI_Main/GetPowerBtn/GetPowerText").GetComponent<Text>();
		m_SacredAltarBtn = selfTransform.FindChild("UI_Main/SacredAltarBtn").GetComponent<Button>();
		m_SacredAltarBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSacredAltarBtn));
		m_SacredAltarText = selfTransform.FindChild("UI_Main/SacredAltarBtn/SacredAltarText").GetComponent<Text>();
        m_ExplorationBtn = selfTransform.FindChild("UI_Main/ExplorationBtn").GetComponent<Button>();
        m_ExplorationBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickExplorationBtn));
		m_ExplorationText = selfTransform.FindChild("UI_Main/ExplorationBtn/ExplorationText").GetComponent<Text>();
        M_CapPos = selfTransform.FindChild("pos");
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickGetPowerBtn()
	{
	}

	protected virtual void OnClickSacredAltarBtn()
	{
	}

    protected virtual void OnClickExplorationBtn()
    { 
    }
}
