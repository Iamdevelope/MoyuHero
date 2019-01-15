using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public class UI_QualityProWinMgr : BaseUI 
{
    public static string UI_ResPath = "HeroStrengthen/UI_QualityProWin_1_20";

    private ObjectCard m_Card = null;
    private HeroTemplate m_HeroT = null;

    private HeroCellItem m_HeroCellItem_Now = null;
    private HeroCellItem m_HeroCellItem_Next = null;
    private Image m_SkillIconImg = null;
    private Image m_SkillTypeImg = null;

    public override void InitUIData()
    {
        base.InitUIData();

        m_HeroCellItem_Now = selfTransform.FindChild("HeroCellItemNow").GetComponent<HeroCellItem>();
        m_HeroCellItem_Next = selfTransform.FindChild("HeroCellItemNext").GetComponent<HeroCellItem>();
        m_SkillIconImg = selfTransform.FindChild("Unlockskills/Img_SkillIcon").GetComponent<Image>();
        m_SkillTypeImg = selfTransform.FindChild("Unlockskills/Img_Active01").GetComponent<Image>();
    }

    public override void InitUIView()
    {
        base.InitUIView();

        Invoke("onClose",1.5f);
    }

    public void ShowHeroItem(ObjectCard card)
    {
        m_Card = card;
        m_HeroT = card.GetHeroRow();

        m_HeroCellItem_Now.UpdateHeroShow(m_Card);
        if (m_HeroT.getStageUpTargetID() > 0)
            m_HeroCellItem_Next.ShowHeroT(m_HeroT.getStageUpTargetID(),m_Card);
        int[] skillArray = m_HeroT.getTotalskill();
        int skillid = 0;
        if (card.GetHeroData().QualityLev < skillArray.Length)
        {
            skillid = skillArray[card.GetHeroData().QualityLev];
            SkillTemplate skillT = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(skillid);
            m_SkillIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + skillT.getSkillIcon());
            InterfaceControler.GetInst().ShowSkillTypeIcon(skillT, m_SkillTypeImg);
            //m_SkillNameTxt.text = GameUtils.getString(skillT.getSkillName());
        }
    }



    public override void UpdateUIView()
    {
        base.UpdateUIView();

        if (Input.GetMouseButtonDown(0))
        {
            onClose();
        }
    }

    
    private void onClose()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
        CancelInvoke();
    }


}
