using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using System.Collections.Generic;


public class UI_QualityPvwMgr : BaseUI 
{
    public static string UI_ResPath = "HeroStrengthen/UI_QualityPvwUI_1_20";
    private Button m_CloseBtn = null;

    private Text m_SkillNameTxt = null;
    private Image m_SkillIconImg = null;
    private Image m_SkillTyptImg = null;

    private Transform m_GridTrans = null;
    private GameObject m_AttrItemPrefab = null;
    private Dictionary<string, string> m_AttrDic = new Dictionary<string, string>();

    private ObjectCard m_Card = null;
    private HeroTemplate m_HeroT = null;
    private HeroTemplate m_HeroNextT = null;
    private Text m_SkillUnlockTxt = null;
    

    public override void InitUIData()
    {
        base.InitUIData();

        m_GridTrans = selfTransform.FindChild("Panel/Attribute/ScrollRect/LayoutList");
        m_AttrItemPrefab = selfTransform.FindChild("Panel/Attribute/AttrItem").gameObject;

        m_SkillUnlockTxt = selfTransform.FindChild("Panel/Unlock/Text_heroquality").GetComponent<Text>();
        m_SkillNameTxt = selfTransform.FindChild("Panel/Unlock/Text_loong").GetComponent<Text>();
        m_SkillTyptImg = selfTransform.FindChild("Panel/Unlock/Img_active").GetComponent<Image>();
        m_SkillIconImg = selfTransform.FindChild("Panel/Unlock/Img_skillIcon").GetComponent<Image>();
        m_CloseBtn = selfTransform.FindChild("Panel/Btn_close").GetComponent<Button>();
        m_CloseBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(onCloseBtnClick));
    }


    /// <summary>
    /// 显示UI数据
    /// </summary>
    /// <param name="card"></param>
    public void ShowUIData(ObjectCard card)
    {
        m_Card = card;
        m_HeroT = card.GetHeroRow();
        m_HeroNextT = (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(m_HeroT.getStageUpTargetID());

        AddData();
        CreateAttrItem();

        int[] skillArray = m_HeroT.getTotalskill();
        int skillid = 0;
        if (card.GetHeroData().QualityLev < skillArray.Length)
        {
            skillid = skillArray[card.GetHeroData().QualityLev];
            SkillTemplate skillT = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(skillid);
            m_SkillIconImg.sprite = UIResourceMgr.LoadSprite(common.defaultPath + skillT.getSkillIcon());
            InterfaceControler.GetInst().ShowSkillTypeIcon(skillT, m_SkillTyptImg);
            m_SkillNameTxt.text = GameUtils.getString(skillT.getSkillName());

            m_SkillUnlockTxt.text = GameUtils.GetSkillColorClear(card.GetHeroData().QualityLev + 1);
        }
    }

    /// <summary>
    /// 添加属性Item
    /// </summary>
    private void AddData()
    {
        if (m_HeroNextT.getInitMaxHP() > m_HeroT.getInitMaxHP())//生命
        {
            int poor = m_HeroNextT.getInitMaxHP() - m_HeroT.getInitMaxHP();
            string hp = GameUtils.GetAttriName(1);
            m_AttrDic.Add(hp,string.Format("+{0}",poor));
        }
        if (m_HeroNextT.getInitPhysicalAttack() > m_HeroT.getInitPhysicalAttack())//攻击力
        {
            int poor = m_HeroNextT.getInitPhysicalAttack() - m_HeroT.getInitPhysicalAttack();
            string attack = GameUtils.GetAttriName(3);
            m_AttrDic.Add(attack, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getInitPhysicalDefence() > m_HeroT.getInitPhysicalDefence())//防御
        {
            int poor = m_HeroNextT.getInitPhysicalDefence() - m_HeroT.getInitPhysicalDefence();
            string def = GameUtils.GetAttriName(5);
            m_AttrDic.Add(def, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBaseHit() > m_HeroT.getBaseHit())//命中率
        {
            int poor = m_HeroNextT.getBaseHit() - m_HeroT.getBaseHit();
            string hit = GameUtils.GetAttriName(23);
            m_AttrDic.Add(hit, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBaseDodge() > m_HeroT.getBaseDodge())//闪避率
        {
            int poor = m_HeroNextT.getBaseDodge() - m_HeroT.getBaseDodge();
            string dodge = GameUtils.GetAttriName(24);
            m_AttrDic.Add(dodge, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBaseCritical() > m_HeroT.getBaseCritical())//暴击率
        {
            int poor = m_HeroNextT.getBaseCritical() - m_HeroT.getBaseCritical();
            string crit = GameUtils.GetAttriName(25);
            m_AttrDic.Add(crit, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBaseTenacity() > m_HeroT.getBaseTenacity())//韧性率
        {
            int poor = m_HeroNextT.getBaseTenacity() - m_HeroT.getBaseTenacity();
            string ten = GameUtils.GetAttriName(26);
            m_AttrDic.Add(ten, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getDamageBonusHit() > m_HeroT.getDamageBonusHit())//伤害加深率
        {
            int poor = m_HeroNextT.getDamageBonusHit() - m_HeroT.getDamageBonusHit();
            string damageB = GameUtils.GetAttriName(31);
            m_AttrDic.Add(damageB, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getDamageReductionHit() > m_HeroT.getDamageReductionHit())//伤害减免率
        {
            int poor = m_HeroNextT.getDamageReductionHit() - m_HeroT.getDamageReductionHit();
            string damageR = GameUtils.GetAttriName(32);
            m_AttrDic.Add(damageR, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBaseCriticalDamage() > m_HeroT.getBaseCriticalDamage())//暴击伤害率
        {
            int poor = m_HeroNextT.getBaseCriticalDamage() - m_HeroT.getBaseCriticalDamage();
            string criticalD = GameUtils.GetAttriName(33);
            m_AttrDic.Add(criticalD, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getBlockHit() > m_HeroT.getBlockHit())//格挡率
        {
            int poor = m_HeroNextT.getBlockHit() - m_HeroT.getBlockHit();
            string block = GameUtils.GetAttriName(56);
            m_AttrDic.Add(block, string.Format("+{0}", poor));
        }
        if (m_HeroNextT.getSabotageHit() > m_HeroT.getSabotageHit())//破甲率
        {
            int poor = m_HeroNextT.getSabotageHit() - m_HeroT.getSabotageHit();
            string sabotage = GameUtils.GetAttriName(57);
            m_AttrDic.Add(sabotage, string.Format("+{0}", poor));
        }
    }

    /// <summary>
    /// 创建属性Item OBJ
    /// </summary>
    private void CreateAttrItem()
    {
        foreach (KeyValuePair<string, string> item in m_AttrDic)
        {
            GameObject go = Instantiate(m_AttrItemPrefab) as GameObject;
            go.transform.parent = m_GridTrans;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);

            go.transform.FindChild("Text_life").GetComponent<Text>().text = item.Key;
            go.transform.FindChild("Text_lifevalue_number").GetComponent<Text>().text = item.Value;
        }

    }

    //关闭按钮回调
    private void onCloseBtnClick()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

}
