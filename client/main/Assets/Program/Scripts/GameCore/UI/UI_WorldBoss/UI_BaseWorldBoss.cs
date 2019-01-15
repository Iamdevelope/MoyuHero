using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_BaseWorldBoss : BaseUI
{
	protected Text m_SelectBossTopTittleText;
	protected Button m_SelectBossBackBtn;
	protected Text m_SelectBossBackText;
	protected Text m_DescriptionTittleText;
	protected Text m_DescriptionText1;
	protected Text m_DescriptionText2;
	protected Text m_DescriptionText3;
	protected Button m_ChallengeBtn;
    protected Text m_ChallengeBtnCDText;
	protected Text m_ChallengeBtnText;
	protected Button m_AwardCheckBtn;
	protected Text m_AwardCheckText;
	protected Button m_LegendTempleBtn;
	protected Text m_LegendTempleText;
	protected Text m_RankingTittleText;
	protected Text m_FightTittleText;
	protected Button m_FightBackBtn;
	protected Text m_FightBackText;
	protected Button m_AddResouseBtn1;
	protected Text m_ResouseText1;
	protected Button m_AddResouseBtn2;
	protected Text m_ResouseText2;
    protected Button m_BlessingTipsBtn;
	protected Button m_BlessingBtn;
	protected Text m_BlessingBtnText;
	protected Text m_BlessingCountText;
	protected Text m_AttackText;
	protected Text m_DefenseText;
	protected Text m_BossNameText;
	protected Text m_BossRankText;
	protected Text m_CountDownText;
	protected Button m_FightBtn;
	protected Text m_FightBtnCDText;
	protected Text m_FightBtnText;
	protected Button m_PayFightBtn;
	protected Text m_CostText;
	protected Text m_PayFightBtText;
	protected Text m_MyDamageText;
	protected Text m_MyRank;
	protected Text m_MyTotalDamage;
	protected Text m_BlessingTopTipsText;
	protected Text m_BlessingBottomTipsText;
	protected Text m_ConfirmTittleText;
	protected Text m_PlayerWalletText;
	protected Button m_PayForBlessingBtn;
	protected Text m_PayForBlessingBtnText;
	protected Text m_PayForBlessingCostText;
    protected Text m_PayForBlessingCountText;
	protected Button m_BlessingCloseBtn;
	protected Text m_BlessingCloseBtnText;
	protected Text m_BlessingEffectTittleText;
	protected Text m_BlessingPanelAttackText;
    protected Text m_BlessingPanelAttackPercentText;
	protected Text m_BlessingPanelDefenseText;
    protected Text m_BlessingPanelDefensePercentText;
	protected Text m_LegendTempleTopTittleText;
	protected Button m_LegendTempleCloseBtn;
	protected Text m_ResourceCountText;
	protected Text m_BloodText;
	protected Text m_RuneText;
	protected Text m_HunterText;
	protected Text m_BloodBottomTipsText;
	protected Text m_ItemBottomTipsText;
	protected Text m_ItemExchangeCountText;
	protected Text m_ClaimCheckTittleText;
	protected Button m_AwardCloseBtn;
	protected Text m_AwardCloseBtnText;
	protected Text m_AwardPanelRankingText;
	protected Text m_Rank1;
	protected Text m_Rank2;
	protected Text m_Rank3;
	protected Text m_Rank4_5;
	protected Text m_Rank6_10;
	protected Text m_AwardCheckBottomTipsText;

    protected Text m_WatcherSoulTopTipsText;
    protected Text m_WatcherSoulTittleText;
    protected Text m_PlayerDiamondText;
    protected Button m_PayForSoulBtn;
    protected Text m_PayForSoulBtnText;
    protected Text m_PayForSoulCostText;
    protected Button m_SoulCloseBtn;
    protected Text m_SoulCloseBtnText;
    protected Text m_SoulCountText;
    protected Button m_SoulSubButton;
    protected Button m_SoulAddButton;

    protected Text m_SoulTipsText;
    protected Text m_SoulTipsTitleText;
    protected Button m_SoulTipsPayBtn;
    protected Text m_SoulTipsPayBtnText;
    protected Text m_SoulTipsPayCount;
    protected Button m_SoulTipsCancelBtn;
    protected Text m_SoulTipsCancelBtnText;
    protected Text m_SoulTipsPlayerDiamondText;

	public override void InitUIData()
	{
		base.InitUIData();
        m_SelectBossTopTittleText = selfTransform.FindChild("BossPanel/SelectBossPanel/SelectBossTopPanel/SelectBossTittle/SelectBossTopTittleText").GetComponent<Text>();
		m_SelectBossBackBtn = selfTransform.FindChild("BossPanel/SelectBossPanel/SelectBossTopPanel/SelectBossBackBtn").GetComponent<Button>();
		m_SelectBossBackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelectBossBackBtn));
		m_SelectBossBackText = selfTransform.FindChild("BossPanel/SelectBossPanel/SelectBossTopPanel/SelectBossBackBtn/SelectBossBackText").GetComponent<Text>();
		m_DescriptionTittleText = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/DescriptionImage/DescriptionTittleText").GetComponent<Text>();
        m_DescriptionText1 = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/DescriptionImage/DescriptionScroolRect/Layout/DescriptionText1").GetComponent<Text>();
        m_DescriptionText2 = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/DescriptionImage/DescriptionScroolRect/Layout/DescriptionText2").GetComponent<Text>();
        m_DescriptionText3 = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/DescriptionImage/DescriptionScroolRect/Layout/DescriptionText3").GetComponent<Text>();
		m_ChallengeBtn = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/ChallengeBtn").GetComponent<Button>();
		m_ChallengeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickChallengeBtn));
        m_ChallengeBtnCDText = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/ChallengeBtn/ChallengeBtnCDText").GetComponent<Text>();
        m_ChallengeBtnText = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/ChallengeBtn/ChallengeBtnText").GetComponent<Text>();
		m_AwardCheckBtn = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/AwardCheckBtn").GetComponent<Button>();
		m_AwardCheckBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAwardCheckBtn));
		m_AwardCheckText = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/AwardCheckBtn/AwardCheckText").GetComponent<Text>();
		m_LegendTempleBtn = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/LegendTempleBtn").GetComponent<Button>();
		m_LegendTempleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLegendTempleBtn));
		m_LegendTempleText = selfTransform.FindChild("BossPanel/SelectBossPanel/DescriptionPanel/LegendTempleBtn/LegendTempleText").GetComponent<Text>();
		m_RankingTittleText = selfTransform.FindChild("BossPanel/RankingPanel/RankingTittleImage/RankingTittleText").GetComponent<Text>();
		m_FightTittleText = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/FightTittle/FightTittleText").GetComponent<Text>();
		m_FightBackBtn = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/FightBackBtn").GetComponent<Button>();
		m_FightBackBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickFightBackBtn));
		m_FightBackText = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/FightBackBtn/FightBackText").GetComponent<Text>();
        m_AddResouseBtn1 = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/ResouseTable1/AddResouseBtn1").GetComponent<Button>();
		m_AddResouseBtn1.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddResouseBtn1));
        m_ResouseText1 = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/ResouseTable1/ResouseText1").GetComponent<Text>();
        m_AddResouseBtn2 = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/ResouseTable2/AddResouseBtn2").GetComponent<Button>();
		m_AddResouseBtn2.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAddResouseBtn2));
        m_ResouseText2 = selfTransform.FindChild("BossPanel/FightPanel/FightTopPanel/ResouseTable2/ResouseText2").GetComponent<Text>();
        m_BlessingTipsBtn = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingTipsBtn").GetComponent<Button>();
        m_BlessingTipsBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBlessingTipsBtn));
        m_BlessingBtn = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingBtn").GetComponent<Button>();
		m_BlessingBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBlessingBtn));
		m_BlessingBtnText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingBtn/BlessingBtnText").GetComponent<Text>();
		m_BlessingCountText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingBtn/BlessingCountImage/BlessingCountText").GetComponent<Text>();
		m_AttackText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingTips/AttackText").GetComponent<Text>();
		m_DefenseText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BlessingTips/DefenseText").GetComponent<Text>();
		m_BossNameText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/BossNameImage/BossNameText").GetComponent<Text>();
		m_BossRankText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/HPBar/BossRankImage/BossRankText").GetComponent<Text>();
		m_CountDownText = selfTransform.FindChild("BossPanel/FightPanel/CurrentBossPanel/CountDownImage/CountDownText").GetComponent<Text>();
		m_FightBtn = selfTransform.FindChild("BossPanel/FightPanel/FightBtn").GetComponent<Button>();
		m_FightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickFightBtn));
		m_FightBtnCDText = selfTransform.FindChild("BossPanel/FightPanel/FightBtn/FightBtnCDText").GetComponent<Text>();
		m_FightBtnText = selfTransform.FindChild("BossPanel/FightPanel/FightBtn/FightBtnText").GetComponent<Text>();
		m_PayFightBtn = selfTransform.FindChild("BossPanel/FightPanel/PayFightBtn").GetComponent<Button>();
		m_PayFightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPayFightBtn));
		m_CostText = selfTransform.FindChild("BossPanel/FightPanel/PayFightBtn/CostText").GetComponent<Text>();
		m_PayFightBtText = selfTransform.FindChild("BossPanel/FightPanel/PayFightBtn/PayFightBtText").GetComponent<Text>();
		m_MyDamageText = selfTransform.FindChild("BossPanel/FightPanel/TotalDamageImage/MyDamageText").GetComponent<Text>();
		m_MyRank = selfTransform.FindChild("BossPanel/FightPanel/TotalDamageImage/MyDamageText/MyRank").GetComponent<Text>();
		m_MyTotalDamage = selfTransform.FindChild("BossPanel/FightPanel/TotalDamageImage/MyDamageText/MyTotalDamage").GetComponent<Text>();
		m_BlessingTopTipsText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingTopTipsText").GetComponent<Text>();
        m_BlessingBottomTipsText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingBottomTips/Bottom/BlessingBottomTipsText").GetComponent<Text>();
		m_ConfirmTittleText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingTittleImage/ConfirmTittleText").GetComponent<Text>();
		m_PlayerWalletText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/PlayerWalletImage/PlayerWalletText").GetComponent<Text>();
		m_PayForBlessingBtn = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/PayForBlessingBtn").GetComponent<Button>();
		m_PayForBlessingBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPayForBlessingBtn));
		m_PayForBlessingBtnText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/PayForBlessingBtn/PayForBlessingBtnText").GetComponent<Text>();
		m_PayForBlessingCostText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/PayForBlessingBtn/PayForBlessingCostText").GetComponent<Text>();
        m_PayForBlessingCountText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/PayForBlessingBtn/Image/PayForBlessingCountText").GetComponent<Text>();
		m_BlessingCloseBtn = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingCloseBtn").GetComponent<Button>();
		m_BlessingCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBlessingCloseBtn));
		m_BlessingCloseBtnText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingCloseBtn/BlessingCloseBtnText").GetComponent<Text>();
        m_BlessingEffectTittleText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingEffectImage/BlessingEffectTittleText").GetComponent<Text>();
		m_BlessingPanelAttackText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingEffectImage/BlessingPanelAttackText").GetComponent<Text>();
        m_BlessingPanelAttackPercentText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingEffectImage/BlessingPanelAttackText/BlessingPanelAttackPercentText").GetComponent<Text>();
		m_BlessingPanelDefenseText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingEffectImage/BlessingPanelDefenseText").GetComponent<Text>();
        m_BlessingPanelDefensePercentText = selfTransform.FindChild("BossPanel/FightPanel/BlessingPanel/BlessingEffectImage/BlessingPanelDefenseText/BlessingPanelDefensePercentText").GetComponent<Text>();
		m_LegendTempleTopTittleText = selfTransform.FindChild("LegendTemplePanel/LegendTempleTopPanel/LegendTempleTittleImage/LegendTempleTopTittleText").GetComponent<Text>();
		m_LegendTempleCloseBtn = selfTransform.FindChild("LegendTemplePanel/LegendTempleTopPanel/LegendTempleCloseBtn").GetComponent<Button>();
		m_LegendTempleCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLegendTempleCloseBtn));
		m_ResourceCountText = selfTransform.FindChild("LegendTemplePanel/LegendTempleTopPanel/ResourceImage/ResourceCountText").GetComponent<Text>();
		m_BloodText = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/BloodToggle/BloodText").GetComponent<Text>();
		m_RuneText = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/RuneToggle/RuneText").GetComponent<Text>();
		m_HunterText = selfTransform.FindChild("LegendTemplePanel/LegendTempleLeftPanel/HunterToggle/HunterText").GetComponent<Text>();
        m_BloodBottomTipsText = selfTransform.FindChild("LegendTemplePanel/BloodPanel/BloodBottomTips/Bottom/BloodBottomTipsText").GetComponent<Text>();
        m_ItemBottomTipsText = selfTransform.FindChild("LegendTemplePanel/ItemPanel/ItemBottomTips/Bottom/ItemBottomTipsText").GetComponent<Text>();
		m_ItemExchangeCountText = selfTransform.FindChild("LegendTemplePanel/ItemPanel/ItemExchangeCountText").GetComponent<Text>();
		m_ClaimCheckTittleText = selfTransform.FindChild("AwardCheckPanel/SubBackgroundImage/AwardTittleImage/ClaimCheckTittleText").GetComponent<Text>();
		m_AwardCloseBtn = selfTransform.FindChild("AwardCheckPanel/AwardCloseBtn").GetComponent<Button>();
		m_AwardCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickAwardCloseBtn));
		m_AwardCloseBtnText = selfTransform.FindChild("AwardCheckPanel/AwardCloseBtn/AwardCloseBtnText").GetComponent<Text>();
		m_AwardPanelRankingText = selfTransform.FindChild("AwardCheckPanel/Ranking/AwardPanelRankingText").GetComponent<Text>();
		m_Rank1 = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank1/BackImage/RankImage/Rank1").GetComponent<Text>();
		m_Rank2 = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank2/BackImage/RankImage/Rank2").GetComponent<Text>();
		m_Rank3 = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank3/BackImage/RankImage/Rank3").GetComponent<Text>();
		m_Rank4_5 = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank4_5/BackImage/RankImage/Rank4_5").GetComponent<Text>();
		m_Rank6_10 = selfTransform.FindChild("AwardCheckPanel/Ranking/RankingAwardList/Layout/AwardRank6_10/BackImage/RankImage/Rank6_10").GetComponent<Text>();
        m_AwardCheckBottomTipsText = selfTransform.FindChild("AwardCheckPanel/AwardCheckBottomTips/Bottom/AwardCheckBottomTipsText").GetComponent<Text>();

        m_WatcherSoulTopTipsText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/WatcherSoulTopTipsText").GetComponent<Text>();
        m_WatcherSoulTittleText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/WatcherSoulTittleImage/WatcherSoulTittleText").GetComponent<Text>();
        m_PlayerDiamondText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/PlayerDiamondText").GetComponent<Text>();
        m_PayForSoulBtn = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/PayForSoulBtn").GetComponent<Button>();
        m_PayForSoulBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickPayForSoulBtn));
        m_PayForSoulBtnText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/PayForSoulBtn/PayForSoulBtnText").GetComponent<Text>();
        m_PayForSoulCostText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/PayForSoulBtn/PayForSoulCostText").GetComponent<Text>();
        m_SoulCloseBtn = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulCloseBtn").GetComponent<Button>();
        m_SoulCloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSoulCloseBtn));
        m_SoulCloseBtnText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulCloseBtn/SoulCloseBtnText").GetComponent<Text>();
        m_SoulCountText = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulImage/SoulCountImage/SoulCountText").GetComponent<Text>();
        m_SoulSubButton = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulImage/SoulCountImage/SoulSubButton").GetComponent<Button>();
        m_SoulSubButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSoulSubButton));
        m_SoulAddButton = selfTransform.FindChild("BossPanel/FightPanel/WatcherSoulPanel/SoulImage/SoulCountImage/SoulAddButton").GetComponent<Button>();
        m_SoulAddButton.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSoulAddButton));

        m_SoulTipsText = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/Image/SoulTipsText").GetComponent<Text>();
        m_SoulTipsTitleText = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/Image/SoulTipsTitleText").GetComponent<Text>();
        m_SoulTipsPayBtn = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/SoulTipsPayBtn").GetComponent<Button>();
        m_SoulTipsPayBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSoulTipsPayBtn));
        m_SoulTipsPayBtnText = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/SoulTipsPayBtn/SoulTipsPayBtnText").GetComponent<Text>();
        m_SoulTipsPayCount = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/SoulTipsPayBtn/Image/SoulTipsPayCount").GetComponent<Text>();
        m_SoulTipsCancelBtn = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/SoulTipsCancelBtn").GetComponent<Button>();
        m_SoulTipsCancelBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSoulTipsCancelBtn));
        m_SoulTipsCancelBtnText = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/SoulTipsCancelBtn/SoulTipsCancelBtnText").GetComponent<Text>();
        m_SoulTipsPlayerDiamondText = selfTransform.FindChild("BossPanel/FightPanel/SoulTipsPanel/Image/SoulTipsPlayerDiamondText").GetComponent<Text>();
	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickSelectBossBackBtn()
	{
	}

	protected virtual void OnClickChallengeBtn()
	{
	}

	protected virtual void OnClickAwardCheckBtn()
	{
	}

	protected virtual void OnClickLegendTempleBtn()
	{
	}

	protected virtual void OnClickFightBackBtn()
	{
	}

	protected virtual void OnClickAddResouseBtn1()
	{
	}

	protected virtual void OnClickAddResouseBtn2()
	{
	}
    protected virtual void OnClickBlessingTipsBtn()
    { 
    
    }
	protected virtual void OnClickBlessingBtn()
	{
	}

	protected virtual void OnClickFightBtn()
	{
	}

	protected virtual void OnClickPayFightBtn()
	{
	}

	protected virtual void OnClickPayForBlessingBtn()
	{
	}

	protected virtual void OnClickBlessingCloseBtn()
	{
	}

	protected virtual void OnClickLegendTempleCloseBtn()
	{
	}

	protected virtual void OnClickAwardCloseBtn()
	{
	}


    protected virtual void OnClickPayForSoulBtn()
    {
    }

    protected virtual void OnClickSoulCloseBtn()
    {
    }

    protected virtual void OnClickSoulSubButton()
    {
    }

    protected virtual void OnClickSoulAddButton()
    {
    }

    protected virtual void OnClickSoulTipsPayBtn()
    { 
    
    }
    protected virtual void OnClickSoulTipsCancelBtn()
    { 
    
    }
}
