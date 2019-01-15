using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DreamFaction.Utils;
using DreamFaction.GameCore;
using DreamFaction.LogSystem;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameSceneEditor;
using DreamFaction.LogSystem;
using GNET;

/// <summary>
/// 世界BOSS管理器
/// </summary>
public class WorldBossManager
{
    public  bool                        m_bStartEnter;          //是否已进入。标示已进入世界BOSS的状态标记
    public  int                         m_ShouWangZL;           //守望之灵 
    public  int                         m_ChuanShuoZS;          //传说之石
    public  int                         m_ShopExchangeNum;      //商店兑换次数
    public  int                         m_CurBossDataKey;       //当前进入战斗的bossdata的key
    public  long                        m_CurBossHP;            //当前进入战斗的boss的血量
    public  int                         m_MyRanking;            // 排名
    public  long                        m_MyTotalDamage;        // 伤害



    public List<BossRankInfo>             m_RankingList   = new List<BossRankInfo>();
    public Dictionary<int, WorldBossData> m_BossDataMap = new Dictionary<int, WorldBossData>();  //boss列表数据  key为 EM_WORLD_BOSS_TYPE [6/29/2015 Zmy]
    public Dictionary<int,int>            m_DropItemMap = new Dictionary<int,int>();             //掉落 key为物品或资源ID，value为数量 [6/30/2015 Zmy]
    public List<int>                      m_ShopList    = new List<int>();                       //商店列表

    public WorldBossManager()
    {
        WorldBossData _data1 = new WorldBossData();
        m_BossDataMap.Add((int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_1, _data1);

        WorldBossData _data2 = new WorldBossData();
        m_BossDataMap.Add((int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_2, _data2);

        WorldBossData _data3 = new WorldBossData();
        m_BossDataMap.Add((int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_3, _data3);

        WorldBossData _data4 = new WorldBossData();
        m_BossDataMap.Add((int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_4, _data4);

        

        m_bStartEnter = false;
        m_ShouWangZL = 0;
        m_ChuanShuoZS = 0;
        m_ShopExchangeNum = 0;
        m_CurBossDataKey = -1;
        m_CurBossHP = 0;
    }

    public void UpdateWorldBossData()
    {
        for (int nType = (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_1; nType < (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_NUM; nType++)
        {
            if (m_BossDataMap.ContainsKey(nType))
            {
                m_BossDataMap[nType].UpdateTime();
            }
        }
    }
    public void RefeashWorldBoss(bossdata data)
    {
        for (int nType = (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_TYPE_1; nType < (int)EM_WORLD_BOSS_TYPE.EM_WORLD_BOSS_NUM; nType++)
        {
            if (m_BossDataMap.ContainsKey(nType))
            {
                WorldBossData _info = m_BossDataMap[nType];
                _info.ClearUp();
                _info.Copy(nType,data);
            }
        }

        m_ShouWangZL    = data.shouwangzl;
        m_ChuanShuoZS   = data.chuanshuozs;
    }

    public void RefeashBossRole(int nBossType, bossrole data)
    {
        if (m_BossDataMap.ContainsKey(nBossType))
        {
            m_BossDataMap[nBossType].m_BossRoleDB.ClearUp();
            m_BossDataMap[nBossType].m_BossRoleDB.Copy(data);
        }
        else
        {
            LogManager.LogError("!!!!!Error:BossDataMap key is null" + nBossType);
        }
    }

    //BOSS战，英雄全部死亡 或者杀死boss时候发送 [6/29/2015 Zmy]
    public void SendRoundOver()
    {
        List<FightInfo> _InfoList = SceneObjectManager.GetInstance().GetFightInfoList();
        if (_InfoList.Count <= 0)
            return;

        CBossPass packet = new CBossPass();
        for (int i = 0; i < _InfoList.Count; i++)
        {

            bool isAdd = false;
            if (i + 30 > _InfoList.Count)
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

    public void OnCalcDamage(ObjectCreature pCaster,ref int nHurt)
    {
        if (m_BossDataMap.ContainsKey(m_CurBossDataKey) == false)
            return;

        if (m_BossDataMap[m_CurBossDataKey].IsBossBattle() == false)
            return;

        if (pCaster is ObjectMonster)
        {
            //boss造成的伤害修正 = 普通战斗伤害 * ( 1 - 玩家祝福层数 * 祝福受到伤害减免 )   [7/21/2015 Zmy]
            nHurt = (int)(nHurt * (1f - m_BossDataMap[m_CurBossDataKey].m_BossRoleDB.m_BlessNum * DataTemplate.GetInstance().m_GameConfig.getLegend_wish_hurt_down()));
        }
        else
        {
            //玩家造成的伤害修正 =普通战斗伤害 / ( 1 - 玩家祝福层数 * 祝福造成伤害加成 )  [7/21/2015 Zmy]
            nHurt = (int)(nHurt / (1f - m_BossDataMap[m_CurBossDataKey].m_BossRoleDB.m_BlessNum * DataTemplate.GetInstance().m_GameConfig.getLegend_wish_damage_up()));
        }
    }
}
