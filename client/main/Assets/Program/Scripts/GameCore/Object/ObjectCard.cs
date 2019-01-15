using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.SkillCore;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.Utils;
/// <summary>
/// 英雄卡牌。本地属性计算公式，与战斗中的属性计算有区别
/// </summary>
public class ObjectCard : ObjectCreature
{
    private HeroData m_ObjCardDB = new HeroData ();

    private int [] m_ItemEffect = new int [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER ];  //符文的影响
    private int [] m_SpellEffect = new int [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER ]; //技能buff的影响
    private int [] m_TeamEffect = new int [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER ];  //神器影响
    private int [] m_DowerEffect = new int [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER ]; //星图天赋的影响
    private int [] m_TrainEffect = new int [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER ]; //培养属性的影响

    private SpellInfo m_PassvitySpellInfo = new SpellInfo ();
    private SpellInfo m_ItemPassiveSpellInfo = new SpellInfo ();
    private List<int> m_RunePassiveList = new List<int> ();//符文组合条件

    public ObjectCard ()
    {

    }

    public HeroData GetHeroData ()
    {
        return m_ObjCardDB;
    }

    public override X_GUID GetGuid ()
    {
        return m_ObjCardDB.GUID;
    }
    public HeroTemplate GetHeroRow ()
    {
        int nTableID = m_ObjCardDB.TableID;
        return ( HeroTemplate ) DataTemplate.GetInstance ().m_HeroTable.getTableData ( nTableID );
    }
    public HeroaddstageTemplate GetHeroaddStageRow ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return GameUtils.GetCurAdvancedData ( pPartnerRow.getBorn (), pPartnerRow.getQosition (), m_ObjCardDB.StarLevel, m_ObjCardDB.CurStage );
    }
    public LevelamendmentTemplate GetPartnerLevelParamRow ()
    {
        int nLevel = m_ObjCardDB.Level;
        return ( LevelamendmentTemplate ) DataTemplate.GetInstance ().m_LevelamendmentTable.getTableData ( nLevel );
    }

    private bool IsHaveCabalaAddtion ( int nID )
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        for ( int i = 0; i < pPartnerRow.getMsid ().Length; ++i )
        {
            if ( pPartnerRow.getMsid () [ i ] == nID )
            {
                return true;
            }
        }
        return false;
    }

    public int GetCabalaAddtionValue ( int nID )
    {
        for ( int i = 0; i < m_ObjCardDB.HeroCabalaDB.CabalaList.Count; ++i )
        {
            int _tableID = m_ObjCardDB.HeroCabalaDB.CabalaList [ i ].TableID;
            if ( _tableID == nID )
            {
                int nLev = m_ObjCardDB.HeroCabalaDB.CabalaList [ i ].IntensifyLev;
                if ( nLev <= 0 )
                    return 0;

                MsTemplate _row = ( MsTemplate ) DataTemplate.GetInstance ().m_MsTable.getTableData ( _tableID );
                if ( nLev > _row.getLevel ().Length )
                {
                    LogManager.LogError ( "!!!Error: GetPhysicalBaseDefence() CabalaLev > _row.Lenght：" + nLev );
                }
                return _row.getValue () [ nLev - 1 ];
            }
        }
        return 0;
    }
    private void ClearEffect ( EM_EFFECT_SOURCE_TYPE nType )
    {
        if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE )
        {
            for ( int i = 0; i < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER; i++ )
            {
                m_ItemEffect [ i ] = 0;
            }

            m_ObjCardDB.OnResetSpellData ();
        }
        else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT )
        {
            for ( int i = 0; i < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER; i++ )
            {
                m_SpellEffect [ i ] = 0;
            }
        }
        else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM )
        {
            for ( int i = 0; i < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER; i++ )
            {
                m_TeamEffect [ i ] = 0;
            }
        }
        else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_DOWER )
        {
            for ( int i = 0; i < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER; i++ )
            {
                m_DowerEffect [ i ] = 0;
            }
        }
        else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN )
        {
            for ( int i = 0; i < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER; i++ )
            {
                m_TrainEffect [ i ] = 0;
            }
        }
    }
    //清除身上是所有装备信息 [6/2/2015 Zmy]
    public void ClearEquipState ()
    {
        for ( int i = 0; i < m_ObjCardDB.GetEquipItems ().Count; i++ )
        {
            ObjectSelf.GetInstance ().CommonItemContainer.OnUpdateRuneEquipState ( m_ObjCardDB.GetEquipItems () [ i ], false );
        }
    }
    public override void ChangeEffect ( EM_EXTEND_ATTRIBUTE nAttrType, int nValue, EM_EFFECT_SOURCE_TYPE nType, bool bRemove = false )
    {
        switch ( nAttrType )
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
                    if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE )
                    {
                        if ( bRemove )
                        {
                            m_ItemEffect [ ( int ) nAttrType ] -= nValue;
                        }
                        else
                        {
                            m_ItemEffect [ ( int ) nAttrType ] += nValue;
                        }
                    }
                    else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT )
                    {
                        if ( bRemove )
                        {
                            m_SpellEffect [ ( int ) nAttrType ] -= nValue;
                        }
                        else
                        {
                            m_SpellEffect [ ( int ) nAttrType ] += nValue;
                        }
                    }
                    else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM )
                    {
                        if ( bRemove )
                        {
                            m_TeamEffect [ ( int ) nAttrType ] -= nValue;
                        }
                        else
                        {
                            m_TeamEffect [ ( int ) nAttrType ] += nValue;
                        }
                    }
                    else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_DOWER )
                    {
                        if ( bRemove )
                        {
                            m_DowerEffect [ ( int ) nAttrType ] -= nValue;
                        }
                        else
                        {
                            m_DowerEffect [ ( int ) nAttrType ] += nValue;
                        }
                    }
                    else if ( nType == EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN )
                    {
                        if ( bRemove )
                        {
                            m_TrainEffect [ ( int ) nAttrType ] -= nValue;
                        }
                        else
                        {
                            m_TrainEffect [ ( int ) nAttrType ] += nValue;
                        }
                    }
                }
                break;
            case EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID:
                //无效不处理
                break;
            default:
                {
                    LogManager.Log ( "!!!!Waraing: ObjectCard ChangeEffect AttributeType OutRange For Changed fail!!! ATTRIBUTE:{" + nAttrType + "}SOURCE_TYPE :" + nType.ToString () );
                }
                break;
        }
    }
    public override long GetBaseMaxHP ()			//本体血上限
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getHPGrowthMultiple () > 0 ) && ( pPartnerRow.getHPGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getHPGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getHPGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if (addStageRow.getAttribute()[i] == (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP)
                {
                    nStageAddtion += addStageRow.getValue()[i];
                }
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        int nCabalaAddtion = 0;
        if ( IsHaveCabalaAddtion ( 6 ) )//生命秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue ( 6 );
        }
        return ( pPartnerRow.getInitMaxHP () + ( int ) fLevelValue + nStageAddtion + nCabalaAddtion );
        //return (pPartnerRow.getInitMaxHP() + (int)fLevelValue + m_ObjCardDB.TrainingMaxHP);
    }

    public override long GetMaxHP ()				//生命值
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP ) )
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

            long nBaseValue = GetBaseMaxHP ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_MaxHP = ( long ) Mathf.Max ( 1, ( nBaseValue + nSubPoint ) *
                                               ( 1f + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP ] / 1000f ) +
                                               m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP ] );

            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP );
        }
        return m_MaxHP;
    }

    public override int GetPhysicalBaseAttack ()		//本体攻击
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getPhysicalAttackGrowthMultiple () > 0 ) && ( pPartnerRow.getPhysicalAttackGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getPhysicalAttackGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getPhysicalAttackGrowth () * fLevelParam;

        int nTrainingValue = 0;
        if ( GetHeroRow ().getClientSignType () [ 1 ] == 0 )//物攻
        {
            nTrainingValue = m_ObjCardDB.TrainingPhysicalAttack;
        }

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        int nCabalaAddtion = 0;
        if ( IsHaveCabalaAddtion ( 7 ) )//攻击秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue ( 7 );
        }

        return ( pPartnerRow.getInitPhysicalAttack () + ( int ) fLevelValue + nStageAddtion + nCabalaAddtion );
        //return (pPartnerRow.getInitPhysicalAttack() + (int)fLevelValue + nTrainingValue);
    }

    public override int GetPhysicalAttack ()			//攻击力
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK ) )
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
            int nBaseValue = GetPhysicalBaseAttack ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_PhysicalAttack = ( int ) Mathf.Max ( 1, ( nBaseValue + nSubPoint ) *
                                               ( 1f + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK ] / 1000f ) +
                                               m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK );
        }
        return m_PhysicalAttack;
    }
    public override int GetPhysicalBaseDefence ()		//本体防御点数
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getPhysicalDefenceGrowthMultiple () > 0 ) && ( pPartnerRow.getPhysicalDefenceGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getPhysicalDefenceGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getPhysicalDefenceGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        int nCabalaAddtion = 0;
        if ( IsHaveCabalaAddtion ( 8 ) )//防御秘术ID的属性增加 [10/23/2015 Zmy]
        {
            nCabalaAddtion += GetCabalaAddtionValue ( 8 );
        }

        return ( pPartnerRow.getInitPhysicalDefence () + ( int ) fLevelValue + nStageAddtion + nCabalaAddtion );

        //return (pPartnerRow.getInitPhysicalDefence() + (int)fLevelValue + m_ObjCardDB.TrainingPhysicalDefence);
    }

    public override int GetPhysicalDefence ()			//防御力
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE ) )
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

            int nBaseValue = GetPhysicalBaseDefence ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_PhysicalDefence = ( int ) Mathf.Max ( 1, ( nBaseValue + nSubPoint ) *
                                               ( 1f + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE ] / 1000f ) +
                                               m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK );
        }
        return m_PhysicalDefence;
    }

    public override int GetMagicBaseAttack ()	  //本体攻击点数
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getMagicAttackGrowthMultiple () > 0 ) && ( pPartnerRow.getMagicAttackGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getMagicAttackGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getMagicAttackGrowth () * fLevelParam;

        int nTrainingValue = 0;
        if ( GetHeroRow ().getClientSignType () [ 1 ] == 1 )//法攻
        {
            nTrainingValue = m_ObjCardDB.TrainingMagicAttack;
        }
        return ( pPartnerRow.getInitMagicAttack () + ( int ) fLevelValue + nTrainingValue );
    }

    public override int GetMagicAttack ()		  //攻击
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK ) )
        {
            //基础血量
            int nBaseValue = GetMagicBaseAttack ();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK ];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK ];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK ];
            //最终基础
            nBaseValue = nBaseValue + ( nBaseValue * ( nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil ) ) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK ];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK ];
            //team影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK ];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK ];
            //最终
            m_MagicAttack = ( int ) Mathf.Max ( 1, ( ( float ) nBaseValue + ( ( float ) nEquipPoint + ( float ) nSpellPoint + ( float ) nTeamPoint ) * ( 1.0f + ( ( float ) ( nSpellPermil ) ) / 1000.0f ) ) );

            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK );
        }
        return m_MagicAttack;
    }

    public override int GetMagicBaseDefence ()	 //本体防御点数
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getMagicDefenceGrowthMultiple () > 0 ) && ( pPartnerRow.getMagicDefenceGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getMagicDefenceGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getMagicDefenceGrowth () * fLevelParam;

        return ( pPartnerRow.getInitMagicDefence () + ( int ) fLevelValue + m_ObjCardDB.TrainingMagicDefence );
    }

    public override int GetMagicDefence ()		//防御
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE ) )
        {
            //基础血量
            int nBaseValue = GetMagicBaseDefence ();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE ];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE ];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE ];
            //最终基础
            nBaseValue = nBaseValue + ( nBaseValue * ( nEquipSelfPermil + nSpellSelfPermil ) ) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE ];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE ];
            //team影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE ];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE ];
            //最终
            m_MagicDefence = ( int ) Mathf.Max ( 1, ( ( float ) nBaseValue + ( ( float ) nEquipPoint + ( float ) nSpellPoint + ( float ) nTeamPoint ) * ( 1.0f + ( ( float ) ( nSpellPermil ) ) / 1000.0f ) ) );

            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE );
        }
        return m_MagicDefence;
    }

    public override int GetBaseDodge ()			//本体闪避
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getDodgeGrowthMultiple () > 0 ) && ( pPartnerRow.getDodgeGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getDodgeGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getDodgeGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        return ( pPartnerRow.getBaseDodge () + ( int ) fLevelValue + nStageAddtion );

        //return (pPartnerRow.getInitDodge() + (int)fLevelValue);
    }

    public override int GetDodge ()				//总闪避
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE ) )
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

            int nBaseValue = GetBaseDodge ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Dodge = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE );
        }
        return m_Dodge;
    }

    public override int GetBaseCritical ()		//本体暴击
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getCriticalGrowthMultiple () > 0 ) && ( pPartnerRow.getCriticalGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getCriticalGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getCriticalGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        return ( pPartnerRow.getBaseCritical () + ( int ) fLevelValue + nStageAddtion );

        //return (pPartnerRow.getInitCritical() + (int)fLevelValue);
    }

    public override int GetCritical ()			//总暴击
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL ) )
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

            int nBaseValue = GetBaseCritical ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Critical = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL );
        }
        return m_Critical;
    }

    public override int GetBaseHit ()			//本体命中
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getHitGrowthMultiple () > 0 ) && ( pPartnerRow.getHitGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getHitGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getHitGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        return ( pPartnerRow.getBaseHit () + ( int ) fLevelValue + nStageAddtion );

        //return (pPartnerRow.getInitHit() + (int)fLevelValue);
    }
    public override int GetHit ()				//总命中
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_HIT ) )
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

            int nBaseValue = GetBaseHit ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Hit = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_HIT );
        }
        return m_Hit;
    }
    public override int GetBaseTenacity ()		//本体韧性
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getTenacityGrowthMultiple () > 0 ) && ( pPartnerRow.getTenacityGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getTenacityGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getTenacityGrowth () * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }

        return ( pPartnerRow.getBaseTenacity () + ( int ) fLevelValue + nStageAddtion );

        //return (pPartnerRow.getInitTenacity() + (int)fLevelValue);
    }
    public override int GetTenacity ()			//总韧性
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY ) )
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

            int nBaseValue = GetBaseTenacity ();
            //装备血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ];
            //星图血量点数
            int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ];
            //神器影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ];
            //培养影响点数
            int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ];

            int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

            m_Tenacity = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ] );
            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY );
        }
        return m_Tenacity;
    }
    public override int GetBaseSpeed ()		//本体速度
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow ();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        float fLevelParam = 1.0f;
        if ( ( pPartnerRow.getSpeedGrowthMultiple () > 0 ) && ( pPartnerRow.getSpeedGrowthMultiple () <= 1000 ) )
        {
            fLevelParam = plevelamendmentRow.getLevelAmendment () [ pPartnerRow.getSpeedGrowthMultiple () - 1 ];
        }
        double fLevelValue = m_ObjCardDB.Level * pPartnerRow.getSpeedGrowth () * fLevelParam;

        return ( pPartnerRow.getInitSpeed () + ( int ) fLevelValue );
    }
    public override int GetSpeed ()				//总速度
    {
        if ( IsBitSet ( EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED ) )
        {
            //基础血量
            int nBaseValue = GetBaseSpeed ();
            //基础血量计算
            //装备影响本体千分比
            int nEquipSelfPermil = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED ];
            //技能影响本体千分比
            int nSpellSelfPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED ];
            //team影响本体千分比
            int nTeamEffectPermil = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED ];
            //最终基础
            nBaseValue = nBaseValue + ( nBaseValue * ( nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil ) ) / 1000;
            //装备产生血量点数
            int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED ];
            //技能产生血量点数
            int nSpellPoint = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED ];
            //team影响点数
            int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED ];
            //技能增加千分比
            int nSpellPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED ];
            //最终
            m_Speed = ( int ) Mathf.Max ( 1, ( ( float ) nBaseValue + ( ( float ) nEquipPoint + ( float ) nSpellPoint + ( float ) nTeamPoint ) * ( 1.0f + ( ( float ) ( nSpellPermil ) ) / 1000.0f ) ) );

            ClearBitFlag ( EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED );
        }
        return m_Speed;
    }

    public override int GetHpRecover ()    //生命恢复力
    {
        HeroTemplate pPartnerRow = GetHeroRow ();

        int nBaseValue = pPartnerRow.getLifeRestoringForce ();

        int nEquipSelfPermil = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL ];
        //技能影响本体千分比
        int nSpellSelfPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL ];
        //team影响本体千分比
        int nTeamEffectPermil = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL ];
        //最终基础
        nBaseValue = nBaseValue + ( nBaseValue * ( nEquipSelfPermil + nSpellSelfPermil + nTeamEffectPermil ) ) / 1000;
        //装备产生血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER ];
        //技能产生血量点数
        int nSpellPoint = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER ];
        //team影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER ];
        //技能增加千分比
        int nSpellPermil = m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL ];

        m_HpRecover = ( int ) Mathf.Max ( 1, ( ( float ) nBaseValue + ( ( float ) nEquipPoint + ( float ) nSpellPoint + ( float ) nTeamPoint ) * ( 1.0f + ( ( float ) ( nSpellPermil ) ) / 1000.0f ) ) );

        return m_HpRecover;
    }

    public override float GetHitRate ()		//命中率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( pPartnerRow.getBaseHit ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE ] ) / 1000f;
    }
    public override float GetDodgeRate ()    //闪避率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( pPartnerRow.getBaseDodge ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE ] ) / 1000f;
    }
    public override float GetCriticalRate () //暴击率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( pPartnerRow.getBaseCritical ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE ] ) / 1000f;
    }
    public override float GetTenacityRate () //韧性率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( pPartnerRow.getBaseTenacity ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE ] ) / 1000f;
    }
    public override float GetPhysicalHurtAddPermil () //物理伤害加深率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( ( pPartnerRow.getBasePhyDamageIncrease ()
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT ]
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ] ) / 1000f );
    }
    public override float GetPhysicalHurtReducePermil () //物理伤害减免率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( ( pPartnerRow.getBasePhyDamageDecrease ()
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT ]
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ] ) / 1000f );
    }
    public override float GetMagicHurtAddPermil () //法术伤害加深率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( ( pPartnerRow.getBaseMagDamageIncrease ()
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT ]
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ] ) / 1000f );
    }
    public override float GetMagicHurtReducePermil () //法术伤害减免率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return ( ( pPartnerRow.getBaseMagDamageDecrease ()
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT ]
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ] ) / 1000f );
    }
    public override float GetCriticalHurtAddRate () //暴击伤害加成率
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        float fPower = DataTemplate.GetInstance ().m_GameConfig.getCritical_base_power (); //基础暴击伤害倍率 [7/20/2015 Zmy]
        return ( ( pPartnerRow.getBaseCriticalDamage ()
                       + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ]
                       + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ] ) / 1000f + fPower );
    }

    public override float GetCriticalHurtReduceRate () //暴击伤害减少千分比
    {
        //暂不需要 [7/16/2015 Zmy]
        return 0;
    }

    public override int GetExtraHurt () //伤害附加值
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return pPartnerRow.getDamageIncrease ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT ];
    }
    public override int GetReduceHurtPoint () //伤害减免值
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        return pPartnerRow.getDamageDecrease ()
                + m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT ]
                + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT ];
    }

    public override float GetInitPowerAddition () //初始怒气值 
    {
        return base.GetInitPowerAddition ();
    }

    public override float GetNormalSuckRate ()  //普攻吸血率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK ] ) / 1000f;
    }
    public override float GetSpellSuckRate ()  //技能吸血率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK ] ) / 1000f;
    }
    public override float GetCoolDownRate ()   //冷却缩减率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD ] ) / 1000f;
    }

    public override float GetInitPowerAdditionRate ()//初始怒气加成率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL ] ) / 1000f;
    }
    public override float GetAttackPowerAdditionRate ()//攻击怒气加成率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL ] ) / 1000f;
    }
    public override float GetHurtPowerAdditionRate ()//受击怒气加成率
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL ] ) / 1000f;
    }
    public override int GetCampType ()
    {
        return GetHeroRow ().getCamp ();
    }
    public override float GetAddDamageRateToCampA ()//对生灵阵营1伤害加成率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA ] ) / 1000f;
    }
    public override float GetAddDamageRateToCampB ()//对神族阵营2伤害加成率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB ] ) / 1000f;
    }
    public override float GetAddDamageRateToCampC ()//对恶魔阵营3伤害加成率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC ] ) / 1000f;
    }
    public override float GetReducDamageRateToCampA ()//受生灵阵营1伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA ] ) / 1000f;
    }
    public override float GetReducDamageRateToCampB ()//受神族阵营2伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB ] ) / 1000f;
    }
    public override float GetReducDamageRateToCampC ()//受恶魔阵营3伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC ] ) / 1000f;
    }
    public override float GetAddDamageRateToFightNear ()//对近战伤害加成率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTNEAR ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC ] ) / 1000f;
    }
    public override float GetAddDamageRateToFightFar ()//对远程伤害加成率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR ] ) / 1000f;
    }
    public override float GetReducDamageRateToFightNear ()//受近战伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR ] ) / 1000f;
    }
    public override float GetReducDamageRateToFightFar ()//受远程伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR ] ) / 1000f;
    }
    public override float GetAddDamageRateToBoss () //对boss伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS ] ) / 1000f;
    }
    public override float GetReducDamageRateToBoss () //受boss伤害减免率  [7/20/2015 Zmy]
    {
        return ( m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS ] + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS ] ) / 1000f;
    }
    public override float GetCampAttackParam ( ObjectCreature pTarget )//攻击方阵营对防御方阵营攻击系数  [7/20/2015 Zmy]
    {
        switch ( GetCampType () )//攻击方
        {
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                switch ( pTarget.GetCampType () )//防御方
                {
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_AtoA ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_AtoB ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_AtoC ();
                }
                break;
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                switch ( pTarget.GetCampType () )//防御方
                {
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_BtoA ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_BtoB ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_BtoC ();
                }
                break;
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                switch ( pTarget.GetCampType () )//防御方
                {
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_CtoA ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_CtoB ();
                    case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                        return DataTemplate.GetInstance ().m_GameConfig.getAttackCoefficient_CtoC ();
                }
                break;
            default:
                LogManager.LogError ( "!!!Error : campType is error" );
                return -1;
        }
        return -1;
    }
    public override float GetCampAddDamageRate ( ObjectCreature pTarget ) //攻击方阵营对防御方阵营伤害加成率  [7/20/2015 Zmy]
    {
        switch ( pTarget.GetCampType () )//防御方
        {
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                return GetAddDamageRateToCampA ();
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                return GetAddDamageRateToCampB ();
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                return GetAddDamageRateToCampC ();
            default:
                LogManager.LogError ( "!!!Error : campType is error" );
                return -1;
        }
    }

    public override float GetCampReducDamageRate ( ObjectCreature pTarget ) //防御方阵营对攻击方阵营的伤害减免率  [7/20/2015 Zmy]
    {
        switch ( pTarget.GetCampType () )//防御方
        {
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE1:
                return GetReducDamageRateToCampA ();
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE2:
                return GetReducDamageRateToCampB ();
            case ( int ) EM_HERO_CAMP_TYPE.EM_HERO_CAMP_TYPE3:
                return GetReducDamageRateToCampC ();
            default:
                LogManager.LogError ( "!!!Error : campType is error" );
                return -1;
        }
    }
    public override float GetAddDamageRateForAttackMode ( ObjectCreature pTarget ) //攻击方对防御方攻击距离类型的伤害加成率  [7/20/2015 Zmy]
    {
        if ( pTarget.GetIsNearAttackMold () ) // 近战 [7/20/2015 Zmy]
        {
            return GetAddDamageRateToFightNear ();
        }
        else
        {
            return GetAddDamageRateToFightFar ();
        }
    }
    public override float GetReducDamageRateForAttackMode ( ObjectCreature pTarget ) //防御方对攻击方攻击距离类型的伤害减免率  [7/20/2015 Zmy]
    {
        if ( pTarget.GetIsNearAttackMold () ) // 近战 [7/20/2015 Zmy]
        {
            return GetReducDamageRateToFightNear ();
        }
        else
        {
            return GetReducDamageRateToFightFar ();
        }
    }
    public override float GetAddDamageRateForBossType ( ObjectCreature pTarget ) //攻击方对BOSS伤害加成率  [7/20/2015 Zmy]
    {
        if ( pTarget.GetIsBossType () )
        {
            return GetAddDamageRateToBoss ();
        }
        return 0f;
    }

    public override float GetReducDamageRateForBossType ( ObjectCreature pTarget ) //防御方对BOSS伤害减免率  [7/20/2015 Zmy]
    {
        if ( pTarget.GetIsBossType () )
        {
            return GetReducDamageRateToBoss ();
        }
        return 0f;
    }

    public int GetTrainingMaxHP ()	//生命力值训练值
    {
        return m_ObjCardDB.TrainingMaxHP;
    }

    public int GetTrainingPhysicalAttack ()	//物理攻击力训练值
    {
        return m_ObjCardDB.TrainingPhysicalAttack;
    }

    public int GetTrainingPhysicalDefence ()	//物理防御力训练值	
    {
        return m_ObjCardDB.TrainingPhysicalDefence;
    }

    public int GetTrainingMagicAttack ()		//魔法攻击力训练值
    {
        return m_ObjCardDB.TrainingMagicAttack;
    }

    public int GetTrainingMagicDefence ()		//魔法防御力训练值	
    {
        return m_ObjCardDB.TrainingMagicDefence;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override int GetBaseCriticalHurt ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getBaseCriticalDamage () + ( int ) fLevelValue + nStageAddtion );
    }
    public override int GetCriticalHurtRate () //暴击伤害率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseCriticalHurt ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT ] );
        return _value;
    }

    public override int GetBaseHurtAdd ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getDamageBonusHit () + ( int ) fLevelValue + nStageAddtion );
    }
    public override int GetHurtAddRate () //伤害加成率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseHurtAdd ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE ] );
        return _value;
    }

    public override int GetBaseHurtReduce ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getDamageReductionHit () + ( int ) fLevelValue + nStageAddtion );
    }

    public override int GetHurtReduceRate ()//伤害减免率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseHurtReduce ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE ] );
        return _value;
    }

    public override int GetBaseBlock ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getBlockHit () + ( int ) fLevelValue + nStageAddtion );
    }
    public override int GetBlockRate () //格挡率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseBlock ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE ] );
        return _value;
    }
    public override int GetBasePierce ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getSabotageHit () + ( int ) fLevelValue + nStageAddtion );
    }
    public override int GetPierceRate ()//破甲率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBasePierce ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE ] );
        return _value;
    }
    public override int GetBaseSuck ()
    {
        HeroTemplate pPartnerRow = GetHeroRow ();
        //LevelamendmentTemplate plevelamendmentRow = GetPartnerLevelParamRow();

        //1.伙伴属性=伙伴基础属性+伙伴成长属性*参数
        //         float fLevelParam = 1.0f;
        //         if ((pPartnerRow.getHPGrowthMultiple() > 0) && (pPartnerRow.getHPGrowthMultiple() <= 1000))
        //         {
        //             fLevelParam = plevelamendmentRow.getLevelAmendment()[pPartnerRow.getHPGrowthMultiple() - 1];
        //         }
        double fLevelValue = 0f;//m_ObjCardDB.Level * pPartnerRow.getHPGrowth() * fLevelParam;

        int nStageAddtion = 0;
        HeroaddstageTemplate addStageRow = GetHeroaddStageRow ();
        if ( addStageRow != null )
        {
            for ( int i = 0; i < addStageRow.getAttribute ().Length; i++ )
            {
                if ( addStageRow.getAttribute () [ i ] == ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE )
                {
                    nStageAddtion += addStageRow.getValue () [ i ];
                }
            }
        }
        return ( pPartnerRow.getSabotageHit () + ( int ) fLevelValue + nStageAddtion );
    }
    public override int GetSuckRate ()//吸血率 [10/15/2015 Zmy]
    {
        int nBaseValue = GetBaseSuck ();
        //装备血量点数
        int nEquipPoint = m_ItemEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE ];
        //星图血量点数
        int nDowerPoint = m_DowerEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE ];
        //神器影响点数
        int nTeamPoint = m_TeamEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE ];
        //培养影响点数
        int nTrainPoint = m_TrainEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE ];

        int nSubPoint = nEquipPoint + nDowerPoint + nTeamPoint + nTrainPoint;

        int _value = ( int ) Mathf.Max ( 1, nBaseValue + nSubPoint + m_SpellEffect [ ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE ] );
        return _value;
    }
    public void UpdateSpellEffectValue ()
    {
        ClearEffect ( EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_IMPACT );

//         m_PassvitySpellInfo.Init ( m_ObjCardDB.SpellDataList [ 1 ].SpellID );
// 
//         Spell pSkill = new Spell ();
//         pSkill.SetHolder ( this );
//         pSkill.Init ( m_PassvitySpellInfo );
//         pSkill.ActivePassivityOnce ();
//         pSkill = null;
// 
//         if ( m_ObjCardDB.ItemPassiveSpell.SpellID != int.MaxValue )
        {
//             m_ItemPassiveSpellInfo.Init ( m_ObjCardDB.ItemPassiveSpell.SpellID );
// 
//             Spell pSkill_Item = new Spell ();
//             pSkill_Item.SetHolder ( this );
//             pSkill_Item.Init ( m_ItemPassiveSpellInfo );
//             pSkill_Item.ActivePassivityOnce ();
//             pSkill_Item = null;
        }

        Spell pSkill = new Spell();
        if (m_ObjCardDB.QualityLev > 2)
        {
            m_PassvitySpellInfo.Init(m_ObjCardDB.HeroSkillDB.SkillList[2]);
            pSkill.SetHolder(this);
            pSkill.Init(m_PassvitySpellInfo);
            pSkill.ActivePassivityOnce();
        }
        if (m_ObjCardDB.QualityLev > 4)
        {
            m_PassvitySpellInfo.Init(m_ObjCardDB.HeroSkillDB.SkillList[4]);
            pSkill.SetHolder(this);
            pSkill.Init(m_PassvitySpellInfo);
            pSkill.ActivePassivityOnce();
        }
        if (m_ObjCardDB.QualityLev > 5)
        {
            m_PassvitySpellInfo.Init(m_ObjCardDB.HeroSkillDB.SkillList[5]);
            pSkill.SetHolder(this);
            pSkill.Init(m_PassvitySpellInfo);
            pSkill.ActivePassivityOnce();
        }

        pSkill = null;
    }
    public void UpdateAttributeValue ()
    {
        UpdateItemEffectValue ();
        UpdateSpellEffectValue ();
        UpdateTeamEffectValue ();
        UpdateTrainEffectValue ();
    }
    //培养属性计算 [10/23/2015 Zmy]
    public void UpdateTrainEffectValue ()
    {
        ClearEffect ( EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN );
        for ( int i = 0; i < GlobalMembers.MAX_TRAIN_SLOT_COUNT; ++i )
        {
            int nTrainLev = m_ObjCardDB.HeroTrainDB.TrainLevel [ i ];
            int nTrainType = i + 1;
            HerocultureTemplate _HerocultureRow = DataTemplate.GetInstance ().GetHerocultureTemplate ( GetHeroRow ().getBorn (), GetHeroRow ().getQosition (), nTrainType, nTrainLev );
            if ( _HerocultureRow == null )
                continue;

            for ( int j = 0; j < _HerocultureRow.getAttribute ().Length; ++j )
            {
                EM_EXTEND_ATTRIBUTE _type = ( EM_EXTEND_ATTRIBUTE ) _HerocultureRow.getAttribute () [ j ];
                int nValue = _HerocultureRow.getValue () [ j ];
                ChangeEffect ( _type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TRAIN );
            }
        }
    }
    //新装备属性计算 [10/22/2015 Zmy]
    public void UpdateItemEffectValue ()
    {
        ClearEffect ( EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );

        int nEquipCount = m_ObjCardDB.HeroEqupDB.EquipList.Count;
        for ( int i = 0; i < nEquipCount; ++i )
        {
            int nEquipID = m_ObjCardDB.HeroEqupDB.EquipList [ i ].TableID;
            int nIntensifyLev = m_ObjCardDB.HeroEqupDB.EquipList [ i ].IntensifyLev;
            //装备本身属性加成
            EquipmentqualityTemplate _EquipQualityRow = ( EquipmentqualityTemplate ) DataTemplate.GetInstance ().m_EquipmentqualityTable.getTableData ( nEquipID );
            if ( _EquipQualityRow == null )
                continue;

            for ( int n = 0; n < _EquipQualityRow.getQualityAttribute ().Length; ++n )
            {
                EM_EXTEND_ATTRIBUTE _type = ( EM_EXTEND_ATTRIBUTE ) _EquipQualityRow.getQualityAttribute () [ n ];
                int nValue = _EquipQualityRow.getNumerical () [ n ];
                ChangeEffect ( _type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
            }
            //强化属性加成 [10/22/2015 Zmy]
            EquipmentstrengthTemplate _EquipStrengthRow = DataTemplate.GetInstance ().GetEquipStrengthTemplate ( GetHeroRow ().getQosition (), _EquipQualityRow.getParts (), nIntensifyLev );
            if ( _EquipStrengthRow == null )
                continue;

            for ( int j = 0; j < _EquipStrengthRow.getAttribute ().Length; ++j )
            {
                EM_EXTEND_ATTRIBUTE _type = ( EM_EXTEND_ATTRIBUTE ) _EquipStrengthRow.getAttribute () [ j ];
                int nValue = _EquipStrengthRow.getValue () [ j ];
                ChangeEffect ( _type, nValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
            }
        }
    }
    /// <summary>
    /// 符文装备属性刷新计算  [5/21/2015 Zmy]
    /// 符文属性计算 废弃  [10/22/2015 Zmy]
    /// </summary>
    public void UpdateItemEffectValue_Back ()
    {
        //属性刷新计算前清空一次 [5/28/2015 Zmy]
        ClearEffect ( EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
        //缓存符文搭配条件，用于后面符文组合属性计算 [5/22/2015 Zmy]
        m_RunePassiveList.Clear ();
        if ( GetHeroRow ().getRunePair1 () != -1 )
            m_RunePassiveList.Add ( GetHeroRow ().getRunePair1 () );
        if ( GetHeroRow ().getRunePair2 () != -1 )
            m_RunePassiveList.Add ( GetHeroRow ().getRunePair2 () );
        if ( GetHeroRow ().getRunePair3 () != -1 )
            m_RunePassiveList.Add ( GetHeroRow ().getRunePair3 () );
        if ( GetHeroRow ().getRunePair4 () != -1 )
            m_RunePassiveList.Add ( GetHeroRow ().getRunePair4 () );

        bool IsActiveRunePassive = false;// 是否满足符文组合效果 [5/22/2015 Zmy]
        for ( int i = 0; i < ( int ) EM_RUNE_POINT.EM_RUNE_POINT_NUMBER; i++ )
        {
            ItemEquip _equip = m_ObjCardDB.GetRuneItemInfo ( ( EM_RUNE_POINT ) i );
            if ( _equip != null )
            {
                CalcRuneBaseAttribute ( _equip );

                CalcRuneAppendAttribute ( _equip );

                if ( IsActiveRunePassive == false )
                {
                    for ( int n = 0; n < m_RunePassiveList.Count; n++ )
                    {
                        if ( _equip.GetItemRowData ().getRune_type () == m_RunePassiveList [ n ] )
                        {
                            m_RunePassiveList.RemoveAt ( n );//满足条件移除一个 直到条件列表为空后激活组合属性
                            break;
                        }
                    }

                    if ( m_RunePassiveList.Count == 0 )
                        IsActiveRunePassive = true;

                }
            }
        }

        //满足符文组合条件，计算符文组合属性 [5/22/2015 Zmy]
        if ( IsActiveRunePassive )
        {
            CalcRunePassive ( GetHeroRow ().getRunePassive () );
        }
    }
    // 所有神器属性加成计算 [5/27/2015 Zmy]
    public void UpdateTeamEffectValue ()
    {
        ClearEffect ( EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );

        for ( int i = ( int ) EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAXHP; i < ( int ) EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_NUM; i++ )
        {
            int nValue = ObjectSelf.GetInstance ().ArtifactContainerBag.GetAttributeTotal ( ( EM_ARTIFACT_ATTRIBUTE_TYPE ) i );
            CalcArtifactAttribute ( ( EM_ARTIFACT_ATTRIBUTE_TYPE ) i, nValue );
        }
    }

    //符文组合属性计算 [5/22/2015 Zmy]
    private void CalcRunePassive ( int nPassiveID )
    {
        RunepassiveTemplate _row = ( RunepassiveTemplate ) DataTemplate.GetInstance ().m_RunepassiveTable.getTableData ( nPassiveID );
        if ( _row != null )
        {
            if ( _row.getAttribute1 () != -1 )
            {
                CalcRunePassiveAttribute ( _row.getAttribute1 (), _row.getValue1 () );
            }

            if ( _row.getAttribute2 () != -1 )
            {
                CalcRunePassiveAttribute ( _row.getAttribute2 (), _row.getValue2 () );
            }

            if ( _row.getAttribute3 () != -1 )
            {
                CalcRunePassiveAttribute ( _row.getAttribute3 (), _row.getValue3 () );
            }
        }
    }
    //符文基础属性计算 [5/21/2015 Zmy]
    private void CalcRuneBaseAttribute ( ItemEquip _equip )
    {
        for ( int baseIndex = 0; baseIndex < GlobalMembers.MAX_RUNE_BASE_ATTRIBUTE_COUNT; baseIndex++ )
        {
            int nBaseAttriID = _equip.GetRuneData ().BaseAttributeID [ baseIndex ];
            if ( nBaseAttriID == -1 )
                continue;
            int nSkill0 = 0;
            int nSkill1 = 0;
            int nSkill2 = 0;
            BaseruneattributeTemplate _rowBase = ( BaseruneattributeTemplate ) DataTemplate.GetInstance ().m_BaseruneattributeTable.getTableData ( nBaseAttriID );
            if ( _rowBase != null )
            {
                int nAttributeType = _rowBase.getAttriType ();
                //符文基础属性有超过属性枚举的范畴，特殊验证一下有效枚举属性的区间（[1,54]和[100,105]） [7/28/2015 Zmy]
                if ( nAttributeType < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID ||
                   ( nAttributeType >= ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER && nAttributeType < ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_PASSIVE_SKILL ) ||
                    nAttributeType >= ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_NUMBER_MAX )
                {
                    Debug.LogError ( "!!!Error : ObjectCard::CalcRuneBaseAttribute() nAttributeType RangeOut!:" + nAttributeType );
                    return;
                }

                switch ( nAttributeType )
                {
                    case 0:
                    case 34:
                        {
                            Debug.LogError ( "!!!Error : ObjectCard::CalcRuneBaseAttribute() nAttributeType Param Error!" );
                            return;
                        }
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_PASSIVE_SKILL:
                        nSkill0 = _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemPassiveSpell.SpellID = nSkill0;
                        break;
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_COMMON_SKILL:
                        nSkill0 = m_ObjCardDB.SpellDataList [ 0 ].SpellID + _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemChangeSkill ( 0, nSkill0 );
                        return;
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PASSIVE_SKILL:
                        nSkill1 = m_ObjCardDB.SpellDataList [ 1 ].SpellID + _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemChangeSkill ( 1, nSkill1 );
                        return;
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_SKILL:
                        nSkill2 = m_ObjCardDB.SpellDataList [ 2 ].SpellID + _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemChangeSkill ( 2, nSkill2 );
                        return;
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_PVP_COMMON_SKILL:
                        nSkill0 = m_ObjCardDB.SpellDataList [ 0 ].SpellID + _rowBase.getAttriValue ();
                        nSkill2 = m_ObjCardDB.SpellDataList [ 2 ].SpellID + _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemChangeSkill ( 0, nSkill0 );
                        m_ObjCardDB.ItemChangeSkill ( 2, nSkill2 );
                        return;
                    case ( int ) EM_RUNE_BASE_ATTRIBUTE_TYPE.EM_RUNE_BASE_ATTRIBUTE_ADD_ALL_SKILL:
                        nSkill0 = m_ObjCardDB.SpellDataList [ 0 ].SpellID + _rowBase.getAttriValue ();
                        nSkill1 = m_ObjCardDB.SpellDataList [ 1 ].SpellID + _rowBase.getAttriValue ();
                        nSkill2 = m_ObjCardDB.SpellDataList [ 2 ].SpellID + _rowBase.getAttriValue ();
                        m_ObjCardDB.ItemChangeSkill ( 0, nSkill0 );
                        m_ObjCardDB.ItemChangeSkill ( 1, nSkill1 );
                        m_ObjCardDB.ItemChangeSkill ( 2, nSkill2 );
                        return;
                    default:
                        ChangeEffect ( ( EM_EXTEND_ATTRIBUTE ) nAttributeType, _rowBase.getAttriValue (), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
                        break;
                }
            }
        }
    }

    //符文附加属性计算 [5/21/2015 Zmy]
    private void CalcRuneAppendAttribute ( ItemEquip _equip )
    {
        if ( _equip == null )
            return;

        int activeNum = HeroRuneModule.GetAppendAttriActiveNum ( _equip.GetStrenghLevel () );

        for ( int appendIndex = 0; appendIndex < GlobalMembers.MAX_RUNE_APPEND_ATTRIBUTE_COUNT; appendIndex++ )
        {
            if ( appendIndex >= activeNum )
                break;

            int nBaseAttriID = _equip.GetRuneData ().AppendAttribute [ appendIndex ];
            if ( nBaseAttriID == -1 )
                continue;

            AddruneattributeTemplate _rowData = ( AddruneattributeTemplate ) DataTemplate.GetInstance ().m_AddruneattributeTable.getTableData ( nBaseAttriID );
            if ( _rowData != null )
            {
                int nAttributeType = _rowData.getAttriType ();
                if ( nAttributeType < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID || nAttributeType >= ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER )
                {
                    Debug.LogError ( "!!!Error : ObjectCard::CalcRuneAppendAttribute() nAttributeType RangeOut!:" + nAttributeType );
                    return;
                }
                switch ( nAttributeType )
                {
                    // 尚未定义的返回error [7/27/2015 Zmy]
                    case 0:
                    case 34:
                        {
                            Debug.LogError ( "!!!Error : ObjectCard::CalcRuneAppendAttribute() nAttributeType Param Error!" );
                            return;
                        }
                    default:
                        ChangeEffect ( ( EM_EXTEND_ATTRIBUTE ) nAttributeType, _rowData.getAttriValue (), EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
                        break;
                }
            }
        }
    }

    private void CalcRunePassiveAttribute ( int nAttributeType, int nAttributeValue )
    {
        if ( nAttributeType < ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_INVALID || nAttributeType >= ( int ) EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER )
        {
            Debug.LogError ( "!!!Error : ObjectCard::CalcRunePassiveAttribute() nAttributeType RangeOut!:" + nAttributeType );
            return;
        }

        switch ( nAttributeType )
        {
            // 尚未定义的返回error [7/27/2015 Zmy]
            case 0:
            case 34:
                {
                    Debug.LogError ( "!!!Error : ObjectCard::CalcRunePassiveAttribute() nAttributeType Param Error!" );
                    return;
                }
            default:
                ChangeEffect ( ( EM_EXTEND_ATTRIBUTE ) nAttributeType, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_RUNE );
                break;
        }
    }

    public void CalcArtifactAttribute ( EM_ARTIFACT_ATTRIBUTE_TYPE nAttributeType, int nAttributeValue )
    {
        switch ( nAttributeType )
        {
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAXHP:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALATTACK:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_PHYSICALDEFENCE:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICATTACK:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_MAGICDEFENCE:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_HIT:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_DODGE:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_CRITICAL:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            case EM_ARTIFACT_ATTRIBUTE_TYPE.EM_ARTIFACT_ATTRIBUTE_TENACITY:
                ChangeEffect ( EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY, nAttributeValue, EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_TEAM );
                break;
            default:
                break;
        }
    }


    // 获取英雄的所有经验
    public int GetAllExp ()
    {
        HeroTemplate _HeroItem = this.GetHeroRow ();
        int expid = _HeroItem.getExpNum () - 1;
        int level = this.GetHeroData ().Level;
        if ( level < _HeroItem.getMaxLevel () )
        {
            HeroexpTemplate _HeroExp = ( HeroexpTemplate ) DataTemplate.GetInstance ().m_HeroExpTable.getTableData ( level );
            int needxp = _HeroExp.getExp () [ expid ];
            return needxp;
        }
        return -1;
    }
}
