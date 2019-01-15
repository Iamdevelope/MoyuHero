using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_GainHeroBase : BaseUI
{
	protected Text m_UI_Btn_Rune;
	protected Button m_UI_Btn_Back;
	protected Text m_GainTipsText;
	protected Text m_NumTipsText;
	protected Button m_ShipBtn;
	protected Text m_Text;

	public override void InitUIData()
	{
		base.InitUIData();
		m_UI_Btn_Rune = selfTransform.FindChild("UI_BG_Top/UI_Btn_Rune").GetComponent<Text>();
		m_UI_Btn_Back = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
		m_UI_Btn_Back.onClick.AddListener(OnClickUI_Btn_Back);
		m_GainTipsText = selfTransform.FindChild("UI_Center/GainTipsText").GetComponent<Text>();
		m_NumTipsText = selfTransform.FindChild("UI_Center/NumTipsText").GetComponent<Text>();
		m_ShipBtn = selfTransform.FindChild("ShipBtn").GetComponent<Button>();
		m_ShipBtn.onClick.AddListener(OnClickShipBtn);
		m_Text = selfTransform.FindChild("ShipBtn/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickUI_Btn_Back()
	{
	}

	protected virtual void OnClickShipBtn()
	{
	}

    void OnDestroy()
    {
        Destroy(m_UI_Btn_Rune);
        Destroy(m_UI_Btn_Back);
        Destroy(m_GainTipsText);
        Destroy(m_NumTipsText);
        Destroy(m_ShipBtn);
        Destroy(m_Text);
    }
}
