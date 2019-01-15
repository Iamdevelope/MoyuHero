using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
public class SpellInfo
{
    private int m_SpellID;						// 技能id
    private int m_SpellTime;					// 技能时间
    private int m_nIntervalCount;		//间隔次数
    private int m_CurIntervalNode;      //当前分段的节点
    private int[] m_nInterval = new int[GlobalMembers.MAX_SPELL_INTERVERAL_COUNT];				//间隔
    private float m_fSpellMaxDistance;			// 技能最大释放距离
    private int m_MaxTargetNumber;				// 目标个数
    private float m_SpellRange;					// 技能范围
    private int m_CoolDownTime;					// 冷却时间
    private ObjectCreature m_SkillClickTag;            //技能点击目标
    public List<ObjectCreature> m_pObjectList;//技能所有目标
    private Flag32 m_BitFlag = new Flag32();	// 技能修正标志
    private int[] m_EquipRefixAttay = new int[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER];					// impact对本技能修正
    private int[] m_ImpactRefixAttay = new int[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER];					// impact对本技能修正
    private SkillTemplate m_pTableRowSpell = new SkillTemplate();				// 技能表格数据


    public SpellInfo()
    {
        m_BitFlag.CleanUp();
	    m_SpellID = -1;
	    m_SpellTime = 0;
	    m_nIntervalCount = 0;
	    m_SpellRange = 0.0f;
	    m_CoolDownTime = 0;
	    m_fSpellMaxDistance = 0.0f;
	    m_MaxTargetNumber = 0;
        m_CurIntervalNode = 0;
        m_SkillClickTag = null;
        m_pObjectList = new List<ObjectCreature>();
        for (int i = 0; i < (int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER; ++i)
	    {
		    m_EquipRefixAttay[i] = 0;
		    m_ImpactRefixAttay[i] = 0;
	    }
    }

    public bool Init(int nSpellID, int nParam = 0)
    {
	    if(nSpellID != -1)
	    {
            SkillTemplate pData = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(nSpellID);
            m_pTableRowSpell = pData;
		    LogManager.LogAssert(m_pTableRowSpell);
		    m_SpellID = nSpellID;
		    //获取数据(如果有imact对此产生影响需要考虑impact)
		    m_CoolDownTime = m_pTableRowSpell.getCooldown();
            switch (m_pTableRowSpell.getSkillReleaseType())
		    {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI1:
                {
                    m_SpellTime = m_pTableRowSpell.getParam()[0];
                }
                    break;
		    case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI2:
			    {
				    m_SpellTime = 0;
			    }
			    break;
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHARGE:
                {
                    m_SpellTime = m_pTableRowSpell.getParam()[0];
                }
                break;
		    case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_MULTISECTION:
			    {
				    m_nIntervalCount = 0;
				    for (int i = 0; i < GlobalMembers.MAX_SPELL_LOGIC_PARAM_COUNT; i = i + 5)
				    {
					    if (m_pTableRowSpell.getParam()[i] > 0)
					    {
						    m_nInterval[m_nIntervalCount] = m_pTableRowSpell.getParam()[i];
						    ++m_nIntervalCount;
					    }
					    else
					    {
						    break;
					    }
				    }
                    m_CurIntervalNode = 0;
			    }
			    break;
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
			    {
                    m_nIntervalCount = 0;
                    m_nInterval[m_nIntervalCount] = m_pTableRowSpell.getParam()[0];
                    ++m_nIntervalCount;
                    for (int i = 0; i < m_pTableRowSpell.getParam()[2]; ++i)
                    {
                        m_nInterval[m_nIntervalCount] = m_pTableRowSpell.getParam()[1];
                        ++m_nIntervalCount;
                    }
			    }
			    break;
		    }
            m_BitFlag.MarkAllFlags((int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER);
	    }

	    return true;
	    
    }
    public void SetTagObjectList(List<ObjectCreature> Taglist)
    {
        m_pObjectList = Taglist;
    }
    public List<ObjectCreature> GetTagObjectList()
    {
        return m_pObjectList;
    }
    public ObjectCreature GetSkillClickTag()
    {
        return m_SkillClickTag;
    }
    public void SetSkillClickTag(ObjectCreature tag)
    {
        m_SkillClickTag = tag;
    }
    public bool	IsValid()
    {
	    if(m_SpellID == -1)
	    {
		    return false;
	    }
	    return true;
    }

    public bool Reset()
    {
	    
	    m_SpellID = -1;
	    m_SpellTime = 0;
	    m_nIntervalCount = 0;
	    m_SpellRange = 0.0f;
	    m_MaxTargetNumber = 0;
        for (int i = 0; i < (int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER; ++i)
	    {
		    m_EquipRefixAttay[i] = 0;
		    m_ImpactRefixAttay[i] = 0;
	    }
	    m_pTableRowSpell = null;
        m_BitFlag.CleanUp();
	    return true;

    }

    public int GetSpellID()
    {
	    return m_SpellID;
    }
    public int GetSpellNum()
    {
        return m_pTableRowSpell.getSkillNo();
    }

    public int GetSpellType()
    {
	    
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getSkillType();
	    
    }

    public int GetSpellSectionTime(int nSection)
    {
	    
		    LogManager.LogAssert(IsValid());

	    return m_nIntervalCount;

    }

    public int GetSpellSectionCount()
    {
	    
		    LogManager.LogAssert(IsValid());

	   // return m_pTableRowSpell.m_SectionCount;
	    
		    return 0;
    }

    public bool IsSelfHPOK(int nValue)
    {
	    
		    LogManager.LogAssert(IsValid());
	    if (m_pTableRowSpell.getHpConditionType() == -1)
	    {
		    return true;
	    }

        else if (m_pTableRowSpell.getHpConditionType() == 1)
	    {
		    if (m_pTableRowSpell.getHpConditionNum() <= nValue)
		    {
			    return true;
		    }
	    }
	    else
	    {
            if (m_pTableRowSpell.getHpConditionNum() >= nValue)
		    {
			    return true;
		    }
	    }

	    return false;
	    
    }

    public bool IsSelfMPOK(int nValue)
    {
	    
		    LogManager.LogAssert(IsValid());

	    if (m_pTableRowSpell.getRpConditionType() == -1)
	    {
		    return true;
	    }

        else if (m_pTableRowSpell.getRpConditionType() == 1)
	    {
		    if (m_pTableRowSpell.getRpConditionNum() <= nValue)
		    {
			    return true;
		    }
	    }
	    else
	    {
            if (m_pTableRowSpell.getRpConditionNum() >= nValue)
		    {

			    return true;
		    }
	    }

	    return false;
	    
    }

    public bool CheckConsume(ObjectCreature pObject)
    {
	    
	    LogManager.LogAssert(IsValid());
	    LogManager.LogAssert(pObject);
// 	    for (int i = 0; i < GlobalMembers.MAX_SPELLCAST_CONSUME_COUNT; i++)
// 	    {
// 		    switch (m_pTableRowSpell.m_skillCostType[i])
// 		    {
//             case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_COMMON:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_GOLD:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_RONGHUN:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP_PERCENT:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_HP:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_HP_PERCENT:
// 			    {
// 			    }
// 			    break;
// 		    case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_RUNEPOINT:
// 			    {
// 			    }
// 			    break;
// 		    default:
//			    break;
// 		    }
// 	    }

	    return true;
	    
    }

    public bool CheckStatusOK(ObjectCreature pObject)
    {    
	    LogManager.LogAssert(IsValid());
	    return false;
    }

    public bool CheckTarget(ObjectCreature pObject, ObjectCreature pTarget)
    {
	    
		LogManager.LogAssert(IsValid());
	    if (CheckAttackDistance(pObject, pTarget))
	    {
		    return true;
	    }

	    return false;
	    
    }

    public bool CheckNormalRobotCondition(ObjectCreature pObject)
    {
	    
		    LogManager.LogAssert(IsValid());

	    return false;
    }

    public bool CheckAttackRobotCondition(ObjectCreature pObject)
    {
	    
		    LogManager.LogAssert(IsValid());

	    return false;

    }

    public bool CheckHealRobotCondition(ObjectCreature pObject)
    {
	    
		    LogManager.LogAssert(IsValid());

	    return false;

    }

    public int GetInterruptCooldownTime()
    {
	    
		    LogManager.LogAssert(IsValid());

	    //return m_pTableRowSpell.m_interruptCooldown;
            return 0;
    }
    public bool IsMoveInterrupt()
    {
        LogManager.LogAssert(IsValid());
        return false;
    }
    public bool CheckHurtInterrupt(ObjectCreature pObject, int nValue)
    {
	    
		    LogManager.LogAssert(IsValid());
	    LogManager.LogAssert(pObject);
	    if (m_pTableRowSpell.getDamageInterruptType() == 1)
	    {
		    if (m_pTableRowSpell.getRpConditionNum() <= nValue)
		    {
			    return true;
		    }
	    }

	    else if (m_pTableRowSpell.getDamageInterruptType() == 2)
	    {
		    if (m_pTableRowSpell.getRpConditionNum() <= pObject.GetHP()*100/pObject.GetMaxHP())
		    {
			    return true;
		    }
	    }

	    return false;
    }

    public bool IsCanInterruptSpell()
    {
	    
		    LogManager.LogAssert(IsValid());

	    if (m_pTableRowSpell.getInterruptSkill() != 0)
	    {
		    return true;
	    }
	    return false;
    }

    public float GetAttackDistance()
    {
	    
		    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getAttDistance();

    }

    public float GetAttackDistanceMax()
    {
	    
		    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getAttDistance();

    }

    public float GetAttackDistanceMin()
    {
	    
		    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getAttDistance();

    }

    public bool CheckAttackDistance(ObjectCreature pObject, ObjectCreature pTarget)
    {
	    
		LogManager.LogAssert(IsValid());
	    LogManager.LogAssert(pObject);
	    LogManager.LogAssert(pTarget);
	    Vector3 _SrcPos = pObject.getWorldPos();
        Vector3 _DstPos = pTarget.getWorldPos();
        float _myLengthSq = (_SrcPos.x - _DstPos.x) * (_SrcPos.x - _DstPos.x) + (_SrcPos.z - _DstPos.z) * (_SrcPos.z - _DstPos.z);
        if (_myLengthSq > m_pTableRowSpell.getAttDistance())
	    {
		    return false;
	    }
	    return true;
    }

    public bool IsNeedTarget()
    {
	    
		    LogManager.LogAssert(IsValid());
	    if (IsEquals(m_pTableRowSpell.getCoverage(), 0.0f))
	    {
            return true;
	    }
        return false;
    }

    public int GetBulletSpeed()
    {
	    
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getBallIsticSpeed();

    }

    public int GetHitFury()
    {
	    
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getInitHitFury();

    }
    public int IsSubSpellReferFather()
    {
        LogManager.LogAssert(IsValid());
        return -1;
    }
    public int GetSubSpell()
    {
        LogManager.LogAssert(IsValid());
        return -1;
    }
    public int GetskillAttackFury()
    {   
		LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getSkillAttackFury();

    }

    public int GetWeakenTargetFuryReward()
    {
	    
		LogManager.LogAssert(IsValid());

        return m_pTableRowSpell.getWeakenTargetFuryReward();

    }

    public int GetkillFury()
    {
	    
		LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getKillFury();

    }


    public int GetTargetType()
    {
	    
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getTarget();

    }

    public int GetBaseSpellTime()
    {
	    
	    LogManager.LogAssert(IsValid());

	    return m_SpellTime;

    }

    public int GetSpellTime()
    {
	    
	    LogManager.LogAssert(IsValid());

	    m_SpellTime = GetBaseSpellTime() + m_EquipRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_TIME] + m_ImpactRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_TIME];

	    return m_SpellTime;

    }

    public int GetBaseSpellIntervalTime()
    {
	    
	    //	LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    //return m_pTableRowSpell.m_SpellIntervalTime;
	    
	     return 0;
    }

    public int GetBaseCoolDownTime()
    {
	    
	    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getCooldown();

    }

    public int GetCoolDownTime()
    {
	    
	    LogManager.LogAssert(IsValid());

	    m_CoolDownTime = GetBaseCoolDownTime() + m_EquipRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLCOOLDOWN] + m_ImpactRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLCOOLDOWN];

	    return m_CoolDownTime;
	    
    }

    public void SetCoolDownTime(int downTime)
    {
        if (downTime < 0) downTime = 0; 
        m_CoolDownTime = downTime;
    }
    //技能释放条件
    public bool IsSkillRelease(ObjectCreature value)
    {
        return (IsEnoughHp(value) && IsEnoughRp(value)) ? true : false;
    }
    //技能释放消耗资源扣除
    public void SkillResourceUpdate(ObjectCreature value)
    {
        SwithResourceType(m_pTableRowSpell.getSkillCostType1(), m_pTableRowSpell.getSkillCostId1(), m_pTableRowSpell.getSkillCostNum1(), value);
        SwithResourceType(m_pTableRowSpell.getSkillCostType2(), m_pTableRowSpell.getSkillCostId2(), m_pTableRowSpell.getSkillCostNum2(), value);
        SwithResourceType(m_pTableRowSpell.getSkillCostType3(), m_pTableRowSpell.getSkillCostId3(), m_pTableRowSpell.getSkillCostNum3(), value);
    }
    private bool IsEnoughHp(ObjectCreature value)
    {
        switch(m_pTableRowSpell.getHpConditionType())
        {
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_INVALID:
                return true;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_LESSVALUE:
                if (value.GetHP()<= m_pTableRowSpell.getHpConditionNum())
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_MOREVALUE:
                if (value.GetHP() >= m_pTableRowSpell.getHpConditionNum())
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_LESSPERCENT:
                if ((value.GetHP() / value.GetMaxHP()) <= (m_pTableRowSpell.getHpConditionNum()/100))
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_MOREPERCENT:
                if ((value.GetHP() / value.GetMaxHP()) >= (m_pTableRowSpell.getHpConditionNum()/100))
                    return true;
                break;
        }
        return false;
    }
    private bool IsEnoughRp(ObjectCreature value)
    {
        switch (m_pTableRowSpell.getRpConditionType())
        {
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_INVALID:
                return true;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_LESSVALUE:
                if (FightControler.Inst.GetPowerValue(value.GetGroupType()) <= m_pTableRowSpell.getRpConditionNum())
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_MOREVALUE:
                if (ObjectSelf.GetInstance().isSkillShow)
                    return true;
                else if (FightControler.Inst.GetPowerValue(value.GetGroupType()) >= m_pTableRowSpell.getRpConditionNum())
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_LESSPERCENT:
                if ((FightControler.Inst.GetPowerValue(value.GetGroupType()) / ObjectSelf.GetInstance().GetMaxPowerValue()) <= (m_pTableRowSpell.getRpConditionNum()/100))
                    return true;
                break;
            case (int)EM_SPELL_CONDITION_TYPE.EM_SPELL_CONDITION_TYPE_MOREPERCENT:
                if ((FightControler.Inst.GetPowerValue(value.GetGroupType()) / ObjectSelf.GetInstance().GetMaxPowerValue()) >= (m_pTableRowSpell.getRpConditionNum() / 100))
                    return true;
                break;
        }
        return false;
    }
    private void SwithResourceType(int Type, int ID, int Num, ObjectCreature value)
    {
        switch (Type)
        {
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_TYPE_INVALID:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP:
                if (!ObjectSelf.GetInstance().isSkillShow)
                    FightControler.Inst.OnUpdatePowerValue(value.GetGroupType(),-Num);
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MP_PERCENT:
                FightControler.Inst.OnUpdatePowerValue(value.GetGroupType(),-(FightControler.Inst.GetPowerValue(value.GetGroupType())*Num/100));
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_HP:
                value.SetHP((value.GetHP() - Num));
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_CURRENTHP_PERCENT:
                value.SetHP((value.GetHP()-(value.GetHP()*(Num/100))));
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_MAXHP_PERCENT:
                value.SetHP((value.GetHP() - (value.GetMaxHP() * (Num / 100))));
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_COMMON:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_GOLD:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_RONGHUN:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_RUNEPOINT:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_PARTNER:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_ITEM:
                break;
            case (int)EM_EXTRAITEM_TYPE.EM_EXTRAITEM_EQUIP:
                break;
        }
    }
    public bool IsEquals(float f1, float f2)
    {
        if (f1 < f2)
        {
            if (System.Math.Abs(f2-f1) < 0.0001f)
            {
                return true;
            }
        }
        else
        {
            if (System.Math.Abs(f1 - f2) < 0.0001f)
            {
                return true;
            }
        }

        return false;
    }
    public float GetBaseSpellMaxDistance()
    {
	    
	    LogManager.LogAssert(IsValid());
	    if (IsEquals(m_pTableRowSpell.getAttDistance(), 1.0f))
	    {
		    return 0.0f;
	    }

	    if (IsEquals(m_pTableRowSpell.getAttDistance(), -1.0f))
	    {
		    return 100000.0f;
	    }

	    return m_pTableRowSpell.getAttDistance();
	    
    }

    public float GetSpellMaxDistance()
    {
	    
		//LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    /*if (m_BitFlag.GetFlagByIndex((int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLMAXDISTANCE))
	    {*/
		    m_fSpellMaxDistance = GetBaseSpellMaxDistance() + m_EquipRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLMAXDISTANCE]/100.0f + m_ImpactRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLMAXDISTANCE]/100.0f;
		    /*m_BitFlag.ClearFlagByIndex((int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLMAXDISTANCE);
	    }*/

	    return m_fSpellMaxDistance;
	    
    }

    public int GetBaseSpellTargetNumber()
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    return m_pTableRowSpell.getTargetNum();
	    
    }

    public int GetSpellTargetNumber()
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    /*if (m_BitFlag.GetFlagByIndex((int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLTARGETCOUNT))
	    {*/
		    m_MaxTargetNumber = GetBaseSpellTargetNumber() + m_EquipRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLTARGETCOUNT] + m_ImpactRefixAttay[(int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLTARGETCOUNT];
	    /*	m_BitFlag.ClearFlagByIndex((int)EM_SPELL_REFIX.EM_SPELL_REFIX_SPELLTARGETCOUNT);
	    }*/
	    if (m_MaxTargetNumber == -1)
	    {
            m_MaxTargetNumber = GlobalMembers.MAX_TEAM_CELL_COUNT;
	    }
	    return m_MaxTargetNumber;
	    
    }

    public bool IsBitSet(EM_SPELL_REFIX nType)
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

        return m_BitFlag.isSetBit((int)nType);

	    
		    return false;
    }

    public void SetBitFlag(EM_SPELL_REFIX nType)
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

        m_BitFlag.UpdateBits((int)nType,true);

	    
    }

    void	ClearBitFlag(EM_SPELL_REFIX nType)
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

        m_BitFlag.UpdateBits((int)nType, false);

	    
    }

    public void ChangeSpellRefix(EM_EFFECT_SOURCE_TYPE nEffectType, EM_SPELL_REFIX nType, int nValue, bool bRemove)
    {
	    
	    //LogManager.LogAssert(m_pSpellDB);
        if (((int)nType <= (int)EM_SPELL_REFIX.EM_SPELL_REFIX_INVALID) || ((int)nType >= (int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER))
	    {
		    LogManager.LogAssert(0);
	    }
	    if (bRemove)
	    {
            if (nEffectType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE)
		    {
                m_EquipRefixAttay[(int)nType] = m_EquipRefixAttay[(int)nType] - nValue;
		    }
            else if (nEffectType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT)
		    {
                m_ImpactRefixAttay[(int)nType] = m_ImpactRefixAttay[(int)nType] - nValue;
		    }
	    }
	    else
	    {
            if (nEffectType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE)
		    {
                m_EquipRefixAttay[(int)nType] = m_EquipRefixAttay[(int)nType] + nValue;
		    }
            else if (nEffectType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT)
		    {
                m_ImpactRefixAttay[(int)nType] = m_ImpactRefixAttay[(int)nType] + nValue;
		    }
	    }
	    //m_BitFlag.MarkFlagByIndex(nType);
        SetBitFlag(nType);
    }

    public void ClearRefix()
    {
	    
		    //LogManager.LogAssert(m_pSpellDB);
	    LogManager.LogAssert(IsValid());

	    for (int i=0; i<(int)EM_SPELL_REFIX.EM_SPELL_REFIX_NUMBER; ++i)
	    {
		    m_EquipRefixAttay[i] = 0;
		    m_ImpactRefixAttay[i] = 0;
	    }
    }
    public SkillTemplate GetSpellRow() { return m_pTableRowSpell; }

    public void UpdateCurInterval()
    {
        if (m_nIntervalCount < 0 && m_CurIntervalNode >= m_nIntervalCount)
            return;

        ++m_CurIntervalNode;
    }
    public void ClearIntervalNode() { m_CurIntervalNode = 0; }
    public int GetCurIntervalNode() { return m_CurIntervalNode; }
}
