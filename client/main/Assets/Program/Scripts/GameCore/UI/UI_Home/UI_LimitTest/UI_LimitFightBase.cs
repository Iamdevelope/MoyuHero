using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_LimitFightBase : BaseUI
{
	protected Text m_PowerSuplusTxt;
	protected Text m_LevelNameTxt;
	protected Button m_AngerBtn;
	protected Text m_AngerDesTxt;
	protected Text m_AngerSuplusCountTxt;
	protected Text m_BraveNumTxt;
	protected Text m_CosBraveTxt;
	protected Button m_PhyDefBtn;
	protected Text m_PhyDefDesTxt;
	protected Text m_PhyDefSuplusCountTxt;
	protected Button m_PhyAttackBtn;
	protected Text m_PhyAttackDesTxt;
	protected Text m_PhyAttatkSuplusCountTxt;
	protected Button m_ReAllBeingBtn;
	protected Text m_ReAllBeingDesText;
	protected Text m_ReAllBeingSuplusCountTxt;

	public override void InitUIData()
	{
		base.InitUIData();
		m_PowerSuplusTxt = selfTransform.FindChild("PowerSuplusTxt").GetComponent<Text>();
		m_LevelNameTxt = selfTransform.FindChild("LevelNameTxt").GetComponent<Text>();
		m_AngerBtn = selfTransform.FindChild("AngerBtn").GetComponent<Button>();
		m_AngerBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAngerBtn));
		m_AngerDesTxt = selfTransform.FindChild("AngerBtn/AngerDesTxt").GetComponent<Text>();
		m_AngerSuplusCountTxt = selfTransform.FindChild("AngerBtn/AngerSuplusCountTxt").GetComponent<Text>();
		m_BraveNumTxt = selfTransform.FindChild("Brave/BraveNumTxt").GetComponent<Text>();
		m_CosBraveTxt = selfTransform.FindChild("CosBraveTxt").GetComponent<Text>();
		m_PhyDefBtn = selfTransform.FindChild("PhyDefBtn").GetComponent<Button>();
		m_PhyDefBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPhyDefBtn));
		m_PhyDefDesTxt = selfTransform.FindChild("PhyDefBtn/PhyDefDesTxt").GetComponent<Text>();
		m_PhyDefSuplusCountTxt = selfTransform.FindChild("PhyDefBtn/PhyDefSuplusCountTxt").GetComponent<Text>();
		m_PhyAttackBtn = selfTransform.FindChild("PhyAttackBtn").GetComponent<Button>();
		m_PhyAttackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPhyAttackBtn));
		m_PhyAttackDesTxt = selfTransform.FindChild("PhyAttackBtn/PhyAttackDesTxt").GetComponent<Text>();
		m_PhyAttatkSuplusCountTxt = selfTransform.FindChild("PhyAttackBtn/PhyAttatkSuplusCountTxt").GetComponent<Text>();
		m_ReAllBeingBtn = selfTransform.FindChild("ReAllBeingBtn").GetComponent<Button>();
		m_ReAllBeingBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickReAllBeingBtn));
		m_ReAllBeingDesText = selfTransform.FindChild("ReAllBeingBtn/ReAllBeingDesText").GetComponent<Text>();
		m_ReAllBeingSuplusCountTxt = selfTransform.FindChild("ReAllBeingBtn/ReAllBeingSuplusCountTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickAngerBtn()
	{
	}

	protected virtual void OnClickPhyDefBtn()
	{
	}

	protected virtual void OnClickPhyAttackBtn()
	{
	}

	protected virtual void OnClickReAllBeingBtn()
	{
	}

}
