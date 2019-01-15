using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using GNET;
using DreamFaction.GameSceneEditor;
using DreamFaction.Utils;
using DreamFaction.LogSystem;

//用于排行榜显示的其他玩家信息 [6/16/2015 Zmy]
public class OtherHeroInfo 
{
    public int m_HeroId;           // 英雄配表ID
    public int m_Exp;              // 当前经验
    public int m_HeroLevel;        // 英雄等级
    public int m_Hp;               // 血量
    public int m_PysicalAttack;    // 物理攻击
    public int m_Physicaldefence;  // 物理防御
    public int m_MagicAttack;      // 魔法攻击
    public int m_MagicDefence;     // 魔法防御
    public int m_Skill1;           // 技能1编号（未开通为0）
    public int m_Skill2;           // 技能2编号（未开通为0）
    public int m_Skill3;           // 技能3编号（未开通为0）
    public int m_HeroViewId;       // 英雄外观

    public void Copy(GNET.OtherHero _data)
    {
        this.m_HeroId               = _data.heroid;
        this.m_Exp                  = _data.exp;
        this.m_HeroLevel            = _data.herolevel;
        this.m_Hp                   = _data.hp;
        this.m_PysicalAttack        = _data.physicalattack;
        this.m_Physicaldefence      = _data.physicaldefence;
        this.m_MagicAttack          = _data.magicattack;
        this.m_MagicDefence         = _data.magicdefence;
        this.m_Skill1               = _data.skill1;
        this.m_Skill2               = _data.skill2;
        this.m_Skill3               = _data.skill3;
        this.m_HeroViewId           = _data.heroviewid;
    }
}

//排行榜元素数据 [6/16/2015 Zmy]
public class LimitRankInfo 
{
    public long         m_RoleId;           // 玩家guid
    public string       m_RoleName;         // 玩家名称
    public int          m_Level;            // 玩家等级
    public int          m_GroupNum;         // 第几轮
    public int          m_TroopType;        // 战队类型
    public int          m_AlldropNum;       // 勇者证明总数量
    public int          m_OnRankNum;        // 连续在榜次数
    public Dictionary<int, OtherHeroInfo> m_HeroAttribute = new Dictionary<int, OtherHeroInfo>(); // 使用英雄的位置和属性信息（key为位置，value为OtherHero属性信息）

    public void Copy(EndlessRankInfo _data)
    {
        this.m_RoleId       = _data.roleid;
        this.m_RoleName     = _data.rolename;
        this.m_Level        = _data.level;
        this.m_GroupNum     = _data.groupnum;
        this.m_TroopType    = _data.trooptype;
        this.m_AlldropNum   = _data.alldropnum;
        this.m_OnRankNum    = _data.onranknum;

        foreach (DictionaryEntry kvp in _data.heroattribute)
        {
            OtherHeroInfo _other = new OtherHeroInfo();
            _other.Copy(kvp.Value as OtherHero);

            m_HeroAttribute.Add((int)kvp.Key, _other);
        }
    }
}

public class LimitFightManager
{
    private bool        m_bActivate;            //是否可进入。标示当天是否已经进行过极限试炼
    public  bool        m_bStartEnter;          //是否已进入。标示已进入极限试炼的状态标记
    public  bool        m_bRuntime;             //是否进行中。用于启动游戏进入主场景时的逻辑条件验证是否直接进入极限试炼 [6/16/2015 Zmy]

    public  int         m_TroopType;            //战队类型
    public  int         m_MonsterTroopType;     //怪物阵型类型
    public  int         m_RoundNum;             //当前回合
    public  int         m_BeginRoundNum;        //初始回合数，进入场景后赋值一次;
    public  int         m_Pact;                 //强者之约关数
    public  int         m_PactIspass;           // 强者之约是否达成（0为未达成，1为达成）
    public  int         m_IsHalfCostPact;       //本次购买是否半价（0是本次全价，1是半价）
    public  int         m_TodayRanking;         //预测今日排名（-1未排名，1~20为具体排名，20以上为20名之外）

    public  Hashtable   m_DropMap = new Hashtable();// 掉落收益（key为物品或资源ID，value为数量）
    public  X_GUID[]    m_HeroInfo = new X_GUID[GlobalMembers.MAX_TEAM_CELL_COUNT];
    public  int[]       m_HeroHp   = new int[GlobalMembers.MAX_TEAM_CELL_COUNT];

    public int          m_DropNum;              //剩余勇者证明数量（战斗中显示）
    public int          m_AllDropNum;           //获得的勇者证明总数（试炼外显示）
    public int          m_AttrAdd1;             //怒气提升购买次数
    public int          m_AttrAdd2;             //攻击属性提升购买次数
    public int          m_AttrAdd3;             //防御属性提升购买次数
    public int          m_AttrAdd4;             //回复血量购买次数



    public List<LimitRankInfo> m_RankFirstStage     = new List<LimitRankInfo>(); //排行榜第一阶段 1-50级玩家信息 [6/16/2015 Zmy]
    public List<LimitRankInfo> m_RankSecondStage    = new List<LimitRankInfo>(); //排行榜第二阶段 51-100级玩家信息 [6/16/2015 Zmy] 
    public List<LimitRankInfo> m_RankLastStage      = new List<LimitRankInfo>(); //排行榜第三阶段 101级以后玩家信息 [6/16/2015 Zmy]
    public bool Activate
    {
        get
        {
            return m_bActivate;
        }
        set
        {
            m_bActivate = value;
        }
    }

    public LimitFightManager()
    {
        m_bActivate = true;
        m_bStartEnter = false;
        m_bRuntime = false;
        for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT;++i )
        {
           if (m_HeroInfo[i] == null)
           {
               m_HeroInfo[i] = new X_GUID();
           }
           m_HeroInfo[i].CleanUp();
           m_HeroHp[i] = -1;//初始值。代表英雄今日没有进入过试炼。进入之后血量为最大值 [6/19/2015 Zmy]
        }
        m_Pact = -1;
        m_IsHalfCostPact = 0;
        m_TodayRanking = -1;
        
        m_DropNum = 0;
        m_AllDropNum = 0;
        m_AttrAdd1 = 0;
        m_AttrAdd2 = 0;          
        m_AttrAdd3 = 0;            
        m_AttrAdd4 = 0;
    }
    public void InitHeroObject()
    {

    }

    public void InitMonsterData()
    {

    }

    public void UpdateData()
    {
        if (m_bActivate == false)
            return;

    }


    public void SendRoundOver()
    {
        CEndlessPass packet = new CEndlessPass();
        List<FightInfo> _InfoList = SceneObjectManager.GetInstance().GetFightInfoList();
        for (int i = 0; i < _InfoList.Count; i++ )
        {
            
            bool isAdd = false;
            if (i + 30 > _InfoList.Count || i % 10 == 0)
                isAdd = true;
            else
                isAdd = IsAdd(_InfoList[i]);
            
            if (!isAdd)
                continue;
            

            GNET.fightInfo _info = new GNET.fightInfo();
            GameUtils.CopyFightInfo(ref _info, _InfoList[i]);
            packet.fightinfolist.AddLast(_info);
        }
        IOControler.GetInstance().SendProtocol(packet);
    }
    private bool IsAdd(FightInfo _DstInfo) 
    {
        for (int i = _DstInfo.m_DefenceInfo.Length - 1; i >= 0; i--)
        {
            if (_DstInfo.m_DefenceInfo[i].m_Defencer == 0)
                continue;

            if (_DstInfo.m_DefenceInfo[i].m_RemainHP == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void RoundOverProcess(endlessAttr data,bool _initEnter = true)
    {
        this.m_DropNum      = data.dropnum;
        this.m_AllDropNum   = data.alldropnum;
        this.m_AttrAdd1     = data.add1;
        this.m_AttrAdd2     = data.add2;
        this.m_AttrAdd3     = data.add3;
        this.m_AttrAdd4     = data.add4;

        //// 使用英雄的血量（key为位置，value为血量）
        //初次进入的时候，英雄血量的记录hash为空。 [6/19/2015 Zmy]
        foreach (DictionaryEntry item in data.herobloodlist)
        {
            int nIndex = (int)item.Key;
            if (nIndex >= 1 && nIndex <= GlobalMembers.MAX_TEAM_CELL_COUNT)
            {
                m_HeroHp[nIndex - 1] = (int)item.Value;
            }
        }

        if (_initEnter == true)//初次进入的时候。不用操作战斗控制器 [6/19/2015 Zmy]
            return;
        
        
        FightEditorContrler.GetInstantiate().CamPause();
        FightControler.Inst.SetFightState(FightState.HeroMove);
    }

    //提升怒气效果百分比
    public int OnHeroAngerUp(int nCurValue)
    {
        float _value = DataTemplate.GetInstance().m_GameConfig.getUltimatetrial_honestdiploma_num4();
        float _SUM = nCurValue * (m_AttrAdd1 * _value);
        return (int)_SUM;
    }

    // 极限试炼怪物属性计算 [6/4/2015 Zmy]
    // 公式：怪物属性=怪物基础属性*属性系数+玩家队伍英雄最高对应属性*附加系数
    public int GetLimitMonsterAttribute(EM_ATTRIBUTE _type,float _curValue)
    {
        float nSum = 0;
        int nCurRound = m_RoundNum;
        UltimatetrialmonsterTemplate _row = (UltimatetrialmonsterTemplate)DataTemplate.GetInstance().m_UltimatetrialmonsterTable.getTableData(nCurRound);
        switch (_type)
        {
            case EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP:
                nSum = _curValue * _row.getMaxHPCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP) * _row.getAdditionalMaxHP();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK:
                nSum = _curValue * _row.getPhysicalAttackCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK) * _row.getAdditionalPhysicalAttack();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK:
                nSum = _curValue * _row.getMagicAttackCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK) * _row.getAdditionalMagicAttack();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE:
                nSum = _curValue * _row.getPhysicalDefenceCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE) * _row.getAdditionalPhysicalDefence();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE:
                nSum = _curValue * _row.getMagicDefenceCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE) * _row.getAdditionalMagicDefence();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_HIT:
                nSum = _curValue * _row.getHitCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT) * _row.getAdditionalHit();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE:
                nSum = _curValue * _row.getDodgeCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE) * _row.getAdditionalDodge();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL:
                nSum = _curValue * _row.getCriticalCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL) * _row.getAdditionalCritical();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY:
                nSum = _curValue * _row.getTenacityCoefficient() + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY) * _row.getAdditionalTenacity();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED) * _row.getAdditionalSpeed();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_HIT_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_HIT_RATE) * _row.getAdditionalBaseHit();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE_RATE) * _row.getAdditionalBaseDodge();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_RATE) * _row.getAdditionalBaseCritical();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY_RATE) * _row.getAdditionalBaseTenacity();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HURT_ADD_PERMIL:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HURT_ADD_PERMIL) * _row.getAdditionalBasePhyDamageIncrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HRUT_REDUCE_PERMIL:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HRUT_REDUCE_PERMIL) * _row.getAdditionalBasePhyDamageDecrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_ADD_PERMIL:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_ADD_PERMIL) * _row.getAdditionalBaseMagDamageIncrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_REDUCE_PERMIL:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_REDUCE_PERMIL) * _row.getAdditionalBaseMagDamageDecrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_ADD_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_ADD_RATE) * _row.getAdditionalBaseCriticalDamage();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_REDUCE_RATE:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_REDUCE_RATE) * _row.getAdditionalBaseCriticalDamage();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_EXTRA_HURT:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_EXTRA_HURT) * _row.getAdditionalDamageIncrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_REDUCE_HURT_POINT:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_REDUCE_HURT_POINT) * _row.getAdditionalDamageDecrease();
                break;
            case EM_ATTRIBUTE.EM_ATTRIBUTE_HPRECOVER:
                nSum = _curValue /** _row.getPhysicalAttackCoefficient()*/ + GetHeroMaxAttributeValue(EM_ATTRIBUTE.EM_ATTRIBUTE_HPRECOVER) * _row.getAdditionallifeRestoringForce();
                break;
            default:
                break;
        }

        return (int)nSum;
    }

    // 英雄最大属性值 [6/4/2015 Zmy]
    public float GetHeroMaxAttributeValue(EM_ATTRIBUTE _type)
    {
        float[] _attriValue = new float[GlobalMembers.MAX_TEAM_CELL_COUNT];
        for (int i = 0; i < GlobalMembers.MAX_TEAM_CELL_COUNT;i++ )
        {
            ObjectCard pHero = ObjectSelf.GetInstance().HeroContainerBag.FindHero(m_HeroInfo[i]);
            if (pHero != null)
            {
                switch (_type)
                {
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MAXHP:
                        _attriValue[i] = pHero.GetMaxHP();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALATTACK:
                        _attriValue[i] = pHero.GetPhysicalAttack();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICATTACK:
                        _attriValue[i] = pHero.GetMagicAttack();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICALDEFENCE:
                        _attriValue[i] = pHero.GetPhysicalDefence();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGICDEFENCE:
                        _attriValue[i] = pHero.GetMagicDefence();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_HIT:
                        _attriValue[i] = pHero.GetHit();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE:
                        _attriValue[i] = pHero.GetDodge();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL:
                        _attriValue[i] = pHero.GetCritical();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY:
                        _attriValue[i] = pHero.GetTenacity();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MOVESPEED:
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_SPEED:
                        _attriValue[i] = pHero.GetSpeed();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_HIT_RATE:
                        _attriValue[i] = pHero.GetHitRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_DODGE_RATE:
                        _attriValue[i] = pHero.GetDodgeRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_RATE:
                        _attriValue[i] = pHero.GetCriticalRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_TENACITY_RATE:
                        _attriValue[i] = pHero.GetTenacityRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HURT_ADD_PERMIL:
                        _attriValue[i] = pHero.GetPhysicalHurtAddPermil();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_PHYSICAL_HRUT_REDUCE_PERMIL:
                        _attriValue[i] = pHero.GetPhysicalHurtReducePermil();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_ADD_PERMIL:
                        _attriValue[i] = pHero.GetMagicHurtAddPermil();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_MAGIC_HURT_REDUCE_PERMIL:
                        _attriValue[i] = pHero.GetMagicHurtReducePermil();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_ADD_RATE:
                        _attriValue[i] = pHero.GetCriticalHurtAddRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_CRITICAL_HURT_REDUCE_RATE:
                        _attriValue[i] = pHero.GetCriticalHurtReduceRate();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_EXTRA_HURT:
                        _attriValue[i] = pHero.GetExtraHurt();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_REDUCE_HURT_POINT:
                        _attriValue[i] = pHero.GetReduceHurtPoint();
                        break;
                    case EM_ATTRIBUTE.EM_ATTRIBUTE_HPRECOVER:
                        _attriValue[i] = pHero.GetHpRecover();
                        break;
                    default:
                        break;
                }
            }
        }

        return GameUtils.MaxValue(_attriValue);
    }

    public void CopyRankInfo(LinkedList<EndlessRankInfo> _first,LinkedList<EndlessRankInfo> _second,LinkedList<EndlessRankInfo> _last)
    {
        m_RankFirstStage.Clear();
        foreach (EndlessRankInfo item in _first)
        {
            LimitRankInfo _info = new LimitRankInfo();
            _info.Copy(item);

            m_RankFirstStage.Add(_info);
        }
        m_RankSecondStage.Clear();
        foreach (EndlessRankInfo item in _second)
        {
            LimitRankInfo _info = new LimitRankInfo();
            _info.Copy(item);

            m_RankSecondStage.Add(_info);
        }
        m_RankLastStage.Clear();
        foreach (EndlessRankInfo item in _last)
        {
            LimitRankInfo _info = new LimitRankInfo();
            _info.Copy(item);

            m_RankLastStage.Add(_info);
        }
    }
}
