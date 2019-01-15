using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_LivenessManage : BaseUI
{
	protected Button m_UI_Btn_Liveness;
	protected Button m_UI_Btn_Prop;
	protected Button m_UI_Btn_Back;
	protected Text m_NewDesText;
	protected Text m_NewNumText;
    protected RectTransform m_NewNumTextRectTF;
    protected RectTransform LivenessSliderTF;
    protected RectTransform LivenessSlider_2_TF;
    protected Slider m_slider;
    protected Slider m_slider_2;
    protected Text m_HuoYueDuText;
    protected Text m_QianDaoText;
    protected Transform M_CapPos;
	protected Button m_Box50;
	protected Text m_Box50Text;
	protected Button m_Box90;
	protected Text m_Box80Text;
	protected Button m_Box120;
	protected Text m_Box120Text;
	protected Button m_Box150;
	protected Text m_Box150Text;

	public override void InitUIData()
	{
		base.InitUIData();
		m_UI_Btn_Liveness = selfTransform.FindChild("UI_BG_Top/UI_Btn_Liveness").GetComponent<Button>();
		m_UI_Btn_Liveness.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickUI_Btn_Liveness));
		m_UI_Btn_Prop = selfTransform.FindChild("UI_BG_Top/UI_Btn_Prop").GetComponent<Button>();
		m_UI_Btn_Prop.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickUI_Btn_Prop));
		m_UI_Btn_Back = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
		m_UI_Btn_Back.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickUI_Btn_Back));
		m_NewDesText = selfTransform.FindChild("Liveness/LivenessSlider/Fill/NewDesText").GetComponent<Text>();
		m_NewNumText = selfTransform.FindChild("Liveness/LivenessSlider/Fill/NewDesText/NewNumText").GetComponent<Text>();
        m_NewNumTextRectTF = selfTransform.FindChild("Liveness/LivenessSlider/Fill/NewDesText/NewNumText").GetComponent<RectTransform>();
        LivenessSliderTF = selfTransform.FindChild("Liveness/LivenessSlider").GetComponent<RectTransform>();
        LivenessSlider_2_TF = selfTransform.FindChild("Liveness/LivenessSlider2").GetComponent<RectTransform>();
        m_slider = selfTransform.FindChild("Liveness/LivenessSlider").GetComponent<Slider>();
        m_slider_2 = selfTransform.FindChild("Liveness/LivenessSlider2").GetComponent<Slider>();
        m_HuoYueDuText = selfTransform.FindChild("UI_BG_Top/UI_Btn_Liveness/Text").GetComponent<Text>();
        m_QianDaoText = selfTransform.FindChild("UI_BG_Top/UI_Btn_Prop/Text").GetComponent<Text>();
        M_CapPos = selfTransform.FindChild("pos");
        //m_Box50 = selfTransform.FindChild("Liveness/BoxList/Box50").GetComponent<Button>();
        //m_Box50.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBox50));
        //m_Box50Text = selfTransform.FindChild("Liveness/BoxList/Box50/Box50Text").GetComponent<Text>();
        //m_Box90 = selfTransform.FindChild("Liveness/BoxList/Box90").GetComponent<Button>();
        //m_Box90.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBox90));
        //m_Box80Text = selfTransform.FindChild("Liveness/BoxList/Box90/Box80Text").GetComponent<Text>();
        //m_Box120 = selfTransform.FindChild("Liveness/BoxList/Box120").GetComponent<Button>();
        //m_Box120.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBox120));
        //m_Box120Text = selfTransform.FindChild("Liveness/BoxList/Box120/Box120Text").GetComponent<Text>();
        //m_Box150 = selfTransform.FindChild("Liveness/BoxList/Box150").GetComponent<Button>();
        //m_Box150.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBox150));
        //m_Box150Text = selfTransform.FindChild("Liveness/BoxList/Box150/Box150Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickUI_Btn_Liveness()
	{
	}

	protected virtual void OnClickUI_Btn_Prop()
	{
	}

	protected virtual void OnClickUI_Btn_Back()
	{
	}

	protected virtual void OnClickBox50()
	{
	}

	protected virtual void OnClickBox90()
	{
	}

	protected virtual void OnClickBox120()
	{
	}

	protected virtual void OnClickBox150()
	{
	}

}
