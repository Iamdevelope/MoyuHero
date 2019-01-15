using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
/// <summary>
/// 以手动更改过
/// </summary>
public class UI_LookFormBase : BaseUI
{
	protected Button m_CloseBtn;
	protected Text m_TiliteText;
	protected Button m_Button1;
	protected Button m_Button2;
	protected Button m_Button3;
	protected Button m_Button4;
	protected Button m_Button5;
    protected Button m_Button01;
    protected Button m_Button02;
    protected Button m_Button03;
    protected Button m_Button04;
    protected Button m_Button05;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("PlayerInfoItem/CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_TiliteText = selfTransform.FindChild("PlayerInfoItem/Image/TiliteText").GetComponent<Text>();
		m_Button1 = selfTransform.FindChild("Team1Buttons/Button1").GetComponent<Button>();
		m_Button1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton1));
		m_Button2 = selfTransform.FindChild("Team1Buttons/Button2").GetComponent<Button>();
		m_Button2.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton2));
		m_Button3 = selfTransform.FindChild("Team1Buttons/Button3").GetComponent<Button>();
		m_Button3.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton3));
		m_Button4 = selfTransform.FindChild("Team1Buttons/Button4").GetComponent<Button>();
		m_Button4.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton4));
		m_Button5 = selfTransform.FindChild("Team1Buttons/Button5").GetComponent<Button>();
		m_Button5.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton5));
        m_Button01 = selfTransform.FindChild("Team2Buttons/Button1").GetComponent<Button>();
        m_Button01.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton01));
        m_Button02 = selfTransform.FindChild("Team2Buttons/Button2").GetComponent<Button>();
        m_Button02.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton02));
        m_Button03 = selfTransform.FindChild("Team2Buttons/Button3").GetComponent<Button>();
        m_Button03.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton03));
        m_Button04 = selfTransform.FindChild("Team2Buttons/Button4").GetComponent<Button>();
        m_Button04.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton04));
        m_Button05 = selfTransform.FindChild("Team2Buttons/Button5").GetComponent<Button>();
        m_Button05.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickButton05));

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickButton1()
	{
	}

	protected virtual void OnClickButton2()
	{
	}

	protected virtual void OnClickButton3()
	{
	}

	protected virtual void OnClickButton4()
	{
	}

	protected virtual void OnClickButton5()
	{
	}

    protected virtual void OnClickButton01()
    {
    }

    protected virtual void OnClickButton02()
    {
    }

    protected virtual void OnClickButton03()
    {
    }

    protected virtual void OnClickButton04()
    {
    }

    protected virtual void OnClickButton05()
    {
    }

}
