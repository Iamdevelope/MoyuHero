using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using UnityEngine.Events;

public class UI_GuideBase : BaseUI
{
	protected Image m_Mask;
	protected Image m_Target;
	protected Image m_BgUp;
	protected Image m_BgLower;
	protected Image m_BgLeft;
	protected Image m_BgRight;
	protected Image m_Transparent;
	protected Image m_Girl;
	protected Image m_TipsImage;
	protected Text m_TipsText;
	public Button m_LeaveBtn;
	protected Image m_LeaveBtn_1;
	protected Text m_LeaveText;
    protected Text m_ContuineText;
    protected Button m_ContuineBtn;
	protected Button m_SkipBtn;
	protected Image m_SkipBtn_1;
	protected Text m_Text;
	protected Image m_Finger;

	public override void InitUIData()
	{
		base.InitUIData();
		m_Mask = selfTransform.FindChild("Mask").GetComponent<Image>();
		m_Target = selfTransform.FindChild("Mask/Target").GetComponent<Image>();
		m_BgUp = selfTransform.FindChild("Mask/BgUp").GetComponent<Image>();
		m_BgLower = selfTransform.FindChild("Mask/BgLower").GetComponent<Image>();
		m_BgLeft = selfTransform.FindChild("Mask/BgLeft").GetComponent<Image>();
		m_BgRight = selfTransform.FindChild("Mask/BgRight").GetComponent<Image>();
		m_Transparent = selfTransform.FindChild("Transparent").GetComponent<Image>();
		m_Girl = selfTransform.FindChild("Girl").GetComponent<Image>();
		m_TipsImage = selfTransform.FindChild("TipsImage").GetComponent<Image>();
		m_TipsText = selfTransform.FindChild("TipsImage/TipsText").GetComponent<Text>();
		m_LeaveBtn = selfTransform.FindChild("TipsImage/BtnGuide/LeaveBtn").GetComponent<Button>();
		m_LeaveBtn_1 = selfTransform.FindChild("TipsImage/BtnGuide/LeaveBtn").GetComponent<Image>();
        m_LeaveText = selfTransform.FindChild("TipsImage/BtnGuide/LeaveBtn/LeaveText").GetComponent<Text>();
        m_ContuineText = selfTransform.FindChild("TipsImage/BtnGuide/LaterBtn/LaterText").GetComponent<Text>();
        m_ContuineBtn = selfTransform.FindChild("TipsImage").GetComponent<Button>();
        m_ContuineBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickContuineBtn));
		m_SkipBtn = selfTransform.FindChild("SkipBtn").GetComponent<Button>();
		m_SkipBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkipBtn));
		m_SkipBtn_1 = selfTransform.FindChild("SkipBtn").GetComponent<Image>();
		m_Text = selfTransform.FindChild("SkipBtn/Text").GetComponent<Text>();
		m_Finger = selfTransform.FindChild("Finger").GetComponent<Image>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickLeaveBtn()
	{
	}

    protected virtual void OnClickContuineBtn()
	{

	}

	protected virtual void OnClickSkipBtn()
	{
	}

}
