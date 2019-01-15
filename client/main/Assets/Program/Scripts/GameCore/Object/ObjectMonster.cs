using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.SkillCore;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameAudio;
using DreamFaction.LogSystem;
public class ObjectMonster: ObjectCreature
{

    private X_GUID                  m_Guid;
    private int                     m_TableID;
    private int[]                   m_BuffEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];  //buffer的影响
    private SpellInfo               m_SpellNormal = new SpellInfo();
    private List<SpellInfo>         m_SpellInfoList = new List<SpellInfo>(GlobalMembers.MAX_DB_SPELL_NUM);
    private SpellInfo               m_SpellInfoNow = new SpellInfo();                  //当前技能信息
    private Spell                   m_pTempSell = new Spell();                         //临时技能对象,用于释放技能前验证条件
    private ObjectHero              m_CurLockTarget;                                   //当前普通攻击目标
    private ObjectCreature          m_SkillLockTarget;                                 //当前技能锁定目标
    private NavMeshAgent            m_NavMesh;
    private GameObject              m_pMonsterObject;

    protected bool                  m_IsDeathBuff = false;//标记是否死亡后触发buff
    protected int                   m_DeathSkillID;
    protected string                m_DeathSkillShader;

    private float                   m_fModleEnlarge;//模型放大系数
    public ObjectMonster()
    {
        m_Guid = new X_GUID();
        m_Guid.CreateMonsterGUID();// 怪物要从客户端生成一个guid [4/2/2015 Zmy]
        SetGroupType(EM_OBJECT_TYPE.EM_OBJECT_TYPE_MONSTER);
    }
    public override X_GUID GetGuid()
    {
        return m_Guid;
    }
    public SpellInfo GetSpellNormal() { return m_SpellNormal; }
    public void SetSpellInfoNow(SpellInfo info) { m_SpellInfoNow = info; }
    public SpellInfo GetSpellInfoNow() { return m_SpellInfoNow; }
    public ObjectHero GetCurLockTarget() {return  m_CurLockTarget;}
    public List<SpellInfo> Getm_SpellInfo() { return m_SpellInfoList; }
    public void SetSkillLockTarget(ObjectCreature skillobj)
    {
        m_SkillLockTarget = skillobj;
    }
    public void SetMonsterTableID(int nID)
    {
        m_TableID = nID;
    }
    public X_GUID GUID
    {
        get
        {
            return m_Guid;
        }
        set
        {
            m_Guid.Copy(value);
        }
    }
    public void SetMonsterObject(GameObject obj)
    {
        m_pMonsterObject    = obj;
        //m_fModleEnlarge     = GetMonsterRow().getMonsterEnlarge();
        //m_pMonsterObject.transform.localScale = new UnityEngine.Vector3(m_fModleEnlarge, m_fModleEnlarge, m_fModleEnlarge);
        m_NavMesh           = obj.GetComponent<NavMeshAgent>();
        //m_NavMesh.enabled = false;
        m_AnimControl       = obj.AddComponent<AnimationControl>();
        m_AnimControl.SetOwnerType(0);
        m_AnimControl.InitEventData(GetMonsterRow().getArtresources());
    }
    public void SetSpellNormalData()
    {
        if (m_SpellInfoList.Count > 0)
            m_SpellInfoList.Clear();
        MonsterTemplate pRow = GetMonsterRow();
        m_SpellNormal.Init(pRow.getNormalattack());
        for (int i = 0; i < pRow.getMonsterskill().Length; i++)
        {
            SpellInfo _spell = new SpellInfo();
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                int nCurRound = ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum;
                UltimatetrialmonsterTemplate _row = (UltimatetrialmonsterTemplate)DataTemplate.GetInstance().m_UltimatetrialmonsterTable.getTableData(nCurRound);
                int _skillID = pRow.getMonsterskill()[i] + _row.getAdditionalSkill();

                _spell.Init(_skillID);
            }
            else
            {
                _spell.Init(pRow.getMonsterskill()[i]);
            }
            m_SpellInfoList.Add(_spell);
        }
    }
    public override GameObject GetGameObject()
    {
        return m_pMonsterObject;
    }

    public override bool GetIsNearAttackMold()
    {
        MonsterTemplate pRow = GetMonsterRow();

        return pRow.getClientSignType()[0] == 0 ? true : false;
    }

    public override bool GetIsBossType()
    {
        MonsterTemplate pRow = GetMonsterRow();

        return pRow.getMonstertype() == 2 ? true : false;
    }

    public override int GetFuryIdForTable()
    {
        return GetMonsterRow().getFuryId();
    }

    public NavMeshAgent GetNavMesh()
    {
        return m_NavMesh;
    }

    public override Vector3 getWorldPos()
    {
        return m_pMonsterObject.transform.position;
    }
    public MonsterTemplate GetMonsterRow()
    {
        return (MonsterTemplate)DataTemplate.GetInstance().m_MonsterTable.getTableData(m_TableID);
    }
    // Add By ZCD
    public AnimationControl GetAnimation()
    {
        return m_AnimControl;
    }
    public int GetTableID()
    {
        return m_TableID;
    }
    public LevelamendmentTemplate GetPartnerLevelParamRow()
    {
        MonsterTemplate pRow = GetMonsterRow();
        //怪物等级 [1/21/2015 Zmy]
        return (LevelamendmentTemplate)DataTemplate.GetInstance().m_LevelamendmentTable.getTableData(pRow.getMonsterlevel());
    }

    public float GetAttackDistance()
    {
        MonsterTemplate pRow = GetMonsterRow();
        SkillTemplate pskill = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(pRow.getNormalattack());

        return pskill.getAttDistance();
    }

    public override void ChangeEffect(EM_EXTEND_ATTRIBUTE nAttrType, int nValue, EM_EFFECT_SOURCE_TYPE nType, bool bRemove = false)
    {
        switch (nAttrType)
        {
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MONEY:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_EXP:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MPRECOVER:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MPNORMALATT:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTNEAR:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE:
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE:
                {
                    if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT)
                    {
                        if (bRemove)
                        {
                            m_BuffEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_BuffEffect[(int)nAttrType] += nValue;
                        }
                    }
                }
                break;
        }
    }
	public override long			    GetBaseMaxHP()			//本体血上限
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
        {
            int nKey = ObjectSelf.GetInstance().WorldBossMgr.m_CurBossDataKey;
            if (ObjectSelf.GetInstance().WorldBossMgr.m_BossDataMap.ContainsKey(nKey))
            {
                return ObjectSelf.GetInstance().WorldBossMgr.m_BossDataMap[nKey].m_BossRoleDB.m_BossMaxHp;
            }
            else
            {
                return ObjectSelf.GetInstance().WorldBossMgr.m_CurBossHP;
            }
            
        }
        else
        {
            return pMonsterRow.getMaxHP();
        }
        
    }

	public override long			    GetMaxHP()				//血上限
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP))
        {
            //基础血量
            long nBaseValue = GetBaseMaxHP();
//             //基础血量计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生血量点数
//             int nEquipPoint = 0;
//             //技能产生血量点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
//             //最终
//             m_MaxHP = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_MaxHP = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP, m_MaxHP);
//             }

            m_MaxHP = (int)Mathf.Max(1, nBaseValue * (1 + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP] / 1000f) + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP]);
            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP);
        }
        return m_MaxHP;
    }

	public override int			    GetPhysicalBaseAttack()		//本体攻击
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getPhysicalAttack();
    }

	public override int			    GetPhysicalAttack()			//总攻击点数
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK))
        {
            //基础血量
            int nBaseValue = GetPhysicalBaseAttack();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
//             //最终
//             m_PhysicalAttack = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_PhysicalAttack = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK, m_PhysicalAttack);
//             }

            m_PhysicalAttack = (int)Mathf.Max(1, nBaseValue * (1 + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK] / 1000f) + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK]);
            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK);
        }
        return m_PhysicalAttack;
    }
	public override int			    GetPhysicalBaseDefence()		//本体防御点数
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getPhysicalDefence();
    }

	public override int			    GetPhysicalDefence()			//总防御点数
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE))
        {
            //基础血量
            int nBaseValue = GetPhysicalBaseDefence();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
//             //最终
//             m_PhysicalDefence = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_PhysicalDefence = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE, m_PhysicalDefence);
//             }

            m_PhysicalDefence = (int)Mathf.Max(1, nBaseValue * (1 + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE] / 1000f) + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE);
        }
        return m_PhysicalDefence;
    }

	public override int			    GetMagicBaseAttack()	  //本体攻击点数
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getMagicAttack();
    }

	public override int			    GetMagicAttack()		  //攻击
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK))
        {
            //基础血量
            int nBaseValue = GetMagicBaseAttack();
            //基础计算
            //符文影响本体千分比
            int nEquipSelfPermil = 0;
            //技能影响本体千分比
            int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];
            //team影响本体千分比
            int nTeamEffectPermil = 0;
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //符文产生点数
            int nEquipPoint = 0;
            //技能影响点数
            int nSpellPoint = 0;
            //team影响点数
            int nTeamPoint = 0;
            //技能增加千分比
            int nSpellPermil = 0;
            //战斗buff影响点数
            int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK];
            //战斗buff增加百分比
            int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];
            //最终
            m_MagicAttack = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));

            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                m_MagicAttack = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK, m_MagicAttack);
            }

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK);
        }
        return m_MagicAttack;
    }

	public override int			    GetMagicBaseDefence()	 //本体防御点数
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getMagicDefence();
    }

	public override int			    GetMagicDefence()		//防御
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE))
        {
            //基础血量
            int nBaseValue = GetMagicBaseDefence();
            //基础计算
            //符文影响本体千分比
            int nEquipSelfPermil = 0;
            //技能影响本体千分比
            int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];
            //team影响本体千分比
            int nTeamEffectPermil = 0;
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //符文产生点数
            int nEquipPoint = 0;
            //技能影响点数
            int nSpellPoint = 0;
            //team影响点数
            int nTeamPoint = 0;
            //技能增加千分比
            int nSpellPermil = 0;
            //战斗buff影响点数
            int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE];
            //战斗buff增加百分比
            int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];
            //最终
            m_MagicDefence = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));

            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                m_MagicDefence = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE, m_MagicDefence);
            }

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE);
        }
        return m_MagicDefence;
    }

	public override int			    GetBaseDodge()			//本体闪避
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getBaseDodge();
    }

	public override int			    GetDodge()				//总闪避
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE))
        {
            //基础血量
            int nBaseValue = GetBaseDodge();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
//             //最终
//             m_Dodge = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_Dodge = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE, m_Dodge);
//             }

            m_Dodge = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE);
        }
        return m_Dodge;
    }

	public override int			    GetBaseCritical()		//本体暴击
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getBaseCritical();
    }

	public override int			    GetCritical()			//总暴击
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL))
        {
            //基础血量
            int nBaseValue = GetBaseCritical();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
//             //最终
//             m_Critical = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_Critical = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL, m_Critical);
//             }

            m_Critical = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE]);


            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL);
        }
        return m_Critical;
    }

	public override int			    GetBaseHit()			//本体命中
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getBaseHit();
    }
    public override int			    GetHit()				//总命中
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT))
        {
            //基础血量
            int nBaseValue = GetBaseHit();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
//             //最终
//             m_Hit = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_Hit = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT, m_Hit);
//             }

            m_Hit = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT);
        }
        return m_Hit;
    }
	public override int			    GetBaseTenacity()		//本体韧性
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getBaseTenacity();
    }
	public override int			    GetTenacity()			//总韧性
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY))
        {
            //基础血量
            int nBaseValue = GetBaseHit();
//             //基础计算
//             //符文影响本体千分比
//             int nEquipSelfPermil = 0;
//             //技能影响本体千分比
//             int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
//             //team影响本体千分比
//             int nTeamEffectPermil = 0;
//             //最终基础
//             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
//             //符文产生点数
//             int nEquipPoint = 0;
//             //技能影响点数
//             int nSpellPoint = 0;
//             //team影响点数
//             int nTeamPoint = 0;
//             //技能增加千分比
//             int nSpellPermil = 0;
//             //战斗buff影响点数
//             int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY];
//             //战斗buff增加百分比
//             int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
//             //最终
//             m_Tenacity = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));
// 
//             if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
//             {
//                 m_Tenacity = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY, m_Tenacity);
//             }

            m_Tenacity = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY);
        }
        return m_Tenacity;
    }
	public override int			    GetBaseSpeed()		//本体速度
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        return pMonsterRow.getSpeed();
    }
	public override int			    GetSpeed()				//总速度
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED))
        {
            //基础血量
            int nBaseValue = GetBaseSpeed();
            //基础计算
            //符文影响本体千分比
            int nEquipSelfPermil = 0;
            //技能影响本体千分比
            int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //team影响本体千分比
            int nTeamEffectPermil = 0;
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //符文产生点数
            int nEquipPoint = 0;
            //技能影响点数
            int nSpellPoint = 0;
            //team影响点数
            int nTeamPoint = 0;
            //技能增加千分比
            int nSpellPermil = 0;
            //战斗buff影响点数
            int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED];
            //战斗buff增加百分比
            int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //最终
            m_Speed = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nBuffPoint) * (1.0f + (float)nBuffPermil / 1000.0f)));

            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                m_Speed = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED, m_Speed);
            }

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED);
        }
        return m_Speed;
    }
    public override int GetHpRecover()    //生命恢复力
    {
        MonsterTemplate pPartnerRow = GetMonsterRow();

        int nBaseValue = pPartnerRow.getLifeRestoringForce();

        int nEquipSelfPermil = 0;
        //技能影响本体千分比
        int nSpellSelfPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];
        //team影响本体千分比
        int nTeamEffectPermil = 0;
        //最终基础
        nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
        //装备产生血量点数
        int nEquipPoint = 0;
        //技能产生血量点数
        int nSpellPoint = 0;
        //team影响点数
        int nTeamPoint = 0;
        //技能增加千分比
        int nSpellPermil = 0;
        //战斗buff影响点数
        int nBuffPoint = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER];
        //战斗buff增加百分比
        int nBuffPermil = m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];

        m_HpRecover = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            m_HpRecover = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_HPRECOVER, m_HpRecover);
        }

        return m_HpRecover;
    }
	public override float			GetHitRate()		//命中率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = (pMonsterRow.getBaseHit() +  m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE]) / 1000f;

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT_RATE, _value);
        }
        return _value;
    }
	public override float			GetDodgeRate()    //闪避率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = (pMonsterRow.getBaseDodge() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE]) / 1000f;

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE_RATE, _value);
        }
        return _value;
    }
	public override float			GetCriticalRate() //暴击率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = (pMonsterRow.getBaseCritical() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE]) / 1000f;

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_RATE, _value);
        }
        return _value;
    }
	public override float			GetTenacityRate() //韧性率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = (pMonsterRow.getBaseTenacity() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE]) / 1000f;

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY_RATE, _value);
        }
        return _value;
    }
    public override float GetPhysicalHurtAddPermil() //物理伤害加深率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = ((pMonsterRow.getBasePhyDamageIncrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT] + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]) / 1000f);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HURT_ADD_PERMIL, _value);
        }
        return _value;
    }
    public override float GetPhysicalHurtReducePermil() //物理伤害减免率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = ((pMonsterRow.getBasePhyDamageDecrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT] + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]) / 1000f);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HRUT_REDUCE_PERMIL, _value);
        }
        return _value;
    }
    public override float GetMagicHurtAddPermil() //法术伤害加深率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = ((pMonsterRow.getBaseMagDamageIncrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT] + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]) / 1000f);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_ADD_PERMIL, _value);
        }
        return _value;
    }
    public override float GetMagicHurtReducePermil() //法术伤害减免率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float _value = ((pMonsterRow.getBaseMagDamageDecrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT] + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]) / 1000f);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_REDUCE_PERMIL, _value);
        }
        return _value;
    }
    public override float GetCriticalHurtAddRate() //暴击伤害加成率
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        float fPower = DataTemplate.GetInstance().m_GameConfig.getCritical_base_power(); //基础暴击伤害倍率 [7/20/2015 Zmy]
        float _value = ((pMonsterRow.getBaseCriticalDamage() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT]) / 1000f + fPower);

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_ADD_RATE, _value);
        }
        return _value;
    }

    public override float GetCriticalHurtReduceRate() //暴击伤害增加百分比
    {
        return 0;
    }

	public override int			    GetExtraHurt() //伤害附加值
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        int _value = pMonsterRow.getDamageIncrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT];

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_EXTRA_HURT, _value);
        }
        return _value;
    }
	public override int			    GetReduceHurtPoint() //伤害减免值
    {
        MonsterTemplate pMonsterRow = GetMonsterRow();
        int _value = pMonsterRow.getDamageDecrease() + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT];

        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            _value = ObjectSelf.GetInstance().LimitFightMgr.GetLimitMonsterAttribute(EM_ATTRIBUTE.EM_ATTRIBUTE_REDUCE_HURT_POINT, _value);
        }
        return _value;
    }

    public override float GetNormalSuckRate()  //普攻吸血率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK]) / 1000f;
    }
    public override float GetSpellSuckRate()  //技能吸血率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK]) / 1000f;
    }
    public override float GetCoolDownRate()   //冷却缩减率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD]) / 1000f;
    }
    public override float GetInitPowerAdditionRate()//初始怒气加成率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL]) / 1000f;
    }
    public override float GetAttackPowerAdditionRate()//攻击怒气加成率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL]) / 1000f;
    }
    public override float GetHurtPowerAdditionRate()//受击怒气加成率
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL]) / 1000f;
    }
    public override void SetFightState(int nState, X_GUID guid)
    {
        base.SetFightState(nState, guid);

        if (nState == ((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))//眩晕
        {
            SetObjectActionState(ObjectActionState.dizzy);
        }
    }
    public override void RemoveFightState(int nState, X_GUID guid)
    {
        base.RemoveFightState(nState, guid);

        if (nState == ((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO) || nState == ((int)EM_FIGHT_STATE.EM_FIGHT_STATE_NONORMAL))//眩晕
        {
            SetObjectActionState(ObjectActionState.normalAttack);
        }
    }

    public override int GetCampType()
    {
        return GetMonsterRow().getCamp();
    }

    public override float GetAddDamageRateToCampA()//对生灵阵营1伤害加成率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA]) / 1000f;
    }
    public override float GetAddDamageRateToCampB()//对神族阵营2伤害加成率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB]) / 1000f;
    }
    public override float GetAddDamageRateToCampC()//对恶魔阵营3伤害加成率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetReducDamageRateToCampA()//受生灵阵营1伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA]) / 1000f;
    }
    public override float GetReducDamageRateToCampB()//受神族阵营2伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB]) / 1000f;
    }
    public override float GetReducDamageRateToCampC()//受恶魔阵营3伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetAddDamageRateToFightNear()//对近战伤害加成率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetAddDamageRateToFightFar()//对远程伤害加成率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR]) / 1000f;
    }
    public override float GetReducDamageRateToFightNear()//受近战伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR]) / 1000f;
    }
    public override float GetReducDamageRateToFightFar()//受远程伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR]) / 1000f;
    }
    public override float GetAddDamageRateToBoss() //对boss伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS]) / 1000f;
    }
    public override float GetReducDamageRateToBoss() //受boss伤害减免率  [7/20/2015 Zmy]
    {
        return (m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS]) / 1000f;
    }

    public override float GetCampAttackParam(ObjectCreature pTarget)//攻击方阵营对防御方阵营攻击系数  [7/20/2015 Zmy]
    {
        switch (GetCampType())//攻击方
        {
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                switch (pTarget.GetCampType())//防御方
                {
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_AtoA();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_AtoB();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_AtoC();
                }
                break;
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                switch (pTarget.GetCampType())//防御方
                {
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_BtoA();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_BtoB();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_BtoC();
                }
                break;
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                switch (pTarget.GetCampType())//防御方
                {
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_CtoA();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_CtoB();
                    case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance().m_GameConfig.getAttackCoefficient_CtoC();
                }
                break;
            default:
                LogManager.LogError("!!!Error : campType is error");
                return -1;
        }
        return -1;
    }
    public override float GetCampAddDamageRate(ObjectCreature pTarget) //攻击方阵营对防御方阵营伤害加成率  [7/20/2015 Zmy]
    {
        switch (pTarget.GetCampType())
        {
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                return GetAddDamageRateToCampA();
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                return GetAddDamageRateToCampB();
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                return GetAddDamageRateToCampC();
            default:
                LogManager.LogError("!!!Error : campType is error");
                return -1;
        }
    }

    public override float GetCampReducDamageRate(ObjectCreature pTarget) //防御方阵营对攻击方阵营的伤害减免率  [7/20/2015 Zmy]
    {
        switch (pTarget.GetCampType())
        {
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                return GetReducDamageRateToCampA();
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                return GetReducDamageRateToCampB();
            case (int)EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                return GetReducDamageRateToCampC();
            default:
                LogManager.LogError("!!!Error : campType is error");
                return -1;
        }
    }

    public override float GetAddDamageRateForAttackMode(ObjectCreature pTarget) //攻击方对防御方攻击距离类型的伤害加成率  [7/20/2015 Zmy]
    {
        if (pTarget.GetIsNearAttackMold()) // 近战 [7/20/2015 Zmy]
        {
            return GetAddDamageRateToFightNear();
        }
        else
        {
            return GetAddDamageRateToFightFar();
        }
    }
    public override float GetReducDamageRateForAttackMode(ObjectCreature pTarget) //防御方对攻击方攻击距离类型的伤害减免率  [7/20/2015 Zmy]
    {
        if (pTarget.GetIsNearAttackMold()) // 近战 [7/20/2015 Zmy]
        {
            return GetReducDamageRateToFightNear();
        }
        else
        {
            return GetReducDamageRateToFightFar();
        }
    }

    public override float GetAddDamageRateForBossType(ObjectCreature pTarget) //攻击方对BOSS伤害加成率  [7/20/2015 Zmy]
    {
        if (pTarget.GetIsBossType())
        {
            return GetAddDamageRateToBoss();
        }
        return 0f;
    }

    public override float GetReducDamageRateForBossType(ObjectCreature pTarget) //防御方对BOSS伤害减免率  [7/20/2015 Zmy]
    {
        if (pTarget.GetIsBossType())
        {
            return GetReducDamageRateToBoss();
        }
        return 0f;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override int GetBaseHurtAdd()
    {
        return GetMonsterRow().getDamageBonusHit();
    }
    public override int GetHurtAddRate()
    {
        int nBaseValue = GetBaseHurtAdd();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]);

        return _value;
    }

    public override int GetBaseHurtReduce()
    {
        return GetMonsterRow().getDamageReductionHit();
    }
    public override int GetHurtReduceRate()
    {
        int nBaseValue = GetBaseHurtReduce();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]);

        return _value;
    }

    public override int GetBaseCriticalHurt()
    {
        return GetMonsterRow().getBaseCriticalDamage();
    }
    public override int GetCriticalHurtRate()
    {
        int nBaseValue = GetBaseCriticalHurt();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT]);

        return _value;
    }

    public override int GetBaseBlock()
    {
        return GetMonsterRow().getBlockHit();
    }
    public override int GetBlockRate()
    {
        int nBaseValue = GetBaseBlock();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE]);

        return _value;
    }
    public override int GetBasePierce()
    {
        return GetMonsterRow().getSabotageHit();
    }
    public override int GetPierceRate()
    {
        int nBaseValue = GetBasePierce();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE]);

        return _value;
    }
    public override int GetBaseSuck()
    {
        return GetMonsterRow().getVampireRate();
    }
    public override int GetSuckRate()
    {
        int nBaseValue = GetBaseSuck();

        int _value = (int)Mathf.Max(1, nBaseValue + m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE]);

        return _value;
    }

    public override int GetSpellAttibute(int nType)
    {
        return m_BuffEffect[nType];
    }
    /// <summary>
    /// 被伤害
    /// </summary>
    /// <param name="nHurt"></param>
    public override void OnBeHurt(int nHurt, SpellInfo _BeSpellInfo, bool bCritical)
    {
        if (_BeSpellInfo != null)
        {
            SkillTemplate pRow = _BeSpellInfo.GetSpellRow();

            //敌人受到攻击播放音效
            //AudioControler.Inst.PlaySound(pRow.getUnderAttackSound());

            if (string.IsNullOrEmpty(pRow.getUnderAttackEffID()) == false)
            {
                Transform paran = GetGameObject().GetComponent<AnimationEventControler>().GetTransform(GetGameObject().transform,_BeSpellInfo.GetSpellRow().getEffectHitPoint());
                paran.transform.rotation = Quaternion.identity;
                EffectManager.GetInstance().InstanceEffect_Static(pRow.getUnderAttackEffID(),this,paran, 0f);
            }

            //多段技能会造成多次伤害的，受击怒气只奖励一次 [10/17/2015 Zmy]
            AngertableTemplate _data = null;
            if (_BeSpellInfo.GetCurIntervalNode() >= 0 && _BeSpellInfo.GetCurIntervalNode() <= 1)
            {
                _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(GetMonsterRow().getFuryId());

                FightControler.Inst.OnUpdatePowerValue(GetGroupType(), _data.getGethitFury());
            }

            float nNum = ((float)nHurt / (float)GetMaxHP()) * 100 * _data.getHPTransformFury();
            FightControler.Inst.OnUpdatePowerValue(GetGroupType(), (int)nNum);

            //受击怒气奖励公式: 奖励怒气 =（1 + buff影响 –  debuff影响）*（1 - 攻击者技能特性）
            //int nValue = (int)((1f + (float)m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP] / 1000f) * (1f - (float)_BeSpellInfo.GetSpellRow().getWeakenTargetFuryReward() / 100f));
            //if(FightControler.Inst != null)
            //    FightControler.Inst.OnUpdatePowerValue(GetGroupType(), nValue);

            //根据掉血百分比转换成怒气奖励 ：奖励怒气 = 每流失1%的生命奖励X点怒气*（1 + buff影响 –  debuff影响）*（1 - 攻击者技能特性）*（1+受击额外怒气）
//             HerofuryTemplate pFury = (HerofuryTemplate)DataTemplate.GetInstance().m_HeroFuryTable.getTableData(GetMonsterRow().getMonsterlevel());
//             int nTemplate = GetMonsterRow().getHPTransformFury() - 1;
//             float nNum = ((float)nHurt / (float)GetMaxHP()) * 100 * pFury.getTemplate()[nTemplate] *
//                           (1f + (float)m_BuffEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP] / 1000f) *
//                           (1f - (float)_BeSpellInfo.GetSpellRow().getWeakenTargetFuryReward() / 100f) *
//                           (1f);
//             if (FightControler.Inst != null)
//                 FightControler.Inst.OnUpdatePowerValue(GetGroupType(), (int)nNum);
            
        }
        if (nHurt > 0)
        {
            UI_HurtInfo pData = new UI_HurtInfo();
            pData.pTarget = this;
            pData.nHurt = -nHurt;
            pData.bCritical = bCritical;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_ChangeHP, pData);

            if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
            {
                SceneObjectManager.GetInstance().WorldBossDamageSum += nHurt;
            }

            OnTriggerHurtAnim(nHurt);
        }
        
    }
    /// <summary>
    /// 被治疗
    /// </summary>
    /// <param name="nHeal"></param>
    /// <param name="pInfo"></param>
    public override void OnHeal(int nHeal, SpellInfo pInfo = null)
    {
        base.OnHeal(nHeal, pInfo);

        if (pInfo != null)
        {
            SkillTemplate pRow = pInfo.GetSpellRow();
            if (string.IsNullOrEmpty(pRow.getUnderAttackEffID()) == false)
            {
                Transform paran = GetGameObject().GetComponent<AnimationEventControler>().GetTransform(GetGameObject().transform, pInfo.GetSpellRow().getEffectHitPoint());
                EffectManager.GetInstance().InstanceEffect_Static(pRow.getUnderAttackEffID(), this, paran, 0f);
            }
        }
        if (nHeal > 0)
        {
            UI_HurtInfo pData = new UI_HurtInfo();
            pData.pTarget = this;
            pData.nHurt = nHeal;
            pData.bCritical = false;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_ChangeHP, pData);
        }
    }
    /// <summary>
    /// 技能被打断
    /// </summary>
    public override void OnBeBreak(int nHurt, SpellInfo _BeSpellInfo)
    {
        if (!IsSkillType())
            return;
        if (_BeSpellInfo != null)
        {
            SkillTemplate pRow = _BeSpellInfo.GetSpellRow();
            switch (pRow.getInterruptSkill())
            {
                case (int)EM_SPELL_BREAK_TYPE.EM_SPELL_BREAK_TYPE_NOBREAK:
                    break;
                case (int)EM_SPELL_BREAK_TYPE.EM_SPELL_BREAK_TYPE_BREAK:
                    if (IsNormalAttack())
                    {
                        switch (GetSpellNormal().GetSpellRow().getDamageInterruptType())
                        {
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_NOBEBREAK:
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVALUE:
                                if (nHurt >= GetSpellNormal().GetSpellRow().getDamageInterrupt())
                                {
                                    IsGuidanceSkill(GetSpellNormal().GetSpellRow().getSkillReleaseType(), GetSpellNormal().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVPERCENT:
                                if (nHurt >= (GetMaxHP() * (float)GetSpellNormal().GetSpellRow().getDamageInterrupt() / 100))
                                {
                                    IsGuidanceSkill(GetSpellNormal().GetSpellRow().getSkillReleaseType(), GetSpellNormal().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                        }
                    }
                    else if (IsSkillAttack())
                    {
                        switch (GetSpellInfoNow().GetSpellRow().getDamageInterruptType())
                        {
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_NOBEBREAK:
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVALUE:
                                if (nHurt >= GetSpellInfoNow().GetSpellRow().getDamageInterrupt())
                                {
                                    IsGuidanceSkill(GetSpellInfoNow().GetSpellRow().getSkillReleaseType(), GetSpellInfoNow().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVPERCENT:
                                if (nHurt >= (GetMaxHP() * (float)GetSpellInfoNow().GetSpellRow().getDamageInterrupt() / 100))
                                {
                                    IsGuidanceSkill(GetSpellInfoNow().GetSpellRow().getSkillReleaseType(), GetSpellInfoNow().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                        }
                    }
                    break;
            }
        }
    }
    //是否是攻击状态
    private bool IsSkillType()
    {
        return (GetActionState() == ObjectActionState.normalAttack || GetActionState() == ObjectActionState.AttackIdle || GetActionState() == ObjectActionState.Attacking
            || GetActionState() == ObjectActionState.skillAttack || GetActionState() == ObjectActionState.skilling || GetActionState() == ObjectActionState.skillEnd || GetActionState() == ObjectActionState.AttackIdleing) ? true : false;
    }
    //是否是普通攻击状态
    private bool IsNormalAttack()
    {
        return (GetActionState() == ObjectActionState.normalAttack || GetActionState() == ObjectActionState.AttackIdle || GetActionState() == ObjectActionState.Attacking || GetActionState() == ObjectActionState.AttackIdleing) ? true : false;
    }
    //是否是技能攻击状态
    public bool IsSkillAttack()
    {
        return (GetActionState() == ObjectActionState.skillAttack || GetActionState() == ObjectActionState.skilling || GetActionState() == ObjectActionState.skillEnd) ? true : false;
    }
    //是否是引导技能
    private bool IsGuidanceSkill(int type, string[] name)
    {
        switch (type)
        {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
                for (int i = 0; i < name.Length; ++i)
                {
                    EffectManager.GetInstance().DisableStaticEffect(this, name[i]);
                }
                return true;
        }
        return false;
    }
    public void OnLockTarget()
    {
        if (m_CurLockTarget != null)
        {
            SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
            return;
        }

        //SceneObjectManager.GetInstance().LockHeroTarget(m_pMonsterObject, out m_CurLockTarget);
        //SetObjectActionState(ObjectCreature.ObjectActionState.moveTarget);
        SceneObjectManager.GetInstance().LockHeroTarget(this, out m_CurLockTarget);
        SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
    }
    public void OnClearUpLockTarget(X_GUID id)
    {
        if (m_CurLockTarget == null)
            return;
        if (m_CurLockTarget.GetGuid().Equals(id) == true)
        {
            m_CurLockTarget = null;
            if (IsBreakSpellAction() == false)
                return;
            if (m_AnimControl.IsEndFrame == false)
            {
                m_AnimControl.Anim_Fidle(false);
            }

            SetObjectActionState(ObjectCreature.ObjectActionState.scanning); 
        }
    }
    public bool IsBreakSpellAction()
    {
        if (GetActionState() == ObjectCreature.ObjectActionState.skillAttack || GetActionState() == ObjectCreature.ObjectActionState.skilling)
        {
            //以下技能释放时，不会因为锁定目标的死亡而中断当前正在释放的技能 [3/9/2015 Zmy]
            switch (GetSpellInfoNow().GetTargetType())
            {
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND:
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL:
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_MIN_HPPERCENT:
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL_NO_SELF:
                    return false;
                default:
                    break;
            }
        }
        return true;
    }
    public void OnAttackMoveToTarget()
    {
        //if (m_CurLockTarget == null || m_CurLockTarget.GetGameObject() == null)
        //{
        //    SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        //    return;
        //}

        //float distance = Vector3.Distance(m_pMonsterObject.transform.position, m_CurLockTarget.GetGameObject().transform.position);
        //distance = distance - GetNavMesh().radius - m_CurLockTarget.GetNavMesh().radius;
        //if (m_NavMesh.enabled == false)
        //    m_NavMesh.enabled = true;
        //if (distance > GetAttackDistance())
        //{
        //    m_NavMesh.SetDestination(m_CurLockTarget.GetGameObject().transform.position);
        //}
        //else
        //{
        //    m_NavMesh.SetDestination(GetGameObject().transform.position);
        //    SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
        //}

        int roundIdx = StoryAnimEditorContrler.GetInst().GetCurrentFightCount();
        Vector3 pos = FightEditorContrler.GetInstantiate().GetMonsterMovePos(roundIdx, this);

        if (m_NavMesh.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if (m_CurLockTarget == null || m_CurLockTarget.GetGameObject() == null)
            {
                SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            }
            else
            {
                SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
            }
        }
    }

    public void OnNormalAttack()
    {
        if (m_CurLockTarget == null || m_CurLockTarget.GetGameObject() == null)
        {
            SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            return;
        }

        SetActivationSpellCD(m_SpellNormal);

        SkillTemplate pData = m_SpellNormal.GetSpellRow();

        //释放技能音效
        AudioControler.Inst.PlaySound(pData.getAttackSound());

        if (GetIsNearAttackMold())
        {
            OnCommonSkillActiveOnce();
        }
        else
        {
            if (string.IsNullOrEmpty(pData.getBallIsticEffID()[0]) == false)
            {
                EffectManager.GetInstance().InstanceEffect_Bullet(pData.getBallIsticEffID()[0], this, m_CurLockTarget, m_SpellNormal);
            }
        }

        SetObjectActionState(ObjectCreature.ObjectActionState.checkHurting);
    }

    public override void OnCommonSkillActiveOnce()
    {
        //base.OnSpellActiveOnce();
        if (m_CurLockTarget == null)
            return;

        Spell pSkill = new Spell();
        pSkill.SetHolder(this);
        pSkill.SetTargetGuid(m_CurLockTarget.GetGuid());
        pSkill.Init(m_SpellNormal);
        pSkill.ActiveOnce();

        pSkill = null;
    }
    public void OnUpdateRander()
    {
        if (IsAlive() == false)
            return;
        if (m_CurLockTarget == null || m_CurLockTarget.GetGameObject() == null)
            return;

        Debug.DrawLine(getWorldPos(), m_CurLockTarget.GetGameObject().transform.position, Color.yellow);
        //Quaternion ratation = Quaternion.LookRotation(m_CurLockTarget.GetGameObject().transform.position - getWorldPos());
        //m_pMonsterObject.transform.rotation = Quaternion.Slerp(m_pMonsterObject.transform.rotation, ratation, 2 * Time.deltaTime);
        if (GetActionState() != ObjectActionState.skillAttack && GetActionState() != ObjectActionState.skilling)
            m_pMonsterObject.transform.LookAt(m_CurLockTarget.GetGameObject().transform);
    }

    public void UpdateCheckTarget()
    {
        if (m_CurLockTarget == null && IsAlive())
        {
            if (IsSkillType())
                return;

            SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        }
    }
    //进入一次的状态机 [10/19/2015 Zmy]
    public void OnUpdateOnceLogicState()
    {
        if (GetActionState() != GetLastActionState())
        {
            SetLastActionState(GetActionState());
            switch (GetActionState())
            {
                case ObjectCreature.ObjectActionState.Hurting:
                    m_AnimControl.Anim_Hurt();
                    //SetObjectActionState(ObjectCreature.ObjectActionState.none);
                    break;
                case ObjectActionState.dizzy:
                    m_AnimControl.Anim_Dizzy();
                    //SetObjectActionState(ObjectCreature.ObjectActionState.none);
                    break;
                default:
                    break;
            }
        }
    }
    public override void OnAttackStateLogicUpdate()
    {
        base.OnAttackStateLogicUpdate();

        OnUpdateRander();
        OnUpdateOnceLogicState();

        switch (GetActionState())
        {
            case ObjectActionState.idle:
                //播放待机 [1/22/2015 Zmy]
                UpdateCheckTarget();
                break;
            case ObjectActionState.forward:
                //播放移动
                m_AnimControl.Anim_Run();
                break;
            case ObjectActionState.scanning:
                if (ObjectSelf.GetInstance().isSkillShow)
                    return;
                //m_AnimControl.Anim_Run();
                m_AnimControl.Anim_Fidle(false);
                //搜索目标
                OnLockTarget();
                break;
            case ObjectActionState.moveTarget:
                m_AnimControl.Anim_Run();
                OnAttackMoveToTarget();
                break;
            case ObjectActionState.normalAttack:
                if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_NONORMAL))
                {
                    m_AnimControl.Anim_Fidle(false);
                    SetObjectActionState(ObjectCreature.ObjectActionState.none);
                    break;
                }
                m_AnimControl.Anim_Attack();
                m_AnimControl.SetNormalAttackSpeed(GetSpeed());
                m_AnimControl.Anim_StartAttack();
                SetObjectActionState(ObjectCreature.ObjectActionState.Attacking);
                //普通攻击
                //OnNormalAttack();
                break;
            case ObjectCreature.ObjectActionState.checkHurting:
                m_AnimControl.UpdateSurplusNormalAttackTime();
                break;
            case ObjectActionState.AttackIdle:
                m_AnimControl.Anim_Fidle();
                if (m_AnimControl.GetFidLengh() <= 0)
                {
                    SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
                }
                else
                {
                    SetObjectActionState(ObjectCreature.ObjectActionState.AttackIdleing);
                }
                break;
            case ObjectActionState.AttackIdleing:
                m_AnimControl.UpdateFidTime();
                break;
            case ObjectActionState.destory:
                OnDestorySelf();
                break;
            case ObjectActionState.skillAttack:
                //使用技能
                OnSkillActionLogic(m_SpellInfoNow);
                if (m_SkillLockTarget != null && m_SkillLockTarget.GetGameObject() != null)
                    GetGameObject().transform.LookAt(m_SkillLockTarget.GetGameObject().transform);

                //释放技能音效
                AudioControler.Inst.PlaySound(m_SpellInfoNow.GetSpellRow().getAttackSound());

                SetObjectActionState(ObjectCreature.ObjectActionState.skilling);
                break;
            case ObjectActionState.skilling:
                //技能状态中。。。
                OnGuidancesSkillLogic();//引导技能逻辑

                break;
            case ObjectActionState.skillEnd:
                // 技能结束状态(只是用于引导类技能)
                break;
            default:
                break;
        }
    }
  
    //引导技能逻辑
    private void OnGuidancesSkillLogic()
    {
        switch (GetSpellInfoNow().GetSpellRow().getSkillReleaseType())
        {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
                if (m_MaxGuidanceTime == -1)
                {
                    m_AnimControl.Anim_GuidanceSkill(GetSpellInfoNow().GetSpellRow().getAction()[2], false);
                    SetObjectActionState(ObjectCreature.ObjectActionState.skillEnd);
                    return;
                }
                if (m_MaxGuidanceTime == 0)
                    m_MaxGuidanceTime = ((float)GetSpellInfoNow().GetSpellRow().getParam()[0]) / 1000;
                m_GuidanceTime += Time.deltaTime * Time.timeScale;
                if (m_GuidanceTime >= m_MaxGuidanceTime)
                {
                    OnSpecialSkillActiveOnce();
                    if (GetSpellInfoNow().GetSpellRow().getParam()[m_count * 5] == -1)
                    {
                        m_MaxGuidanceTime = -1;
                    }
                    else
                        m_MaxGuidanceTime = ((float)GetSpellInfoNow().GetSpellRow().getParam()[m_count * 5]) / 1000;
                    m_count++;
                }
                break;
            default:
                {
                    //LogManager.LogError("!!!Error: OnGuidancesSkillLogic() HeroState is skilling FreeSkill Param is error : " + GetSpellInfoNow().GetSpellRow().getId());
                }
                break;
        }
    }

    //检查自身是否可以释放技能
    public bool CheckSelfCanCastSkill()
    {
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_FORBID))
        {
            return false;
        }
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))
        {
            return false;
        }
        if (GetActionState() == ObjectActionState.moveTarget)
        {
            return false;
        }
        return true;
    }
    //判断技能释放条件
    public bool OnPre_CheckUseSkillCondtion()
    {

        if (m_SkillLockTarget == null && GetSpellInfoNow().IsNeedTarget())
        {
            Debug.Log("无法正确获取目标" + " skillID："+ GetSpellInfoNow().GetSpellID());
            return false;
        }

        m_pTempSell.SetHolder(this);
       // m_pTempSell.SetTargetGuid(m_CurLockTarget.GetGuid());
        m_pTempSell.Init(GetSpellInfoNow());

        //释放消耗
        m_pTempSell._OnFreeConsume();
        //释放奖励
        m_pTempSell._OnFreeAward();

        return true;
    }

    //判断技能释放条件
    public bool CheckSkillCondtion(SpellInfo spellInfo)
    {
        if (spellInfo.GetSpellType() != 1)
        {
            return false;
        }

        m_pTempSell.SetHolder(this);
        // m_pTempSell.SetTargetGuid(m_CurLockTarget.GetGuid());
        m_pTempSell.Init(spellInfo);

        //CD 检查：
        if (m_pTempSell._CheckSpellCooldown() == false)
        {
            return false;
        }

        //消耗检查：
        if (spellInfo.IsSkillRelease(this) == false)
        {
            return false;
        }
        return true;
    }
    public override void OnSpecialSkillActiveOnce()
    {
        if (GetSpellInfoNow().IsNeedTarget())
        {
            OnSkillTag(GetSpellInfoNow().GetTargetType());
        }
        else
        {
            Spell pSkill = new Spell();
            pSkill.SetHolder(this);
            pSkill.Init(GetSpellInfoNow());
            pSkill.ImmActiveOnce();
            pSkill = null;
        }
    }
    //根据不同的技能目标类型添加不同的目标
    private void OnSkillTag(int value)
    {
        Spell pSkill = new Spell();
        switch (value)
        {
            case (int)EM_TARGET_TYPE.EM_TARGET_INVALID:
                if (m_CurLockTarget == null)
                    return;
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(m_CurLockTarget.GetGuid());
                pSkill.Init(GetSpellInfoNow());
                pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND://带完善选择自己方目标
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RANDOM:
            case (int)EM_TARGET_TYPE.EM_TARGET_SELF_RANDOM:
                if (m_SkillLockTarget == null)
                    return;
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(m_SkillLockTarget.GetGuid());
                pSkill.Init(GetSpellInfoNow());
                pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY://带完善选择敌对方目标
            case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RANDOM:
                if (m_SkillLockTarget == null)
                    return;
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(m_SkillLockTarget.GetGuid());
                pSkill.Init(GetSpellInfoNow());
                pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(this.GetGuid());
                pSkill.Init(GetSpellInfoNow());
                pSkill.ImmActiveOnce();
                break;
            default:
                pSkill.SetHolder(this);
                pSkill.Init(GetSpellInfoNow());
                pSkill.ImmActiveOnce();
                break;
        }

        pSkill = null;
    }
    public override void SetObjectActionState(ObjectCreature.ObjectActionState state)
    {
        if (state != base.GetActionState())
        {
            base.SetObjectActionState(state);
        }
    }

    public override ObjectCreature.ObjectActionState GetActionState()
    {
        return base.GetActionState();
    }

    public override void InitBaseData()
    {
        base.InitBaseData();
        if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
        {
            SetHP((int)ObjectSelf.GetInstance().WorldBossMgr.m_CurBossHP);
        }
        else
        {
            SetHP(this.GetMaxHP());
        }

        CampaignMonsterGroupData pData = SceneObjectManager.GetInstance().GetCapaignMonsterGroupData();
        SetTeamPos(pData.FindMonsterTeamPosByGUID(m_Guid));

        //设置特殊怪物的表现 [3/28/2015 Zmy]
        //TODO
        InitSpecialMonster();

    }

    public override void OnKillTarget(ObjectCreature pTarget, SpellInfo pSpellInfo)
    {
        base.OnKillTarget(pTarget, pSpellInfo);

        //if (m_CurLockTarget != null && pTarget.GetGuid().Equals(m_CurLockTarget.GetGuid()))
        //{
        //    m_CurLockTarget = null;
        //    SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
        //}
        if (IsSkillAttack())
        {
            if (m_SkillLockTarget != null && pTarget.GetGuid().Equals(m_SkillLockTarget.GetGuid()))
            {
                m_SkillLockTarget = null;
                SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            }
            if (m_CurLockTarget != null && pTarget.GetGuid().Equals(m_CurLockTarget.GetGuid()))
            {
                m_CurLockTarget = null;
                SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            }
        }
        else if (IsNormalAttack())
        {
            if (m_CurLockTarget != null && pTarget.GetGuid().Equals(m_CurLockTarget.GetGuid()))
            {
                m_CurLockTarget = null;
                SetObjectActionState(ObjectCreature.ObjectActionState.scanning);
            }
        }
    }

    public override void OnBeKilled(ObjectCreature pCaster, SpellInfo pSpellInfo)
    {
        base.OnBeKilled(pCaster, pSpellInfo);

        m_AnimControl.Anim_Die();

        SetObjectActionState(ObjectCreature.ObjectActionState.deathing);
        
        // Add By ZCD
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_EnemyOnDie, m_Guid);

        SceneObjectManager.GetInstance().OnClearTargetForHero(m_Guid);
        SceneObjectManager.GetInstance().OnUpdateImpactOfMakeDeath(m_Guid);
        SceneObjectManager.GetInstance().OnCacheDeadObj(this,GetGroupType());
        //GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_EnemyOnDie);

        m_CurLockTarget = null;
        m_SkillLockTarget = null;

        if (m_IsDeathBuff)
            OnCreateDeathImpact();

    }

    public void OnDestorySelf()
    {
        EffectManager.GetInstance().OnRemoveDeadObjOfEffect(this);
        SceneObjectManager.GetInstance().OnDeleteDeadObj(GUID);
    }

    // 启动飞行技能弹道特效 [3/4/2015 Zmy]
    public void OnSkillFly_Effect(int nHitCount)
    {
        string _EffectName = string.Empty;
        SkillTemplate pData = GetSpellInfoNow().GetSpellRow();
        if (nHitCount > 0 && nHitCount <= pData.getBallIsticEffID().Length)
        {
            _EffectName = pData.getBallIsticEffID()[nHitCount - 1];
            GetSpellInfoNow().UpdateCurInterval();
        }
        else
        {
            _EffectName = pData.getBallIsticEffID()[0];
        }

        if (string.IsNullOrEmpty(_EffectName) == false && GetSpellInfoNow() != null && pData != null && m_SkillLockTarget != null)
        {
            string effectname = _EffectName;
            GameObject gameObject = GetGameObject();
            GameObject _gameObject = m_SkillLockTarget.GetGameObject();
            SpellInfo spell = GetSpellInfoNow();
            EffectManager.GetInstance().InstanceEffect_Bullet(_EffectName, this, m_SkillLockTarget, GetSpellInfoNow());
        }
    }

    // 启动瞬发技能特效 [3/4/2015 Zmy]
    public void OnSkillMoment_Effect()
    {
        //瞬发技能技能释放中
        AudioControler.Inst.PlaySound(m_SpellInfoNow.GetSpellRow().getBulletsFiredSound());
        OnSpecialSkillActiveOnce();
    }
    //引导技能特效
    public void OnSkillGuidance_Effect()
    {

        SkillTemplate pData = GetSpellInfoNow().GetSpellRow();
        int[] _data = GetSpellInfoNow().GetSpellRow().getParam();
        List<int> data = new List<int>();
        for (int i = 0; i < _data.Length / 5; i++)
        {
            if (_data[i * 5] != -1)
                data.Add(_data[i * 5]);
        }
        float _efftime = ((float)data[data.Count - 1]) / 1000 - m_GuidanceTime;
        string[] _EffectName;
        _EffectName = pData.getBallIsticEffID();
        m_AnimControl.Anim_GuidanceSkill(GetSpellInfoNow().GetSpellRow().getAction()[1], true);
        if (!_EffectName[0].Equals("-1"))
        {
            EffectManager.GetInstance().InstanceEffect_Static(_EffectName[0], this, GetGameObject().transform.position, _efftime, GetSpellInfoNow());
        }
        if (!_EffectName[1].Equals("-1"))
        {
            EffectManager.GetInstance().InstanceEffect_Static(_EffectName[1], this, FightEditorContrler.GetInstantiate().GetHerosCenter(), _efftime, GetSpellInfoNow());
        }
    }

    // 创建特殊的怪物对象 [3/28/2015 Zmy]
    protected void InitSpecialMonster()
    {
        MonsterTemplate pData = GetMonsterRow();
        //只有一个表字段且是0的值 表示是正常怪物
        if (pData.getDeathSkillType().Length == 1 && pData.getDeathSkillType()[0] == 0)
            return;

        int nSumProb = 0;
        for (int i = 0; i < pData.getDeathSkillProb().Length; ++i )
        {
            nSumProb += pData.getDeathSkillProb()[i];
        }
        System.Random ran = new System.Random();
        int nRand = ran.Next(1, nSumProb);
        int nReturnIndex = -1;
        int nExtentA = 0;
        int nExtentB = 0;
        // 根据权重区间取得所要选择的索引 [3/28/2015 Zmy]
        for (int i = 0; i < pData.getDeathSkillProb().Length; ++i)
        {
            nExtentB = nExtentA + pData.getDeathSkillProb()[i];
            if (nRand > nExtentA && nRand <= nExtentB)
            {
                nReturnIndex = i;
                break;
            }
            else
            {
                nExtentA = nExtentB;
            }
        }
        m_IsDeathBuff = true;
        m_DeathSkillID = DataTemplate.GetInstance().m_GameConfig.getDeathSkillType()[nReturnIndex];
        m_DeathSkillShader = DataTemplate.GetInstance().m_GameConfig.getDeathSKillTypeShader()[nReturnIndex];

        GameObject obj = GetAnimation().EventControl.MainBody.gameObject;
        if (obj != null)
        {
            //重设模型的shader [3/28/2015 Zmy]
            obj.renderer.material.shader = Shader.Find(m_DeathSkillShader);
        }
    }

    //生成死亡skill,此函数只在怪物死亡时触发，修改怪物附带的技能信息 [3/28/2015 Zmy]
    protected void OnCreateDeathImpact()
    {
        List<ObjectHero> pList = SceneObjectManager.GetInstance().GetSceneHeroList();
        for (int i = 0; i < pList.Count; ++i )
        {
            if (pList[i] != null && pList[i].IsAlive())
            {
                SkillTemplate pSkillRow = (SkillTemplate)DataTemplate.GetInstance().m_SkillTable.getTableData(m_DeathSkillID);
                if (pSkillRow != null && string.IsNullOrEmpty(pSkillRow.getBallIsticEffID()[0]) == false)
                {
                    // 怪物死亡后，临时修改他的普攻技能信息，用于触发死亡后掉落的技能流程 [3/30/2015 Zmy]
                    m_SpellNormal.Init(m_DeathSkillID);
                    SetSkillLockTarget(pList[i]);
                    m_CurLockTarget = pList[i];
                    EffectManager.GetInstance().InstanceEffect_Bullet(pSkillRow.getBallIsticEffID()[0], this, pList[i], m_SpellNormal, DeathImpactEffectHit_Callback);
                }
            }
        }
    }
    // 觸發死亡掉落buff后，特效命中對象后回調處理 [3/30/2015 Zmy]
    private void DeathImpactEffectHit_Callback(ObjectCreature Targetobj)
    {
        Spell pSkill = new Spell();
        pSkill.SetHolder(this);
        pSkill.SetTargetGuid(Targetobj.GetGuid());
        pSkill.Init(m_SpellNormal);
        pSkill.ActiveOnce();

        pSkill = null;
    }

    //技能动作结束帧完毕，清除一些必要的技能状态 [10/17/2015 Zmy]
    public void OnClearSpellState()
    {
        GetSpellInfoNow().ClearIntervalNode();
    }

    //触发受击动作 [10/20/2015 Zmy]
    private void OnTriggerHurtAnim(int nHurt)
    {
        // 在普通攻击过程中的造成伤害或者制弹道物的时间点到普攻动作结束的时间点期间 [10/19/2015 Zmy]
        if (GetActionState() == ObjectCreature.ObjectActionState.checkHurting)
        {
            if (m_AnimControl.LastAnim.Equals("Hurt1") == false)
            {
                //此状态下。不切受击状态直接播放动作，用于剩余攻击时间的检测 [10/19/2015 Zmy]
                m_AnimControl.Anim_Hurt();
                return;
            }
        }
        else if (GetActionState() == ObjectCreature.ObjectActionState.dizzy) // 在眩晕动作持续播放时 受到攻击播放受击动作[10/19/2015 Zmy]
        {
            if (m_AnimControl.LastAnim.Equals("Hurt1") == false)
            {
                SetObjectActionState(ObjectCreature.ObjectActionState.Hurting);
                return;
            }
        }
        else
        {
            if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO) || GetActionState() == ObjectCreature.ObjectActionState.deathing || GetActionState() == ObjectCreature.ObjectActionState.none)
                return;
            // 在受到的单次伤害大于等于自身血量上限的%时 [10/19/2015 Zmy]
            float nPerc = (float)(nHurt / GetMaxHP()) * 100f;
            if (nPerc >= DataTemplate.GetInstance().m_GameConfig.getDamage_interrupt_life_perc())
            {
                SetObjectActionState(ObjectCreature.ObjectActionState.Hurting);
                return;
            }
        }
    }
}
