using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.SkillCore;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using DreamFaction.GameSceneEditor;
using DreamFaction.GameAudio;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
public class ObjectHero: ObjectCreature
{
    private HeroData                m_HeroData = new HeroData();    //英雄数据
    private int[]                   m_ItemEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];   //符文的影响
    private int[]                   m_SpellEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];  //技能buff的影响
    private int[]                   m_TeamEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];   //神器影响
    private int[]                   m_DowerEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];  //星图天赋的影响
    private int[]                   m_TrainEffect = new int[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER];  //培养属性的影响

    private SpellInfo               m_SpellNormal = new SpellInfo();
    private SpellInfo[]             m_SpellInfo = new SpellInfo[GlobalMembers.MAX_DB_SPELL_NUM];
    private Spell                   m_pTempSell = new Spell();                         //临时技能对象,用于释放技能前验证条件
    private ObjectMonster           m_CurLockTarget;                                   //当前普通攻击锁定的目标 [1/22/2015 Zmy]
    private ObjectCreature          m_SkillLockTarget;                                 //当前技能锁定目标
    
	private GameObject              m_HeroObject;   
    private NavMeshAgent            m_NavMesh;

    private List<int>               m_RunePassiveList = new List<int>();//符文组合条件

    private PassiveSpellLogic       m_PassiveSpellLogic = new PassiveSpellLogic();
    private EM_SPELL_PASSIVE_INDEX  m_LaunchFreeSpellIndex = EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_INITIATIVE;

    public ObjectHero()
    {
        for (int i = 0; i < GlobalMembers.MAX_DB_SPELL_NUM; ++i)
        {
            if (m_SpellInfo[i] == null)
            {
                m_SpellInfo[i] = new SpellInfo();
            }
        }
        SetGroupType(EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO);

    }
    public void SetSkillLockTarget(ObjectCreature skillobj)
    {
        m_SkillLockTarget = skillobj;
    }
    public ObjectCreature GetSkillLockTarget()
    {
        return m_SkillLockTarget;
    }
    public ObjectMonster GetCurLockTarget()
    {
        return m_CurLockTarget;
    }
    public SpellInfo GetSpellNormal() { return m_SpellNormal;}
    public SpellInfo[] Getm_SpellInfo() { return m_SpellInfo;}
    public Spell GetTempSpell() { return m_pTempSell; }
    public HeroTemplate GetHeroRow()
    {
        int nTableID = m_HeroData.TableID;
        return (HeroTemplate)DataTemplate.GetInstance().m_HeroTable.getTableData(nTableID);
    }
    public HeroaddstageTemplate GetHeroaddStageRow()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return GameUtils.GetCurAdvancedData(pPartnerRow.getBorn(), pPartnerRow.getQosition(), m_HeroData.StarLevel, m_HeroData.CurStage);
    }
    public LevelamendmentTemplate GetPartnerLevelParamRow()
    {
        int nLevel = m_HeroData.Level;
        return (LevelamendmentTemplate)DataTemplate.GetInstance().m_LevelamendmentTable.getTableData(nLevel);
    }
    public void SetHeroData(HeroData heroData)
    {
        m_HeroData.Copy(heroData);
    }

    public void ClearPassiveSpellLogic()
    {
        m_PassiveSpellLogic.ClearUp();
    }
    public void InitEventData()
    {
        m_AnimControl.InitEventData(GetHeroRow().getArtresources());
    }

    public override HeroData GetHeroData()
    {
        return m_HeroData;
    }

    public EM_SPELL_PASSIVE_INDEX GetLaunchFreeSpellIndex()
    {
        return m_LaunchFreeSpellIndex;
    }
	public void SetHeroObject(GameObject _object)
	{
		m_HeroObject = _object;
        m_NavMesh    = _object.GetComponent<NavMeshAgent>();
        m_NavMesh.enabled = true;
        m_AnimControl= _object.AddComponent<AnimationControl>();
        m_AnimControl.SetOwnerType(1);
	}
    public void SetSpellNormalData()
    {
//         for (int i = 0; i < m_HeroData.SpellDataList.Length; i++ )
//         {
//             int nSkillID = m_HeroData.SpellDataList[i].SpellID;
//             m_SpellInfo[i].Init(nSkillID);
//         }

        for (int i = 0; i < m_HeroData.HeroSkillDB.SkillList.Count; ++i )
        {
            int nSkillID = m_HeroData.HeroSkillDB.SkillList[i];
            m_SpellInfo[i].Init(nSkillID);
        }

        m_SpellNormal.Init(GetHeroRow().getNormalskill());
    }

    public void SetWorldPosRotation(Vector3 pos, Quaternion rotation)
    {
        m_HeroObject.transform.position = pos;
        m_HeroObject.transform.rotation = rotation;
    }

    public override Vector3 getWorldPos()
    {
        return m_HeroObject.transform.position;
    }
    public override GameObject GetGameObject()
    {
        return m_HeroObject;
    }
    public NavMeshAgent GetNavMesh()
    {
        return m_NavMesh;
    }
    public AnimationControl GetAnimation()
    {
        return m_AnimControl;
    }
    public override X_GUID GetGuid()
    {
        return m_HeroData.GUID;
    }

    public override int GetFuryIdForTable()
    {
        return GetHeroRow().getFuryId();
    }

    //是否是近戰攻击模式 [3/6/2015 Zmy]
    public bool GetIsNearAttackMold()
    {
        HeroTemplate pRow = GetHeroRow();

        return pRow.getClientSignType()[0] == 0 ? true : false;
    }
    private bool IsHaveCabalaAddtion(int nID)
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        for (int i = 0; i < pPartnerRow.getMsid().Length; ++i)
        {
            if (pPartnerRow.getMsid()[i] == nID)
            {
                return true;
            }
        }
        return false;
    }

    public int GetCabalaAddtionValue(int nID)
    {
        for (int i = 0; i < m_HeroData.HeroCabalaDB.CabalaList.Count; ++i)
        {
            int _tableID = m_HeroData.HeroCabalaDB.CabalaList[i].TableID;
            if (_tableID == nID)
            {
                int nLev = m_HeroData.HeroCabalaDB.CabalaList[i].IntensifyLev;
                if (nLev <= 0)
                    return 0;

                MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
                if (nLev > _row.getLevel().Length)
                {
                    LogManager.LogError("!!!Error: GetPhysicalBaseDefence() CabalaLev > _row.Lenght：" + nLev);
                }
                return _row.getValue()[nLev - 1];
            }
        }
        return 0;
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
                    if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE)
                    {
                        if (bRemove)
                        {
                            m_ItemEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_ItemEffect[(int)nAttrType] += nValue;
                        }
                    }
                    else if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT)
                    {
                        if (bRemove)
                        {
                            m_SpellEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_SpellEffect[(int)nAttrType] += nValue;
                        }
                    }
                    else if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM)
                    {
                        if (bRemove)
                        {
                            m_TeamEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_TeamEffect[(int)nAttrType] += nValue;
                        }
                    }
                    else if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_DOWER)
                    {
                        if (bRemove)
                        {
                            m_DowerEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_DowerEffect[(int)nAttrType] += nValue;
                        }
                    }
                    else if (nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN)
                    {
                        if (bRemove)
                        {
                            m_TrainEffect[(int)nAttrType] -= nValue;
                        }
                        else
                        {
                            m_TrainEffect[(int)nAttrType] += nValue;
                        }
                    }
                }
                break;
            default:
                {
                    LogManager.Log("!!!!Waraing: ObjectHero ChangeEffect AttributeType OutRange For Changed fail!!! ATTRIBUTE:{" + nAttrType + "}SOURCE_TYPE :" + nType.ToString());
                }
                break;
        }
    }
    public override long GetBaseMaxHP()			//本体血上限
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        int nCabalaAddtion = 0;
        if (IsHaveCabalaAddtion(6))//生命秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue(6);
        }

        return (pPartnerRow.getInitMaxHP() + (int)fLevelValue + nStageAddtion);
        //return (pPartnerRow.getInitMaxHP() + (int)fLevelValue + m_HeroData.TrainingMaxHP);
    }

    public override long GetMaxHP()				//生命值
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP))
        {
            //             //基础血量
            //             long nBaseValue = GetBaseMaxHP();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP];
            //             //最终
            //             m_MaxHP = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            long nBaseValue = GetBaseMaxHP();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_MaxHP = (long)Mathf.Max(1, (nBaseValue + nSubPoint) *
                                               (1f + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP] / 1000f) +
                                               m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP);
        }
        return m_MaxHP;
    }

	public override int			    GetPhysicalBaseAttack()		//本体攻击
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getPhysicalAttackGrowthMultiple() > 0) && (pPartnerRow.getPhysicalAttackGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getPhysicalAttackGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getPhysicalAttackGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        int nCabalaAddtion = 0;
        if (IsHaveCabalaAddtion(7))//攻击秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue(7);
        }

        return (pPartnerRow.getInitPhysicalAttack() + (int)fLevelValue + nStageAddtion);
        //return (pPartnerRow.getInitPhysicalAttack() + (int)fLevelValue + nTrainingValue);
    }

	public override int			    GetPhysicalAttack()			//总攻击点数
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK))
        {
            //             //基础血量
            //             int nBaseValue = GetPhysicalBaseAttack();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK];
            //             //最终
            //             m_PhysicalAttack = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));
            int nBaseValue = GetPhysicalBaseAttack();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_PhysicalAttack = (int)Mathf.Max(1, (nBaseValue + nSubPoint) *
                                               (1f + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK] / 1000f) +
                                               m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK]);
            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK);
        }
        return m_PhysicalAttack;
    }
	public override int			    GetPhysicalBaseDefence()		//本体防御点数
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getPhysicalDefenceGrowthMultiple() > 0) && (pPartnerRow.getPhysicalDefenceGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getPhysicalDefenceGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getPhysicalDefenceGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        int nCabalaAddtion = 0;
        if (IsHaveCabalaAddtion(8))//防御秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue(8);
        }

        return (pPartnerRow.getInitPhysicalDefence() + (int)fLevelValue + nStageAddtion);

        //return (pPartnerRow.getInitPhysicalDefence() + (int)fLevelValue + m_HeroData.TrainingPhysicalDefence);
    }

	public override int			    GetPhysicalDefence()			//总防御点数
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE))
        {
            //             //基础血量
            //             int nBaseValue = GetPhysicalBaseDefence();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE];
            //             //最终
            //             m_PhysicalDefence = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            int nBaseValue = GetPhysicalBaseDefence();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_PhysicalDefence = (int)Mathf.Max(1, (nBaseValue + nSubPoint) *
                                               (1f + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE] / 1000f) +
                                               m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK);
        }
        return m_PhysicalDefence;
    }

	public override int			    GetMagicBaseAttack()	  //本体攻击点数
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getMagicAttackGrowthMultiple() > 0) && (pPartnerRow.getMagicAttackGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getMagicAttackGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getMagicAttackGrowth() * fLevelParam;

        return (pPartnerRow.getInitMagicAttack() + (int)fLevelValue + m_HeroData.TrainingMagicAttack);
    }

	public override int			    GetMagicAttack()		  //攻击
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK))
        {
            //基础血量
            int nBaseValue = GetMagicBaseAttack();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK];
            //team影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK];

            float nLimitPermil = 0;
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                nLimitPermil = DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_honestdiploma_num2() * ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd2 * 1000f;
            }
            //最终
            m_MagicAttack = (int)Mathf.Max(1, ((float)nBaseValue + (float)(nEquipPoint + nSpellPoint + nTeamPoint) * (1.0f + (float)nSpellPermil / 1000.0f)) * (1.0f + nLimitPermil / 1000f));

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK);
        }
        return m_MagicAttack;
    }

	public override int			    GetMagicBaseDefence()	 //本体防御点数
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getMagicDefenceGrowthMultiple() > 0) && (pPartnerRow.getMagicDefenceGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getMagicDefenceGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getMagicDefenceGrowth() * fLevelParam;

        return (pPartnerRow.getInitMagicDefence() + (int)fLevelValue + m_HeroData.TrainingMagicDefence);
    }

	public override int			    GetMagicDefence()		//防御
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE))
        {
            //基础血量
            int nBaseValue = GetMagicBaseDefence();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil)) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE];
            //team影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE];

            float nLimitPermil = 0;
            if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
            {
                nLimitPermil = DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_honestdiploma_num3() * ObjectSelf.GetInstance().LimitFightMgr.m_AttrAdd3 * 1000f;
            }
            //最终
            m_MagicDefence = (int)Mathf.Max(1, ((float)nBaseValue + (float)(nEquipPoint + nSpellPoint + nTeamPoint) * (1.0f + (float)nSpellPermil / 1000.0f)) * (1.0f + nLimitPermil / 1000f ));

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE);
        }
        return m_MagicDefence;
    }

	public override int			    GetBaseDodge()			//本体闪避
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getDodgeGrowthMultiple() > 0) && (pPartnerRow.getDodgeGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getDodgeGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getDodgeGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        return (pPartnerRow.getBaseDodge() + (int)fLevelValue + nStageAddtion);

        //return (pPartnerRow.getInitDodge() + (int)fLevelValue);
    }

	public override int			    GetDodge()				//总闪避
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE))
        {
            //             //基础血量
            //             int nBaseValue = GetBaseDodge();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE];
            //             //最终
            //             m_Dodge = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            int nBaseValue = GetBaseDodge();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Dodge = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE);
        }
        return m_Dodge;
    }

	public override int			    GetBaseCritical()		//本体暴击
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getCriticalGrowthMultiple() > 0) && (pPartnerRow.getCriticalGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getCriticalGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getCriticalGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        return (pPartnerRow.getBaseCritical() + (int)fLevelValue + nStageAddtion);

        //return (pPartnerRow.getInitCritical() + (int)fLevelValue);
    }

	public override int			    GetCritical()			//总暴击
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL))
        {
            //             //基础血量
            //             int nBaseValue = GetBaseCritical();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL];
            //             //最终
            //             m_Critical = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            int nBaseValue = GetBaseCritical();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Critical = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL);
        }
        return m_Critical;
    }

	public override int			    GetBaseHit()			//本体命中
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getHitGrowthMultiple() > 0) && (pPartnerRow.getHitGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHitGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getHitGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        return (pPartnerRow.getBaseHit() + (int)fLevelValue + nStageAddtion);

        //return (pPartnerRow.getInitHit() + (int)fLevelValue);
    }
    public override int			    GetHit()				//总命中
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT))
        {
            //             //基础血量
            //             int nBaseValue = GetBaseHit();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT];
            //             //最终
            //             m_Hit = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            int nBaseValue = GetBaseHit();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Hit = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT);
        }
        return m_Hit;
    }
	public override int			    GetBaseTenacity()		//本体韧性
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getTenacityGrowthMultiple() > 0) && (pPartnerRow.getTenacityGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getTenacityGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getTenacityGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }

        return (pPartnerRow.getBaseTenacity() + (int)fLevelValue + nStageAddtion);

        //return (pPartnerRow.getInitTenacity() + (int)fLevelValue);
    }
	public override int			    GetTenacity()			//总韧性
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY))
        {
            //             //基础血量
            //             int nBaseValue = GetBaseTenacity();
            //             //基础血量计算
            //             //装备影响本体千分比
            //             int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
            //             //技能影响本体千分比
            //             int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
            //             //team影响本体千分比
            //             int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
            //             //最终基础
            //             nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //             //装备产生血量点数
            //             int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY];
            //             //技能产生血量点数
            //             int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY];
            //             //team影响点数
            //             int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY];
            //             //技能增加千分比
            //             int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY];
            //             //最终
            //             m_Tenacity = (int)Mathf.Max(1,((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            int nBaseValue = GetBaseTenacity();
            //装备血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE];
            //星图血量点数
            int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE];
            //神器影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE];
            //培养影响点数
            int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Tenacity = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE]);

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY);
        }
        return m_Tenacity;
    }
	public override int			    GetBaseSpeed()		//本体速度
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ((pPartnerRow.getSpeedGrowthMultiple() > 0) && (pPartnerRow.getSpeedGrowthMultiple() <= 1000))
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getSpeedGrowthMultiple() - 1];
        }
        double fLevelValue = m_HeroData.Level * pPartnerRow.getSpeedGrowth() * fLevelParam;

        return (pPartnerRow.getInitSpeed() + (int)fLevelValue);
    }
	public override int			    GetSpeed()				//总速度
    {
        if (IsBitSet(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED))
        {
            //基础血量
            int nBaseValue = GetBaseSpeed();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //最终基础
            nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED];
            //team影响点数
            int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED];
            //最终
            m_Speed = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

            ClearBitFlag(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED);
        }
        return m_Speed;
    }
    public override int GetHpRecover()    //生命恢复力
    {
        HeroTemplate pPartnerRow = GetHeroRow();

        int nBaseValue = pPartnerRow.getLifeRestoringForce();

        int nEquipSelfPermil = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];
        //技能影响本体千分比
        int nSpellSelfPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];
        //team影响本体千分比
        int nTeamEffectPermil = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];
        //最终基础
        nBaseValue = nBaseValue + (nBaseValue * (nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil)) / 1000;
        //装备产生血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER];
        //技能产生血量点数
        int nSpellPoint = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER];
        //team影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER];
        //技能增加千分比
        int nSpellPermil = m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL];

        m_HpRecover = (int)Mathf.Max(1, ((float)nBaseValue + ((float)nEquipPoint + (float)nSpellPoint + (float)nTeamPoint) * (1.0f + ((float)(nSpellPermil)) / 1000.0f)));

        return m_HpRecover;
    }
    public override float           GetMoveSpeed()      //移动速度 [1/21/2015 Zmy]
    {
        HeroTemplate pRow = GetHeroRow();

        return pRow.getMovespeed();
    }

    public override float GetHitRate()		//命中率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return (pPartnerRow.getBaseHit()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE]) / 1000f;
    }
    public override float GetDodgeRate()    //闪避率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return (pPartnerRow.getBaseDodge()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE]) / 1000f;
    }
    public override float GetCriticalRate() //暴击率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return (pPartnerRow.getBaseCritical()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE]) / 1000f;
    }
    public override float GetTenacityRate() //韧性率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return (pPartnerRow.getBaseTenacity()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE]) / 1000f;
    }
    public override float GetPhysicalHurtAddPermil() //物理伤害加深率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return ((pPartnerRow.getBasePhyDamageIncrease()
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT]
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]) / 1000f);
    }
    public override float GetPhysicalHurtReducePermil() //物理伤害减免率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return ((pPartnerRow.getBasePhyDamageDecrease()
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT]
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]) / 1000f);
    }
    public override float GetMagicHurtAddPermil() //法术伤害加深率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return ((pPartnerRow.getBaseMagDamageIncrease()
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT]
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]) / 1000f);
    }
    public override float GetMagicHurtReducePermil() //法术伤害减免率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return ((pPartnerRow.getBaseMagDamageDecrease()
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT]
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]) / 1000f);
    }
    public override float GetCriticalHurtAddRate() //暴击伤害加成率
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        float fPower = DataTemplate.GetInstance().m_GameConfig.getCritical_base_power(); //基础暴击伤害倍率 [7/20/2015 Zmy]

        return ((pPartnerRow.getBaseCriticalDamage()
                       + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT]
                       + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT]) / 1000f + fPower);
    }

    public override float GetCriticalHurtReduceRate() //暴击伤害减少千分比
    {
        //暂不需要 [7/16/2015 Zmy]
        return 0;
    }

    public override int GetExtraHurt() //伤害附加值
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return pPartnerRow.getDamageIncrease()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT];
    }
    public override int GetReduceHurtPoint() //伤害减免值
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        return pPartnerRow.getDamageDecrease()
                + m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT]
                + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT];
    }

    public override float GetInitPowerAddition() //初始怒气值 
    {
        return base.GetInitPowerAddition();
    }
    public override float GetNormalSuckRate()  //普攻吸血率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK]) / 1000f;
    }
    public override float GetSpellSuckRate()  //技能吸血率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK]) / 1000f;
    }
    public override float GetCoolDownRate()   //冷却缩减率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD]) / 1000f;
    }
    public override float GetInitPowerAdditionRate()//初始怒气加成率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL]) / 1000f;
    }
    public override float GetAttackPowerAdditionRate()//攻击怒气加成率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL]) / 1000f;
    }
    public override float GetHurtPowerAdditionRate()//受击怒气加成率
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL]) / 1000f;
    }
    public override int GetCampType()
    {
        return GetHeroRow().getCamp();
    }

    public override float GetAddDamageRateToCampA()//对生灵阵营1伤害加成率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA]) / 1000f;
    }
    public override float GetAddDamageRateToCampB()//对神族阵营2伤害加成率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB]) / 1000f;
    }
    public override float GetAddDamageRateToCampC()//对恶魔阵营3伤害加成率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetReducDamageRateToCampA()//受生灵阵营1伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA]) / 1000f;
    }
    public override float GetReducDamageRateToCampB()//受神族阵营2伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB]) / 1000f;
    }
    public override float GetReducDamageRateToCampC()//受恶魔阵营3伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetAddDamageRateToFightNear()//对近战伤害加成率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTNEAR] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC]) / 1000f;
    }
    public override float GetAddDamageRateToFightFar()//对远程伤害加成率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR]) / 1000f;
    }
    public override float GetReducDamageRateToFightNear()//受近战伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR]) / 1000f;
    }
    public override float GetReducDamageRateToFightFar()//受远程伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR]) / 1000f;
    }
    public override float GetAddDamageRateToBoss() //对boss伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS]) / 1000f;
    }
    public override float GetReducDamageRateToBoss() //受boss伤害减免率  [7/20/2015 Zmy]
    {
        return (m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS] + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS]) / 1000f;
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
        switch (pTarget.GetCampType())//防御方
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
        switch (pTarget.GetCampType())//防御方
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
    public int GetTrainingMaxHP()	//生命力值训练值
    {
        return m_HeroData.TrainingMaxHP;
    }
    
    public int GetTrainingPhysicalAttack()	//物理攻击力训练值
    {
        return m_HeroData.TrainingPhysicalAttack;
    }
    
    public int GetTrainingPhysicalDefence()	//物理防御力训练值	
    {
        return m_HeroData.TrainingPhysicalDefence;
    }
    
    public int GetTrainingMagicAttack()		//魔法攻击力训练值
    {
        return m_HeroData.TrainingMagicAttack;
    }
   
    public int GetTrainingMagicDefence()		//魔法防御力训练值	
    {
        return m_HeroData.TrainingMagicDefence;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override int GetBaseCriticalHurt()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getBaseCriticalDamage() + (int)fLevelValue + nStageAddtion);
    }
    public override int GetCriticalHurtRate() //暴击伤害率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseCriticalHurt();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT]);
        return _value;
    }

    public override int GetBaseHurtAdd()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getDamageBonusHit() + (int)fLevelValue + nStageAddtion);
    }
    public override int GetHurtAddRate() //伤害加成率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseHurtAdd();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE]);
        return _value;
    }

    public override int GetBaseHurtReduce()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getDamageReductionHit() + (int)fLevelValue + nStageAddtion);
    }

    public override int GetHurtReduceRate()//伤害减免率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseHurtReduce();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE]);
        return _value;
    }

    public override int GetBaseBlock()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getBlockHit() + (int)fLevelValue + nStageAddtion);
    }
    public override int GetBlockRate() //格挡率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseBlock();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE]);
        return _value;
    }
    public override int GetBasePierce()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getSabotageHit() + (int)fLevelValue + nStageAddtion);
    }
    public override int GetPierceRate()//破甲率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBasePierce();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE]);
        return _value;
    }
    public override int GetBaseSuck()
    {
        HeroTemplate pPartnerRow = GetHeroRow();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_HeroData.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow();
        if (addStageRow != null)
        {
            for (int i = 0; i < addStageRow.getAttribute().Length; i++)
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
            }
        }
        return (pPartnerRow.getSabotageHit() + (int)fLevelValue + nStageAddtion);
    }
    public override int GetSuckRate()//吸血率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseSuck();
        //装备血量点数
        int nEquipPoint = m_ItemEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE];
        //培养影响点数
        int nTrainPoint = m_TrainEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = (int)Mathf.Max(1, nBaseValue + nSubPoint + m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE]);
        return _value;
    }

    public override int GetDevelopAttributeSub(int nType)
    {
        //装备血量点数
        int nEquipPoint = m_ItemEffect[nType];
        //星图血量点数
        int nDowerPoint = m_DowerEffect[nType];
        //神器影响点数
        int nTeamPoint = m_TeamEffect[nType];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint;

        return nSubPoint;
    }

    public override int GetSpellAttibute(int nType)
    {
        return m_SpellEffect[nType];
    }
    public void UpdateSpellEffectValue()
    {
//         if (InterfaceControler.GetInst().IsOpenSkill(this, m_SpellInfo[1].GetSpellNum()))
//         {
//             Spell pSkill = new Spell();
//             pSkill.SetHolder(this);
//             pSkill.Init(m_SpellInfo[1]);
//             pSkill.ImmActiveOnce();
//             pSkill = null;
// 
//             if (m_HeroData.ItemPassiveSpell.SpellID != int.MaxValue)
//             {
//                 m_ItemSpellInfo.Init(m_HeroData.ItemPassiveSpell.SpellID);
// 
//                 Spell pSkill_Item = new Spell();
//                 pSkill_Item.SetHolder(this);
//                 pSkill_Item.Init(m_ItemSpellInfo);
//                 pSkill_Item.ImmActiveOnce();
//                 pSkill_Item = null;
//             }
//         }
        Spell pSkill = new Spell();
        if (m_HeroData.QualityLev > 2)
        {
            pSkill.SetHolder(this);
            pSkill.Init(m_SpellInfo[2]);
            pSkill.ImmActiveOnce();
        }
        if (m_HeroData.QualityLev > 4)
        {
            pSkill.SetHolder(this);
            pSkill.Init(m_SpellInfo[4]);
            pSkill.ImmActiveOnce();
        }
        if (m_HeroData.QualityLev > 5)
        {
            pSkill.SetHolder(this);
            pSkill.Init(m_SpellInfo[5]);
            pSkill.ImmActiveOnce();
        }
        pSkill = null;
    }

    //培养属性计算 [10/23/2015 Zmy]
    public void UpdateTrainEffectValue()
    {
        for (int i = 0; i < GlobalMembers.MAX_TRAIN_SLOT_COUNT; ++i)
        {
            int nTrainLev = m_HeroData.HeroTrainDB.TrainLevel[i];
            int nTrainType = i + 1;
            HerocultureTemplate _HerocultureRow = DataTemplate.GetInstance().GetHerocultureTemplate(GetHeroRow().getBorn(), GetHeroRow().getQosition(), nTrainType, nTrainLev);
            if (_HerocultureRow == null)
                continue;

            for (int j = 0; j < _HerocultureRow.getAttribute().Length; ++j)
            {
                EM_EXTEND_ATTRIBUTE _type = (EM_EXTEND_ATTRIBUTE)_HerocultureRow.getAttribute()[j];
                int nValue = _HerocultureRow.getValue()[j];
                ChangeEffect(_type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN);
            }
        }
    }

    //新装备属性计算 [10/22/2015 Zmy]
    public void UpdateItemEffectValue()
    {
        int nEquipCount = m_HeroData.HeroEqupDB.EquipList.Count;
        for (int i = 0; i < nEquipCount; ++i)
        {
            int nEquipID = m_HeroData.HeroEqupDB.EquipList[i].TableID;
            int nIntensifyLev = m_HeroData.HeroEqupDB.EquipList[i].IntensifyLev;
            //装备本身属性加成
            EquipmentqualityTemplate _EquipQualityRow = (EquipmentqualityTemplate)DataTemplate.GetInstance().m_EquipmentqualityTable.getTableData(nEquipID);
            if (_EquipQualityRow == null)
                continue;

            for (int n = 0; n < _EquipQualityRow.getQualityAttribute().Length; ++n)
            {
                EM_EXTEND_ATTRIBUTE _type = (EM_EXTEND_ATTRIBUTE)_EquipQualityRow.getQualityAttribute()[n];
                int nValue = _EquipQualityRow.getNumerical()[n];
                ChangeEffect(_type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE);
            }
            //强化属性加成 [10/22/2015 Zmy]
            EquipmentstrengthTemplate _EquipStrengthRow = DataTemplate.GetInstance().GetEquipStrengthTemplate(GetHeroRow().getQosition(), _EquipQualityRow.getParts(), nIntensifyLev);
            if (_EquipStrengthRow == null)
                continue;

            for (int j = 0; j < _EquipStrengthRow.getAttribute().Length; ++j)
            {
                EM_EXTEND_ATTRIBUTE _type = (EM_EXTEND_ATTRIBUTE)_EquipStrengthRow.getAttribute()[j];
                int nValue = _EquipStrengthRow.getValue()[j];
                ChangeEffect(_type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE);
            }
        }
    }
    /// <summary>
    /// 符文装备属性刷新计算  [5/21/2015 Zmy]
    /// 符文属性计算 废弃  [10/22/2015 Zmy]
    /// </summary>
    public void UpdateItemEffectValue_Back()
    {
        //缓存符文搭配条件，用于后面符文组合属性计算 [5/22/2015 Zmy]
        m_RunePassiveList.Clear();
        if (GetHeroRow().getRunePair1() != -1)
            m_RunePassiveList.Add(GetHeroRow().getRunePair1());
        if (GetHeroRow().getRunePair2() != -1)
            m_RunePassiveList.Add(GetHeroRow().getRunePair2());
        if (GetHeroRow().getRunePair3() != -1)
            m_RunePassiveList.Add(GetHeroRow().getRunePair3());
        if (GetHeroRow().getRunePair4() != -1)
            m_RunePassiveList.Add(GetHeroRow().getRunePair4());

        bool IsActiveRunePassive = false;// 是否满足符文组合效果 [5/22/2015 Zmy]
        for (int i = 0; i < (int)EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; i++)
        {
            ItemEquip _equip = m_HeroData.GetRuneItemInfo((EM_RUNE_POINT)i);
            if (_equip != null)
            {
                CalcRuneBaseAttribute(_equip);

                CalcRuneAppendAttribute(_equip);

                if (IsActiveRunePassive == false)
                {
                    for (int n = 0; n < m_RunePassiveList.Count; n++)
                    {
                        if (_equip.GetItemRowData().getRune_type() == m_RunePassiveList[n])
                        {
                            m_RunePassiveList.RemoveAt(n);//满足条件移除一个 直到条件列表为空后激活组合属性
                            break;
                        }
                    }

                    if (m_RunePassiveList.Count == 0)
                        IsActiveRunePassive = true;

                }
            }
        }

        //满足符文组合条件，计算符文组合属性 [5/22/2015 Zmy]
        if (IsActiveRunePassive)
        {
            CalcRunePassive(GetHeroRow().getRunePassive());
        }
    }

    // 所有神器属性加成计算 [5/27/2015 Zmy]
    public void UpdateTeamEffectValue()
    {
        for (int i = (int)EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAXHP; i < (int)EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_NUM; i++)
        {
            int nValue = ObjectSelf.GetInstance().ArtifactContainerBag.GetAttributeTotal((EM_ARTIFACT_ATTRIBUTE_TYPE)i);
            CalcArtifactAttribute((EM_ARTIFACT_ATTRIBUTE_TYPE)i, nValue);
        }
    }

    //符文组合属性计算 [5/22/2015 Zmy]
    private void CalcRunePassive(int nPassiveID)
    {
        RunepassiveTemplate _row = (RunepassiveTemplate)DataTemplate.GetInstance().m_RunepassiveTable.getTableData(nPassiveID);
        if (_row != null)
        {
            if (_row.getAttribute1() != -1)
            {
                CalcRunePassiveAttribute(_row.getAttribute1(), _row.getValue1());
            }

            if (_row.getAttribute2() != -1)
            {
                CalcRunePassiveAttribute(_row.getAttribute2(), _row.getValue2());
            }

            if (_row.getAttribute3() != -1)
            {
                CalcRunePassiveAttribute(_row.getAttribute3(), _row.getValue3());
            }
        }
    }
    //符文基础属性计算 [5/21/2015 Zmy]
    private void CalcRuneBaseAttribute(ItemEquip _equip)
    {
        for (int baseIndex = 0; baseIndex < GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT; baseIndex++)
        {
            int nBaseAttriID = _equip.GetRuneData().BaseAttributeID[baseIndex];
            if (nBaseAttriID == -1)
                continue;
            BaseruneattributeTemplate _rowBase = (BaseruneattributeTemplate)DataTemplate.GetInstance().m_BaseruneattributeTable.getTableData(nBaseAttriID);
            if (_rowBase != null)
            {
                int nAttributeType = _rowBase.getAttriType();
                //符文基础属性有超过属性枚举的范畴，特殊验证一下有效枚举属性的区间（[1,54]和[100,105]） [7/28/2015 Zmy]
                if (nAttributeType < (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID ||
                   (nAttributeType >= (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER && nAttributeType < (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_PASSIVE_SKILL) ||
                    nAttributeType >= (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_NUMBER_MAX)
                {
                    Debug.LogError("!!!Error : ObjectHero::CalcRuneBaseAttribute() nAttributeType RangeOut!:" + nAttributeType);
                    return;
                }

                switch (nAttributeType)
                {
                    case 0:
                    case 34:
                        {
                            Debug.LogError("!!!Error : ObjectHero::CalcRuneBaseAttribute() nAttributeType Param Error!");
                            return;
                        } 
                        // 战斗英雄无需重新计算专属符文的技能ID的改变 [7/21/2015 Zmy]
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_PASSIVE_SKILL:
//                         nSkill0 = _rowBase.getAttriValue();
//                         m_HeroData.ItemPassiveSpell.SpellID = nSkill0;
//                         break;
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_COMMON_SKILL:
//                         nSkill0 = m_HeroData.SpellDataList[0].SpellID + _rowBase.getAttriValue();
//                         m_HeroData.ItemChangeSkill(0, nSkill0);
//                         return;
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PASSIVE_SKILL:
//                         nSkill1 = m_HeroData.SpellDataList[1].SpellID + _rowBase.getAttriValue();
//                         m_HeroData.ItemChangeSkill(1, nSkill1);
//                         return;
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_SKILL:
//                         nSkill2 = m_HeroData.SpellDataList[2].SpellID + _rowBase.getAttriValue();
//                         m_HeroData.ItemChangeSkill(2, nSkill2);
//                         return;
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_COMMON_SKILL:
//                         nSkill0 = m_HeroData.SpellDataList[0].SpellID + _rowBase.getAttriValue();
//                         nSkill2 = m_HeroData.SpellDataList[2].SpellID + _rowBase.getAttriValue();
//                         m_HeroData.ItemChangeSkill(0, nSkill0);
//                         m_HeroData.ItemChangeSkill(2, nSkill2);
//                         return;
//                     case (int)EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_ALL_SKILL:
//                         nSkill0 = m_HeroData.SpellDataList[0].SpellID + _rowBase.getAttriValue();
//                         nSkill1 = m_HeroData.SpellDataList[1].SpellID + _rowBase.getAttriValue();
//                         nSkill2 = m_HeroData.SpellDataList[2].SpellID + _rowBase.getAttriValue();
//                         m_HeroData.ItemChangeSkill(0, nSkill0);
//                         m_HeroData.ItemChangeSkill(1, nSkill1);
//                         m_HeroData.ItemChangeSkill(2, nSkill2);
//                         return;
                    default:
                        ChangeEffect((EM_EXTEND_ATTRIBUTE)nAttributeType, _rowBase.getAttriValue(), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE);
                        break;
                }
            }
        }
    }

    //符文附加属性计算 [5/21/2015 Zmy]
    private void CalcRuneAppendAttribute(ItemEquip _equip)
    {
        for (int appendIndex = 0; appendIndex < GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT; appendIndex++)
        {
            int nBaseAttriID = _equip.GetRuneData().AppendAttribute[appendIndex];
            if (nBaseAttriID == -1)
                continue;

            AddruneattributeTemplate _rowData = (AddruneattributeTemplate)DataTemplate.GetInstance().m_AddruneattributeTable.getTableData(nBaseAttriID);
            if (_rowData != null)
            {
                int nAttributeType = _rowData.getAttriType();
                if (nAttributeType < (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID || nAttributeType >= (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER)
                {
                    Debug.LogError("!!!Error : ObjectHero::CalcRuneAppendAttribute() nAttributeType RangeOut!:" + nAttributeType);
                    return;
                }
                switch (nAttributeType)
                {
                    // 尚未定义的返回error [7/27/2015 Zmy]
                    case 0:
                    case 34:
                        {
                            Debug.LogError("!!!Error : ObjectHero::CalcRuneAppendAttribute() nAttributeType Param Error!");
                            return;
                        }
                    default:
                        ChangeEffect((EM_EXTEND_ATTRIBUTE)nAttributeType, _rowData.getAttriValue(), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE);
                        break;
                }
            }
        }
    }

    private void CalcRunePassiveAttribute(int nAttributeType, int nAttributeValue)
    {
        if (nAttributeType < (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID || nAttributeType >= (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER)
        {
            Debug.LogError("!!!Error : ObjectHero::CalcRunePassiveAttribute() nAttributeType RangeOut!:" + nAttributeType);
            return;
        }

        switch (nAttributeType)
        {
            // 尚未定义的返回error [7/27/2015 Zmy]
            case 0:
            case 34:
                {
                    Debug.LogError("!!!Error : ObjectHero::CalcRunePassiveAttribute() nAttributeType Param Error!");
                    return;
                }
            default:
                ChangeEffect((EM_EXTEND_ATTRIBUTE)nAttributeType, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE);
                break;
        }
    }

    public void CalcArtifactAttribute(EM_ARTIFACT_ATTRIBUTE_TYPE nAttributeType, int nAttributeValue)
    {
        switch (nAttributeType)
        {
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAXHP:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALATTACK:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALDEFENCE:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICATTACK:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICDEFENCE:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_DODGE:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_CRITICAL:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_TENACITY:
                ChangeEffect(EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM);
                break;
            default:
                break;
        }
    }


    public override void SetObjectActionState(ObjectCreature.ObjectActionState state)
    {
        if (base.GetActionState() != state)
        {
            base.SetObjectActionState(state);
        }
    }

    public override ObjectCreature.ObjectActionState GetActionState()
    {
        return base.GetActionState();
    }

    public override void SetFightState(int nState, X_GUID guid)
    {
        base.SetFightState(nState, guid);

        if ( nState == ((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))//眩晕
        {
            SetObjectActionState(ObjectActionState.dizzy);
        }
    }
    public override void RemoveFightState(int nState, X_GUID guid)
    {
        base.RemoveFightState(nState, guid);

        if (nState == ((int)EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))//眩晕
        {
            SetObjectActionState(ObjectActionState.normalAttack);
        }
    }

    /// <summary>
    /// 被伤害
    /// </summary>
    /// <param name="nHurt"></param>
    public override void OnBeHurt(int nHurt, SpellInfo _BeSpellInfo, bool bCritical)
    {
        //m_AnimControl.Anim_Hurt();
        if (_BeSpellInfo != null)
        {
            SkillTemplate pRow = _BeSpellInfo.GetSpellRow();

            //英雄受到攻击播放音效           
            //AudioControler.Inst.PlaySound(pRow.getUnderAttackSound());            

            if (string.IsNullOrEmpty(pRow.getUnderAttackEffID()) == false)
            {
                Transform paran = GetGameObject().GetComponent<AnimationEventControler>().GetTransform(GetGameObject().transform, _BeSpellInfo.GetSpellRow().getEffectHitPoint());
                paran.transform.rotation = Quaternion.identity;
                EffectManager.GetInstance().InstanceEffect_Static(pRow.getUnderAttackEffID(), this, paran, 0f);
            }
            //多段技能会造成多次伤害的，受击怒气只奖励一次 [10/17/2015 Zmy]
            AngertableTemplate _data = null;
            if (_BeSpellInfo.GetCurIntervalNode() >= 0 && _BeSpellInfo.GetCurIntervalNode() <= 1)
            {
                _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(GetHeroRow().getFuryId());

                FightControler.Inst.OnUpdatePowerValue(GetGroupType(), _data.getGethitFury());
            }

            float nNum = ((float)nHurt / (float)GetMaxHP()) * 100 * _data.getHPTransformFury();
            for (int n = 0; n < GetHeroData().HeroCabalaDB.CabalaList.Count; ++n)
            {
                int _tableID = GetHeroData().HeroCabalaDB.CabalaList[n].TableID;
                MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
                if (_row.getMstype() == 2)//生命流失额外怒气增加
                {
                    int nLev = GetHeroData().HeroCabalaDB.CabalaList[n].IntensifyLev;
                    if (nLev <= 0)
                        continue;
                    int _Addtion = _row.getValue()[nLev - 1];
                    nNum += ((float)nHurt / (float)GetMaxHP()) * 100 * _Addtion;
                }
            }
            FightControler.Inst.OnUpdatePowerValue(GetGroupType(), (int)nNum);

            //受击怒气奖励公式: 奖励怒气 =（1 + buff影响 –  debuff影响）*（1 - 攻击者技能特性）
            //int nValue = (int)((1f + (float)m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP] / 1000f) * (1f - (float)_BeSpellInfo.GetSpellRow().getWeakenTargetFuryReward() / 100f));
            //FightControler.Inst.OnUpdatePowerValue(GetGroupType(),nValue);

            //根据掉血百分比转换成怒气奖励 ：奖励怒气 = 每流失1%的生命奖励X点怒气*（1 + buff影响 –  debuff影响）*（1 - 攻击者技能特性）*（1+受击额外怒气）
//             HerofuryTemplate pFury = (HerofuryTemplate)DataTemplate.GetInstance().m_HeroFuryTable.getTableData(m_HeroData.Level);
//             int nTemplate = GetHeroRow().getHPTransformFury() - 1;
//             float nNum = ((float)nHurt / (float)GetMaxHP()) * 100 * pFury.getTemplate()[nTemplate] *
//                           (1f + (float)m_SpellEffect[(int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP] / 1000f) * 
//                           (1f - (float)_BeSpellInfo.GetSpellRow().getWeakenTargetFuryReward() / 100f) *
//                           (1f + 0f);
//             FightControler.Inst.OnUpdatePowerValue(GetGroupType(),(int)nNum);
        }
        if (nHurt > 0)
        {
            UI_HurtInfo pData = new UI_HurtInfo();
            pData.pTarget = this;
            pData.nHurt = -nHurt;
            pData.bCritical = bCritical;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_ChangeHP, pData);

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

        if (pInfo != null )
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
    /// 被打断
    /// </summary>
    public override void OnBeBreak(int nHurt, SpellInfo _BeSpellInfo)
    {
        if (!IsSkillType())
            return;
        if(_BeSpellInfo!=null)
        {
            SkillTemplate pRow = _BeSpellInfo.GetSpellRow();
            switch(pRow.getInterruptSkill())
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
                        switch (GetLaunchFreeSpellInfo().GetSpellRow().getDamageInterruptType())
                        {
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_NOBEBREAK:
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVALUE:
                                if (nHurt >= GetLaunchFreeSpellInfo().GetSpellRow().getDamageInterrupt())
                                {
                                    IsGuidanceSkill(GetLaunchFreeSpellInfo().GetSpellRow().getSkillReleaseType(), GetLaunchFreeSpellInfo().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                            case (int)EM_SPELL_BEBREAK_TYPE.EM_SPELL_BREAK_TYPE_BEBREAKVPERCENT:
                                if (nHurt >= (GetMaxHP() * (float)GetLaunchFreeSpellInfo().GetSpellRow().getDamageInterrupt() / 100))
                                {
                                    IsGuidanceSkill(GetLaunchFreeSpellInfo().GetSpellRow().getSkillReleaseType(), GetLaunchFreeSpellInfo().GetSpellRow().getBallIsticEffID());
                                    SetObjectActionState(ObjectActionState.AttackIdle);
                                }
                                break;
                        }
                    }
                     //Debug.Log("被打断!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        break;
            }
        }
    }
    //是否是引导技能
    private bool IsGuidanceSkill(int type,string[] name)
    {
        switch(type)
        {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
                for (int i = 0; i < name.Length;++i)
                {
                    EffectManager.GetInstance().DisableStaticEffect(this,name[i]);
                }
                return true;
        }
        return false;
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
    //是否是攻击状态
    private bool IsSkillType()
    {
        return (GetActionState() == ObjectActionState.normalAttack || GetActionState() == ObjectActionState.AttackIdle || GetActionState() == ObjectActionState.Attacking
            || GetActionState() == ObjectActionState.skillAttack || GetActionState() == ObjectActionState.skilling || GetActionState() == ObjectActionState.skillEnd || GetActionState() == ObjectActionState.AttackIdleing) ? true : false;
    }
    // 锁定一个目标 [1/22/2015 Zmy]
    public void OnLockTarget()
    {
        if (m_CurLockTarget != null)
        {
            SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
            return;
        }
            
        //SceneObjectManager.GetInstance().LockMonsterTarget(m_HeroObject, out m_CurLockTarget);
        SceneObjectManager.GetInstance().LockMonsterTarget(this, out m_CurLockTarget);
        if (m_CurLockTarget != null)
        {
            if (ObjectSelf.GetInstance().isSkillShow)
                return;
            SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
        }
        else
        {
            //扫描目标如果没找到，说明当前回合已经没有怪物。强制修复一下战斗状态 [2/2/2015 Zmy]
            //FightControler.Inst.SetFightState(FightState.FightOver);
            if (IsAlive())
            {
                m_NavMesh.SetDestination(getWorldPos());
                SetObjectActionState(ObjectCreature.ObjectActionState.none);
            }
           
        }
        
    }
    //瞬间移动自己到阵型位置
    public void OnConcealThis()
    {
        //.............
        GetGameObject().transform.position = FightEditorContrler.GetInstantiate().GetFormationPos(this);
        GetGameObject().transform.rotation = FightEditorContrler.GetInstantiate().GetFormationAngle(this);
    }
    public void OnClearUpLockTarget(X_GUID id)
    {
       // Debug.Log(m_HeroObject.name + "~~~~" + IsSkillAttack() + "~~~~~~~~~~~~~~~~~~" + GetActionState());
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
      
        if (IsSkillAttack())
        {
            //以下技能释放时，不会因为锁定目标的死亡而中断当前正在释放的技能 [3/9/2015 Zmy]
            switch (GetLaunchFreeSpellInfo().GetTargetType())
            {
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND:
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL:
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_MIN_HPPERCENT:
                case (int)EM_TARGET_TYPE.EM_TARGET_ALL_NO_SELF:
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RANDOM:
                case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RANDOM:
                case (int)EM_TARGET_TYPE.EM_TARGET_SELF_RANDOM:
                    return false;
                case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY:
                    if (GetLaunchFreeSpellInfo().GetSpellRow().getCoverage() < 0)
                        return false;
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
            

        //float distance = Vector3.Distance(m_HeroObject.transform.position, m_CurLockTarget.GetGameObject().transform.position);
        //distance = distance - GetNavMesh().radius - m_CurLockTarget.GetNavMesh().radius;
        
        //if ( distance > m_SpellNormal.GetAttackDistance())
        //{
        //    m_NavMesh.speed = GetMoveSpeed();
        //    m_NavMesh.SetDestination(m_CurLockTarget.GetGameObject().transform.position);
        //    Debug.DrawLine(getWorldPos(), m_CurLockTarget.GetGameObject().transform.position, Color.red);
        //}
        //else
        //{
        //    Debug.DrawLine(getWorldPos(), m_CurLockTarget.GetGameObject().transform.position, Color.red);
        //    m_NavMesh.SetDestination(getWorldPos());
        //    SetObjectActionState(ObjectCreature.ObjectActionState.normalAttack);
        //}
        
        Vector3 pos = FightEditorContrler.GetInstantiate().GetFormationMovePos(this);
        m_NavMesh.SetDestination(pos);
        Debug.DrawLine(getWorldPos(), pos, Color.red);
        
        //if(m_NavMesh.pathStatus == NavMeshPathStatus.PathComplete)
        //if (m_NavMesh.remainingDistance <= 0.5f)
        
        //TODO::这里需要忽略Y轴，否则导致距离判断永远达不到（这里会有一个问题，这里只考虑了战斗在没有Y轴差别的时候计算的，战斗在斜坡上时候会有误差）;
        if (Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(this.getWorldPos().x, this.getWorldPos().z)) <= 0.2f)
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

        //近战直接触发普通攻击技能逻辑
        if (GetIsNearAttackMold())
        {
            OnCommonSkillActiveOnce();
        }
        else
        {
            if (string.IsNullOrEmpty(pData.getBallIsticEffID()[0]) == false && m_SpellNormal != null && pData != null && m_CurLockTarget != null)
            {
                string effectname = pData.getBallIsticEffID()[0];
                GameObject gameObject = GetGameObject();
                GameObject _gameObject = m_CurLockTarget.GetGameObject();
                SpellInfo spell = m_SpellNormal;
                EffectManager.GetInstance().InstanceEffect_Bullet(pData.getBallIsticEffID()[0], this, m_CurLockTarget, m_SpellNormal);
            }
        }

        //触发完普攻攻击命中后，进入受击检测状态。 [9/28/2015 Zmy]
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
        SetSpell(m_SpellNormal);
        pSkill.ActiveOnce();
        pSkill = null;
    }
    public void OnUpdateRander()
    {
        if (IsAlive() == false)
            return;
        if (m_CurLockTarget == null || m_CurLockTarget.GetGameObject()==null)
            return;        
        //Quaternion ratation = Quaternion.LookRotation(m_CurLockTarget.GetGameObject().transform.position - getWorldPos());
        //m_HeroObject.transform.rotation = Quaternion.Slerp(m_HeroObject.transform.rotation, ratation, 2 * Time.deltaTime);
        if (GetActionState() != ObjectActionState.skillAttack && GetActionState() != ObjectActionState.skilling)//技能状态下不能看向普通攻击目标【By mcj】
            m_HeroObject.transform.LookAt(m_CurLockTarget.GetGameObject().transform);
        Debug.DrawLine(getWorldPos(), m_CurLockTarget.GetGameObject().transform.position, Color.red);
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
                case ObjectCreature.ObjectActionState.normalAttack:
                    if (m_PassiveSpellLogic.CheckFreeLogic(this) == false)//不满足条件，更新逻辑标记。满足条件的内部更新 [11/3/2015 Zmy]
                    {
                        m_PassiveSpellLogic.UpdateNormalAttackCount();
                    }
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
            case ObjectActionState.monmentmoveIng://瞬间移动中
                m_AnimControl.Anim_Fidle(false);
                break;
            case ObjectActionState.scanning:
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
                    // 当前无法普攻
                    GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips1");
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
                if (m_AnimControl.GetFidLengh()<=0)
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
                OnSkillActionLogic(GetLaunchFreeSpellInfo());
                if (m_SkillLockTarget!=null&&m_SkillLockTarget.GetGameObject() != null)
                    GetGameObject().transform.LookAt(m_SkillLockTarget.GetGameObject().transform);

                AudioControler.Inst.PlaySound(GetLaunchFreeSpellInfo().GetSpellRow().getAttackSound());

                SetObjectActionState(ObjectCreature.ObjectActionState.skilling);
                break;
            case ObjectActionState.skilling:
                //技能状态中。。。
                OnGuidancesSkillLogic();//引导技能逻辑

                break;
            case ObjectActionState.skillEnd:
                // 技能结束状态(只是用于引导类技能)
                break;
            case ObjectCreature.ObjectActionState.boarding:
                m_AnimControl.Anim_Fidle(false);
                break;
            default:
                break;
        }
    }

    public override void InitBaseData()
    {
        base.InitBaseData();

        SetHP(this.GetMaxHP());
        int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        int _pos = ObjectSelf.GetInstance().Teams.FindTeamMemberIndex(nGroup,m_HeroData.GUID);
        SetTeamPos((byte)(_pos));
    }
    public int GetFormationPos()
    {
        int nGroup = ObjectSelf.GetInstance().Teams.GetDefaultGroup();
        return ObjectSelf.GetInstance().Teams.FindTeamMemberIndex(nGroup, m_HeroData.GUID);
    }
    public override void OnKillTarget(ObjectCreature pTarget, SpellInfo pSpellInfo)
    {
        base.OnKillTarget(pTarget, pSpellInfo);

        //if (m_CurLockTarget != null && pTarget.GetGuid().Equals(m_CurLockTarget.GetGuid()))
        //{
        //    //Debug.Log(m_HeroObject.name + "!!!!!!!!!!!!!!!!!!!!!!");
        //    if (IsBreakSpellAction() == false)
        //        return;
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
        SceneObjectManager.GetInstance().DieHeroCount++;
        m_AnimControl.Anim_Die();

        SetObjectActionState(ObjectCreature.ObjectActionState.deathing);
        SceneObjectManager.GetInstance().OnClearTargetForEnemy(m_HeroData.GUID);
        SceneObjectManager.GetInstance().OnUpdateImpactOfMakeDeath(m_HeroData.GUID);
        SceneObjectManager.GetInstance().OnCacheDeadObj(this,GetGroupType());

        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_HeroOnDie,m_HeroData);

        m_CurLockTarget = null;
        m_SkillLockTarget = null;

    }

    public void OnDestorySelf()
    {
        EffectManager.GetInstance().OnRemoveDeadObjOfEffect(this);
        SceneObjectManager.GetInstance().OnDeleteDeadObj(m_HeroData.GUID);
    }

    ////技能动作逻辑 [3/4/2015 Zmy]
    //private void OnSkillActionLogic()
    //{
    //    if (m_SpellInfo[0] == null)
    //        return;
    //    switch(m_SpellInfo[0].GetSpellRow().getSkillReleaseType())
    //    {
    //        case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI1:
    //            m_AnimControl.Anim_Skill(m_SpellInfo[0].GetSpellRow().getAction());
    //            break;
    //        case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI2:
    //            m_AnimControl.Anim_Skill(m_SpellInfo[0].GetSpellRow().getAction());
    //            break;
    //        case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_MULTISECTION:
    //            m_AnimControl.Anim_Skill(m_SpellInfo[0].GetSpellRow().getAction());
    //            break;
    //        case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
    //            OnGuidancesSkillLogicEnd();
    //            m_AnimControl.Anim_GuidanceSkill(m_SpellInfo[0].GetSpellRow().getAction()[0]);
    //            break;
    //        default:
    //            break;
    //    }

    //    m_SpellInfo[0].Init(m_SpellInfo[0].GetSpellID());
    //    SetActivationSpellCD(m_SpellInfo[0]);
    //}
    //引导技能逻辑(缺被打断处理)
    private void OnGuidancesSkillLogic()
    {
        switch(GetLaunchFreeSpellInfo().GetSpellRow().getSkillReleaseType())
        {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
                if (m_MaxGuidanceTime == -1)
                {
                    m_AnimControl.Anim_GuidanceSkill(GetLaunchFreeSpellInfo().GetSpellRow().getAction()[2],false);
                    SetObjectActionState(ObjectCreature.ObjectActionState.skillEnd);
                    return;
                }
                if (m_MaxGuidanceTime == 0)
                    m_MaxGuidanceTime = ((float)GetLaunchFreeSpellInfo().GetSpellRow().getParam()[0])/1000;
                m_GuidanceTime += Time.deltaTime * Time.timeScale;
                if (m_GuidanceTime >=m_MaxGuidanceTime)
                {
                    OnSpecialSkillActiveOnce();
                    if (GetLaunchFreeSpellInfo().GetSpellRow().getParam()[m_count * 5] == -1)
                    {
                        m_MaxGuidanceTime = -1;
                    }
                    else
                        m_MaxGuidanceTime = ((float)GetLaunchFreeSpellInfo().GetSpellRow().getParam()[m_count * 5]) / 1000;
                    m_count++;
                }
                break;
            default:
                {
                    //LogManager.LogError("!!!Error: OnGuidancesSkillLogic() HeroState is skilling FreeSkill Param is error : " + m_SpellInfo[0].GetSpellRow().getId());
                }
                break;
        }
    }
    //引导技点能逻辑结束清除计时数据
    private void OnGuidancesSkillLogicEnd()
    {
        m_MaxGuidanceTime = 0;
        m_GuidanceTime = 0;
        m_count = 1;
    }
    //技能条件UI第一次判断
    public bool OnUIPre_CheckUseSkillCondtion()
    {
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_FORBID))
        {
            // 被沉默
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips2");
            return false;
        }
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))
        {
            // 被晕眩
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips3");
            return false;
        }
        if (GetActionState() == ObjectActionState.moveTarget)
        {
            // 正在移动到目标中
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips4");
            return false;
        }
        m_pTempSell = new Spell();
        m_pTempSell.SetHolder(this);
        m_pTempSell.Init(m_SpellInfo[0]);

        //CD 检查：
        if (m_pTempSell._CheckSpellCooldown() == false)
        {
            // 技能冷却中
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips5");
            return false;
        }
        //消耗检查：
        if (m_SpellInfo[0].IsSkillRelease(this) == false)
        {
            // 释放条件不足
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips6");
            return false;
        }
        m_pTempSell = null;
        return true;
    }
    //技能逻辑前条件2次判断 [3/6/2015 Zmy]
    public bool OnPre_CheckUseSkillCondtion()
    {
        if (FightControler.Inst == null)
            return true;
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_FORBID))
        {
            // 被沉默
            if ((int)FightControler.Inst.GetFightAIState() == 0)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips2");
            return false;
        }
        if (IsInFightState(EM_FIGHT_STATE.EM_FIGHT_STATE_VERTIGO))
        {
            // 被晕眩
            if ((int)FightControler.Inst.GetFightAIState() == 0)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips3");
            return false;
        }
      
        if (GetActionState() == ObjectActionState.moveTarget)
        {
            // 正在移动到目标中
            if ((int)FightControler.Inst.GetFightAIState() == 0)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips4");
            return false;
        }
        m_pTempSell = new Spell();
        m_pTempSell.SetHolder(this);
       // m_pTempSell.SetTargetGuid(m_SkillLockTarget.GetGuid());
        m_pTempSell.Init(m_SpellInfo[0]);
        if (m_SpellInfo[0].IsNeedTarget()&&m_SkillLockTarget == null)
        {
            return false;
        }
        //CD 检查：
        if (m_pTempSell._CheckSpellCooldown() == false)
        {
            // 技能冷却中
            if ((int)FightControler.Inst.GetFightAIState() == 0)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips5");
            return false;
        }

        //消耗检查：
        if (m_SpellInfo[0].IsSkillRelease(this) == false)
        {
            // 释放条件不足
            if ((int)FightControler.Inst.GetFightAIState() == 0)
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_MessageAlert, "program_tips6");
            return false;
        }
        return true;
    }
    //技能消耗
    public void OnSkillConsume()
    {
        if (m_pTempSell == null)
            return;
        //释放消耗
        m_pTempSell._OnFreeConsume();
        //释放奖励
        m_pTempSell._OnFreeAward();
        m_pTempSell = null;
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.SE_ResetSkillCD,GetGuid());
    }
    public override void OnSpecialSkillActiveOnce()
    {
        if (GetLaunchFreeSpellInfo().IsNeedTarget())
        {
            OnSkillTag(GetLaunchFreeSpellInfo().GetTargetType());
        }
        else
        {
            Spell pSkill = new Spell();
            pSkill.SetHolder(this);
            pSkill.Init(GetLaunchFreeSpellInfo());
            pSkill.ImmActiveOnce();
            pSkill = null;
        }
    }
    //根据不同的技能目标类型添加不同的目标
    private void OnSkillTag(int value)
    {
        Spell pSkill = new Spell();
        switch(value)
        {
            case (int)EM_TARGET_TYPE.EM_TARGET_INVALID:
                if (m_CurLockTarget == null)
                    return;
                 pSkill.SetHolder(this);
                 pSkill.SetTargetGuid(m_CurLockTarget.GetGuid());
                 pSkill.Init(GetLaunchFreeSpellInfo());
                 pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND://单体地方目标选择
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RANDOM:
            case (int)EM_TARGET_TYPE.EM_TARGET_SELF_RANDOM:
                if (m_SkillLockTarget == null)
                    return;
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(m_SkillLockTarget.GetGuid());
                pSkill.Init(GetLaunchFreeSpellInfo());
                pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY://单体己方目标选择
            case (int)EM_TARGET_TYPE.EM_TARGET_ENEMY_RANDOM:
                if (m_SkillLockTarget == null)
                    return;
                 pSkill.SetHolder(this);
                 pSkill.SetTargetGuid(m_SkillLockTarget.GetGuid());
                 pSkill.Init(GetLaunchFreeSpellInfo());
                 pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_SELF:
                pSkill.SetHolder(this);
                pSkill.SetTargetGuid(this.GetGuid());
                pSkill.Init(GetLaunchFreeSpellInfo());
                pSkill.ImmActiveOnce();
                break;
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_NO_SELF:
            case (int)EM_TARGET_TYPE.EM_TARGET_FRIEND_RAND:
                pSkill.SetHolder(this);
                pSkill.Init(GetLaunchFreeSpellInfo());
                pSkill.ImmActiveOnce();
                break;
            default:
                pSkill.SetHolder(this);
                pSkill.Init(GetLaunchFreeSpellInfo());
                pSkill.ImmActiveOnce();
                break;
        }
        pSkill = null;
    }
    // 启动飞行技能弹道特效 [3/4/2015 Zmy]
    public void OnSkillFly_Effect(int nHitCount)
    {
        string _EffectName = string.Empty;
        SkillTemplate pData = GetLaunchFreeSpellInfo().GetSpellRow();
        if (nHitCount > 0 && nHitCount <= pData.getBallIsticEffID().Length)
        {
            _EffectName = pData.getBallIsticEffID()[nHitCount - 1];
            GetLaunchFreeSpellInfo().UpdateCurInterval();
        }
        else
        {
            _EffectName = pData.getBallIsticEffID()[0];
        }


        SceneObjectManager pScene = SceneObjectManager.GetInstance();
        SCANOPERATOR_INIT init = new SCANOPERATOR_INIT();
        init.m_Type = GetLaunchFreeSpellInfo().GetTargetType();
        pScene.ScanByObject(this, ref init);

        if (GetLaunchFreeSpellInfo().GetSpellRow().getCoverage() == -1)
        {
            for (int i = 0; i < init.m_ObjectList.Count; i++)
            {
                ObjectCreature _skillTarget = init.m_ObjectList[i];
                //SetSkillLockTarget(init.m_ObjectList[i]);
                if (string.IsNullOrEmpty(_EffectName) == false && GetLaunchFreeSpellInfo() != null && pData != null && _skillTarget != null)
                {
                    string effectname = _EffectName;
                    GameObject gameObject = GetGameObject();
                    //GameObject _gameObject = m_SkillLockTarget.GetGameObject();
                    SpellInfo spell = GetLaunchFreeSpellInfo();
                    EffectManager.GetInstance().InstanceEffect_Bullet(_EffectName, this, _skillTarget, GetLaunchFreeSpellInfo());
                }
            }
        }
        else
        {   
            if (string.IsNullOrEmpty(_EffectName) == false && GetLaunchFreeSpellInfo() != null && pData != null && m_SkillLockTarget != null)
            {
                string effectname = _EffectName;
                GameObject gameObject = GetGameObject();
                GameObject _gameObject = m_SkillLockTarget.GetGameObject();
                SpellInfo spell = GetLaunchFreeSpellInfo();
                EffectManager.GetInstance().InstanceEffect_Bullet(_EffectName, this, m_SkillLockTarget, GetLaunchFreeSpellInfo());
            }
        }

        init = null;
    }

    // 启动瞬发技能特效 [3/4/2015 Zmy]
    public void OnSkillMoment_Effect()
    {
        //瞬发技能音效（技能释放中音效）
        AudioControler.Inst.PlaySound(GetLaunchFreeSpellInfo().GetSpellRow().getBulletsFiredSound());

        OnSpecialSkillActiveOnce();
    }
    //引导技能特效
    public void OnSkillGuidance_Effect()
    {
        SkillTemplate pData = GetLaunchFreeSpellInfo().GetSpellRow();
        int[] _data = GetLaunchFreeSpellInfo().GetSpellRow().getParam();
        List<int> data=new List<int>();
        for (int i = 0; i < _data.Length/5; i++)
        {
            if(_data[i * 5]!=-1)
               data.Add(_data[i * 5]);
        }
        float _efftime = ((float)data[data.Count - 1]) / 1000 - m_GuidanceTime;
        string[] _EffectName;
        _EffectName = pData.getBallIsticEffID();
        m_AnimControl.Anim_GuidanceSkill(GetLaunchFreeSpellInfo().GetSpellRow().getAction()[0],true);
        if(_EffectName[0]!=null&&!_EffectName[0].Equals("-1"))
        {
            EffectManager.GetInstance().InstanceEffect_Static(_EffectName[0], this, FightEditorContrler.GetInstantiate().GetMonstersCenter(), _efftime, GetLaunchFreeSpellInfo());
         
        }
        if (_EffectName.Length>=2&&_EffectName[1] != null && !_EffectName[1].Equals("-1"))
        {
            EffectManager.GetInstance().InstanceEffect_Static(_EffectName[1], this, GetGameObject().transform.position, _efftime, GetLaunchFreeSpellInfo());
        }
    }

    //技能动作结束帧完毕，清除一些必要的技能状态 [10/17/2015 Zmy]
    public void OnClearSpellState()
    {
        GetLaunchFreeSpellInfo().ClearIntervalNode();
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

    private SpellInfo GetLaunchFreeSpellInfo()
    {
        switch (m_LaunchFreeSpellIndex)
        {
            case EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_INITIATIVE:
                return m_SpellInfo[0];
            case EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_FIRST:
                return m_SpellInfo[1];
            case EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_SECOND:
                return m_SpellInfo[3];
            default:
                break;
        }
        return null;
    }
    //启动释放主动技能流程逻辑。 [11/3/2015 Zmy]
    public void LaunchFreeSpellLogic(EM_SPELL_PASSIVE_INDEX _index)
    {
        m_LaunchFreeSpellIndex = _index;

        if (_index == EM_SPELL_PASSIVE_INDEX.EM_SPELL_PASSIVE_INITIATIVE)
        {
            OnSkillConsume();
        }
        else
        {
            //被动触发的主动技能需要指定一下技能目标对象，此处技能AI逻辑进行选择 [11/4/2015 Zmy]
            AILogicHero.GetInstance().SelectSpellTarget(this);
        }
       
        SetObjectActionState(ObjectCreature.ObjectActionState.skillAttack);
        SkillTemplate info = Getm_SpellInfo()[(int)_index].GetSpellRow();
        SkillShowNamePackage package = new SkillShowNamePackage(m_HeroData.GUID, info.getSkillNameRes());
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_ShowSkillName, package);

        if (info.getSkillhittype() == 1)// 治疗技能不加成怒气 [10/17/2015 Zmy]
            return;
        // 英雄攻击怒气加成。治疗技能不加怒气 [10/17/2015 Zmy]
        AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(GetHeroRow().getFuryId());
        int nValue = _data.getAttackFury();
        for (int n = 0; n < GetHeroData().HeroCabalaDB.CabalaList.Count; ++n)
        {
            int _tableID = GetHeroData().HeroCabalaDB.CabalaList[n].TableID;
            MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
            if (_row.getMstype() == 3)//攻击额外怒气增加
            {
                int nLev = GetHeroData().HeroCabalaDB.CabalaList[n].IntensifyLev;
                if (nLev <= 0)
                    continue;
                nValue += _row.getValue()[nLev - 1];
            }
        }
        FightControler.Inst.OnUpdatePowerValue(GetGroupType(), nValue);
    }

    
}
