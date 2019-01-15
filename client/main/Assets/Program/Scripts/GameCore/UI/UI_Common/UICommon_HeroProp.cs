using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;
using DreamFaction.UI;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using System.Collections.Generic;

public class UICommon_HeroProp : UICommon_HeroPropBase, UICommonInterface
{
    public override void InitUIData()
    {
        base.InitUIData();
    }

    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();

        UICommonManager.Inst.RemoveUI(UICommonType.CommonHeroProp, this);
    }

    /// <summary>
    /// 攻击
    /// 防御
    /// 生命
    /// 命中率
    /// 闪避率
    /// 暴击率
    /// 韧性率
    /// 格挡率
    /// 破甲率
    /// 伤害加成率
    /// 伤害减免率
    /// 暴击伤害率
    /// </summary>
    /// <param name="heroData"></param>
    public void SetData(ObjectCreature creature)
    {
        m_AText1.text = GameUtils.getString("attribute1name");
        m_AText2.text = GameUtils.getString("attribute2name");
        m_AText3.text = GameUtils.getString("attribute3name");
        m_AText4.text = GameUtils.getString("attribute4name");
        m_AText5.text = GameUtils.getString("attribute5name");
        m_AText6.text = GameUtils.getString("attribute6name");
        m_AText7.text = GameUtils.getString("attribute7name");
        m_AText8.text = GameUtils.getString("attribute8name");
        m_AText9.text = GameUtils.getString("attribute9name");
        m_AText10.text = GameUtils.getString("attribute10name");
        m_AText11.text = GameUtils.getString("attribute11name");
        m_AText12.text = GameUtils.getString("attribute12name");

        m_BText1.text = GameUtils.getString("attribute1name");
        m_BText2.text = GameUtils.getString("attribute2name");
        m_BText3.text = GameUtils.getString("attribute3name");
        m_BText4.text = GameUtils.getString("attribute4name");
        m_BText5.text = GameUtils.getString("attribute5name");
        m_BText6.text = GameUtils.getString("attribute6name");
        m_BText7.text = GameUtils.getString("attribute7name");
        m_BText8.text = GameUtils.getString("attribute8name");
        m_BText9.text = GameUtils.getString("attribute9name");
        m_BText10.text = GameUtils.getString("attribute10name");
        m_BText11.text = GameUtils.getString("attribute11name");
        m_BText12.text = GameUtils.getString("attribute12name");

        m_AValue1.text = creature.GetPhysicalAttack().ToString();
        m_AValue2.text = creature.GetPhysicalDefence().ToString();
        m_AValue3.text = creature.GetMaxHP().ToString();
        m_AValue4.text = creature.GetHit().ToString();
        m_AValue5.text = creature.GetDodge().ToString();
        m_AValue6.text = creature.GetCritical().ToString();
        m_AValue7.text = creature.GetTenacity().ToString();
        m_AValue8.text = creature.GetBlockRate().ToString();
        m_AValue9.text = creature.GetPierceRate().ToString();
        m_AValue10.text = creature.GetHurtAddRate().ToString();
        m_AValue11.text = creature.GetHurtReduceRate().ToString();
        m_AValue12.text = creature.GetCriticalHurtRate().ToString();

        m_BValue1.text = GameUtils.getString("attribute1des");
        m_BValue2.text = GameUtils.getString("attribute2des");
        m_BValue3.text = GameUtils.getString("attribute3des");
        m_BValue4.text = GameUtils.getString("attribute4des");
        m_BValue5.text = GameUtils.getString("attribute5des");
        m_BValue6.text = GameUtils.getString("attribute6des");
        m_BValue7.text = GameUtils.getString("attribute7des");
        m_BValue8.text = GameUtils.getString("attribute8des");
        m_BValue9.text = GameUtils.getString("attribute9des");
        m_BValue10.text = GameUtils.getString("attribute10des");
        m_BValue11.text = GameUtils.getString("attribute11des");
        m_BValue12.text = GameUtils.getString("attribute12des");
    }
}
