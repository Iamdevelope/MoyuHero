using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.Utils;
using DreamFaction.UI;
using DreamFaction.GameCore;

public class UI_SkillPopMgr : BaseUI 
{
    public enum SkillPopUIType
    {
        Default,
        Locked,
        LevelUp,
        UnLocked,
    }

    private SkillTemplate m_SkillT = null;
    private SkillupcostTemplate m_SkillUpT = null;
    private ObjectCard m_Card = null;


    private Image m_SkillIconImg = null;
    private Image m_SkillTypeImg = null;
    private Text m_SkillNameTxt = null;
    private Text m_SkillDesTxt = null;
    private Button m_CloseBtn = null;

    private Text m_SkillLevelTxt = null;
    private Text m_SkillAngerTxt = null;
    private Text m_SkillCDTxt = null;
    private Text m_SkillNextDesTxt = null;

    private Text m_SkillUnlockTxt = null;

    private int m_Index = -1;




    public override void InitUIData()
    {
        base.InitUIData();

        //公用的
        m_SkillIconImg = selfTransform.FindChild("Panel/Skillicon/Img_SkillIcon").GetComponent<Image>();
        m_SkillTypeImg = selfTransform.FindChild("Panel/Skillicon/Img_Active").GetComponent<Image>();
        m_SkillNameTxt = selfTransform.FindChild("Panel/Text_Skillname").GetComponent<Text>();
        m_SkillDesTxt = selfTransform.FindChild("Panel/Text_Des").GetComponent<Text>();
        m_CloseBtn = selfTransform.FindChild("Panel/Btn_Close").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onCleseBtnClick));
    }

    public void ShowSkillPopUI(SkillTemplate skillT,ObjectCard card,int index, SkillPopUIType type = SkillPopUIType.Default)
    {
        bool isSkillOpen = card.GetHeroData().QualityLev >= index;

        m_Index = index;
        m_Card = card;
        m_SkillT = skillT;
        m_SkillUpT = (SkillupcostTemplate)DataTemplate.GetInstance().m_SkillupcostTable.getTableData(m_SkillT.getId());


        switch (type)
        {
            case SkillPopUIType.Default:
                if (isSkillOpen)
                {
                    SkillOpenInitUI();
                }
                else
                {
                    SkillNotOpenInitUI();
                }
                break;
            case SkillPopUIType.Locked:
                SkillNotOpenInitUI();
                break;
            case SkillPopUIType.LevelUp:
                SkillLevelUpInitUI();
                break;
            case SkillPopUIType.UnLocked:
                SkillOpenInitUI();
                break;
            default:
                break;
        }

        m_SkillIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + skillT.getSkillIcon());
        InterfaceControler.GetInst().ShowSkillTypeIcon(m_SkillT, m_SkillTypeImg);
        m_SkillNameTxt.text = GameUtils.getString(m_SkillT.getSkillName());
        m_SkillDesTxt.text =  GameUtils.getHeroString(m_SkillT.getSkillDes(), m_Card/*).Replace("\\n", "\n"*/);
    }
    /// <summary>
    /// 初始化技能开启UI
    /// </summary>
    private void SkillOpenInitUI()
    {
        m_SkillLevelTxt = selfTransform.FindChild("Panel/Frame/Text_Level1").GetComponent<Text>();
        m_SkillAngerTxt = selfTransform.FindChild("Panel/Frame/Text_Consumption1").GetComponent<Text>();
        m_SkillCDTxt = selfTransform.FindChild("Panel/Frame/Text_Cooldown1").GetComponent<Text>();
        m_SkillNextDesTxt = selfTransform.FindChild("Panel/Frame/Text_Damege1").GetComponent<Text>();

        m_SkillLevelTxt.text = m_SkillT.getSkillLevel().ToString();
        m_SkillAngerTxt.text = string.Format("{0}怒气",  m_SkillT.getSkillCostNum1());
        m_SkillCDTxt.text = string.Format("{0}秒",(m_SkillT.getCooldown() / 1000));
        m_SkillNextDesTxt.text = GameUtils.SetUpShow(m_Card, m_SkillUpT); 
    }

    /// <summary>
    /// 初始化技能未开启UI
    /// </summary>
    private void SkillNotOpenInitUI()
    {
        m_SkillUnlockTxt = selfTransform.FindChild("Panel/Text_Unlock").GetComponent<Text>();
        m_SkillUnlockTxt.text = GameUtils.GetSkillColorClear(m_Index);
    }

    private void SkillLevelUpInitUI()
    {
        m_SkillLevelTxt = selfTransform.FindChild("Panel/Word/Text_Level1").GetComponent<Text>();
        m_SkillAngerTxt = selfTransform.FindChild("Panel/Word/Text_Consumption1").GetComponent<Text>();
        m_SkillCDTxt = selfTransform.FindChild("Panel/Word/Text_Cooldown1").GetComponent<Text>();
        m_SkillUnlockTxt = selfTransform.FindChild("Panel/Text_Unlock").GetComponent<Text>();

        m_SkillLevelTxt.text = m_SkillT.getSkillLevel().ToString();
        m_SkillAngerTxt.text = string.Format("{0}怒气", m_SkillT.getSkillCostNum1());
        m_SkillCDTxt.text = string.Format("{0}秒", (m_SkillT.getCooldown() / 1000));

        selfTransform.FindChild("Panel/Word/Text_Level").gameObject.SetActive(true);
/*        selfTransform.FindChild("Word/Text_Consumption").gameObject.SetActive(true);*/
        selfTransform.FindChild("Panel/Word/Text_Cooldown").gameObject.SetActive(true);
        m_SkillLevelTxt.gameObject.SetActive(true);
/*        m_SkillAngerTxt.gameObject.SetActive(true);*/
        m_SkillCDTxt.gameObject.SetActive(true);
        m_SkillUnlockTxt.gameObject.SetActive(false);
    }

    private void onCleseBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }




    
}
