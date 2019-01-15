using UnityEngine;
using System.Collections;
using DreamFaction.SkillCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using System.Collections.Generic;
public class ImpactLogic
{
    /// <summary>
    /// //每(param_1)点现有生命值转换(param_2)点(param_3)--属性枚举,(param_4)点(param_5)--属性枚举,(param_6)点(param_7)--属性枚举,
    /// (param_8)点(param_9)--属性枚举,(param_10)点(param_11)--属性枚举,(param_12)点(param_13)--属性枚举
    /// </summary>
    public static bool DoLogic101(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            long nCurHp = pHolder.GetHP();
            if ((nCurHp > 0) && (pImpactRow.getParam()[0] > 0))
            {
                int nChangeValue = (int)(nCurHp / pImpact.GetParam(0));
                for (int i = 1; i < 11; i = i + 2)
                {
                    if (pImpactRow.getParam()[i] > 0)
                    {
                        EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 1]);
                        if (nExtendAttribute != EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID)
                        {
                            nChangeValue *= pImpact.GetParam(i);
                            if (nChangeValue > 0)
                            {
                                pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                                pImpact.AddAttributeEffectRefix(nExtendAttribute, nChangeValue);
                            }
                        }
                    }
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //每损失(param_1)点生命值转换(param_2)点(param_3)--属性枚举,(param_4)点(param_5)--属性枚举,(param_6)点(param_7)--属性枚举,(param_8)点(param_9)--属性枚举,
    //(param_10)点(param_11)--属性枚举,(param_12)点(param_13)--属性枚举
    /// </summary>
    public static bool DoLogic102(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);

            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //每损失(param_1)‰生命值转换(param_2)点(param_3)--属性枚举,(param_4)点(param_5)--属性枚举,(param_6)点(param_7)--属性枚举,(param_8)点(param_9)--属性枚举,
    //(param_10)点(param_11)--属性枚举,(param_12)点(param_13)--属性枚举
    /// </summary>
    public static bool DoLogic103(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //增减(param_1)点(param_2)--属性枚举
    /// </summary>
    public static bool DoLogic104(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        //if (pHolder is ObjectHero)
        //{
            //Debug.Log("当前  #：" + pHolder.GetSpeed() + "*：" + pHolder.GetMagicAttack());
            LogManager.LogAssert(pHolder);
            LogManager.LogAssert(pImpact);
            BuffTemplate pImpactRow = pImpact.GetImpactRow();
            LogManager.LogAssert(pImpactRow);
            if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
            {
                for (int i = 0; i < pImpactRow.getParam().Length - 2; i = i + 2)
                {
                    if (pImpact.GetParam(i + 1) > 0)
                    {
                        EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpact.GetParam(i + 1));
                        //Debug.Log(nExtendAttribute);
                        if (nExtendAttribute != EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID)
                        {
                            int nChangeValue = pImpact.GetParam(i);
                            //nChangeValue 为+-值
                            pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                            pImpact.AddAttributeEffectRefix(nExtendAttribute, nChangeValue);
                        }
                    }
                }
                //Debug.Log("add 104     #：" + pHolder.GetSpeed() + "*：" + pHolder.GetMagicAttack());
            }
            else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
            {
                for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
                {
                    if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                    {
                        pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                            pImpact.m_AttributeEffectRefix[i].m_Value,
                            EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                            true);
                        int nChangeValue = pImpact.GetParam(0);
                        pImpact.m_AttributeEffectRefix[i].m_Value -= nChangeValue;
                    }
                }
                //Debug.Log("remove 104           删除后  #：" + pHolder.GetSpeed() + "*：" + pHolder.GetMagicAttack());
            }
        //}
        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰点(param_2)--属性枚举
    /// </summary>
    public static bool DoLogic105(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            for (int i = 0; i < 11; i = i + 2)
            {
                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 1]);
                int nValue = pHolder.GetAttribute(pImpactRow.getParam()[i + 1]);
                if (nValue > 0)
                {
                    int nChangeValue = (int)((nValue * pImpact.GetParam(i)) / 1000.0f);
                    if (nChangeValue > 0)
                    {
                        pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                        pImpact.AddAttributeEffectRefix(nExtendAttribute, nChangeValue);
                    }
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰点(param_2)--属性枚举，增减幅度通过(param_3)和(param_4)类型判断
    /// </summary>
    public static bool DoLogic106(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
        }
        return true;
    }
    /// <summary>
    /// 瞬间将一定量(param_3)--属性枚举转给效果释放者。双方有任意单位死亡后是否释放者是否消除次BUFF参数根据判断。
    /// </summary>
    public static bool DoLogic107(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            SceneObjectManager pScene = SceneObjectManager.GetInstance();
            LogManager.LogAssert(pScene);
            X_GUID casterguid = pImpact.GetCaster();//释放者GUID
            ObjectCreature pCasterObject = pScene.GetObjectHeroByGUID(casterguid);
            if ((pCasterObject == null) || (!pCasterObject.IsAlive()))
            {
                pImpact.OnDisappear();
            }
            Impact pCastImpact = pCasterObject.AddImpactDirectly(pImpact.GetImpactID(), pCasterObject, false, pImpact.GetSpellID());
            LogManager.LogAssert(pCastImpact);
            //pCastImpact.SetPartner(pImpact);
            pCastImpact.SetIntervalTime(-1);
            //pImpact.SetPartner(pCastImpact);
            pImpact.SetIntervalTime(pImpactRow.getBuffEffectInterval());

            pCasterObject.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEFORE_DEAD);
            if (pImpactRow.getBuffEffectType() == 1)
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            //只有被施加的才会走这里
            for (int i = 0; i < 12; i = i + 4)
            {
                if (pImpactRow.getParam()[0] > 0)
                {
                    if (pImpactRow.getParam()[i + 1] == -1)
                        break;
                    if (pImpact.GetElapsedTime() <= pImpact.GetParam(i))
                    {
                        EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 1]);
                        //自己降低属性
                        pHolder.ChangeEffect(nExtendAttribute, pImpactRow.getParam()[i], EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                        pImpact.AddAttributeEffectRefix(nExtendAttribute, pImpactRow.getParam()[i]);
                        //释放者增加属性
                        X_GUID casterguid = pImpact.GetCaster();
                        SceneObjectManager pScene = SceneObjectManager.GetInstance();
                        LogManager.LogAssert(pScene);
                        ObjectCreature pCasterObject = pScene.GetObjectHeroByGUID(casterguid);
                        if ((pCasterObject == null) || (!pCasterObject.IsAlive()))
                        {
                            pImpact.OnDisappear();
                            Impact pCastImpact = pImpact.GetPartner();
                            LogManager.LogAssert(pCastImpact);
                            pCastImpact.OnDisappear();
                        }
                        else
                        {
                            //目标增加属性
                            Impact pCastImpact = pImpact.GetPartner();
                            LogManager.LogAssert(pCastImpact);
                            pCastImpact.OnDisappear();
                            pCasterObject.ChangeEffect(nExtendAttribute, -pImpactRow.getParam()[i], EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                            pCastImpact.AddAttributeEffectRefix(nExtendAttribute, -pImpactRow.getParam()[i]);
                        }
                    }
                }
            }

        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            Impact pParner = pImpact.GetPartner();
            if (pParner != null)
            {
                pParner.SetPartner(null);
                X_GUID casterguid = pImpact.GetCaster();
                X_GUID holderguid = pHolder.GetGuid();
                if ((pParner == null) && (pParner.IsActive()) && (casterguid == holderguid))
                {
                    pParner.OnDisappear();
                }
                //pImpact.SetPartner(null);
            }

            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }

            //释放者增加属性
            X_GUID _casterguid = pImpact.GetCaster();
            SceneObjectManager pScene = SceneObjectManager.GetInstance();
            LogManager.LogAssert(pScene);
            ObjectCreature pCasterObject = pScene.GetObjectHeroByGUID(_casterguid);
            if ((pCasterObject == null) || (!pCasterObject.IsAlive()))
            {
                pImpact.OnDisappear();
                Impact pCastImpact = pImpact.GetPartner();
                LogManager.LogAssert(pCastImpact);
                pCastImpact.OnDisappear();
            }
            else
            {
                //目标减少属性
                for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
                {
                    if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                    {
                        pCasterObject.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                            pImpact.m_AttributeEffectRefix[i].m_Value,
                            EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                            true);
                    }
                }
            }

            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEFORE_DEAD);
        }

        return true;
    }
    /// <summary>
    ///   //每(param_1)点(param_2)--属性枚举 转换(param_3)点(param_4)--属性枚举
    /// </summary>
    public static bool DoLogic108(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            for (int i = 0; i < 11; i = i + 4)
            {
                if (pImpactRow.getParam()[i] == -1)
                {
                    break;
                }
                else
                {
                    EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 1]);
                    int nValue = pHolder.GetAttribute(pImpactRow.getParam()[i + 1]);
                    if ((nValue > 0) && (pImpactRow.getParam()[i] > 0) && (pImpactRow.getParam()[i + 2] > 0))
                    {
                        int nChangeValue = (nValue / pImpact.GetParam(i)) * pImpact.GetParam(i + 2);
                        if (nChangeValue > 0)
                        {
                            EM_EXTEND_ATTRIBUTE nDestExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 3]);
                            pHolder.ChangeEffect(nDestExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                            pImpact.AddAttributeEffectRefix(nDestExtendAttribute, nChangeValue);
                        }
                    }
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //受到(param_2)类型的伤害时增减(param_1)点伤害，生效概率(param_3)‰,概率为-1时表示不进行概率判断。正数代表增加，负数代表减少
    /// </summary>
    public static bool DoLogic109(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }
        return true;
    }
    /// <summary>
    ///   //受到伤害时增减(param_1)‰点(param_2)--伤害类型，生效概率(param_3)‰,概率为-1时表示不进行概率判断。正数代表增加，负数代表减少
    /// </summary>
    public static bool DoLogic110(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }
        return true;
    }
    /// <summary>
    ///   效果持续时吸收(param_1)‰点(param_2)--伤害类型，
    ///   生命盾拥有一定生命，生命盾被击破则激活技能(param_3)，生命盾未被击破则将剩余生命盾值*(param_7)/1000增加至当前生命值上。
    ///   (param_3)为-1时表示不调用技能
    /// </summary>
    public static bool DoLogic111(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpact.GetParam(3) == 1)
            {
                int nValue = (int)((pHolder.GetMaxHP() * pImpactRow.getParam()[4]) / 1000 + pImpactRow.getParam()[5]);
                pImpact.SetImpactHP(nValue);
            }
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            //恢复血量，且恢复血量不可以超过人物最大血量;
            if (pImpact.GetImpactHp() > 0)
            {
                long totalHp = pHolder.GetHP() + pImpact.GetImpactHp() * pImpact.GetParam(6) / 1000;
                pHolder.SetHP(totalHp > pHolder.GetMaxHP() ? pHolder.GetMaxHP() : totalHp);
            }
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
        }

        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰点(param_2)--伤害类型，增减幅度通过(param_3)和(param_4)类型判断,有几个满足需求就
    /// </summary>
    public static bool DoLogic112(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            SceneObjectManager pScene = SceneObjectManager.GetInstance();
            LogManager.LogAssert(pScene);
            for (int i = 0; i < 11; i = i + 4)
            {
                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i + 1]);
                int nValue = pHolder.GetAttribute(pImpactRow.getParam()[i + 1]);
                if (nValue > 0)
                {
                    int bufferCount = 0;
                    SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                    init.m_Type = pImpactRow.getParam()[i + 2];
                    init.m_ChildType = (int)EM_SPELL_TARGET_SENIOR_TYPE.EM_SEPLL_TARGET_REQUIRE_IMPACTEFFECTTYPE;
                    pScene.ScanByObject(pHolder, ref init);
                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                    {
                        bufferCount = bufferCount + pHolder.GetImpactCountByType(pImpactRow.getParam()[i + 3]);
                    }

                    int nChangeValue = (int)((nValue * bufferCount * pImpact.GetParam(i)) / 1000.0f);
                    if (nChangeValue > 0)
                    {
                        pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                        pImpact.AddAttributeEffectRefix(nExtendAttribute, nChangeValue);
                    }
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //吸收(param_1)‰点(param_2)--伤害类型，生命盾生效(param_3)次，生命盾被击破则给(param_5)敌人造成(param_4)点法术伤害
    /// </summary>
    public static bool DoLogic113(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);

        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[2] > 0)
            {
                pImpact.SetImpactActiveCount(0);
            }
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
        }

        return true;
    }
    /// <summary>
    ///   //受治疗效果增减(param_1)‰
    /// </summary>
    public static bool DoLogic114(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL, pImpact.GetParam(0), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
            pImpact.AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL, pImpact.GetParam(0));
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }

        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰怒气回复
    /// </summary>
    public static bool DoLogic115(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP, 
                pImpact.GetParam(0), 
                EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, 
                false);
            pImpact.AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP, pImpact.GetParam(0));
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }

        return true;
    }
    /// <summary>
    ///   //增减(param_1)点普通攻击获得的怒气
    /// </summary>
    public static bool DoLogic116(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MPNORMALATT, pImpact.GetParam(0), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
            pImpact.AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MPRECOVER, pImpact.GetParam(0));
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }

        return true;
    }
    /// <summary>
    ///   //效果存在时，(param_1)次释放技能不消耗怒气。剩余次数为0时删除buff
    /// </summary>
    public static bool DoLogic117(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                pImpact.SetImpactActiveCount(pImpact.GetParam(0));
                pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_SPELLCONUMEMP);
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE,
                    pImpact.GetParam(0),
                    EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                    false);
                pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_SPELLCONUMEMP);
            }
        }
        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰暴击率，持续(param_2)次。 持续时间到或者达到param_2次技能释放（包含普攻）后该buff / debuff移除，param_2为 - 1时不判断次数
    ///   增加(param_1)‰暴击率，暴击后删除该buff。
    /// </summary>
    public static bool DoLogic118(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                pImpact.SetImpactActiveCount(1);
                pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
                pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE,
                    pImpact.GetParam(0),
                    EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                    false);
                pImpact.AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE, pImpact.GetParam(0));
            }
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
                for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
                {
                    if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                    {
                        pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                            pImpact.m_AttributeEffectRefix[i].m_Value,
                            EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                            true);
                    }
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //增减(param_1)‰暴击伤害率
    /// </summary>
    public static bool DoLogic119(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT, pImpact.GetParam(0), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
            pImpact.AddAttributeEffectRefix(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE, pImpact.GetParam(0));
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        true);
                }
            }
        }
        return true;
    }
    /// <summary>
    ///   //昏迷
    /// </summary>
    public static bool DoLogic1001(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑19
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO, pImpact.GetCaster());
        }

        return true;
    }
    /// <summary>
    ///   //沉默
    /// </summary>
    public static bool DoLogic1002(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑20
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_FORBID, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_FORBID, pImpact.GetCaster());
        }

        return true;
    }
    /// <summary>
    ///   //无法普攻
    /// </summary>
    public static bool DoLogic1003(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑21
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_NONORMAL, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_NONORMAL, pImpact.GetCaster());
        }

        return true;
    }
    /// <summary>
    ///   //定身
    /// </summary>
    public static bool DoLogic1004(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑22
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_IDLE, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_IDLE, pImpact.GetCaster());
        }

        return true;
    }
    /// <summary>
    ///   //嘲讽，强制转换攻击目标
    /// </summary>
    public static bool DoLogic1005(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑23
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_CHAOFENG, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_CHAOFENG, pImpact.GetCaster());
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时有(param_2)‰几率免疫(param_1)效果组内的效果
    /// </summary>
    public static bool DoLogic1006(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑24
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_IMM_IMPACT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_IMM_IMPACT);
        }

        return true;
    }
    /// <summary>
    ///   //持续增减血(param_1)值 / (param_2)毫秒
    /// </summary>
    public static bool DoLogic1101(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑25
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            //if ( pImpactRow.getBuffEffectType () == 1 )
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            int nValue = pImpact.GetParam(0);
            if (nValue > 0)
            {
                pHolder.OnHeal(nValue);
            }
            else if (nValue < 0)
            {
                pHolder.OnDamage(-nValue);
            }
        }

        return true;
    }
    /// <summary>
    ///   //持续增减血生命最大值(param_1)‰/(param_2)毫秒，掉血量不能超过值（param_3）。并将每次增减血量部分的(param_4)‰数值(取绝对值)作为治疗效果给目标(param_5)。
    //(param_3)(param_4)为-1时表示无效。(param_1)值为正时代表加血，(param_1)值为负数时代表减血。通过该效果掉血造成的是直接伤害
    /// </summary>
    public static bool DoLogic1102(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑26
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[2] == 1)
            {
                pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
            }
            if (pImpactRow.getBuffEffectType() == 1)
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            if (pImpactRow.getParam()[2] == 1)
            {
                pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            int nValue = pImpactRow.getParam()[0];
            if (nValue != 0)
            {
                long nMaxHp = pHolder.GetMaxHP();
                if (pHolder is ObjectHero)
                {
                    nValue = (int)((nValue * nMaxHp) * 0.001f);
                }
                else
                {
                    ObjectMonster pMonster = pHolder as ObjectMonster;
                    nValue = (int)(nValue * pMonster.GetMonsterRow().getMonsterPercentMaxHp() * 0.001f);
                }
                if (nValue > 0)
                {
                    pHolder.OnHeal(nValue);
                }
                else
                {
                    nValue = -nValue;
                    pHolder.OnDamage(nValue);
                }
                if (pImpact.GetParam(3) > 0)
                {
                    nValue = (nValue * pImpact.GetParam(3)) / 1000;
                    SceneObjectManager pScene = SceneObjectManager.GetInstance();
                    LogManager.LogAssert(pScene);
                    SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
                    init.m_Type = pImpactRow.getParam()[4];
                    pScene.ScanByObject(pHolder, ref init);
                    for (int j = 0; j < init.m_ObjectList.Count; j++)
                    {
                        init.m_ObjectList[j].OnHeal(nValue);
                    }
                }
            }
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，每隔(param_2)毫秒获得一次(param_1)效果组内的效果，随机获得(param_3)个。(param_3)为 - 1时为buff组内的效果全部获得
    /// </summary>
    public static bool DoLogic1103(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑26
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getBuffEffectType() == 1)
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(pImpactRow.getParam()[0]);
            if (pRow != null)
            {
                if ((pImpactRow.getParam()[2] == -1) || (pImpactRow.getParam()[2] >= pRow.getParam().Length))
                {
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        pHolder.AddImpact(pRow.getParam()[i], pHolder, false, pImpact.GetSpellID());
                    }
                }
                else
                {
                    int nCount = 0;
                    int[] nID = new int[GlobalMembers.MAX_IMPACT_NUMBER];
                    for (int i = 0; i < pRow.getParam().Length; i++)
                    {
                        nID[i] = pRow.getParam()[i];
                    }

                    for (int i = 0; i < pImpactRow.getParam()[2]; i++)
                    {
                        System.Random pRand = new System.Random();
                        int nRand = pRand.Next(0, pRow.getParam().Length - nCount);
                        pHolder.AddImpact(nID[nRand], pHolder, false, pImpact.GetSpellID());
                        nID[nRand] = nID[pRow.getParam().Length - nCount];
                        ++nCount;
                    }
                }
            }

        }

        return true;
    }
    /// <summary>
    ///   //受击时反弹(param_1)‰，并增减(param_2)‰所受伤害，并对反弹目标造成(param_3)点直接伤害，持续(param_4)次。持续时间到或者达到param_4次数后该buff/debuff移除，
    //param_2为-1时不判断次数。正数代表增加，负数代表减少。通过该效果掉血造成的是直接伤害，并且该效果不能反弹直接伤害。
    /// </summary>
    public static bool DoLogic1201(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑27
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
            //if (pImpactRow.getBuffEffectType() == 1)
            //{
            //    pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            //}
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            LogManager.Log("dot");
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
        }
        return true;
    }
    /// <summary>
    ///   //将所造成的伤害(param_1)‰转换成自身生命(吸血)
    /// </summary>
    public static bool DoLogic1301(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑28
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }

        return true;
    }
    /// <summary>
    ///   
    //盾之后,反弹前
    //效果持续时，将所受伤害增减(param_1)‰，并将增减完的伤害分摊到(param_2)中拥有该类效果的相同buffID/debuffID的角色身上。
    //最终伤害为增减完的伤害/数量。参数1为正数时表示对伤害进行增加，是负数时是对伤害缩减。由该效果分摊出去的伤害为直接伤害
    /// </summary>
    public static bool DoLogic1401(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑29
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURTDISTRIBUTE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURTDISTRIBUTE);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，效果拥有者受到物理/法术攻击造成的伤害后，将掉血量的(param_1)‰数值作为直接伤害给其他(param_2)中拥有该buffID的角色身上
    /// </summary>
    public static bool DoLogic1402(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑29
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若效果拥有者死亡，则效果拥有者激活子技能(param_1)
    /// </summary>
    public static bool DoLogic2001(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑29
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEFORE_DEAD);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEFORE_DEAD);
        }

        return true;
    }
    /// <summary>
    ///   //结算后执行
    //前(param_2)次受到物理/法术伤害时激活技能(param_1)。(param_2)为-1时不对次数进行判断
    /// </summary>
    public static bool DoLogic2002(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑30
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，每触发一次闪避则给效果拥有者(param_1)效果组内的效果，最多生效(param_2)次。 持续时间到或者达到param_2次后该buff / debuff移除，param_2为 - 1时不判断次数
    /// </summary>
    public static bool DoLogic2003(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑31
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_DODGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_DODGE);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若对拥有(param_1)效果组的目标造成伤害，则伤害扩大(param_2)‰并附加(param_3)的固定伤害
    /// </summary>
    public static bool DoLogic2004(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑32
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若对拥有(param_1)效果组的目标造成物理、法术伤害，则激活(param_2)技能
    /// </summary>
    public static bool DoLogic2005(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑33
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }

        return true;
    }
    //public static bool				DoLogic2006(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑34
    /// <summary>
    ///   //效果持续时，若效果拥有者(param_3)(param_4)(param_5)(param_6)技能编号里的技能攻击未暴击则调用(param_1)子技能，
    //若效果拥有者攻击暴击则调用(param_2)子技能。其中(param_3)(param_4)(param_5)(param_6)中技能编号里所属的技能为(param_1)和(param_2)的父技能。(param_3)---(param_6)为-1时表示无效
    /// </summary>
    public static bool DoLogic2007(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑35
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若效果拥有者被暴击则调用(param_1)技能
    /// </summary>
    public static bool DoLogic2008(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑36
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BECRITICAL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BECRITICAL);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若效果拥有者受到治疗则调用(param_1)技能
    /// </summary>
    public static bool DoLogic2009(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑37
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，每隔(param_2)毫秒调用一次(param_1)技能
    /// </summary>
    public static bool DoLogic2010(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑38
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            //if (pImpactRow.getBuffEffectType() == 1)
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            //子技能
            if (pImpactRow.getParam()[0] > 0)
            {
                pImpact.OnActiveChildSpell(pImpactRow.getParam()[0],null);
//                 SpellInfo spellinfo = new SpellInfo();
//                 spellinfo.Init(pImpactRow.getParam()[0]);
//                 Spell subspell = new Spell();
//                 subspell.SetHolder(pHolder);
//                 subspell.Init(spellinfo);
//                 subspell.ImmActiveOnce();
            }
        }

        return true;
    }
    /// <summary>
    ///   //移动速度或攻击速度被减慢时，调用(param_1)技能，检测速度减慢的间隔为(param_2)毫秒
    /// </summary>
    public static bool DoLogic2011(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑39
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getBuffEffectType() == 1)
            {
                pImpact.__DoImpactIntervalLogic(pImpactRow.getBuffType());
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            if (pHolder.GetSpeed() < pHolder.GetBaseSpeed())
            {
                if (pImpactRow.getParam()[0] > 0)
                {
                    pImpact.OnActiveChildSpell(pImpactRow.getParam()[0], null);
//                     SpellInfo spellinfo = new SpellInfo();
//                     spellinfo.Init(pImpactRow.getParam()[0]);
//                     Spell subspell = new Spell();
//                     subspell.SetHolder(pHolder);
//                     subspell.Init(spellinfo);
//                     subspell.ImmActiveOnce();
                }
            }
        }

        return true;
    }
    /// <summary>
    ///   //任意目标施放一个技能编号为(param_3)(param_4)(param_5)的技能便获得+1计数，当计数达到(param_1)则激活技能(param_2)。(param_3)(param_4)(param_5)为-1时表示无效
    /// </summary>
    public static bool DoLogic2012(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑40
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            X_GUID casterguid = pImpact.GetCaster();//释放者
            X_GUID holdguid = pHolder.GetGuid();//持有者
            if (casterguid != holdguid)
            {
                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                LogManager.LogAssert(pScene);
                ObjectCreature pCasterObject = pScene.GetObjectHeroByGUID(casterguid);
                if ((pCasterObject == null) || (!pCasterObject.IsAlive()))
                {
                    LogManager.LogAssert(0);
                }
                Impact pCaserImpact = pCasterObject.GetImpactByIDAndCastGuid(pImpact.GetImpactID(), casterguid);
                pCaserImpact.SetImpactActiveCount(0);
                //pImpact.SetPartner(pCaserImpact);
            }

            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            X_GUID casterguid = pImpact.GetCaster();
            X_GUID holdguid = pHolder.GetGuid();
            if (casterguid == holdguid)
            {
                SceneObjectManager pScene = SceneObjectManager.GetInstance();
                LogManager.LogAssert(pScene);
                for (int j = 0; j < pScene.GetObjectHeroCount(); j++)
                {
                    ObjectCreature pTarget = pScene.GetObjectHeroByIndex(j);
                    LogManager.LogAssert(pTarget);
                    if (pTarget.IsAlive())
                    {
                        Impact pCaserImpact = pTarget.GetImpactByIDAndCastGuid(pImpact.GetImpactID(), casterguid);
                        if (pCaserImpact != null)
                        {
                            pCaserImpact.OnDisappear();
                        }
                    }
                }
            }
            else
            {
                //pImpact.SetPartner(null);
            }
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，若效果拥有者施放编号为(param_2)、(param_3)、(param_4)、(param_5)中的父技能，则有(param_6)‰概率激活子技能(param_1)。
    //(param_2)、(param_3)、(param_4)、(param_5)为(param_1)的父技能且为 - 1时表示无效
    /// </summary>
    public static bool DoLogic2013(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑41
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }

        return true;
    }
    /// <summary>
    ///   //效果持续时，(param_1)技能若击杀目标则技能的冷却时间改变为(param_2) 注：正在冷却中的技能不受影响
    /// </summary>
    public static bool DoLogic2014(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑41
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_KILLTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_KILLTARGET);
        }

        return true;
    }
    /// <summary>
    ///   //当前生命跨越(param_1)‰时激活子技能，大于等于(param_1)激活技能(param_2)，否则激活(param_3)技能。(param_2）与(param_3）不能共存，一个生效时删除另外一个
    /// </summary>
    public static bool DoLogic2015(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑42
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                int _maxHp = (int)pHolder.GetMaxHP();
                int _curHp = (int)pHolder.GetHP();
                int _value = (int)(_curHp / (_maxHp * 1000.0f));
                if (_value >= pImpactRow.getParam()[0])
                {
                    if (pImpactRow.getParam()[1] > 0)
                    {
//                         SpellInfo spellinfo = new SpellInfo();
//                         spellinfo.Init(pImpactRow.getParam()[1]);
//                         Spell subspell = new Spell();
//                         subspell.SetHolder(pHolder);
//                         subspell.Init(spellinfo);
//                         subspell.ImmActiveOnce();
                        pImpact.OnActiveChildSpell(pImpactRow.getParam()[1], null);
                        pImpact.OnRemove();
                    }
                }
                else
                {
                    if (pImpactRow.getParam()[2] > 0)
                    {
//                         SpellInfo spellinfo = new SpellInfo();
//                         spellinfo.Init(pImpactRow.getParam()[2]);
//                         Spell subspell = new Spell();
//                         subspell.SetHolder(pHolder);
//                         subspell.Init(spellinfo);
//                         subspell.ImmActiveOnce();
                        pImpact.OnActiveChildSpell(pImpactRow.getParam()[2], null);
                        pImpact.OnRemove();
                    }
                }
            }
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_SELFHPCHANGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_SELFHPCHANGE);
        }

        return true;
    }
    /// <summary>
    ///   //效果存在时，有(param_2)‰几率将新加入的buff/debuff某个参数扩大(param_1)‰倍。(param_3)效果组内的是修改持续时间参数，
    //(param_4)效果组内的是修改对应效果组内的(param_1)参数，(param_5)效果组内的是修改对应效果组内的(param_2)参数，
    //(param_6)效果组内的是修改对应效果组内的(param_3)参数，(param_7)效果组内的是修改对应效果组内的(param_4)参数，
    //(param_8)效果组内的是修改对应效果组内的(param_5)参数，(param_9)效果组内的是修改对应效果组内的(param_6)参数。(param_3)---(param_9)为-1时无效
    /// </summary>
    public static bool DoLogic2016(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑43
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);

        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ADDIMPACT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_ADDIMPACT);
        }
        return true;
    }
    /// <summary>
    ///   //每受一个debuff则用自身随机一个buff抵消，自身无buff可用来抵消则激活技能(param_1)
    /// </summary>
    public static bool DoLogic2017(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑44
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }

        return true;
    }
    /// <summary>
    ///   //效果存在时，若有单位对该效果拥有者造成物理/法术伤害，则将该次伤害的(param_1)‰数值作为治疗效果给伤害制造者
    /// </summary>
    public static bool DoLogic2018(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑45
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEKILL);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEKILL);
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }

        return true;
    }
    /// <summary>
    ///   //效果存在时，下(param_1)次造成的物理/法术伤害增减(param_2)‰。次数为0时删除该buff
    /// </summary>
    public static bool DoLogic2019(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑46
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);

        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(pImpactRow.getParam()[0]);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTATTACK_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTATTACK_HURT);
        }
        return true;
    }
    /// <summary>
    ///   //变蛋 - 复活。蛋的血量为(param_1)‰生命上限，若蛋在(param_2)毫秒内没被击破，则英雄复活后的血量为(param_3)‰，蛋的模型ID为(param_4)
    /// </summary>

    public static bool DoLogic2020(ObjectCreature pHolder, Impact pImpact, int nFlag)									//逻辑47
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);

        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            int nHp = (int)((pHolder.GetMaxHP() * pImpactRow.getParam()[0]) / 1000);
            if (nHp <= 0)
            {
                nHp = 1;
            }
            pHolder.SetHP(nHp);
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_EGG, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_EGG, pImpact.GetCaster());
        }
        return true;
    }
    /// <summary>
    /// //效果持续时技能每命中一个目标会使此技能CD改变（最小变至0）
    /// </summary>
    public static bool DoLogic2021(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);

        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        return true;
    }
    /// <summary>
    /// 效果持续时不会受到特定类型伤害，并在效果结束时改变自己当前生命
    /// </summary>
    public static bool DoLogic2022(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERABSORB_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERABSORB_HURT);
            int nValue = pImpact.GetParam();
            if (pImpactRow.getParam()[3] == 1)
            {
                nValue = (nValue * pImpactRow.getParam()[4]) / 1000;
            }
            if (pImpactRow.getParam()[2] == 1)
            {
                pHolder.OnDamage(nValue);
            }
            else if (pImpactRow.getParam()[2] == 2)
            {
                pHolder.OnHeal(nValue);
            }
        }
        return true;
    }
    public static bool DoLogic2023(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        return true;
    }
    public static bool DoLogic2024(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        return true;
    }
    public static bool DoLogic2025(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] > 0)
            {
                int nRand = Random.Range(1, 420);
                if (nRand < pImpactRow.getParam()[0])
                {
//                     SpellInfo spellinfo = new SpellInfo();
//                     spellinfo.Init(pImpactRow.getParam()[1]);
//                     Spell subspell = new Spell();
//                     subspell.SetHolder(pHolder);
//                     subspell.Init(spellinfo);
//                     subspell.ImmActiveOnce();
                    pImpact.OnActiveChildSpell(pImpactRow.getParam()[1], null);
                    pImpact.OnRemove();
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            LogManager.Log("Remove 2025");
        }
        return true;
    }
    public static bool DoLogic2026(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_NOHITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_NOHITTARGET);
        }
        return true;
    }

    /// <summary>
    /// 效果持续时受到特定伤害则几率对指定单位反馈BUFF
    /// </summary>
    public static bool DoLogic2028(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
        }
        return true;
    }
    /// <summary>
    /// 效果持续时拥有者属性改变直至特定条件清除此BUFF
    /// </summary>
    public static bool DoLogic2029(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }
        return true;
    }
    /// <summary>
    /// //效果持续时拥有者属性改变直至特定条件清除此BUFF
    /// </summary>
    public static bool DoLogic2030(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            for (int i = 4; i < 7; i = i + 2)
            {
                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[i]);
                int nValue = pHolder.GetAttribute(pImpactRow.getParam()[i + 1]);
                if (nValue > 0)
                {
                    pHolder.ChangeEffect(nExtendAttribute, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                    pImpact.AddAttributeEffectRefix(nExtendAttribute, nValue);
                }
            }
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType,
                        pImpact.m_AttributeEffectRefix[i].m_Value,
                        EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT,
                        false);
                }
            }
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_USESPELL);
        }
        return true;
    }
    //效果持续时拥有者伤害免疫
    public static bool DoLogic2031(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.SetFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_IMM, pImpact.GetCaster());
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.RemoveFightState((int)EM_FIGHT_STATE.EM_FIGHT_STATE_IMM, pImpact.GetCaster());
        }
        return true;
    }
    //效果持续时会记录目标被治疗的次数，当达到特定条件时对目标造成直接伤害
    public static bool DoLogic2032(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
            pImpact.SetImpactActiveCount(0);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
        }
        return true;
    }
    //效果持续时获得特定BUFF组的BUFF时会几率对指定单位反馈BUFF
    public static bool DoLogic2033(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
            pImpact.SetImpactActiveCount(0);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
        }
        return true;
    }
    public static bool DoLogic2034(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        return true;
    }
    public static bool DoLogic2035(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        return true;
    }
    public static bool DoLogic2036(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        }
        return true;
    }

    //效果持续时延缓己方目标所有伤害和治疗，并于BUFF结束时一起结算
    public static bool DoLogic2039(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURTORHEALDELAY);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {


            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURTORHEALDELAY);
        }
        return true;
    }
    //效果持续时改变属性，同时生命流失
    public static bool DoLogic2040(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[2] > 0)
            {
                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[2]);
                int nChangeValue = pHolder.GetAttribute(pImpactRow.getParam()[3]);
                if (nChangeValue > 0)
                {
                    pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                }
            }
            if (pImpactRow.getParam()[4] > 0)
            {
                EM_EXTEND_ATTRIBUTE nExtendAttribute = Impact.GetExtendAttribute(pImpactRow.getParam()[4]);
                int nChangeValue = pHolder.GetAttribute(pImpactRow.getParam()[5]);
                if (nChangeValue > 0)
                {
                    pHolder.ChangeEffect(nExtendAttribute, nChangeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_DOT)
        {
            if (pImpactRow.getParam()[0] == 1)
            {
                List<SPELL_EVENT> pList = pHolder.GetSpellEventQueue().GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
                if (pList != null)
                {
                    foreach (var item in pList)
                    {
                        Impact nImpact = item.m_pImpact;
                        if (nImpact != null)
                        {
                            long nValue = pHolder.GetHP();
                            //nValue = nValue - (nValue * pImpactRow.getParam()[1]) / 1000;
                            //pHolder.SetHP(nValue);
                            nImpact.m_HurtCount += (int)(nValue * pImpactRow.getParam()[1]) / 1000;
                            //nImpact.DoPassvityImpactLogic(nImpact.GetImpactID());
                        }
                    }
                }
            }
            else if (pImpactRow.getParam()[0] == 2)
            {
                List<SPELL_EVENT> pList = pHolder.GetSpellEventQueue().GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
                if (pList != null)
                {
                    foreach (var item in pList)
                    {
                        Impact nImpact = item.m_pImpact;
                        if (nImpact != null)
                        {
                            long nValue = pHolder.GetMaxHP();
                            //nValue = nValue - (nValue * pImpactRow.getParam()[1]) / 1000;
                            //pHolder.SetHP(nValue);
                            nImpact.m_HurtCount += (int)(nValue * pImpactRow.getParam()[1]) / 1000;
                        }
                    }
                }
            }
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            for (int i = 0; i < pImpact.m_AttributeEffectRefixCount; i++)
            {
                if (pImpact.m_AttributeEffectRefix[i].m_Value != 0)
                {
                    pHolder.ChangeEffect((EM_EXTEND_ATTRIBUTE)pImpact.m_AttributeEffectRefix[i].m_AttrType, pImpact.m_AttributeEffectRefix[i].m_Value, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT, false);
                }
            }
        }
        return true;
    }

    // 效果持续时受到普通攻击伤害时几率增减伤害，正数代表增加，负数代表减少
    public static bool DoLogic2041(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        }

        return true;
    }

    // 效果持续时受到伤害后当前生命低于(param_1)‰时调用(param_2)子技能
    public static bool DoLogic2042(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        }
        LogManager.LogAssert(pImpactRow);
        return true;
    }

    // 效果持续时使用指定技能编号技能击杀敌人后获得怒气量改变
    public static bool DoLogic2043(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_KILLTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_KILLTARGET);
        }
        LogManager.LogAssert(pImpactRow);
        return true;
    }

    // 效果持续时使用普通攻击命中则几率对指定目标添加指定buff
    public static bool DoLogic2044(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        LogManager.LogAssert(pImpactRow);
        return true;
    }

    //效果持续时，每触发一次被闪避则激活技能(param_1)，最多生效(param_2)次。 持续时间到或者达到param_2次后该buff/debuff移除，param_2为-1时不判断次数
    public static bool DoLogic2045(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            pImpact.SetImpactActiveCount(0);
            pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_NOHITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_NOHITTARGET);
        }
        LogManager.LogAssert(pImpactRow);
        return true;
    }

    // 效果持续时，对敌方造成伤害几率无视防御
    public static bool DoLogic2046(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] != -1)
                pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
        }
        LogManager.LogAssert(pImpactRow);
        return true;
    }

    // 效果持续时指定ID的技能造成的伤害提升（实际伤害=原伤害*（1+X/1000））
    public static bool DoLogic2047(ObjectCreature pHolder, Impact pImpact, int nFlag)
    {
        LogManager.LogAssert(pHolder);
        LogManager.LogAssert(pImpact);
        BuffTemplate pImpactRow = pImpact.GetImpactRow();
        LogManager.LogAssert(pImpactRow);
        if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_ADD)
        {
            if (pImpactRow.getParam()[0] != -1)
                pHolder.RegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        else if (nFlag == (int)EM_IMPACT_FLAG.EM_IMPACT_FLAG_REMOVE)
        {
            pHolder.UnRegisterSpellEvent(pImpact, SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
        }
        return true;
    }

}
