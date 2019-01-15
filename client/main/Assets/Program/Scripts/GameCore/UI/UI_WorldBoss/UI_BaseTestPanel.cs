using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseTestPanel : BaseUI
{
	protected Text m_TopTittleText;
	protected Button m_BackBtn;
	protected Text m_BackText;
	protected Button m_LengendWarBtn;
	protected Text m_LengendWarBtnText;
	protected Button m_LimitTestBtn;
	protected Text m_LimitTestBtnText;
	protected Button m_NoneBtn;
	protected Text m_NoneBtnText;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TopTittleText = selfTransform.FindChild("TopPanel/TittleImage/TopTittleText").GetComponent<Text>();
		m_BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		m_BackText = selfTransform.FindChild("TopPanel/BackBtn/BackText").GetComponent<Text>();
		m_LengendWarBtn = selfTransform.FindChild("Layout/LengendWarBtn").GetComponent<Button>();
		m_LengendWarBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLengendWarBtn));
        m_LengendWarBtnText = selfTransform.FindChild("Layout/LengendWarBtn/LengendWarBtnText").GetComponent<Text>();
        m_LimitTestBtn = selfTransform.FindChild("Layout/LimitTestBtn").GetComponent<Button>();
		m_LimitTestBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLimitTestBtn));
        m_LimitTestBtnText = selfTransform.FindChild("Layout/LimitTestBtn/LimitTestBtnText").GetComponent<Text>();
        m_NoneBtn = selfTransform.FindChild("Layout/NoneBtn").GetComponent<Button>();
		m_NoneBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickNoneBtn));
        m_NoneBtnText = selfTransform.FindChild("Layout/NoneBtn/NoneBtnText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickLengendWarBtn()
	{
	}

	protected virtual void OnClickLimitTestBtn()
	{
	}

	protected virtual void OnClickNoneBtn()
	{
	}

}
