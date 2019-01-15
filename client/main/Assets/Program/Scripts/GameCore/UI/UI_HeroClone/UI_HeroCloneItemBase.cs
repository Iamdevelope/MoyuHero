using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_HeroCloneItemBase : CellItem
{
	protected Text m_HeroName;
	protected Button m_InjectBtn;
	protected Text m_ConTxt;
	protected Text m_EliteNum;
	protected Text m_OpenTjTilteTxt;
	protected Text m_OpenTjTxt;
    protected Button m_HeroIconBtn;

	public override void InitUIData()
	{
		base.InitUIData();
		m_HeroName = selfTransform.FindChild("HeroName").GetComponent<Text>();
		m_InjectBtn = selfTransform.FindChild("OpenStateObj/InjectBtn").GetComponent<Button>();
		m_InjectBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickInjectBtn));
		m_ConTxt = selfTransform.FindChild("OpenStateObj/ConTxt").GetComponent<Text>();
		m_EliteNum = selfTransform.FindChild("OpenStateObj/EliteNum").GetComponent<Text>();
		m_OpenTjTilteTxt = selfTransform.FindChild("NotOpenStateObj/OpenTjTilteTxt").GetComponent<Text>();
		m_OpenTjTxt = selfTransform.FindChild("NotOpenStateObj/OpenTjTxt").GetComponent<Text>();
        m_HeroIconBtn = selfTransform.FindChild("SelectHeroInfo").GetComponent<Button>();
        m_HeroIconBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickHeroIconBtn));
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickInjectBtn()
	{
	}

    protected virtual void OnClickHeroIconBtn()
	{
	}

    

}
