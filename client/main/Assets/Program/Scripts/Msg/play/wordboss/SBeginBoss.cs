using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;

namespace GNET
{
	public partial class SBeginBoss: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public bossBattleInfo battleinfo;

        public const int PROTOCOL_TYPE = 788888;

        public SBeginBoss()
            : base(PROTOCOL_TYPE)
		 {
             battleinfo = new bossBattleInfo();
		 } 

		public override object Clone()
		{
            SBeginBoss obj = new SBeginBoss();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(battleinfo);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            battleinfo.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
        {
          if (result == END_ERROR)
          {

          }
          else
          {
              CampaignMonsterGroupData pMonsterGroupData = new CampaignMonsterGroupData();
              byte nGroup = 0;
              int nIndex = 0;
              foreach (int value in battleinfo.monstergroup)
              {
                  if (nIndex >= GlobalMembers.MAX_MONSTER_GROUP_COUNT || nGroup >= GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP)
                      continue;
                  if (value != 0)
                  {
                      pMonsterGroupData.IDs[nGroup, nIndex] = value;
                      nIndex++;
                  }
                  else
                  {
                      //用0表示每波怪物的分界标示 [4/1/2015 Zmy]
                      nIndex = 0;//重设索引
                      nGroup++;
                  }
              }
              pMonsterGroupData.Count = ++nGroup; //总波数 
              ObjectSelf.GetInstance().OnCacheMonsterGroupData(pMonsterGroupData, battleinfo.battleid);
              ObjectSelf.GetInstance().WorldBossMgr.m_bStartEnter = true;
              ObjectSelf.GetInstance().WorldBossMgr.m_CurBossHP = battleinfo.bossnowhp;
              ObjectSelf.GetInstance().WorldBossMgr.m_CurBossDataKey = battleinfo.bossid;


              SceneManager.Inst.EnterBattleScene(battleinfo.battleid);
          }

        }
	}	
}
