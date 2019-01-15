using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

/// <summary>
/// 已修改（请勿自动生成）
/// </summary>
public class UI_HandBookManagerBase : CustomUI
{
	protected Button m_BackBtn;
	protected Text m_Text;
	protected Text m_TiliteText;
	protected Text m_BadgeTxt;
	protected Text m_CurNumTxt1;
	protected Text m_MaxNumTxt1;
	protected Text m_CurNumTxt2;
	protected Text m_MaxNumTxt2;
	protected Text m_CurNumTxt3;
	protected Text m_MaxNumTxt3;
	protected Text m_CurNumTxt4;
	protected Text m_MaxNumTxt4;
	protected Button m_DeitiesBtn;
    protected Button m_PeopleBtn;
	protected Button m_DevdilBtn;
	protected Button m_ReardBtn;
	protected Text m_ReardTxt;
	protected Button m_PatentRuneBtn;
	protected Text m_PatentRuneTxt;
	protected Text m_PromptTxt1;
	protected Text m_PromptTxt2;

	public override void InitUIData()
	{
		base.InitUIData();
        m_BackBtn = selfTransform.FindChild("TopPanel/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
        m_Text = selfTransform.FindChild("TopPanel/BackBtn/Text").GetComponent<Text>();
        m_TiliteText = selfTransform.FindChild("TopPanel/BtnGroup/Btn_Name1/Selected/Text").GetComponent<Text>();
        m_BadgeTxt = selfTransform.FindChild("TopPanel/BadgeTxt").GetComponent<Text>();
        m_CurNumTxt1 = selfTransform.FindChild("TopPanel/Badge1/CurNumTxt1").GetComponent<Text>();
        m_MaxNumTxt1 = selfTransform.FindChild("TopPanel/Badge1/MaxNumTxt1").GetComponent<Text>();
        m_CurNumTxt2 = selfTransform.FindChild("TopPanel/Badge2/CurNumTxt2").GetComponent<Text>();
        m_MaxNumTxt2 = selfTransform.FindChild("TopPanel/Badge2/MaxNumTxt2").GetComponent<Text>();
        m_CurNumTxt3 = selfTransform.FindChild("TopPanel/Badge3/CurNumTxt3").GetComponent<Text>();
        m_MaxNumTxt3 = selfTransform.FindChild("TopPanel/Badge3/MaxNumTxt3").GetComponent<Text>();
        m_CurNumTxt4 = selfTransform.FindChild("TopPanel/Badge4/CurNumTxt4").GetComponent<Text>();
        m_MaxNumTxt4 = selfTransform.FindChild("TopPanel/Badge4/MaxNumTxt4").GetComponent<Text>();
		m_DeitiesBtn = selfTransform.FindChild("DeitiesBtn/Button").GetComponent<Button>();
		m_DeitiesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickDeitiesBtn));
        m_PeopleBtn = selfTransform.FindChild("PeopleBtn/Button").GetComponent<Button>();
		m_PeopleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPeopleBtn));
        m_DevdilBtn = selfTransform.FindChild("DevdilBtn/Button").GetComponent<Button>();
		m_DevdilBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickDevdilBtn));
		m_ReardBtn = selfTransform.FindChild("BottomPanel/ReardBtn").GetComponent<Button>();
		m_ReardBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReardBtn));
		m_ReardTxt = selfTransform.FindChild("BottomPanel/ReardBtn/ReardTxt").GetComponent<Text>();
		m_PatentRuneBtn = selfTransform.FindChild("BottomPanel/PatentRuneBtn").GetComponent<Button>();
		m_PatentRuneBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPatentRuneBtn));
		m_PatentRuneTxt = selfTransform.FindChild("BottomPanel/PatentRuneBtn/PatentRuneTxt").GetComponent<Text>();
		m_PromptTxt1 = selfTransform.FindChild("BottomPanel/PromptTxt1").GetComponent<Text>();
        m_PromptTxt2 = selfTransform.FindChild("BottomPanel/HintObj/Bottom/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickDeitiesBtn()
	{
	}

	protected virtual void OnClickPeopleBtn()
	{
	}

	protected virtual void OnClickDevdilBtn()
	{
	}

	protected virtual void OnClickReardBtn()
	{
	}

	protected virtual void OnClickPatentRuneBtn()
	{
	}

}
