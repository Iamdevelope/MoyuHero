using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_GetPowerManage : BaseUI
{
	protected Button m_BackBtn;
	protected Text m_Name;
	protected Text m_TimeText;
	protected Text m_DesText;
	protected Button m_GetPowerBtn;
	protected Text m_GetPowerText;
	protected Text m_PowerText;
	protected Text m_NoontimeDes;
	protected Text m_NoontimeDesText;
	protected Text m_NightDes;
	protected Text m_NightDesText;
    protected Transform MsgBoxGroup;
    protected Transform M_CapPos;
	public override void InitUIData()
	{
		base.InitUIData();
		m_BackBtn = selfTransform.FindChild("UI_Top/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		m_Name = selfTransform.FindChild("UI_Top/Name").GetComponent<Text>();
		m_TimeText = selfTransform.FindChild("Main/Time/TimeText").GetComponent<Text>();
		m_DesText = selfTransform.FindChild("Main/DesText").GetComponent<Text>();
		m_GetPowerBtn = selfTransform.FindChild("Main/GetPowerBtn").GetComponent<Button>();
		m_GetPowerBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickGetPowerBtn));
		m_GetPowerText = selfTransform.FindChild("Main/GetPowerBtn/GetPowerText").GetComponent<Text>();
		m_NoontimeDes = selfTransform.FindChild("Main/TimeDes/NoontimeDes").GetComponent<Text>();
		m_NoontimeDesText = selfTransform.FindChild("Main/TimeDes/NoontimeDesText").GetComponent<Text>();
		m_NightDes = selfTransform.FindChild("Main/TimeDes/NightDes").GetComponent<Text>();
		m_NightDesText = selfTransform.FindChild("Main/TimeDes/NightDesText").GetComponent<Text>();
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

	protected virtual void OnClickGetPowerBtn()
	{
	}

}
