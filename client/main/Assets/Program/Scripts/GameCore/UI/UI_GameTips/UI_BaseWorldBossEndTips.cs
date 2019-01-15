using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseWorldBossEndTips : BaseUI
{

	protected Image m_BossImage;
	protected Text m_BossText;
	protected Text m_KillText;
	protected Text m_DamageText;
	protected Text m_DamagePointText;
	protected Text m_AwardText;


	protected Button m_BackBtn;
	protected Text m_BackBtnText;

	public override void InitUIData()
	{
		base.InitUIData();

		m_BossImage = selfTransform.FindChild("BossImage").GetComponent<Image>();
		m_BossText = selfTransform.FindChild("BossImage/Image/BossText").GetComponent<Text>();
		m_KillText = selfTransform.FindChild("CenterPanel/KillText").GetComponent<Text>();
		m_DamageText = selfTransform.FindChild("CenterPanel/DamageText").GetComponent<Text>();
		m_DamagePointText = selfTransform.FindChild("CenterPanel/DamageText/DamagePointText").GetComponent<Text>();
		m_AwardText = selfTransform.FindChild("AwardPanel/AwardText").GetComponent<Text>();

		m_BackBtn = selfTransform.FindChild("BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBackBtn));
		m_BackBtnText = selfTransform.FindChild("BackBtn/BackBtnText").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

}
