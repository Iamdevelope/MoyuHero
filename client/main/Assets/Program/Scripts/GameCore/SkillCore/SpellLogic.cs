using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DreamFaction.SkillCore;
using DreamFaction.GameNetWork.Data;
using DreamFaction.LogSystem;
using DreamFaction.GameCore;
public class SpellLogic
{
    public static int PhysicalAttack(ObjectCreature pCaster, ObjectCreature pTarget, int nPhysicalAttack, bool bCritical, SkillTemplate pRow = null)				//物理攻击
    {
		LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);
//         float fHurt = 0f;
//         float fParamA = 0f;
//         float fParamB = 0f; //攻击方物理攻击 ^ 物理伤害参数G / ( 攻击方物理攻击 * 物理伤害参数H +防御方物理防御 * 物理伤害参数I ) * ( 1 + 攻击方物伤加成率 - 防御方物伤减免率)
//         float fParamC = 0f; //攻击方阵营对防御方阵营攻击系数 / 100 * ( 1 + 攻击方对防御方阵营伤害加成率 - 防御方对攻击方阵营伤害减免率 )
//         float fParamD = 0f; //1 + 攻击方对防御方距离类型伤害加成率 - 防御方受攻击方距离类型伤害减免
//         float fParamE = 0f; //1 + 防御方是BOSS怪物 ? 攻击方对BOSS伤害加成率:0  - 攻击方是BOSS怪物 ? 防御方对BOSS伤害减免率:0
//         float fParamF = 0f; //Random(伤害浮动系数下限V，伤害浮动系数上限W) 
//         float fParamG = 0f; //攻击方伤害附加值 - 防御方伤害减免值
//         float fParamH = 0f; //MAX( 1 , Critical_base_power + 攻击方暴击伤害加成率 - 防御方暴击伤害减免率 )
// 
// 	    float nParam1 = DataTemplate.GetInstance().m_GameConfig.getPhysicalAttackG();
//         float nParam2 = DataTemplate.GetInstance().m_GameConfig.getPhysicalAttackH();
//         float nParam3 = DataTemplate.GetInstance().m_GameConfig.getPhysicalAttackI();
// 	    int nParam4 = (int)(DataTemplate.GetInstance().m_GameConfig.getDamageOffloatingMin()*100.0f);
// 	    int nParam5 = (int)(DataTemplate.GetInstance().m_GameConfig.getDamageOffloatingMax()*100.0f);
// 
//         //fParamA = (float)nPhysicalAttack;//技能计算取值 [7/20/2015 Zmy]
// 
//         int nCasterImpactAddHurtPermil = 0;//攻击方伤害值的buff处理(不影响技能计算取值，返回加成百分比)[7/23/2015 Zmy]
//         pCaster.GetAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, pTarget, ref nCasterImpactAddHurtPermil,ref nPhysicalAttack);
// 
//         int nTargetImpactBeHurtPermil = 0;// 防御方的伤害buff处理 [7/23/2015 Zmy]
//         pTarget.GetBeAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, ref nPhysicalAttack, ref nTargetImpactBeHurtPermil);
// 
//         float fImpactEffectParam = nCasterImpactAddHurtPermil / 1000f + nTargetImpactBeHurtPermil / 1000f; // buff影响的加成率 [7/23/2015 Zmy]
// 
//         fParamA = (float)nPhysicalAttack;//技能计算取值 [7/20/2015 Zmy]
// 
//         fParamB = (float)(System.Math.Pow(pCaster.GetPhysicalAttack(), nParam1)) / (pCaster.GetPhysicalAttack() * nParam2 + pTarget.GetPhysicalDefence() * nParam3) * (1f + pCaster.GetPhysicalHurtAddPermil() - pTarget.GetPhysicalHurtReducePermil() + fImpactEffectParam);
// 
//         fParamC = pCaster.GetCampAttackParam(pTarget) / 100f * (1f + pCaster.GetCampAddDamageRate(pTarget) - pTarget.GetCampReducDamageRate(pCaster));
// 
//         fParamD = 1f + pCaster.GetAddDamageRateForAttackMode(pTarget) - pTarget.GetReducDamageRateForAttackMode(pCaster);
// 
//         fParamE = 1f + pCaster.GetAddDamageRateForBossType(pTarget) - pTarget.GetReducDamageRateForBossType(pCaster);
// 
//         System.Random ran = new System.Random();
//         int nRand = ran.Next(nParam4, nParam5);
//         fParamF = nRand / 100f;
// 
//         fParamG = pCaster.GetExtraHurt() - pTarget.GetReduceHurtPoint();
// 
// 	    if (!bCritical)
// 	    {
//             // 一般物理伤害 = MAX( 1 , a * b * c * d * e * f + g ) [7/20/2015 Zmy]
//             fHurt = Mathf.Max(1f, fParamA * fParamB * fParamC * fParamD * fParamE * fParamF + fParamG);
// 		    return (int)fHurt;
// 	    }
// 	    else
// 	    {
//             fParamH = Mathf.Max(1f, pCaster.GetCriticalHurtAddRate());
// 
//             // 暴击物理伤害 = MAX( 1 , a * b * c * d * e * h * f + g ) [7/20/2015 Zmy]
//             fHurt = Mathf.Max(1f, fParamA * fParamB * fParamC * fParamD * fParamE * fParamF * fParamH+ fParamG);
//             return (int)fHurt;
// 	    }
        // 喵了个蛋 [10/16/2015 Zmy]
        float fHurt = 0f;
        float nParamX = DataTemplate.GetInstance().m_GameConfig.getAtt_cor_para();
        float nParamY = DataTemplate.GetInstance().m_GameConfig.getDef_cor_para();
        float nParamZ = DataTemplate.GetInstance().m_GameConfig.getGua_val();

        float _leftVaule    = pCaster.GetPhysicalAttack() + pRow.getDmgfixvalue();
        float _rightValue   = pTarget.GetPhysicalDefence() * 1.1f;
        if ( _leftVaule > _rightValue)
        {
            //非暴击命中伤害=(攻击力/X+技能伤害固定值-目标防御力/Y)*(1+伤害加成率/1000-目标伤害减免率/1000+技能伤害系数）  [10/16/2015 Zmy]
//             float _a = pCaster.GetPhysicalAttack() / nParamX;
//             float _b = pTarget.GetPhysicalAttack() / nParamY;
//             float _c = pCaster.GetHurtAddRate() / 1000f;
//             float _d = pTarget.GetHurtReduceRate() / 1000f;
//             float _f = (_a + pRow.getDmgfixvalue() - _b);
//             float _g = (1 + _c - _d + pRow.getSpelldmgparam());
//             fHurt = _f * _g;
            fHurt = (pCaster.GetPhysicalAttack() / nParamX + pRow.getDmgfixvalue() - pTarget.GetPhysicalDefence() / nParamY) * (1 + pCaster.GetHurtAddRate() / 1000f - pTarget.GetHurtReduceRate() / 1000f + pRow.getSpelldmgparam());
        }
        else
        {
            //非暴击命中伤害 =(攻击力/Y)*（1+伤害加成率/1000-目标伤害减免率/1000+技能伤害系数)*保底值系数  [10/16/2015 Zmy]
            fHurt = (pCaster.GetPhysicalAttack() / nParamY) * (1 + pCaster.GetHurtAddRate() / 1000f - pTarget.GetHurtReduceRate() / 1000f + pRow.getSpelldmgparam()) * nParamZ;
        }

        // 暴击命中伤害=非暴击命中伤害*暴击伤害率 [10/16/2015 Zmy]
        if (bCritical)
        {
            fHurt *= pCaster.GetCriticalHurtRate();
        }

        //随机伤害修正 [10/16/2015 Zmy]
        int iRnd = System.DateTime.Now.Millisecond;
        System.Random randomCoor = new System.Random(iRnd);
        int nRand = randomCoor.Next(900, 1100);
        fHurt *= (float)(nRand / 1000f);

        int nBlockValue = pTarget.GetBlockRate();
        if (nBlockValue <= 0)
            nBlockValue = 0;

        if (nBlockValue > 1000)
            nBlockValue = 1000;

        //格挡伤害修正 [10/16/2015 Zmy]
        int iRnd0 = System.DateTime.Now.Millisecond;
        System.Random randomCoor0 = new System.Random(iRnd0);
        int nRand0 = randomCoor.Next(1, 1000);

        if (nRand0 <= nBlockValue)
        {
            fHurt = Mathf.Max(1f, fHurt * 0.7f);
        }

        return (int)fHurt;
    }
    public static int MagicAttack(ObjectCreature pCaster, ObjectCreature pTarget, int nMagicAttack, bool bCritical) 					//法术攻击
    {
        LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);

        float fHurt = 0f;
        float fParamA = 0f;
        float fParamB = 0f; //攻击方法术攻击 ^ 法术伤害参数J / ( 攻击方法术攻击 * 法术伤害参数K +防御方法术防御 * 法术伤害参数L ) * ( 1 + 攻击方法伤加成率 + 攻击方buff加成率 - 防御方法伤减免率 - 防御方buff减免率 )
        float fParamC = 0f; //攻击方阵营对防御方阵营攻击系数 / 100 * ( 1 + 攻击方对防御方阵营伤害加成率 - 防御方对攻击方阵营伤害减免率 )
        float fParamD = 0f; //1 + 攻击方对防御方距离类型伤害加成率 - 防御方受攻击方距离类型伤害减免
        float fParamE = 0f; //1 + 防御方是BOSS怪物 ? 攻击方对BOSS伤害加成率:0  - 攻击方是BOSS怪物 ? 防御方对BOSS伤害减免率:0
        float fParamF = 0f; //Random(伤害浮动系数下限V，伤害浮动系数上限W) 
        float fParamG = 0f; //攻击方伤害附加值 - 防御方伤害减免值
        float fParamH = 0f; //MAX( 1 , Critical_base_power + 攻击方暴击伤害加成率 - 防御方暴击伤害减免率 )

	    float nParam1 = DataTemplate.GetInstance().m_GameConfig.getMagicAttackJ();
        float nParam2 = DataTemplate.GetInstance().m_GameConfig.getMagicAttackK();
        float nParam3 = DataTemplate.GetInstance().m_GameConfig.getMagicAttackL();
	    int nParam4 = (int)(DataTemplate.GetInstance().m_GameConfig.getDamageOffloatingMin()*100.0f);
	    int nParam5 = (int)(DataTemplate.GetInstance().m_GameConfig.getDamageOffloatingMax()*100.0f);

        int nCasterImpactAddHurtPermil = 0;//攻击方伤害值的buff处理(不影响技能计算取值，返回加成百分比)[7/23/2015 Zmy]
        pCaster.GetAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, pTarget, ref nCasterImpactAddHurtPermil,ref nMagicAttack);

        int nTargetImpactBeHurtPermil = 0;// 防御方的伤害buff处理 [7/23/2015 Zmy]
        pTarget.GetBeAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, ref nMagicAttack, ref nTargetImpactBeHurtPermil);

        float fImpactEffectParam = nCasterImpactAddHurtPermil / 1000f + nTargetImpactBeHurtPermil / 1000f; // buff影响的加成率 [7/23/2015 Zmy]

        fParamA = (float)nMagicAttack;//技能计算取值 [7/20/2015 Zmy]

        fParamB = (float)(System.Math.Pow(pCaster.GetMagicAttack(), nParam1)) / (pCaster.GetMagicAttack() * nParam2 + pTarget.GetMagicDefence() * nParam3) * (1f + pCaster.GetMagicHurtAddPermil() - pTarget.GetMagicHurtReducePermil() + fImpactEffectParam);

        fParamC = pCaster.GetCampAttackParam(pTarget) / 100f * (1f + pCaster.GetCampAddDamageRate(pTarget) - pTarget.GetCampReducDamageRate(pCaster));

        fParamD = 1f + pCaster.GetAddDamageRateForAttackMode(pTarget) - pTarget.GetReducDamageRateForAttackMode(pCaster);

        fParamE = 1f + pCaster.GetAddDamageRateForBossType(pTarget) - pTarget.GetReducDamageRateForBossType(pCaster);

        System.Random ran = new System.Random();
        int nRand = ran.Next(nParam4, nParam5);
        fParamF = nRand / 100f;

        fParamG = pCaster.GetExtraHurt() - pTarget.GetReduceHurtPoint();

        if (!bCritical)
	    {
            // 一般法术伤害 = MAX( 1 , a * b * c * d * e * f + g ) [7/20/2015 Zmy]
            fHurt = Mathf.Max(1f, fParamA * fParamB * fParamC * fParamD * fParamE * fParamF + fParamG);
		    return (int)fHurt;
	    }
	    else
	    {
            fParamH = Mathf.Max(1f, pCaster.GetCriticalHurtAddRate());

            // 暴击物理伤害 = MAX( 1 , a * b * c * d * e * h * f + g ) [7/20/2015 Zmy]
            fHurt = Mathf.Max(1f, fParamA * fParamB * fParamC * fParamD * fParamE * fParamF * fParamH + fParamG);
		    return (int)fHurt;
	    }
    }

    public static int DirectAttack(ObjectCreature pCaster, ObjectCreature pTarget, int nHurtPoint)                                      //直接技能攻击
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pTarget);

        float fHurt = 0f;
        float fParamA = 0f;
        float fParamB = 0f; //buff加成
        float fParamC = 0f; //攻击方阵营对防御方阵营攻击系数 / 100 * ( 1 + 攻击方对防御方阵营伤害加成率 - 防御方对攻击方阵营伤害减免率 )
        float fParamD = 0f; //1 + 攻击方对防御方距离类型伤害加成率 - 防御方受攻击方距离类型伤害减免
        float fParamE = 0f; //1 + 防御方是BOSS怪物 ? 攻击方对BOSS伤害加成率:0  - 攻击方是BOSS怪物 ? 防御方对BOSS伤害减免率:0
        float fParamG = 0f; //攻击方伤害附加值 - 防御方伤害减免值

        int nCasterImpactAddHurtPermil = 0;//攻击方伤害值的buff处理(不影响技能计算取值，返回加成百分比)[7/23/2015 Zmy]
        pCaster.GetAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, pTarget, ref nCasterImpactAddHurtPermil,ref nHurtPoint);

        int nTargetImpactBeHurtPermil = 0;// 防御方的伤害buff处理 [7/23/2015 Zmy]
        pTarget.GetBeAttackEffectHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurtPoint, ref nTargetImpactBeHurtPermil);

        float fImpactEffectParam = nCasterImpactAddHurtPermil / 1000f + nTargetImpactBeHurtPermil / 1000f; // buff影响的加成率 [7/23/2015 Zmy]

        fParamA = (float)nHurtPoint;//技能计算取值 [7/20/2015 Zmy]

        fParamB = (1f + fImpactEffectParam);

        fParamC = pCaster.GetCampAttackParam(pTarget) / 100f * (1f + pCaster.GetCampAddDamageRate(pTarget) - pTarget.GetCampReducDamageRate(pCaster));

        fParamD = 1f + pCaster.GetAddDamageRateForAttackMode(pTarget) - pTarget.GetReducDamageRateForAttackMode(pCaster);

        fParamE = 1f + pCaster.GetAddDamageRateForBossType(pTarget) - pTarget.GetReducDamageRateForBossType(pCaster);

        fParamG = pCaster.GetExtraHurt() - pTarget.GetReduceHurtPoint();

        fHurt = fParamA; /** fParamB * fParamC * fParamD * fParamE + fParamG;*/
        return (int)fHurt;
    }
    /// <summary>
    /// 治疗
    /// </summary>
    public static int MagicHeal(ObjectCreature pCaster, ObjectCreature pTarget, int Point, bool bCritical, SkillTemplate pRow = null )	//治疗
    {
        LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);

// 	    //	法术治疗回复量=（0+生命恢复率）*（运算治疗取值^参数M*参数N* random(参数AA,参数AB)+参数O + 0）
//         float nParam1 = DataTemplate.GetInstance().m_GameConfig.getLifeRestoringX();
//         float nParam2 = DataTemplate.GetInstance().m_GameConfig.getLifeRestoringY();
//         float nParam3 = DataTemplate.GetInstance().m_GameConfig.getLifeRestoringZ();
//         int nParam4 = (int)(DataTemplate.GetInstance().m_GameConfig.getHealOffloatingMin() * 100f );
//         int nParam5 = (int)(DataTemplate.GetInstance().m_GameConfig.getHealOffloatingMax() * 100f );
//          
//         System.Random ran = new System.Random();
//         float fRand = (float)(ran.Next(nParam4, nParam5)/100f);
// 
//         int nHealValue = (int)(Point * Mathf.Max(nParam1, Mathf.Min(pTarget.GetHpRecover() / nParam2, nParam3)) * fRand);
// 	    return nHealValue;

        float nParamX = DataTemplate.GetInstance().m_GameConfig.getAtt_cor_para();

        float fHeal = (pCaster.GetPhysicalAttack() / nParamX) * pRow.getSpelldmgparam() + pRow.getDmgfixvalue();

        //随机伤害修正 [10/16/2015 Zmy]
        int iRnd = System.DateTime.Now.Millisecond;
        System.Random randomCoor = new System.Random(iRnd);
        int nRand = randomCoor.Next(900, 1100);

        fHeal *= (float)(nRand / 1000f);

        return (int)fHeal;
    }
    public static void AddMP(ObjectCreature pTarget, int Point)	                                                                    //增加怒气
    {
        if (Point > 0)
        {
            FightControler.Inst.OnUpdatePowerValue(pTarget.GetGroupType(), Point);
        }
    }
    public static void ReduceMP(ObjectCreature pTarget, int Point)                                                                  //减少怒气
    {
        if (Point > 0)
        {
            FightControler.Inst.OnUpdatePowerValue(pTarget.GetGroupType(), -Point);
        }
    }
    public static int GetChildLogicAttributeValue(ObjectCreature pCaster, ObjectCreature pTarget, int nType, int nParam)
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pTarget);
        //加1是为了代码内枚举定义的值与策划数值表已定义的类型相匹配 [3/9/2015 Zmy]
        switch (nType + 1)
        {
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_VALUE:
                {
                    return nParam;
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_PHYATT_PERCENT:
                {
                    return (int)((pCaster.GetPhysicalAttack() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAGATT_PERCENT:
                {
                    return (int)((pCaster.GetMagicAttack() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_PHYDEF_PERCENT:
                {
                    return (int)((pCaster.GetPhysicalDefence() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAGDEF_PERCENT:
                {
                    return (int)((pCaster.GetMagicDefence() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_MAXHP_PERCENT:
                {
                    return (int)((pCaster.GetMaxHP() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_CURHP_PERCENT:
                {
                    return (int)((pCaster.GetHP() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARPHYATT_PERCENT:
                {
                    return (int)((pTarget.GetPhysicalAttack() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAGATT_PERCENT:
                {
                    return (int)((pTarget.GetMagicAttack() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARPHYDFF_PERCENT:
                {
                    return (int)((pTarget.GetPhysicalDefence() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAGDFF_PERCENT:
                {
                    return (int)((pTarget.GetMagicDefence() * nParam) / 100.0f);
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARMAXHP_PERCENT:
                {
                    if (pTarget is ObjectMonster)
                    {
                        ObjectMonster pMonster = pTarget as ObjectMonster;
                        return (int)((pMonster.GetMonsterRow().getMonsterPercentMaxHp() * nParam) / 100.0f);
                    }
                    else
                    {
                        return (int)((pTarget.GetMaxHP() * nParam) / 100.0f);
                    }
                    
                }
            case (int)ENUM_SPELL_CHILDLOGIC_VALUETYPE.ENUM_SPELL_CHILDLOGIC_VALUETYPE_TARHP_PERCENT:
                {
                    return (int)((pTarget.GetHP() * nParam) / 100.0f);
                }
        }
        return 0;
    }
    /// <summary>
    /// //分段计算
    /// </summary>
    public static bool DoLogic1(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pTarget);
        LogManager.LogAssert(pSpellInfo);

        SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
        LogManager.LogAssert(pSpellRow);
        for (int i = 2; i < GlobalMembers.MAX_SPELL_LOGIC_PARAM_COUNT; i = i + 3)
        {
            switch (pSpellRow.getParam()[i - 2])
            {
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                        if (nHeal > 0)
                        {
                            pTarget.OnHeal(nHeal, pSpellInfo);
                            pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                        }
                    }
                    break;
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                        if (nHurt > 0)
                        {
                            pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_PHY, ref nHurt);
                            if ((pCaster.IsAlive()) && (nHurt > 0))
                            {
                                pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                            }
                        }
                    }
                    break;
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                        if (nHurt > 0)
                        {
							pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, ref nHurt);
                            if ((pCaster.IsAlive()) && (nHurt > 0))
                            {
                                pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                            }
                        }
                    }
                    break;
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_REDUCE_MP:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        ReduceMP(pTarget, nPoint);
                    }
                    break;
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_INC_MP:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        AddMP(pTarget, nPoint);
                    }
                    break;
                case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
                    {
                        int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[i - 1], pSpellRow.getParam()[i]);
                        int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                        if (nHurt > 0)
                        {
                            pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                            if ((pCaster.IsAlive()) && (nHurt > 0))
                            {
                                pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                            }
                        }
                    }
                    break;
                default:
                    {
                        break;
                    }
            }
        }
        return true;
    }
    public static bool DoLogic2(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//多段技能
    {
        int nNode = pSpellInfo.GetCurIntervalNode();
        LogManager.LogAssert((nNode > 0) && (nNode < GlobalMembers.MAX_SPELL_INTERVERAL_COUNT));

        SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
        LogManager.LogAssert(pSpellRow);
        if (pSpellRow.getParam()[nNode * 5 + 1] > 0)
        {
            pTarget.AddImpact(pSpellRow.getParam()[nNode * 5 + 1], pCaster, bCritical,pSpellInfo.GetSpellID());
        }
        switch (pSpellRow.getParam()[nNode * 5 + 2])
        {
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                    if (nHeal > 0)
                    {
                        pTarget.OnHeal(nHeal, pSpellInfo);
                        pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                    }
                }
                break;
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                    if (nHurt > 0)
                    {
						pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_PHY, ref nHurt);
                        if ((pCaster.IsAlive()) && (nHurt > 0))
                        {
                            pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                        }
                    }
                }
                break;
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                    if (nHurt > 0)
                    {
						pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, ref nHurt);
                        if ((pCaster.IsAlive()) && (nHurt > 0))
                        {
                            pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                        }
                    }
                }
                break;
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_REDUCE_MP:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    ReduceMP(pTarget, nPoint);
                }
                break;
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_INC_MP:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    AddMP(pTarget, nPoint);
                }
                break;
            case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
                {
                    int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[nNode * 5 + 3], pSpellRow.getParam()[nNode * 5 + 4]);
                    int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                    if (nHurt > 0)
                    {
                        pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                        if ((pCaster.IsAlive()) && (nHurt > 0))
                        {
                            pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                        }
                    }
                }
                break;
            default:
                {
                    break;
                }
        }
        return true;
    }
    public static bool DoLogic3(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//复制敌方目标最后一次使用的技能替换该技能。使用(参数1)次数后技能复原并且进入冷却 
    {
        return true;
    }
    public static bool DoLogic4(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//根据血量判断调用子技能
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pTarget);
        LogManager.LogAssert(pSpellInfo);

        SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
        LogManager.LogAssert(pSpellRow);
        //大于分界值时调用技能	小于分界值时调用技能
        //int nCurHp = pCaster.GetHP();
        ObjectCreature pObject = null;
        if (pSpellRow.getParam()[0]==1)
        {
            pObject = pCaster;
        }
        else if(pSpellRow.getParam()[0]==2)
        {
            pObject = pTarget;
        }
        if (pSpellRow.getParam()[1] == 1)
        {
            float nValue = pObject.GetHPPercent() * 10;
            if (nValue >= pSpellRow.getParam()[2])
            {
                SpellInfo spellinfo=new SpellInfo();
                spellinfo.Init(pSpellRow.getParam()[3]);
                Spell subspell=new Spell();
                subspell.SetHolder(pCaster);
                subspell.Init(spellinfo);
                subspell.ImmActiveOnce();
            }
            else
            {
                SpellInfo spellinfo=new SpellInfo();
                spellinfo.Init(pSpellRow.getParam()[4]);
                Spell subspell=new Spell();
                subspell.SetHolder(pCaster);
                subspell.Init(spellinfo);
                subspell.ImmActiveOnce();
            }
        }
        return true;
    }
    public static bool DoLogic5(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//驱散
    {
        LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);
	    LogManager.LogAssert(pSpellInfo);

	    SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
	    LogManager.LogAssert(pSpellRow);
        for (int i = 0; i<GlobalMembers.MAX_SPELL_LOGIC_PARAM_COUNT; ++i)
        {
            if (pSpellRow.getParam()[i] > 0)
            {
                BuffgroupTemplate pRow = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(pSpellRow.getParam()[i]);
                if (pRow != null)
                {
                    for (int j = 0; j < pRow.getParam().Length; j++)
                    {
                        EM_IMPACT_RESULT nResult = pTarget.RemoveImpact(pRow.getParam()[j]);
                    }
                }
            }
            else
            {
                break;
            }
        }

	    return true;
    }
    public static bool DoLogic6(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)  							    //与目标交换当前生命百分比
    {
        LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);
	    LogManager.LogAssert(pSpellInfo);

	    SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
	    LogManager.LogAssert(pSpellRow);
        long nCasterMaxHP = pCaster.GetMaxHP();
        long nCasterCurHP = pCaster.GetHP();

        long nTargetMaxHP = pTarget.GetMaxHP();
        long nTargetCurHP = pTarget.GetHP();

        nCasterCurHP = (nTargetCurHP *nCasterMaxHP) / nTargetMaxHP;
        if (nCasterCurHP<=0)
        {
            nCasterCurHP = 1;
        }
        nTargetCurHP = (nCasterCurHP *nTargetMaxHP) / nCasterMaxHP;
        if (nTargetCurHP <= 0)
        {
            nTargetCurHP = 1;
        }
        pCaster.SetHP(nCasterCurHP);
        pTarget.SetHP(nTargetCurHP);

	    return true;
    }
    public static bool DoLogic7(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//减少所有技能CD百分比
    {
        LogManager.LogAssert(pCaster);
	    LogManager.LogAssert(pTarget);
	    LogManager.LogAssert(pSpellInfo);
	    //死亡目标
	    LogManager.LogAssert(pTarget.GetHP()<=0);

	    SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
	    LogManager.LogAssert(pSpellRow);
        CoolDownList pCoolDown = pCaster.GetCoolDownList();
        LogManager.LogAssert(pCoolDown);
        for (int i = 0; i < GlobalMembers.MAX_SPELL_COOLDOWN_NUMBER; i++)
        {
            if (pSpellRow.getParam()[0] == 1)
            {
                int nTime = (int)(pCoolDown.m_sCoolDownObject[i].CoolDownTime - (pCoolDown.m_sCoolDownObject[i].CoolDownTime * pSpellRow.getParam()[1]) / 100);
                if (nTime < 0)
                {
                    nTime = 0;
                }
                pCoolDown.m_sCoolDownObject[i].CoolDownTime = (uint)nTime;
            }
            else
            {
                int nTime = (int)(pCoolDown.m_sCoolDownObject[i].CoolDownTime - pCoolDown.m_sCoolDownObject[i].CoolDownTime - pSpellRow.getParam()[1]);
                if (nTime < 0)
                {
                    nTime = 0;
                }
                pCoolDown.m_sCoolDownObject[i].CoolDownTime = (uint)nTime;
            }
        }
	    return true;
    }
    public static bool DoLogic11(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//增减目标身上debuff的持续时间X毫秒 取值类型只能用1（数值）
    {
        LogManager.LogAssert(pTarget);
	    LogManager.LogAssert(pSpellInfo);

	    SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
	    LogManager.LogAssert(pSpellRow);
        if (pSpellRow.getParam()[0] != 0)
        {
            List<Impact> pImpactList = pTarget.GetImpactList();
            foreach (var item in pImpactList)
	        {
		        Impact pImpact = item;
                if (pImpact.GetType_() == 0)
                {
                    pImpact.RefixImpactTime(pSpellRow.getParam()[0]);
                }
	        }
        }  
	    return true;
    }
   public static bool DoLogic12(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)								//斩杀 - 目标生命值小于等于取值类型的值则直接使目标死亡(取值类型只能用6)
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);
	   
       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       if (pSpellRow.getParam()[0] == (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY)
       {
           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[1], pSpellRow.getParam()[2]);
           int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
           if (nHurt > 0)
           {
               long nPercent = (pTarget.GetHPPercent() * 100);
               if (nPercent < pSpellRow.getParam()[3])
               {
                   pTarget.OnBeKilled(pCaster, pSpellInfo);
                   pCaster.OnKillTarget(pTarget, pSpellInfo);
                   //增加怒气
               }
           }
       }
       else if (pSpellRow.getParam()[0] == (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC)
       {
           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[1], pSpellRow.getParam()[2]);
           int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
           if (nHurt > 0)
           {
               long nPercent = (pTarget.GetHPPercent() * 100);
               if (nPercent < pSpellRow.getParam()[3])
               {
                   pTarget.OnBeKilled(pCaster, pSpellInfo);
                   pCaster.OnKillTarget(pTarget, pSpellInfo);
                   //增加怒气
               }
           }
       }

       return true;
   }
   public static bool DoLogic14(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)                            //几率返回子技能
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);

       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       if (pSpellRow.getParam()[0] > 0)
       {
           int nRand = UnityEngine.Random.Range(0, 1000);
           if (nRand < pSpellRow.getParam()[1])
           {
               SpellInfo spellinfo = new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[0]);
               Spell subspell = new Spell();
               subspell.SetTargetGuid(pTarget.GetGuid());
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
       }
       if (pSpellRow.getParam()[2] > 0)
       {
           int nRand = UnityEngine.Random.Range(0, 1000);
           if (nRand < pSpellRow.getParam()[3])
           {
               SpellInfo spellinfo = new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[2]);
               Spell subspell = new Spell();
               subspell.SetTargetGuid(pTarget.GetGuid());
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
       }
       return true;
   }
   public static bool DoLogic15(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)                      //根据目标敌友调用子技能
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);

       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       if (pTarget.IsAttacker() != pCaster.IsAttacker())
       {
           SpellInfo spellinfo=new SpellInfo();
           spellinfo.Init(pSpellRow.getParam()[0]);
           Spell subspell=new Spell();
           subspell.SetHolder(pCaster);
           subspell.Init(spellinfo);
           subspell.ImmActiveOnce();
       }
       else
       {
           SpellInfo spellinfo=new SpellInfo();
           spellinfo.Init(pSpellRow.getParam()[0]);
           Spell subspell=new Spell();
           subspell.SetHolder(pCaster);
           subspell.Init(spellinfo);
           subspell.ImmActiveOnce();
       }
       return true;
   }
   public static bool DoLogic16(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)                      //根据怒气量调用子技能
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);

       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       if (pTarget.IsAttacker())
       {
           int mp = 0;
           if (mp >= pSpellRow.getParam()[2])
           {
               SpellInfo spellinfo=new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[3]);
               Spell subspell=new Spell();
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
           else
           {
               SpellInfo spellinfo=new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[4]);
               Spell subspell=new Spell();
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
       }
       else
       {
           int mp = 0;
           if (mp >= pSpellRow.getParam()[2])
           {
               SpellInfo spellinfo=new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[3]);
               Spell subspell=new Spell();
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
           else
           {
               SpellInfo spellinfo=new SpellInfo();
               spellinfo.Init(pSpellRow.getParam()[4]);
               Spell subspell=new Spell();
               subspell.SetHolder(pCaster);
               subspell.Init(spellinfo);
               subspell.ImmActiveOnce();
           }
       }
       return true;
   }
   public static bool DoLogic17(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)                      //根据目标当前BUFF造成伤害
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);

       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       int nBuffCount = pTarget.GetImpactCountByGroupID(pSpellRow.getParam()[0]);
       if (nBuffCount == 0)
       {
           if (pSpellRow.getParam()[0] == 2)
           {
               switch (pSpellRow.getParam()[3])
               {
                   case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
                       {
                           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                           int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                           if (nHeal > 0)
                           {
                               pTarget.OnHeal(nHeal);
                               pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                           }
                       }
                       break;
                   case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
                       {
                           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                           int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                           if (nHurt > 0)
                           {
                               pTarget.OnDamage(pCaster, pSpellInfo,(int)ENUM_HURT_TYPE.HURT_TYPE_PHY,ref nHurt);
                               if ((pCaster.IsAlive()) && (nHurt > 0))
                               {
                                   pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                               }
                           }
                       }
                       break;
                   case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
                       {
                           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                           int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                           if (nHurt > 0)
                           {
                               pTarget.OnDamage(pCaster, pSpellInfo,(int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC,ref nHurt);
                               if ((pCaster.IsAlive()) && (nHurt > 0))
                               {
                                   pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                               }
                           }
                       }
                       break;
                   case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
                       {
                           int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4],pSpellRow.getParam()[5]);
                           int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                           if (nHurt > 0)
                           {
                               pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                               if ((pCaster.IsAlive()) && (nHurt > 0))
                               {
                                   pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                               }
                           }
                       }
                       break;
                   default:
                       {
                           break;
                       }
               }
           }
       }
       else if (pSpellRow.getParam()[0] == 1)
       {
           switch (pSpellRow.getParam()[3])
           {
               case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
                   {
                       int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                       int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                       if (nHeal > 0)
                       {
                           pTarget.OnHeal(nHeal);
                           pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                       }
                   }
                   break;
               case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
                   {
                       int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                       int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                       if (nHurt > 0)
                       {
                           pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_PHY, ref nHurt);
                           if ((pCaster.IsAlive()) && (nHurt > 0))
                           {
                               pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                           }
                       }
                   }
                   break;
               case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
                   {
                       int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                       int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                       if (nHurt > 0)
                       {
                           pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC,ref nHurt);
                           if ((pCaster.IsAlive()) && (nHurt > 0))
                           {
                               pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                           }
                       }
                   }
                   break;
               case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
                   {
                       int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[4], pSpellRow.getParam()[5]);
                       int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                       if (nHurt > 0)
                       {
                           pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                           if ((pCaster.IsAlive()) && (nHurt > 0))
                           {
                               pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                           }
                       }
                   }
                   break;
               default:
                   {
                       break;
                   }
           }
       }
       return true;
   }
   public static bool DoLogic18(ObjectCreature pCaster, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)                      //循环多段
   {
       LogManager.LogAssert(pTarget);
       LogManager.LogAssert(pSpellInfo);

       SkillTemplate pSpellRow = pSpellInfo.GetSpellRow();
       LogManager.LogAssert(pSpellRow);
       if (pSpellRow.getParam()[3] > 0)
       {
           pTarget.AddImpact(pSpellRow.getParam()[3], pCaster, false,pSpellInfo.GetSpellID());
       }
       switch (pSpellRow.getParam()[4])
       {
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                   if (nHeal > 0)
                   {
                       pTarget.OnHeal(nHeal);
                       pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_PHY,ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC,ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_REDUCE_MP:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   ReduceMP(pTarget, nPoint);
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_INC_MP:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   AddMP(pTarget, nPoint);
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[5], pSpellRow.getParam()[6]);
                   int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           default:
               {
                   break;
               }
       }
       switch (pSpellRow.getParam()[7])
       {
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HEAL_MAGIC:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   int nHeal = MagicHeal(pCaster, pTarget, nPoint, false, pSpellRow);
                   if (nHeal > 0)
                   {
                       pTarget.OnHeal(nHeal);
                       pCaster.SkillTypeFlag |= ENUM_SPELL_TYPE_FLAG.SPELL_MAGIC_HEAL;
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_PHY:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   int nHurt = PhysicalAttack(pCaster, pTarget, nPoint, bCritical, pSpellRow);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_PHY,ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_PHY, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_ATT_MAGIC:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   int nHurt = MagicAttack(pCaster, pTarget, nPoint, bCritical);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC,ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_MAGIC, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_REDUCE_MP:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   ReduceMP(pTarget, nPoint);
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_INC_MP:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   AddMP(pTarget, nPoint);
               }
               break;
           case (int)ENUM_SPELL_CHILD_LOGIC.SPELL_CHILD_LOGIC_HURT_POINT:
               {
                   int nPoint = GetChildLogicAttributeValue(pCaster, pTarget, pSpellRow.getParam()[8], pSpellRow.getParam()[9]);
                   int nHurt = DirectAttack(pCaster, pTarget, nPoint);
                   if (nHurt > 0)
                   {
                       pTarget.OnDamage(pCaster, pSpellInfo, (int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt);
                       if ((pCaster.IsAlive()) && (nHurt > 0))
                       {
                           pCaster.OnHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, pTarget, pSpellInfo, bCritical);
                       }
                   }
               }
               break;
           default:
               {
                   break;
               }
       }
       return true;
   }
}
