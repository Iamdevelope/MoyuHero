using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DreamFaction.GameNetWork.Data;
using DreamFaction.SkillCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.GameAudio;


public class UI_HurtInfo
{
    public ObjectCreature pTarget;            //目标
    public bool bCritical;          //暴击
    public int nHurt;              //伤害
}
public class ObjectCreature
{
    //行为状态
    public enum ObjectActionState
    {
        none,
        idle,
        monmentmoveIng,         //瞬间移动中
        forward,                // 行进 [1/22/2015 Zmy]
        scanning,               // 扫描目标 [1/22/2015 Zmy]
        moveTarget,             // 往目标点移动 [1/22/2015 Zmy]
        normalAttack,           // 普通攻击 [1/22/2015 Zmy]
        AttackIdle,             // 普攻待机
        AttackIdleing,          // 普攻待机中
        Attacking,              // 普通攻击状态中 [3/4/2015 Zmy]
        checkHurting,           // 受击检测状态 [9/28/2015 Zmy]
        Hurting,                // 受击ing [10/19/2015 Zmy]
        deathing,               // 死亡ing
        destory,                // 销毁 [2/7/2015 Zmy]
        skillAttack,            // 技能攻击 [1/22/2015 Zmy]
        skilling,               // 正在技能状态中 [3/4/2015 Zmy]
        skillEnd,               // 技能结束状态(只是用于引导类技能)
        dizzy,                  // 眩晕
        boarding,               // 英雄上船后船移动过程中;
    }

    protected long m_CurHp;                //当前血量
    protected long m_MaxHP;				//血上限
    protected int m_PhysicalAttack;		//攻击
    protected int m_PhysicalDefence;		//防御
    protected int m_MagicAttack;			//攻击
    protected int m_MagicDefence;			//防御
    protected int m_Hit;					//命中
    protected int m_Dodge;				//闪避
    protected int m_Critical;				//暴击
    protected int m_Tenacity;				//韧性
    protected int m_Speed;				//速度
    protected float m_MoveSpeed;			//移动速度
    protected int m_HpRecover;            //生命恢复力
    protected float m_InitPowerAddition;    //初始怒气值
    protected float m_PhysicalDamageRate;   //物理伤害加深率
    protected float m_MagicDamageRate;      //法术伤害加深率
    protected int m_DamageAppend;         //伤害附加值
    protected float m_PhysicalDamageAbate;  //物理伤害减免率
    protected float m_MagicDamageAbate;     //法术伤害减免率
    protected int m_DamageAbate;          //伤害减免值
    protected int m_SuckValue;            //吸血

    protected int[] m_FightState = new int[(int)EM_FIGHT_STATE.EM_FIGHT_STATE_NUMBER];		//战斗中状态
    protected Vector3 m_Pos;
    protected CoolDownList m_CoolDownList = new CoolDownList();
    protected Impact m_pImpactList = new Impact();
    protected List<Impact> m_ImpactList = new List<Impact>();
    protected List<Impact> m_IdleImpactList = new List<Impact>();

    private SpellEventQueue m_SpellEventQueue = new SpellEventQueue();
    private Flag32 m_DirtyMask = new Flag32();
    private ObjectActionState m_CurActionState = ObjectActionState.none;    //当前状态 [10/19/2015 Zmy]
    private ObjectActionState m_LastActionState = ObjectActionState.none;   //上一个状态，在改变状态时，立即设置为改变后的状态，即当前状态[10/19/2015 Zmy]
    private ObjectActionState m_CacheLastActionState = ObjectActionState.none; //缓存的上一个状态。即每改变一次状态时候，记录上一个状态。只在有状态改变时候才做出改变，永远不等于当前状态 [10/19/2015 Zmy]
    private SpellInfo m_ActivationSpell;     //普通攻击信息
    private List<SpellInfo> m_SkillSpellList;      //技能信息数组
    private EM_OBJECT_TYPE m_GroupType;

    private SpellInfo m_CurFreeSpell;
    private byte m_TeamPos;


    protected AnimationControl m_AnimControl;
    protected float m_GuidanceTime = 0;//引导技能逻辑参数
    protected float m_MaxGuidanceTime = 0;//引导技能逻辑参数
    protected int m_count = 1;//引导技能逻辑参数

    //AI逻辑使用
    protected float m_FightingTimestamp = -1;        //开始战斗的时间戳
    protected float m_LastSkillTimestamp = -1;       //释放上个一技能的时间戳
    protected int m_LastSpellID = -1;
    protected ENUM_SPELL_TYPE_FLAG m_SkillTypeFlag;

    public ENUM_SPELL_TYPE_FLAG SkillTypeFlag
    {
        get { return m_SkillTypeFlag; }
        set { m_SkillTypeFlag = value; }
    }
    public float FightingTimestamp
    {
        get { return m_FightingTimestamp; }
        set { m_FightingTimestamp = value; }
    }
    public float LastSkillTimestamp
    {
        get { return m_LastSkillTimestamp; }
        set { m_LastSkillTimestamp = value; }
    }
    public int LastSpellID
    {
        get { return m_LastSpellID; }
        set { m_LastSpellID = value; }
    }
    //public bool isImpactList()
    //{
    //    if (m_ImpactList.Count <= 0)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

    public virtual X_GUID GetGuid()
    {
        return null;
    }
    public bool IsAttacker()
    {
        return false;
    }
    protected bool IsBitSet(EM_ATTRIBUTE nType)
    {
        return m_DirtyMask.isSetBit((int)nType);
    }

    protected void SetBitFlag(EM_ATTRIBUTE nType)
    {
        m_DirtyMask.UpdateBits((int)nType, true);
    }

    protected void ClearBitFlag(EM_ATTRIBUTE nType)
    {
        m_DirtyMask.UpdateBits((int)nType, false);
    }

    public virtual int GetAttribute(int nType)
    {
        switch (nType)
        {
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAXHP:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAXHP:
                {
                    return (int)GetMaxHP();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALATTACK:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALATTACK:
                {
                    return GetPhysicalAttack();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_PHYSICALDEFENCE:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PHYSICALDEFENCE:
                {
                    return GetPhysicalDefence();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICATTACK:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICATTACK:
                {
                    return GetMagicAttack();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MAGICDEFENCE:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MAGICDEFENCE:
                {
                    return GetMagicDefence();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HIT:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HIT:
                {
                    return GetHit();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_DODGE:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGE:
                {
                    return GetDodge();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_CRITICAL:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICAL:
                {
                    return GetCritical();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_TENACITY:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITY:
                {
                    return GetTenacity();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_SPEED:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SPEED:
                {
                    return GetSpeed();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_HPRECOVER:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HEAL:
                {
                    return GetHpRecover();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_HITRATE:
                {
                    return (int)GetHit();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_DODGERATE:
                {
                    return (int)GetDodge();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CRITICALRATE:
                {
                    return (int)GetCritical();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_TENACITYRATE:
                {
                    return (int)GetTenacity();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDPHYSICALHURT:
                {
                    return (int)GetPhysicalHurtAddPermil();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEPHYSICALHURT:
                {
                    return (int)GetPhysicalHurtReducePermil();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADDMAGICHURT:
                {
                    return (int)GetMagicHurtAddPermil();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_REDUCEMAGICHURT:
                {
                    return (int)GetMagicHurtReducePermil();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RATE_CRITICALHURT:
                {
                    return (int)GetCriticalHurtRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_EXTRAHURT:
                {
                    return GetExtraHurt();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_REDUCEHURT:
                {
                    return GetReduceHurtPoint();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ATTACKSUCK:
                {
                    return (int)GetNormalSuckRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_SKILLSUCK:
                {
                    return (int)GetSpellSuckRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_RECUDE_SPELLCD:
                {
                    return (int)GetCoolDownRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPINIT_PERMIL:
                {
                    return (int)GetInitPowerAdditionRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPATTACK_PERMIL:
                {
                    return (int)GetAttackPowerAdditionRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_ADDMPHIT_PERMIL:
                {
                    return (int)GetHurtPowerAdditionRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPA:
                {
                    return (int)GetAddDamageRateToCampA();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPB:
                {
                    return (int)GetAddDamageRateToCampB();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_CAMPC:
                {
                    return (int)GetAddDamageRateToCampC();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPA:
                {
                    return (int)GetReducDamageRateToCampA();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPB:
                {
                    return (int)GetReducDamageRateToCampB();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_CAMPC:
                {
                    return (int)GetReducDamageRateToCampC();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTNEAR:
                {
                    return (int)GetAddDamageRateToFightNear();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_FIGHTFAR:
                {
                    return (int)GetAddDamageRateToFightFar();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTNEAR:
                {
                    return (int)GetReducDamageRateToFightNear();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_FIGHTFAR:
                {
                    return (int)GetReducDamageRateToFightFar();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_ADD_DAMAGE_BOSS:
                {
                    return (int)GetAddDamageRateToBoss();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PREMIL_REDUC_DAMAGE_BOSS:
                {
                    return (int)GetReducDamageRateToBoss();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_BLOCK_RATE:
                {
                    return (int)GetBlockRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_PIERCE_RATE:
                {
                    return (int)GetPierceRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_SUCK_RATE:
                {
                    return (int)GetSuckRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_ADD_DAMAGE:
                {
                    return (int)GetHurtAddRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_CUT_DAMAGE:
                {
                    return (int)GetHurtReduceRate();
                }
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MP:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MONEY:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_EXP:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_PERMIL_MPRECOVER:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_POINT_MPNORMALATT:
            case (int)EM_EXTEND_ATTRIBUTE.EM_EXTEND_ATTRIBUTE_NUMBER:
                break;
            default:
                break;
        }
        return 0;
    }
    public virtual int GetImpactCountByGroupID(int nType) //获得buff组中buf数量 
    {
        BuffgroupTemplate row = (BuffgroupTemplate)DataTemplate.GetInstance().m_BuffGroupTable.getTableData(nType);
        int nCount = 0;
        if (row != null)
        {
            for (int i = 0; i < row.getParam().Length; i++)
            {
                nCount += GetImpactCountByID(row.getParam()[i]);
            }
        }
        return nCount;
    }
    public virtual ObjectActionState GetActionState() { return m_CurActionState; }

    public virtual ObjectActionState GetLastActionState() { return m_LastActionState; }

    public virtual ObjectActionState GetCacheLastActionState() { return m_CacheLastActionState; }

    public virtual void SetObjectActionState(ObjectActionState state) 
    {
        if (state != m_CurActionState)
            m_CacheLastActionState = m_CurActionState;
        
        m_CurActionState = state; 
    }
    public virtual void SetLastActionState(ObjectActionState state) { m_LastActionState = state; }

    public virtual int GetFuryIdForTable() { return -1; }
    //属性
    public virtual void SetHP(long nHP) { m_CurHp = nHP; }				  //设置hp
    public virtual long GetHP() { return m_CurHp; }						  //获得hp
    public virtual int GetMP() { return 0; }
    public virtual void SetMP(int nHP) { }

    public virtual long GetMaxHP() { return -1; }						//获得最大hp
    public virtual long GetBaseMaxHP() { return -1; }				//获得基础hp

    public virtual int GetPhysicalBaseAttack() { return -1; }	  //本体攻击点数
    public virtual int GetPhysicalAttack() { return -1; }		  //攻击

    public virtual int GetPhysicalBaseDefence() { return -1; }	 //本体防御点数
    public virtual int GetPhysicalDefence() { return -1; }		//防御

    public virtual int GetMagicBaseAttack() { return -1; }	  //本体攻击点数
    public virtual int GetMagicAttack() { return -1; }		  //攻击

    public virtual int GetMagicBaseDefence() { return -1; }	 //本体防御点数
    public virtual int GetMagicDefence() { return -1; }		//防御

    public virtual int GetBaseDodge() { return -1; }		   //本体闪避
    public virtual int GetDodge() { return -1; }		   //闪避
    public virtual void SetDodge(int dodge) { }

    public virtual int GetBaseCritical() { return -1; }	  //本体暴击
    public virtual int GetCritical() { return -1; }		 //暴击
    public virtual void SetCritical(int critical) { }

    public virtual int GetBaseHit() { return -1; }		//本体命中
    public virtual int GetHit() { return -1; }			//总命中
    public virtual void SetHit(int nHit) { }

    public virtual GameObject GetGameObject() { return null; }
    public virtual int GetBaseTenacity() { return -1; }		//本体韧性
    public virtual int GetTenacity() { return -1; }			//总韧性
    public virtual void SetTenacity(int nTenacity) { }

    public virtual int GetBaseSpeed() { return -1; }		//本体速度
    public virtual int GetSpeed() { return -1; }			//总速度
    public virtual void SetSpeed(int nSpeed) { }

    public virtual float GetMoveSpeed() { return 0.0f; }
    public virtual float GetBaseMoveSpeed() { return 0.0f; }
    public virtual void SetMoveSpeed(float fValue) { }

    public virtual float GetHitRate() { return 0.0f; }		//命中率
    public virtual float GetDodgeRate() { return 0.0f; }    //闪避率
    public virtual float GetCriticalRate() { return 0.0f; } //暴击率
    public virtual float GetTenacityRate() { return 0.0f; } //韧性率
    public virtual float GetPhysicalHurtAddPermil() { return 0; } //物理伤害加深率
    public virtual float GetPhysicalHurtReducePermil() { return 0; } //物理伤害减免率
    public virtual float GetMagicHurtAddPermil() { return 0; } //法术伤害加深率
    public virtual float GetMagicHurtReducePermil() { return 0; } //法术伤害减免率
    public virtual float GetCriticalHurtAddRate() { return 0; } //暴击伤害加成率
    public virtual float GetCriticalHurtReduceRate() { return 0; } //暴击伤害减免率
    public virtual int GetExtraHurt() { return -1; } //伤害附加值
    public virtual int GetReduceHurtPoint() { return -1; } //伤害减免值
    public virtual int GetHpRecover() { return -1; } //生命恢复力
    public virtual float GetInitPowerAddition() { return 0.0f; } //初始怒气值
    public virtual float GetNormalSuckRate() { return 0f; } //普攻吸血率
    public virtual float GetSpellSuckRate() { return 0f; }  //技能吸血率
    public virtual float GetCoolDownRate() { return 0f; }    //冷却缩减率

    public virtual float GetInitPowerAdditionRate() { return 0.0f; }//初始怒气加成率
    public virtual float GetAttackPowerAdditionRate() { return 0.0f; }//攻击怒气加成率
    public virtual float GetHurtPowerAdditionRate() { return 0.0f; }//受击怒气加成率

    public virtual int GetCampType() { return -1; }// 返回阵营 [7/20/2015 Zmy]
    public virtual float GetAddDamageRateToCampA() { return 1f; } //对生灵阵营1伤害加成率  [7/20/2015 Zmy]
    public virtual float GetAddDamageRateToCampB() { return 1f; } //对神族阵营2伤害加成率  [7/20/2015 Zmy]
    public virtual float GetAddDamageRateToCampC() { return 1f; } //对恶魔阵营3伤害加成率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToCampA() { return 1f; } //受生灵阵营1伤害减免率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToCampB() { return 1f; } //受神族阵营2伤害减免率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToCampC() { return 1f; } //受恶魔阵营3伤害减免率  [7/20/2015 Zmy]

    public virtual float GetAddDamageRateToFightNear() { return 1f; } //对近战伤害加成率  [7/20/2015 Zmy]
    public virtual float GetAddDamageRateToFightFar() { return 1f; }  //对远程伤害加成率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToFightNear() { return 1f; } //受近战伤害减免率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToFightFar() { return 1f; }  //受远程伤害减免率  [7/20/2015 Zmy]

    public virtual float GetAddDamageRateToBoss() { return 0f; }    //对boss伤害减免率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateToBoss() { return 0f; }  //受boss伤害减免率  [7/20/2015 Zmy]

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //新属性计算公式，统一返回值类型int 
    public virtual int GetBaseBlock() { return 1; }
    public virtual int GetBlockRate() { return 1; }                 //格挡率 [10/15/2015 Zmy]
    public virtual int GetBasePierce() { return 1; }
    public virtual int GetPierceRate() { return 1; }                //破甲率 [10/15/2015 Zmy]
    public virtual int GetBaseSuck() { return 1; }
    public virtual int GetSuckRate() { return 1; }                  //吸血率 [10/15/2015 Zmy]
    public virtual int GetBaseCriticalHurt() { return 1; }
    public virtual int GetCriticalHurtRate() { return 1; }       //暴击伤害率 [10/15/2015 Zmy]
    public virtual int GetBaseHurtAdd() { return 1; }
    public virtual int GetHurtAddRate() { return 1; }               //伤害加成率 [10/15/2015 Zmy]
    public virtual int GetBaseHurtReduce() { return 1; }
    public virtual int GetHurtReduceRate() { return 1; }            //伤害减免率 [10/15/2015 Zmy]

    public virtual int GetDevelopAttributeSub(int nType) { return 0; } //返回养成属性的总合 [10/15/2015 Zmy]
    public virtual int GetSpellAttibute(int nType) { return 0; }     //返回buff的属性加成 [10/15/2015 Zmy]
    public virtual void SetTeamPos(byte pos) { m_TeamPos = pos; }
    public virtual byte GetTeamPos() { return m_TeamPos; }
    public virtual SpellInfo GetSpellInfo(int nSpellID)
    {
        if (m_SkillSpellList == null)
            return null;
        SpellInfo temp = null;
        for (int i = 0; i < m_SkillSpellList.Count; ++i)
        {
            if (m_SkillSpellList[i].GetSpellID() == nSpellID)
                temp = m_SkillSpellList[i];
        }
        return temp;
    }
    public void SetActivationSpellCD(SpellInfo info)
    {
        if (info.GetCoolDownTime() > 0)
        {
            // 技能冷却时间 = 基本冷却时间*（1-冷却缩减率） [7/21/2015 Zmy]
            m_CoolDownList.AddElement(info.GetSpellID(), (int)(info.GetCoolDownTime() * (1f - GetCoolDownRate())));
        }
        if (m_SkillSpellList == null)
            m_SkillSpellList = new List<SpellInfo>();
        for (int i = 0; i < m_SkillSpellList.Count; ++i)
        {
            if (m_SkillSpellList[i].GetSpellID() == info.GetSpellID())
                return;
        }
        m_SkillSpellList.Add(info);
    }

    public virtual bool GetIsNearAttackMold() { return false; }

    public virtual bool GetIsBossType() { return false; }
    public virtual CoolDownList GetCoolDownList() { return m_CoolDownList; }		// 冷却
    public virtual bool CoolDownHeartBeat(uint uTime) { m_CoolDownList.HeartBeat(uTime); return true; }	// 冷却

    public virtual float GetCampAttackParam(ObjectCreature pTarget) { return 1.0f; } //攻击方阵营对防御方阵营攻击系数  [7/20/2015 Zmy]
    public virtual float GetCampAddDamageRate(ObjectCreature pTarget) { return 1.0f; } //攻击方阵营对防御方阵营的伤害加成率  [7/20/2015 Zmy]
    public virtual float GetCampReducDamageRate(ObjectCreature pTarget) { return 1.0f; } //防御方阵营对攻击方阵营的伤害减免率  [7/20/2015 Zmy]
    public virtual float GetAddDamageRateForAttackMode(ObjectCreature pTarget) { return 1.0f; } //攻击方对防御方攻击距离类型的伤害加成率  [7/20/2015 Zmy]
    public virtual float GetReducDamageRateForAttackMode(ObjectCreature pTarget) { return 1.0f; } //防御方对攻击方攻击距离类型的伤害减免率  [7/20/2015 Zmy]

    public virtual float GetAddDamageRateForBossType(ObjectCreature pTarget) { return 0f; } //攻击方对BOSS伤害加成率  [7/20/2015 Zmy]

    public virtual float GetReducDamageRateForBossType(ObjectCreature pTarget) { return 0f; } //防御方对BOSS伤害减免率  [7/20/2015 Zmy]

    public virtual int GetEffect(EM_EXTEND_ATTRIBUTE nAttrType, EM_EFFECT_SOURCE_TYPE nType = EM_EFFECT_SOURCE_TYPE.EM_EFFECT_SOURCE_TYPE_INVALID)
    {
        return 0;
    }

    public virtual void ChangeEffect(EM_EXTEND_ATTRIBUTE nAttrType, int nValue, EM_EFFECT_SOURCE_TYPE nType, bool bRemove = false)	//改变effect
    {

    }

    public SpellEventQueue GetSpellEventQueue()
    {
        return m_SpellEventQueue;
    }
    public SpellInfo GetSpell() { return m_CurFreeSpell; }
    public void SetSpell(SpellInfo pData) { m_CurFreeSpell = pData; }
    public Spell GetSubSpell() { return null; }

    public virtual Vector3 getWorldPos() { return m_Pos; }

    public virtual HeroData GetHeroData() { return null; }
    //public virtual void setWorldPos(Vector3 pos) { m_Pos = pos; }


    public virtual bool IsInFightState(EM_FIGHT_STATE nState)
    {
        if ((nState <= EM_FIGHT_STATE.EM_FIGHT_STATE_INVALID) || (nState >= EM_FIGHT_STATE.EM_FIGHT_STATE_NUMBER))
        {
            LogManager.LogAssert(0);
        }

        return m_FightState[(int)nState] > 0;
    }
    public virtual void SetFightState(int nState, X_GUID guid)
    {
        if ((nState <= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_INVALID) || (nState >= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_NUMBER))
        {
            LogManager.LogAssert(0);
        }

        ++m_FightState[nState];
    }
    public virtual void RemoveFightState(int nState, X_GUID guid)
    {
        if ((nState <= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_INVALID) || (nState >= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_NUMBER))
        {
            LogManager.LogAssert(0);
        }

        --m_FightState[nState];
        if (m_FightState[nState] < 0)
        {
            m_FightState[nState] = 0;
        }
    }
    public virtual void ClearFightState(int nState)
    {
        if ((nState <= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_INVALID) || (nState >= (int)EM_FIGHT_STATE.EM_FIGHT_STATE_NUMBER))
        {
            LogManager.LogAssert(0);
        }

        m_FightState[nState] = 0;
    }

    public virtual void RegisterSpellEvent(Impact pImpact, SPELL_EVENT_ID nID)
    {
        if ((pImpact == null) || (nID >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (nID <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            LogManager.LogAssert(0);
        }

        m_SpellEventQueue.Push(pImpact, nID);
    }
    public virtual void UnRegisterSpellEvent(Impact pImpact, SPELL_EVENT_ID id)
    {
        if ((pImpact == null) || (id >= SPELL_EVENT_ID.SPELL_EVENT_ID_NUMBER) || (id <= SPELL_EVENT_ID.SPELL_EVENT_ID_INVALID))
        {
            LogManager.LogAssert(0);
        }

        bool bRet = m_SpellEventQueue.Remove(pImpact, id);
        LogManager.LogAssert(bRet);
    }

    /// <summary>
    /// 被攻击时，触发闪避
    /// </summary>
    /// <param name="pCaster">攻击者</param>
    /// <param name="pSpellInfo"></param>
    public void OnDodge(ObjectCreature pCaster, SpellInfo pSpellInfo)
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pSpellInfo);
        //先考虑吸收
        X_GUID guid = pCaster.GetGuid();
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_DODGE);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnDodge(pCaster, pSpellInfo);
            }
        }

        pList = pCaster.m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_NOHITTARGET);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnNoHitTarget(pSpellInfo,this);
            }
        }

        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_Dodge, this);
        //LogManager.Log("#######################################################");
        //LogManager.Log("【" + pCaster.GetGameObject() + "】 对 【" + GetGameObject() + "】攻击时未命中！目标剩余血量：" + GetHP());
    }

    /// <summary>
    /// 暴击
    /// </summary>
    /// <param name="pTarget"></param>
    /// <param name="pSpellInfo"></param>
    /// <param name="bCritical"></param>
    public void OnCritical(ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)
    {
        LogManager.LogAssert(pTarget);
        LogManager.LogAssert(pSpellInfo);

        if (bCritical == false) return;

        //先考虑吸收
        X_GUID guid = pTarget.GetGuid();
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_CRITICAL);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                //技能暴击之后
                pImpact.OnSpellAfterCaculateCritical(pTarget, pSpellInfo, bCritical);
            }
        }
    }

    /// <summary>
    /// 被暴击
    /// </summary>
    /// <param name="pCaster"></param>
    /// <param name="pSpellInfo"></param>
    public void OnBeCritical(ObjectCreature pCaster, SpellInfo pSpellInfo, bool bCritical = false)
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pSpellInfo);

        if (bCritical == false) return;

        //先考虑吸收
        X_GUID guid = pCaster.GetGuid();
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BECRITICAL);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnBeCritical(pCaster, pSpellInfo);
            }
        }
    }

    public EM_IMPACT_RESULT RemoveImpact(int nImpactID)
    {
        if (nImpactID < 0)
            return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;

        for (int i = m_ImpactList.Count - 1; i >= 0; i--)
        {
            Impact pImpact = m_ImpactList[i];
            if (pImpact.GetImpactID() == nImpactID)
            {
                onImpactUpdate(pImpact, false);
                pImpact.OnDisappear();
                pImpact.CleanUp();
                m_ImpactList.RemoveAt(i);
                m_IdleImpactList.Insert(0, pImpact);
            }
        }

        return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;
    }
    public void AddPassvityImpact(int nImpactID, ObjectCreature pCaster, bool bCritical, int nSpellID, int nParam = -1)
    {
        if (nImpactID < 0)
            return;

        Impact pRet = null;
        if (m_IdleImpactList.Count > 0)
        {
            Impact it = m_IdleImpactList[0];
            pRet = it;
            m_IdleImpactList.RemoveAt(0);
        }
        else
        {
            pRet = new Impact();
        }
        if (pRet != null)
        {
            pRet.Init(nImpactID, pCaster, this, nSpellID);
            pRet.OnAddToObjectCard(pCaster);
            m_ImpactList.Add(pRet);
        }

    }
    public EM_IMPACT_RESULT AddImpact(int nImpactID, ObjectCreature pCaster, bool bCritical, int nSpellID, int nParam = -1)
    {
        if (nImpactID < 0)
            return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;

        BuffTemplate pImpactTableRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);

        LogManager.LogAssert(pImpactTableRow);
        System.Random ran = new System.Random();
        int nRand = ran.Next(1, 100);

        if (nRand < pImpactTableRow.getProbability())
        {
            bool bRet = false;
            List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_IMM_IMPACT);
            LogManager.LogAssert(pList);
            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    bRet = pImpact.OnReadyAddImpact(nImpactID);
                    if (bRet == false)
                    {
                        return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;
                    }
                }
            }

            foreach (var item in m_ImpactList)
            {
                Impact pImpact = item;
                if (pImpact.GetMutexID() != -1 && pImpactTableRow.getExclusionID() != -1)
                {
                    if (pImpact != null && pImpact.GetMutexID() == pImpactTableRow.getExclusionID())
                    {
                        if (pImpact.GetMutexLevel() < pImpactTableRow.getExclusionPriority())
                        {
                            return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;
                        }
                        else
                        {
                            onImpactUpdate(pImpact, false);
                            pImpact.OnDisappear();
                            pImpact.CleanUp();
                            m_ImpactList.Remove(item);
                            m_IdleImpactList.Add(pImpact);
                            break;
                        }
                    }
                }
            }

            Impact pRet = null;
            if (m_IdleImpactList.Count > 0)
            {
                Impact it = m_IdleImpactList[0];
                pRet = it;
                m_IdleImpactList.RemoveAt(0);
            }
            else
            {
                pRet = new Impact();
            }
            if (pRet != null)
            {
                pRet.Init(nImpactID, pCaster, this, nSpellID);
                List<Impact> _list = new List<Impact>();
                for (int i = 0; i < m_ImpactList.Count; i++)
                {
                    if (m_ImpactList[i].GetImpactID() == pRet.GetImpactID())
                    {
                        _list.Add(m_ImpactList[i]);
                    }
                }
                if (pRet.GetImpactRow().getMaxOverlayCount() != -1)
                {
                    if (_list.Count >= pRet.GetImpactRow().getMaxOverlayCount())
                    {
                        _list[0].OnDisappear();
                        onImpactUpdate(_list[0], false);
                        _list[0].CleanUp();
                        m_ImpactList.Remove(_list[0]);
                        m_IdleImpactList.Insert(0, _list[0]);
                    }
                }

                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_ADDIMPACT);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    SPELL_EVENT pAllocRet = item;
                    Impact pImpact = pAllocRet.m_pImpact;
                    LogManager.LogAssert(pImpact);
                    pImpact.OnAddImpact(pRet);
                }

                //
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BEADDIMPACT);
                LogManager.LogAssert(pList);

                foreach (var item in pList)
                {
                    SPELL_EVENT pAllocRet = item;
                    Impact pImpact = pAllocRet.m_pImpact;
                    LogManager.LogAssert(pImpact);
                    bRet = pImpact.OnWhenAddImpact(pCaster, nImpactID);
                    if (bRet == false)
                    {
                        return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;
                    }
                }

                pRet.OnAddToTarget(pCaster);
                m_ImpactList.Add(pRet);

                //音效逻辑
                AudioControler.Inst.PlaySound(pRet.GetImpactRow().getBuffSound());

                onImpactUpdate(pRet, true);

                //生成起始特效，循环特效 [3/12/2015 Zmy]
                if (string.IsNullOrEmpty(pImpactTableRow.getBuffEffectOp()) == false)
                {
                    Transform paran = this.GetGameObject().GetComponent<AnimationEventControler>().GetTransform(GetGameObject().transform, pImpactTableRow.getBuffEffectPosition());
                    EffectManager.GetInstance().InstanceEffect_Static(pImpactTableRow.getBuffEffectOp(), this, paran, 0f, true);
                }
                if (string.IsNullOrEmpty(pImpactTableRow.getBuffEffect()) == false)
                {
                    Transform paran = this.GetGameObject().GetComponent<AnimationEventControler>().GetTransform(GetGameObject().transform, pImpactTableRow.getBuffEffectPosition());
                    EffectManager.GetInstance().InstanceEffect_Static(pImpactTableRow.getBuffEffect(), this, paran, pImpactTableRow.getDurationTime() / 1000f, true);
                }
            }


            return EM_IMPACT_RESULT.EM_IMPACT_RESULT_NORMAL;
        }

        return EM_IMPACT_RESULT.EM_IMPACT_RESULT_FAIL;
    }

    public void GetAttackEffectHurt(int nHurtType, ObjectCreature pTarget, ref int nPermil, ref int nHurtPoint)
    {
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTATTACK_HURT);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                nPermil = nPermil + pImpact.GeneratorHurtPermilWhenAttack();
            }
        }

        List<SPELL_EVENT> nList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_CHANGEHURTEFFECT);
        LogManager.LogAssert(nList);
        foreach (var item in nList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                nPermil = nPermil + pImpact.ChangeTargetHurtPermilEffect(nHurtType, pTarget, ref nHurtPoint);
            }
        }

    }
    public virtual void GetBeAttackEffectHurt(int nHurtType, ref int nHurtPoint, ref int nPermil)
    {
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_EFFECTBEATTACK_HURT);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                nPermil = nPermil + pImpact.GeneratorHurtPermilEffectBeAttack(nHurtType, ref nHurtPoint);
            }
        }

    }
    public List<Impact> GetImpactList() { return m_ImpactList; }

    public virtual long GetHPPercent() { return (100 * GetHP()) / (1 * GetMaxHP()); }

    public bool IsAlive()
    {
        if (0 >= GetHP())
        {
            return false;
        }
        return true;
    }

    public virtual EM_OBJECT_TYPE GetGroupType() { return m_GroupType; }
    public virtual void SetGroupType(EM_OBJECT_TYPE nType)
    {
        if (nType <= EM_OBJECT_TYPE.EM_OBJECT_TYPE_INVALID || nType >= EM_OBJECT_TYPE.EM_OBJECT_TYPE_NUMBER)
            return;

        m_GroupType = nType;
    }
    //获得debuf数量
    public virtual int GetImpactCountByType(int nType)
    {
        if (nType == -1)
            return 0;

        int temp = 0;
        for (int i = 0; i < m_ImpactList.Count; i++)
        {
            Impact nImpact = m_ImpactList[i];
            BuffTemplate buff = nImpact.GetImpactRow();
            if (nType == 3)
            {
                if (buff.getConduce() == 0 || buff.getConduce() == 1)
                {
                    temp++;
                }
            }
            else if (nType == 4)
            {
                if (buff.getConduce() == 0 || buff.getConduce() == 1 || buff.getConduce() == 2)
                {
                    temp++;
                }
            }
            else
            {
                if (buff.getConduce() == nType)
                {
                    temp++;
                }
            }
        }
        return temp;

    }
    public virtual int GetImpactCountByID(int nType)
    {
        int nCount = 0;
        for (int i = 0; i < m_ImpactList.Count; i++)
        {
            Impact pData = m_ImpactList[i];
            if (pData != null && pData.GetImpactID() == nType)
            {
                nCount++;
            }
        }
        return nCount;
    }

    //受到直接伤害
    public virtual void OnDamage(int nHurt, bool bCaculateDistribute = false)
    {
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnAbsorbHurt((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, ref nHurt, null);
                if (nHurt <= 0) break;

            }
        }

        if (nHurt > 0)
        {
            if (bCaculateDistribute)
            {
                //伤害分配
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURTDISTRIBUTE);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.HurtDistribute(ref nHurt);
                    }

                }

                if (nHurt <= 0)
                {
                    nHurt = 1;
                }
            }

            //伤害延迟;
            pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURTORHEALDELAY);
            if (pList != null)
            {
                for (int i = 0; i < pList.Count; i++)
                {
                    Impact pImpact = pList[i].m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnDelayHurtorHeal(true, ref nHurt, null);
                    }
                }
            }

            long nCurHP = GetHP();
            if (nHurt >= nCurHP)
            {
                nHurt = (int)nCurHP;
                this.OnDie();
            }
            else
            {
                SetHP(nCurHP - nHurt);
            }
            if (this is ObjectMonster && ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)
            {
                SceneObjectManager.GetInstance().WorldBossDamageSum += nHurt;
            }
            UI_HurtInfo pData = new UI_HurtInfo();
            pData.pTarget = this;
            pData.nHurt = -nHurt;
            pData.bCritical = false;
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_UI_ChangeHP, pData);

            //受击怒气奖励 [10/17/2015 Zmy]
            AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(GetFuryIdForTable());
            FightControler.Inst.OnUpdatePowerValue(GetGroupType(), _data.getGethitFury());

            float nNum = ((float)nHurt / (float)GetMaxHP()) * 100 * _data.getHPTransformFury();

            if (GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)//生命流失额外怒气增加
            {
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
            }
           
            FightControler.Inst.OnUpdatePowerValue(GetGroupType(), (int)nNum);

            if (GetActionState() == ObjectCreature.ObjectActionState.dizzy) // 在眩晕动作持续播放时 受到攻击播放受击动作[10/19/2015 Zmy]
            {
                if (m_AnimControl.LastAnim.Equals("Hurt1") == false)
                {
                    SetObjectActionState(ObjectCreature.ObjectActionState.Hurting);
                    return;
                }
            }
        }

        // 效果持续时指定ID的技能造成的伤害提升（实际伤害=原伤害*（1+X/1000））


        //结算后出发逻辑
        long nMaxHp = GetMaxHP();
        float fmill = ((float)nHurt / (float)nMaxHp) * 1000;
        pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnAfterDamage((int)ENUM_HURT_TYPE.HURT_TYPE_DIRECT, nHurt, fmill, null);
            }
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="pCaster"></param>
    /// <param name="pSpellInfo"></param>
    /// <param name="nDamageType"></param>
    /// <param name="nHurt"></param>
    public virtual void OnDamage(ObjectCreature pCaster, SpellInfo pSpellInfo, int nDamageType, ref int nHurt)
    {
        LogManager.LogAssert(pCaster);
        LogManager.LogAssert(pSpellInfo);
        if (nHurt > 0)
        {
            if (!IsAlive())
            {
                return;
            }

            OnBeforeDamageLogic(pCaster, pSpellInfo, ref nHurt);

            //先考虑吸收
            X_GUID guid = pCaster.GetGuid();

            List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_ABSORB_HURT);
            if (pList != null)
            {
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnAbsorbHurt(nDamageType, ref nHurt, pCaster);
                        if (nHurt <= 0) { break; }
                    }
                }
            }

            pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERABSORB_HURT);
            if (pList != null)
            {
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnDamageAfter(pSpellInfo, ref nHurt);
                        if (nHurt <= 0) { break; }
                    }
                }
            }

            if (nHurt > 0)
            {
                //伤害分配
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURTDISTRIBUTE);
                if (pList != null)
                {
                    foreach (var item in pList)
                    {
                        Impact pImpact = item.m_pImpact;
                        if (pImpact != null)
                        {
                            pImpact.HurtDistribute(ref nHurt);
                        }
                    }
                }

                //伤害延迟;
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURTORHEALDELAY);
                if (pList != null)
                {
                    for (int i = 0; i < pList.Count; i++)
                    {
                        Impact pImpact = pList[i].m_pImpact;
                        if (pImpact != null)
                        {
                            pImpact.OnDelayHurtorHeal(true, ref nHurt, pCaster);
                        }
                    }
                }

                long nCurHP = GetHP();
                if (nHurt >= nCurHP)
                {
                    nHurt = (int)nCurHP;
                    SetHP(0);
                    //反弹
                    pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
                    LogManager.LogAssert(pList);
                    foreach (var item in pList)
                    {
                        Impact pImpact = item.m_pImpact;
                        if (pImpact != null)
                        {
                            pImpact.OnHurtBack(nDamageType, nHurt, pCaster);
                        }
                    }

                    pCaster.OnKillTarget(this, pSpellInfo);
                    this.OnBeKilled(pCaster, pSpellInfo);

                    return;
                }
                else
                {
                    SetHP(nCurHP - nHurt);
                }

                //结算后出发逻辑
                long nMaxHp = GetMaxHP();
                float fmill = ((float)nHurt / (float)nMaxHp) * 1000;
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnAfterDamage(nDamageType, nHurt, fmill, pCaster);
                    }
                }

                //反弹
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURT_BACK);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnHurtBack(nDamageType, nHurt, pCaster);
                    }
                }

                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_SELFHPCHANGE);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnSelfHpChange();
                    }
                }


                OnAfterDamageLogic(pCaster, pSpellInfo, nHurt);
            }
            else
            {
                //结算后触发逻辑
                long nMaxHp = GetMaxHP();
                float fmill = ((float)nHurt / (float)nMaxHp) * 1000;
                pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
                LogManager.LogAssert(pList);
                foreach (var item in pList)
                {
                    Impact pImpact = item.m_pImpact;
                    if (pImpact != null)
                    {
                        pImpact.OnAfterDamage(nDamageType, nHurt, fmill, pCaster);
                    }
                }
            }
        }
    }

    //造成伤害之后逻辑处理 [7/21/2015 Zmy]
    private void OnAfterDamageLogic(ObjectCreature pCaster, SpellInfo pSpellInfo, int nHurt)
    {
        int nSuckValue = 0;
        if (pSpellInfo.GetSpellRow().getSkillNo() == 0)// 普攻 [7/21/2015 Zmy]
        {
            nSuckValue = (int)(nHurt * pCaster.GetNormalSuckRate());
        }
        else
        {
            nSuckValue = (int)(nHurt * pCaster.GetSpellSuckRate());
        }

        if (nSuckValue > 0)
        {
            pCaster.OnHeal(nSuckValue);
        }
    }
    //造成伤害之前逻辑处理 [7/21/2015 Zmy]
    private void OnBeforeDamageLogic(ObjectCreature pCaster, SpellInfo pSpellInfo, ref int nHurt)
    {
        if (ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter)// 传说之战伤害计算修正 [7/21/2015 Zmy]
        {
            ObjectSelf.GetInstance().WorldBossMgr.OnCalcDamage(pCaster, ref nHurt);
        }
    }
    /// <summary>
    /// //死亡逻辑
    /// </summary>
    public virtual void OnDie()
    {

        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BEFORE_DEAD);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnOwnerDead();
            }
        }


        for (int i = m_ImpactList.Count - 1; i >= 0; i--)
        {
            Impact pImpact = m_ImpactList[i];
            if (pImpact != null)
            {
                onImpactUpdate(pImpact, false);
                pImpact.OnDisappear();
                pImpact.CleanUp();
                m_ImpactList.RemoveAt(i);
                m_IdleImpactList.Insert(0, pImpact);
            }
        }


    }
    /// <summary>
    /// 被治疗
    /// </summary>
    /// <param name="nHeal"></param>
    public virtual void OnHeal(int nHeal, SpellInfo pInfo = null)
    {
        if (nHeal > 0)
        {
            long nCurHP = GetHP();
            long nMaxHP = GetMaxHP();
            if (nCurHP + nHeal >= nMaxHP)
            {
                SetHP(nMaxHP);
            }
            else
            {
                SetHP(nCurHP + nHeal);
            }

            List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BEHEAL);
            LogManager.LogAssert(pList);

            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    pImpact.OnBeHeal();
                }
            }

            pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_AFTERDAMAGE);
            LogManager.LogAssert(pList);

            foreach (var item in pList)
            {
                Impact nImpact = item.m_pImpact;
                if (nImpact != null)
                {
                    nImpact.OnBeHeal(nHeal);
                }
            }

            pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_SELFHPCHANGE);
            LogManager.LogAssert(pList);
            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    pImpact.OnSelfHpChange();
                }
            }

        }
    }
    /// <summary>
    ///   增加impact
    /// </summary>
    public virtual Impact AddImpactDirectly(int nImpactID, ObjectCreature pCaster, bool bCritical, int nSpellID, int nParam = -1)
    {
        BuffTemplate pImpactTableRow = (BuffTemplate)DataTemplate.GetInstance().m_BufferTable.getTableData(nImpactID);
        LogManager.LogAssert(pImpactTableRow);

        Impact pRet = new Impact();

        if (m_IdleImpactList.Count > 0)
        {
            Impact it = m_IdleImpactList[0];
            pRet = it;
            m_IdleImpactList.RemoveAt(0);
        }
        else
        {
            pRet = new Impact();
        }

        if (pRet != null)
        {
            pRet.Init(nImpactID, pCaster, this, nSpellID);
            m_ImpactList.Add(pRet);
            onImpactUpdate(pRet, true);
        }

        //音效代码 添加Buff音效
        AudioControler.Inst.PlaySound(pRet.GetImpactRow().getBuffSound());

        return pRet;

    }
    /// <summary>
    ///   通过释放者id及impact id找buf
    /// </summary>
    public virtual Impact GetImpactByIDAndCastGuid(int nID, X_GUID guid)
    {
        Impact pImpact = null;
        foreach (var item in m_ImpactList)
        {
            pImpact = item;
            if (pImpact != null && pImpact.GetImpactID() == nID && guid == pImpact.GetCaster())
            {
                return pImpact;
            }
        }

        return pImpact;
    }
    /// <summary>
    ///   被杀死
    /// </summary>
    public virtual void OnBeKilled(ObjectCreature pCaster, SpellInfo pSpellInfo)
    {
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_BEKILL);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnBeKillTarget(pCaster, pSpellInfo);
            }
        }
        OnDie();
    }

    /// <summary>
    ///   技能杀死目标
    /// </summary>
    public virtual void OnKillTarget(ObjectCreature pTarget, SpellInfo pSpellInfo)
    {
        LogManager.LogAssert(pTarget);
        LogManager.LogAssert(pSpellInfo);
        //怒气值
        int nConstValue = DataTemplate.GetInstance().m_GameConfig.getKill_rp_inc();

        X_GUID guid = pTarget.GetGuid();
        List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_KILLTARGET);
        LogManager.LogAssert(pList);
        foreach (var item in pList)
        {
            Impact pImpact = item.m_pImpact;
            if (pImpact != null)
            {
                pImpact.OnKillTarget(pTarget, pSpellInfo,ref nConstValue);
            }
        }

        //击杀目标奖励怒气 [3/2/2015 Zmy]
        //if (FightControler.Inst != null)
        //    FightControler.Inst.OnUpdatePowerValue(GetGroupType(), pSpellInfo.GetSpellRow().getKillFury() + nConstValue);

        AngertableTemplate _data = (AngertableTemplate)DataTemplate.GetInstance().m_AngerTable.getTableData(GetFuryIdForTable());
        int nValue = _data.getKillFury();
        if (GetGroupType() == EM_OBJECT_TYPE.EM_OBJECT_TYPE_HERO)
        {
            for (int n = 0; n < GetHeroData().HeroCabalaDB.CabalaList.Count; ++n)
            {
                int _tableID = GetHeroData().HeroCabalaDB.CabalaList[n].TableID;
                MsTemplate _row = (MsTemplate)DataTemplate.GetInstance().m_MsTable.getTableData(_tableID);
                if (_row.getMstype() == 4)//击杀额外怒气增加
                {
                    int nLev = GetHeroData().HeroCabalaDB.CabalaList[n].IntensifyLev;
                    if (nLev <= 0)
                        continue;
                    nValue += _row.getValue()[nLev - 1];
                }
            }
        }
        FightControler.Inst.OnUpdatePowerValue(GetGroupType(), nValue);
    }
    /// <summary>
    ///对目标产生伤害
    /// </summary>
    public virtual void OnHurt(int nDamageType, int nHurt, ObjectCreature pTarget, SpellInfo pSpellInfo, bool bCritical)
    {
        LogManager.LogAssert(pTarget);
        if (nHurt > 0)
        {
            if (!IsAlive())
            {
                return;
            }
            List<SPELL_EVENT> pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HITTARGET);
            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    pImpact.OnHitTarget(pSpellInfo, pTarget);
                }
            }

            pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_HURT);
            LogManager.LogAssert(pList);
            foreach (var item in pList)
            {
                Impact pImpact = item.m_pImpact;
                if (pImpact != null)
                {
                    pImpact.OnHurt(nDamageType, ref nHurt, pTarget, pSpellInfo);
                }
            }
            pTarget.OnBeHurt(nHurt, pSpellInfo, bCritical);
            pTarget.OnBeBreak(nHurt, pSpellInfo);
            //LogManager.Log("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            //LogManager.Log("【"+ GetGameObject() + "】 对 【" + pTarget.GetGameObject() + "】造成 " + nHurt + " 点伤害！目标剩余血量：" + pTarget.GetHP() );
        }
    }
    //被伤害，主要在子类中抛出事件用 [1/30/2015 Zmy]
    public virtual void OnBeHurt(int nHurt, SpellInfo _BeSpellInfo, bool bCritical)
    {

    }
    public virtual void OnBeBreak(int nHurt, SpellInfo _BeSpellInfo)
    {

    }
    //启动普攻技能逻辑 [3/5/2015 Zmy]
    public virtual void OnCommonSkillActiveOnce() { }
    // 启动技能逻辑 [3/5/2015 Zmy]
    public virtual void OnSpecialSkillActiveOnce() { }

    //攻击状态 [1/22/2015 Zmy]
    public virtual void OnAttackStateLogicUpdate()
    {
        switch (m_CurActionState)
        {
            case ObjectActionState.idle:
                break;
            case ObjectActionState.forward:
                break;
            case ObjectActionState.scanning:
                break;
            case ObjectActionState.normalAttack:
                break;
            case ObjectActionState.skillAttack:
                break;
            default:
                break;
        }

        CoolDownHeartBeat((uint)(Time.deltaTime * 1000f));
        ImpactListHeartBeat((int)(Time.deltaTime * 1000f));
    }

    public virtual void InitBaseData()
    {
        for (int i = (int)EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP; i < (int)EM_ATTRIBUTE.EM_ATTRIBUTE_NUMBER; i++)
        {
            SetBitFlag((EM_ATTRIBUTE)i);
        }
    }

    public virtual void ImpactListHeartBeat(int nTime)
    {
        for (int i = 0; i < m_ImpactList.Count; ++i)
        {
            Impact pImpact = m_ImpactList[i];
            if (!pImpact.IsActive())
            {
                onImpactUpdate(pImpact, false);
                pImpact.OnDisappear();
                pImpact.CleanUp();
                m_ImpactList.RemoveAt(i);
                m_IdleImpactList.Insert(0, pImpact);
                //break;
            }
            else
            {
                pImpact.HeartBeat(nTime);
            }


        }
    }
    // 清除身上的所有debuff [3/11/2015 Zmy]
    public virtual void OnClearUpdateImpact()
    {
        for (int i = m_ImpactList.Count - 1; i >= 0; i--)
        {
            Impact pImpact = m_ImpactList[i];
            if (pImpact != null)
            {
                if (pImpact.GetImpactRow().getConduce() == 0)
                {
                    onImpactUpdate(pImpact, false);
                    pImpact.OnDisappear();
                    pImpact.CleanUp();
                    m_ImpactList.RemoveAt(i);
                    m_IdleImpactList.Insert(0, pImpact);
                }
            }
        }
    }
    //清除所有制作者死亡后的buff [3/13/2015 Zmy]
    public virtual void OnClearImpactOfMakerDeath(X_GUID deathObj_guid)
    {
        for (int i = m_ImpactList.Count - 1; i >= 0; i--)
        {
            Impact pImpact = m_ImpactList[i];
            if (pImpact != null && pImpact.GetCaster() != null && pImpact.GetCaster().Equals(deathObj_guid))
            {
                if (pImpact.GetImpactRow().getMakerDeath() == 1)
                {
                    //音效代码
                    AudioControler.Inst.StopSound();

                    onImpactUpdate(pImpact, false);
                    pImpact.OnDisappear();
                    pImpact.CleanUp();
                    m_ImpactList.RemoveAt(i);
                    m_IdleImpactList.Insert(0, pImpact);
                }
            }
        }
    }

    /// <summary>
    /// 当buff表更新时发送事件
    /// </summary>
    private void onImpactUpdate(Impact pImpact, bool isAdd)
    {
        BuffUpdatePackage package = new BuffUpdatePackage();
        package.creature = this;
        package.pImpact = pImpact;
        package.isAdd = isAdd;
        GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_BuffEvent_ShowUI, package);
        package = null;
    }


    //技能动作逻辑 [3/4/2015 Zmy]
    protected void OnSkillActionLogic(SpellInfo spellInfo)
    {
        if (spellInfo == null)
            return;
        switch (spellInfo.GetSpellRow().getSkillReleaseType())
        {
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI1:
                m_AnimControl.Anim_Skill(spellInfo.GetSpellRow().getAction());
                break;
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_IMMIDI2:
                m_AnimControl.Anim_Skill(spellInfo.GetSpellRow().getAction());
                break;
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_MULTISECTION:
                m_AnimControl.Anim_Skill(spellInfo.GetSpellRow().getAction());
                break;
            case (int)EM_SPELL_CASTING_TYPE.EM_SPELL_CASTING_TYPE_CHANNEL:
                OnGuidancesSkillLogicEnd();
                m_AnimControl.Anim_GuidanceSkill(spellInfo.GetSpellRow().getAction()[0]);
                break;
            default:
                break;
        }
        spellInfo.Init(spellInfo.GetSpellID());
        SetSpell(spellInfo);//标记本次释放的技能信息 [8/20/2015 Zmy]
        m_LastSpellID = spellInfo.GetSpellID();
        //刷新技能数据flag位
        m_SkillTypeFlag = ENUM_SPELL_TYPE_FLAG.SPELL_NONE;
        //记录本次释放技能的时间点
        m_LastSkillTimestamp = SceneObjectManager.GetInstance().TimeInBattleScene;


        //         pList = m_SpellEventQueue.GetEventList(SPELL_EVENT_ID.SPELL_EVENT_ID_SPELLCONUMEMP);
        //         LogManager.LogAssert(pList);
        //         foreach (var item in pList)
        //         {
        //             Impact pImpact = item.m_pImpact;
        //             if (pImpact != null)
        //             {
        //                 pImpact.OnSpellBeforeActive(false);
        //             }
        //         }

        SetActivationSpellCD(spellInfo);
    }

    //引导技点能逻辑结束清除计时数据
    protected void OnGuidancesSkillLogicEnd()
    {
        m_MaxGuidanceTime = 0;
        m_GuidanceTime = 0;
        m_count = 1;
    }

    public int[] GetBuffID()
    {
        int[] _idArr = new int[m_ImpactList.Count];

        for (int i = 0; i < _idArr.Length; i++)
        {
            _idArr[i] = m_ImpactList[i].GetImpactID();

        }
        return _idArr;
    }
}