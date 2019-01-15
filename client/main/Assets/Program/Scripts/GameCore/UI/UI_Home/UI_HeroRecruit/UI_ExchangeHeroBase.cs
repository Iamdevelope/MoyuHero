using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_ExchangeHeroBase : BaseUI
{
	protected Text m_TitleText;
	protected Text m_GainTipsText;
	protected Button m_RefreshBtn;
	protected Text m_RefreshText;
	protected Button m_ExchangeBtn;
	protected Text m_ExchangeText;
	protected Text m_FreeTipsText;
	protected Text m_BottomTipsText;
	protected Text m_HeroName;

	public override void InitUIData()
	{
		base.InitUIData();
		m_TitleText = selfTransform.FindChild("UI_BG_Top/TitleText").GetComponent<Text>();
		m_GainTipsText = selfTransform.FindChild("GainTipsText").GetComponent<Text>();
		m_RefreshBtn = selfTransform.FindChild("UI_Bottom/RefreshBtn").GetComponent<Button>();
		m_RefreshBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRefreshBtn));
		m_RefreshText = selfTransform.FindChild("UI_Bottom/RefreshBtn/RefreshText").GetComponent<Text>();
		m_ExchangeBtn = selfTransform.FindChild("UI_Bottom/ExchangeBtn").GetComponent<Button>();
		m_ExchangeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickExchangeBtn));
		m_ExchangeText = selfTransform.FindChild("UI_Bottom/ExchangeBtn/ExchangeText").GetComponent<Text>();
		m_FreeTipsText = selfTransform.FindChild("UI_Bottom/FreeTipsText").GetComponent<Text>();
		m_BottomTipsText = selfTransform.FindChild("UI_Bottom/BottomTipsText").GetComponent<Text>();
		m_HeroName = selfTransform.FindChild("HeroInfo/HeroName").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickRefreshBtn()
	{
	}

	protected virtual void OnClickExchangeBtn()
	{
	}

}
