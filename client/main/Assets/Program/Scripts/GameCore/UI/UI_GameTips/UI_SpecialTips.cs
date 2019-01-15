using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.Utils;
using DreamFaction.GameNetWork;
public class UI_SpecialTips : UI_BaseSpecialTips
{
    public enum TipsType
    {
        MysteriousShop,
        SpecialStage
    }
    public static string Path = "UI_MsgBox/UI_SpecialTipsBox_0_3";
    public static TipsType MessageType;
    public static bool CallByFightScene = true;

    private GameObject m_MysteriousShopImage;
    private GameObject m_SpecialStageImage;
    public override void InitUIData()
    {
        base.InitUIData();

        m_MysteriousShopImage = selfTransform.FindChild("MysteriousShopImage").gameObject;
        m_SpecialStageImage = selfTransform.FindChild("SpecialStageImage").gameObject;
//        roleImage = selfTransform.FindChild("RoleImage").GetComponent<Image>();

    }
    public override void InitUIView()
    {
        base.InitUIView();
        UI_FightControler.Inst.SetIsSpecialStage(false);
        UI_FightControler.Inst.SetIsMysteriousShop(false);
        m_OKBtnText.text = GameUtils.getString("fight_mysteriousman_button1");
        m_CancelBtnText.text = GameUtils.getString("fight_mysteriousman_button2");
//        string tempString;
        switch(MessageType)
        {
            case TipsType.MysteriousShop:
                m_TittleText.text = GameUtils.getString("fight_mysteriousman_title");
                m_ContentText.text = GameUtils.getString("fight_mysteriousman_content1");
                m_TipText.text = GameUtils.getString("fight_mysteriousman_content2");
                m_MysteriousShopImage.SetActive(true);
                m_SpecialStageImage.SetActive(false);
                break;
            case TipsType.SpecialStage:
                m_TittleText.text = GameUtils.getString("fight_special_stage_title");
                m_ContentText.text = GameUtils.getString("fight_special_stage_content1");
                m_TipText.text = GameUtils.getString("fight_special_stage_content2");
                m_MysteriousShopImage.SetActive(false);
                m_SpecialStageImage.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OKBtnMysteriousShopHandler()
    {
        if (CallByFightScene)
        {
            UI_FightControler.Inst.ReMoveUI(gameObject);
            var objSelf = DreamFaction.GameNetWork.ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week != objSelf.GetWeek())
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }
            }
            UI_HomeControler.NeedShowMysteriousShop = true;
            DreamFaction.GameCore.SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }
        else
            UI_HomeControler.Inst.ReMoveUI(gameObject);
    
    }

    private void OKBtnSpacialStageHandler()
    {
        //新手引导相关 点击【立即前往】
        if (GuideManager.GetInstance().isGuideUser)
        {
            //GuideManager.GetInstance().ShowGuideWithIndex(200602);
        }
        if (CallByFightScene)
        {
            UI_FightControler.Inst.ReMoveUI(gameObject);
            var objSelf = DreamFaction.GameNetWork.ObjectSelf.GetInstance();
            if (objSelf.GetIsPrompt())
            {
                if (objSelf.Week != objSelf.GetWeek())
                {
                    objSelf.SetPromptTime(true);
                    objSelf.SetPromptBttleend(true);
                }

            }
            UI_HomeControler.NeedShowMysteriousShop = false;
            DreamFaction.UI.UI_MainHome.NeedShowBattlePanel = true;
            //DreamFaction.UI.UI_SelectFightArea.NeedSetToSpecialStage = true;
            //UI_SelectLevelMgr.NeedSpecialStage = true;
            DreamFaction.GameCore.SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());
        }
        else
            UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

    protected override void OnClickOKBtn()
	{
        switch (MessageType)
        { 
            case TipsType.MysteriousShop:
                OKBtnMysteriousShopHandler();
                break;
            case TipsType.SpecialStage:
                OKBtnSpacialStageHandler();
                break;
            default: break;
        
        }
	}

	protected override void OnClickCancelBtn()
	{
        if (CallByFightScene)
            UI_FightControler.Inst.ReMoveUI(gameObject);
        else
            UI_HomeControler.Inst.ReMoveUI(gameObject);
	}
    
}
