using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_HeroPreBase : BaseUI
{
	protected Text m_UI_Btn_Rune;
	protected Button m_UI_Btn_Back;

	public override void InitUIData()
	{
		base.InitUIData();
		m_UI_Btn_Rune = selfTransform.FindChild("UI_BG_Top/UI_Btn_Rune").GetComponent<Text>();
		m_UI_Btn_Back = selfTransform.FindChild("UI_BG_Top/UI_Btn_Back").GetComponent<Button>();
		m_UI_Btn_Back.onClick.AddListener(OnClickUI_Btn_Back);

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickUI_Btn_Back()
	{
	}

    void OnDestroy()
    {
        Destroy(m_UI_Btn_Rune);
        Destroy(m_UI_Btn_Back);
    }
}
