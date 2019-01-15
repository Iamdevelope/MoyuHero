using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_HandBookHeroInfoPopBase : CustomUI
{
	protected Button m_CloseBtn;
	protected Text m_Text;
	protected Text m_HeroDes_txt;
	protected Text m_InfoName_txt;
	protected Text m_InfoValue_txt;
	protected Text m_InfoName_txt_1;
	protected Text m_InfoValue_txt_1;
	protected Text m_InfoName_txt_2;
	protected Text m_InfoValue_txt_2;
	protected Text m_InfoName_txt_3;
	protected Text m_InfoValue_txt_3;
	protected Text m_InfoName_txt_4;
	protected Text m_InfoValue_txt_4;
	protected Button m_SkillShowBtn;
	protected Text m_Text_1;
	protected Button m_OrderUP_btn;
	protected Text m_Text_2;
	protected Button m_InfoDetail_btn;
	protected Text m_InfoDetailBtn_txt;
	protected Button m_HeroMake_btn;
	protected Text m_SkillMake_txt;
	protected Button m_SkillItem_0;
	protected Text m_SkillLevelNumber_txt;
	protected Text m_Text_3;
	protected Button m_SkillItem_1;
	protected Text m_SkillLevelNumber_txt_1;
	protected Text m_Text_4;
	protected Button m_SkillItem_2;
	protected Text m_Level_txt;
	protected Text m_HeroName_txt;
	protected Text m_PlayerName_txt;
	protected Button m_JobType_Img;
	protected Button m_AttackType_Img;
	protected Button m_RaceTypeImg;
	protected Button m_LeftBtn;
	protected Button m_RightBtn;
	protected Text m_Skilltips_Text;
	protected Text m_Text_5;
	protected Text m_Text_6;
	protected Text m_Text_7;
	protected Button m_RightBtn_1;
	protected Button m_LeftBtn_1;

	public override void InitUIData()
	{
		base.InitUIData();
		m_CloseBtn = selfTransform.FindChild("CloseBtn").GetComponent<Button>();
		m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickCloseBtn));
		m_Text = selfTransform.FindChild("Title/TitleButton_0/Text").GetComponent<Text>();
		m_HeroDes_txt = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/HeroDes_txt").GetComponent<Text>();
		m_InfoName_txt = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_Hp/InfoName_txt").GetComponent<Text>();
		m_InfoValue_txt = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_Hp/InfoValue_txt").GetComponent<Text>();
		m_InfoName_txt_1 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_PhyAttack/InfoName_txt").GetComponent<Text>();
		m_InfoValue_txt_1 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_PhyAttack/InfoValue_txt").GetComponent<Text>();
		m_InfoName_txt_2 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_ApAttack/InfoName_txt").GetComponent<Text>();
		m_InfoValue_txt_2 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_ApAttack/InfoValue_txt").GetComponent<Text>();
		m_InfoName_txt_3 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_PhyDefense/InfoName_txt").GetComponent<Text>();
		m_InfoValue_txt_3 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_PhyDefense/InfoValue_txt").GetComponent<Text>();
		m_InfoName_txt_4 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_ApDefense/InfoName_txt").GetComponent<Text>();
		m_InfoValue_txt_4 = selfTransform.FindChild("HeroInfo/HeroInfo_Mid/Info_ApDefense/InfoValue_txt").GetComponent<Text>();
		m_SkillShowBtn = selfTransform.FindChild("HeroInfo/SkillShowBtn").GetComponent<Button>();
		m_SkillShowBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkillShowBtn));
		m_Text_1 = selfTransform.FindChild("HeroInfo/SkillShowBtn/Text").GetComponent<Text>();
		m_OrderUP_btn = selfTransform.FindChild("HeroInfo/OrderUP_btn").GetComponent<Button>();
		m_OrderUP_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickOrderUP_btn));
		m_Text_2 = selfTransform.FindChild("HeroInfo/OrderUP_btn/Text").GetComponent<Text>();
		m_InfoDetail_btn = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/InfoDetail_btn").GetComponent<Button>();
		m_InfoDetail_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickInfoDetail_btn));
		m_InfoDetailBtn_txt = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/InfoDetail_btn/InfoDetailBtn_txt").GetComponent<Text>();
		m_HeroMake_btn = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/HeroMake_btn").GetComponent<Button>();
		m_HeroMake_btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickHeroMake_btn));
		m_SkillMake_txt = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/HeroMake_btn/SkillMake_txt").GetComponent<Text>();
		m_SkillItem_0 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_0").GetComponent<Button>();
		m_SkillItem_0.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkillItem_0));
		m_SkillLevelNumber_txt = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_0/SkillLevelNumber_txt").GetComponent<Text>();
		m_Text_3 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_0/Text").GetComponent<Text>();
		m_SkillItem_1 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_1").GetComponent<Button>();
		m_SkillItem_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkillItem_1));
		m_SkillLevelNumber_txt_1 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_1/SkillLevelNumber_txt").GetComponent<Text>();
		m_Text_4 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_1/Text").GetComponent<Text>();
		m_SkillItem_2 = selfTransform.FindChild("HeroInfo/HeroInfo_LeftBottom/SkillItem_2").GetComponent<Button>();
		m_SkillItem_2.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSkillItem_2));
		m_Level_txt = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/Level_txt").GetComponent<Text>();
		m_HeroName_txt = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/HeroName_txt").GetComponent<Text>();
		m_PlayerName_txt = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/PlayerName_Img/PlayerName_txt").GetComponent<Text>();
		m_JobType_Img = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/JobType_Img").GetComponent<Button>();
		m_JobType_Img.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickJobType_Img));
		m_AttackType_Img = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/AttackType_Img").GetComponent<Button>();
		m_AttackType_Img.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAttackType_Img));
		m_RaceTypeImg = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/RaceTypeImg").GetComponent<Button>();
		m_RaceTypeImg.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRaceTypeImg));
		m_LeftBtn = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/LeftBtn").GetComponent<Button>();
		m_LeftBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
		m_RightBtn = selfTransform.FindChild("HeroInfo/HeroInof_LeftUP/RightBtn").GetComponent<Button>();
		m_RightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));
		m_Skilltips_Text = selfTransform.FindChild("HeroInfo/Skilltips/Skilltips_Text").GetComponent<Text>();
		m_Text_5 = selfTransform.FindChild("HeroInfo/AttackTypeTips/Text").GetComponent<Text>();
		m_Text_6 = selfTransform.FindChild("HeroInfo/JobTypeTips/Text").GetComponent<Text>();
		m_Text_7 = selfTransform.FindChild("HeroInfo/HeroSkinUI/Text").GetComponent<Text>();
		m_RightBtn_1 = selfTransform.FindChild("HeroInfo/HeroSkinUI/RightBtn").GetComponent<Button>();
		m_RightBtn_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn_1));
		m_LeftBtn_1 = selfTransform.FindChild("HeroInfo/HeroSkinUI/LeftBtn").GetComponent<Button>();
		m_LeftBtn_1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn_1));

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickCloseBtn()
	{
	}

	protected virtual void OnClickSkillShowBtn()
	{
	}

	protected virtual void OnClickOrderUP_btn()
	{
	}

	protected virtual void OnClickInfoDetail_btn()
	{
	}

	protected virtual void OnClickHeroMake_btn()
	{
	}

	protected virtual void OnClickSkillItem_0()
	{
	}

	protected virtual void OnClickSkillItem_1()
	{
	}

	protected virtual void OnClickSkillItem_2()
	{
	}

	protected virtual void OnClickJobType_Img()
	{
	}

	protected virtual void OnClickAttackType_Img()
	{
	}

	protected virtual void OnClickRaceTypeImg()
	{
	}

	protected virtual void OnClickLeftBtn()
	{
	}

	protected virtual void OnClickRightBtn()
	{
	}

	protected virtual void OnClickRightBtn_1()
	{
	}

	protected virtual void OnClickLeftBtn_1()
	{
	}

}
