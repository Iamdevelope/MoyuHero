using UnityEngine;
using System.Collections;
using GNET;

//boss角色数据 [6/29/2015 Zmy]
public class BossRoleDB
{
    public  long            m_CurBossHp;            //当前boss血量
    public  long            m_BossMaxHp;            //BOSS最大血量上限
    public  long            m_AddupDamage;          //累计造成伤害值
    public  int             m_BlessNum;             //祝福次数
    public  int             m_EndTime;              //当前boss剩余结束时间 秒
    public  int             m_BattleCoolDown;       //进入战斗冷却时间 秒

    public BossRoleDB()
    {
        ClearUp();
    }

    public void ClearUp()
    {
        m_CurBossHp = -1;
        m_BossMaxHp = -1;
        m_AddupDamage = 0;
        m_BlessNum = 0;
        m_EndTime = 0;
        m_BattleCoolDown = 0;
    }
    public void Copy(bossrole data)
    {
        this.m_CurBossHp             = data.bossnowhp;
        this.m_BossMaxHp             = data.bosshpall;
        this.m_AddupDamage           = data.killhpall;
        this.m_BlessNum              = data.zhufunum;
        this.m_EndTime               = data.openendtime;
        this.m_BattleCoolDown        = data.nextintime;
    }

    float _tmpEntTimeCount = 0f;
    float _tmpBattleTime = 0f;
    public void UpdateTime()
    {
        if (m_EndTime > 0)
        {
            _tmpEntTimeCount += Time.deltaTime;
            if (_tmpEntTimeCount > 1f)
            {
                m_EndTime--;
                _tmpEntTimeCount = 0;
            }
        }

        if (m_BattleCoolDown > 0)
        {
            _tmpBattleTime += Time.deltaTime;
            if (_tmpBattleTime > 1f)
            {
                m_BattleCoolDown--;
                _tmpBattleTime = 0;
            }
        }
    }
}
public class WorldBossData 
{
    public  int             m_BossTableID;          // 表ID [6/29/2015 Zmy]
    public  int             m_BossType;             // boss标示。1 2 3 4 分别标示第一守门 第一boss 第二守门人 第二boss [6/29/2015 Zmy]
	public  int             m_IsKilled;             // 是否已被击杀 0未击杀 1击杀 [6/29/2015 Zmy]
    public  string          m_KillName;             // 击杀者名称 [6/29/2015 Zmy]
    public  int             m_TimeCount;            // 剩余结束时间 [6/29/2015 Zmy]
    public  int             m_IsOpen;               // 是否已开启 [6/29/2015 Zmy]

    public  BossRoleDB      m_BossRoleDB = new BossRoleDB();
    public WorldBossData()
    {
        ClearUp();
    }
    
    public void ClearUp()
    {
        m_BossTableID = -1;
        m_BossType = -1;
        m_IsKilled = -1;
        m_KillName = string.Empty;
        m_TimeCount = -1;
        m_IsOpen = 0;
    }

    float _tmpTimeCount = 0;
    public void UpdateTime()
    {
        if (m_TimeCount > 0)
        {
            _tmpTimeCount += Time.deltaTime;
            if (_tmpTimeCount > 1f)
            {
                m_TimeCount--;
                _tmpTimeCount = 0;
            }
        }
        m_BossRoleDB.UpdateTime();
    }

    public void Copy(int nType, bossdata data)
    {
        this.m_BossType             = nType;
        switch (nType)
        {
            case (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_1://第一守门人
                this.m_BossTableID  = data.bossid1;
                this.m_IsOpen       = data.openboss == 1 ? 1 : 0;
                
                break;
            case (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_2://第一boss
                this.m_BossTableID  = data.bossid2;
                this.m_KillName     = data.boss1killname;
                this.m_IsOpen       = data.openboss == 2 ? 1 : 0;

                break;
            case (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_3://第二守门人
                this.m_BossTableID  = data.bossid3;
                this.m_IsOpen       = data.openboss == 3 ? 1 : 0;
                break;
            case (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_4://第二boss
                this.m_BossTableID  = data.bossid4;
                this.m_KillName     = data.boss2killname;
                this.m_IsOpen       = data.openboss == 4 ? 1 : 0;

                break;
            default:
                break;
        }

        this.m_TimeCount            = data.openendtime;
        this.m_IsKilled             = data.bossiskill % 10 == 1 ? 1 : 0;//只标示第一boss或者第二boss的击杀情况。守门人此值无效。 [6/29/2015 Zmy]
    }

    //是否是boss战 [7/21/2015 Zmy]
    public bool IsBossBattle()
    {
        return (m_BossType == 2 || m_BossType == 4) ? true : false;
    }
}
