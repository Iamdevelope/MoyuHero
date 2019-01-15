using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_MedalItemBase : BaseUI
{
	protected Text m_YetFetchTxt;
	protected Text m_ReardNumTxt;
	protected Text m_CurCountTxt;
	protected Text m_MaxCountTXt;
	protected Button m_FetchBtn;
	protected Text m_Text;

	public override void InitUIData()
	{
		base.InitUIData();
		m_YetFetchTxt = selfTransform.FindChild("YetFetchObj/YetFetchTxt").GetComponent<Text>();
		m_ReardNumTxt = selfTransform.FindChild("ReardNumTxt").GetComponent<Text>();
		m_CurCountTxt = selfTransform.FindChild("CurCountTxt").GetComponent<Text>();
		m_MaxCountTXt = selfTransform.FindChild("MaxCountTXt").GetComponent<Text>();
		m_FetchBtn = selfTransform.FindChild("FetchBtn").GetComponent<Button>();
		m_FetchBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickFetchBtn));
		m_Text = selfTransform.FindChild("FetchBtn/Text").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickFetchBtn()
	{
	}

}
