using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class UI_StageBase : BaseUI
{
	protected Button m_BackBtn;
	protected Text m_ChapterNameTxt;
	protected Text m_StageNameTxt;
	protected Text m_StageDescTxt;
	protected Text m_EnemysTitleText;
	protected Text m_RewardTitleTxt;
	protected Text m_ResourceCount1;
	protected Text m_ResourceCount2;
	protected Text m_ResourceCount3;
	protected Text m_ItemsTitleTxt;
	protected Text m_DifficultTxt;
	protected Text m_ConsumeTitleTxt;
	protected Text m_ConsumeCountTxt;
	protected Button m_FormationBtn;
	protected Text m_Text;
	protected Text m_SucessTitleTxt;
	protected Text m_SucessConditionTxt;
	protected Text m_RemindTimeTxt;
	protected Button m_ResetBtn;
	protected Text m_ResetBtnTxt;
	protected Button m_WipeOutTenBtn;
	protected Text m_WipOutTenVipLvTxt;
	protected Button m_WipeOutOneBtn;
	protected Button m_StartFightBtn;
	protected Text m_StartFightBtnTxt;
	protected Button m_iconImg;
	protected Text m_CountTxt;
	protected Button m_iconImg_1;
	protected Text m_BossFlagTxt;

	public override void InitUIData()
	{
		base.InitUIData();
		m_BackBtn = selfTransform.FindChild("Panel/TopObj/BackBtn").GetComponent<Button>();
		m_BackBtn.onClick.AddListener(OnClickBackBtn);
		m_ChapterNameTxt = selfTransform.FindChild("Panel/TopObj/ChapterName/ChapterNameTxt").GetComponent<Text>();
		m_StageNameTxt = selfTransform.FindChild("Panel/TopObj/StageName/StageNameTxt").GetComponent<Text>();
		m_StageDescTxt = selfTransform.FindChild("Panel/LeftObj/DetailObj/StageDescTxt").GetComponent<Text>();
		m_EnemysTitleText = selfTransform.FindChild("Panel/LeftObj/Enemys/TitleObj/EnemysTitleText").GetComponent<Text>();
		m_RewardTitleTxt = selfTransform.FindChild("Panel/LeftObj/Rewards/TitleObj/RewardTitleTxt").GetComponent<Text>();
		m_ResourceCount1 = selfTransform.FindChild("Panel/LeftObj/Rewards/Grid/Resource1/ResourceCount1").GetComponent<Text>();
		m_ResourceCount2 = selfTransform.FindChild("Panel/LeftObj/Rewards/Grid/Resource2/ResourceCount2").GetComponent<Text>();
		m_ResourceCount3 = selfTransform.FindChild("Panel/LeftObj/Rewards/Grid/Resource3/ResourceCount3").GetComponent<Text>();
		m_ItemsTitleTxt = selfTransform.FindChild("Panel/LeftObj/Items/TitleObj/ItemsTitleTxt").GetComponent<Text>();
		m_DifficultTxt = selfTransform.FindChild("Panel/RightObj/DifficultTxt").GetComponent<Text>();
		m_ConsumeTitleTxt = selfTransform.FindChild("Panel/RightObj/ConsumeObj/ConsumeTitleTxt").GetComponent<Text>();
		m_ConsumeCountTxt = selfTransform.FindChild("Panel/RightObj/ConsumeObj/ConsumeCountTxt").GetComponent<Text>();
		m_FormationBtn = selfTransform.FindChild("Panel/RightObj/FormationBtn").GetComponent<Button>();
		m_FormationBtn.onClick.AddListener(OnClickFormationBtn);
		m_Text = selfTransform.FindChild("Panel/RightObj/FormationBtn/Text").GetComponent<Text>();
		m_SucessTitleTxt = selfTransform.FindChild("Panel/RightObj/SucessConditionObj/SucessTitleTxt").GetComponent<Text>();
		m_SucessConditionTxt = selfTransform.FindChild("Panel/RightObj/SucessConditionObj/SucessConditionTxt").GetComponent<Text>();
		m_RemindTimeTxt = selfTransform.FindChild("Panel/RightObj/RemindTimeTxt").GetComponent<Text>();
		m_ResetBtn = selfTransform.FindChild("Panel/RightObj/ResetBtn").GetComponent<Button>();
		m_ResetBtn.onClick.AddListener(OnClickResetBtn);
		m_ResetBtnTxt = selfTransform.FindChild("Panel/RightObj/ResetBtn/ResetBtnTxt").GetComponent<Text>();
		m_WipeOutTenBtn = selfTransform.FindChild("Panel/RightObj/WipeOutTenBtn").GetComponent<Button>();
		m_WipeOutTenBtn.onClick.AddListener(OnClickWipeOutTenBtn);
		m_WipOutTenVipLvTxt = selfTransform.FindChild("Panel/RightObj/WipeOutTenBtn/WipOutTenVipLvTxt").GetComponent<Text>();
		m_WipeOutOneBtn = selfTransform.FindChild("Panel/RightObj/WipeOutOneBtn").GetComponent<Button>();
		m_WipeOutOneBtn.onClick.AddListener(OnClickWipeOutOneBtn);
		m_StartFightBtn = selfTransform.FindChild("Panel/RightObj/StartFightBtn").GetComponent<Button>();
		m_StartFightBtn.onClick.AddListener(OnClickStartFightBtn);
		m_StartFightBtnTxt = selfTransform.FindChild("Panel/RightObj/StartFightBtn/StartFightBtnTxt").GetComponent<Text>();
		m_iconImg = selfTransform.FindChild("Items/Item/iconImg").GetComponent<Button>();
		m_iconImg.onClick.AddListener(OnClickiconImg);
		m_CountTxt = selfTransform.FindChild("Items/Item/CountObj/CountTxt").GetComponent<Text>();
		m_iconImg_1 = selfTransform.FindChild("Items/Hero/iconImg").GetComponent<Button>();
		m_iconImg_1.onClick.AddListener(OnClickiconImg_1);
		m_BossFlagTxt = selfTransform.FindChild("Items/Hero/BossObj/BossFlagTxt").GetComponent<Text>();

	}

	public override void InitUIView()
	{
		base.InitUIView();
	}

	protected virtual void OnClickBackBtn()
	{
	}

	protected virtual void OnClickFormationBtn()
	{
	}

	protected virtual void OnClickResetBtn()
	{
	}

	protected virtual void OnClickWipeOutTenBtn()
	{
	}

	public virtual void OnClickWipeOutOneBtn()
	{
	}

	protected virtual void OnClickStartFightBtn()
	{
	}

	protected virtual void OnClickiconImg()
	{
	}

	protected virtual void OnClickiconImg_1()
	{
	}

    public virtual void OnDestroy()
    {
        Destroy(m_BackBtn);
        Destroy(m_ChapterNameTxt);
        Destroy(m_StageNameTxt);
        Destroy(m_StageDescTxt);
        Destroy(m_EnemysTitleText);
        Destroy(m_RewardTitleTxt);
        Destroy(m_ResourceCount1);
        Destroy(m_ResourceCount2);
        Destroy(m_ResourceCount3);
        Destroy(m_ItemsTitleTxt);
        Destroy(m_DifficultTxt);
        Destroy(m_ConsumeTitleTxt);
        Destroy(m_ConsumeCountTxt);
        Destroy(m_FormationBtn);
        Destroy(m_Text);
        Destroy(m_SucessTitleTxt);
        Destroy(m_SucessConditionTxt);
        Destroy(m_RemindTimeTxt);
        Destroy(m_ResetBtn);
        Destroy(m_ResetBtnTxt);
        Destroy(m_WipeOutTenBtn);
        Destroy(m_WipOutTenVipLvTxt);
        Destroy(m_WipeOutOneBtn);
        Destroy(m_StartFightBtn);
        Destroy(m_StartFightBtnTxt);
        Destroy(m_iconImg);
        Destroy(m_CountTxt);
        Destroy(m_iconImg_1);
        Destroy(m_BossFlagTxt);
    }
}
