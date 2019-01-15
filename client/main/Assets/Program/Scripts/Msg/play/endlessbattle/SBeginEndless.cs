using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;

namespace GNET
{
	public partial class SBeginEndless: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public endlessBattleInfo battleinfo;
        public endlessAttr attrinfo;

        public const int PROTOCOL_TYPE = 788934;

        public SBeginEndless()
            : base(PROTOCOL_TYPE)
		 {
             battleinfo = new endlessBattleInfo();
             attrinfo = new endlessAttr();
		 } 

		public override object Clone()
		{
            SBeginEndless obj = new SBeginEndless();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(result);
            _os_.marshal(battleinfo);
            _os_.marshal(attrinfo);
            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            result = _os_.unmarshal_int();
            battleinfo.unmarshal(_os_);
            attrinfo.unmarshal(_os_);
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
               int nGroup = battleinfo.groupnum - 1;//默认初始第一轮为1.对应数组索引-1;
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
               }
               pMonsterGroupData.Count = GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP;//极限试炼最大波数

               for (int i = 1; i <= GlobalMembers.MAX_TEAM_CELL_COUNT;i++ )//初始化英雄阵型信息
               {
                   if (battleinfo.useherokeylist.ContainsKey(i))
                   {
                       if ((int)battleinfo.useherokeylist[i] != 0)
                       {
                           ObjectSelf.GetInstance().LimitFightMgr.m_HeroInfo[i - 1].GUID_value = (int)battleinfo.useherokeylist[i]; 
                       }
                   }
               }
               ObjectSelf.GetInstance().OnCacheMonsterGroupData(pMonsterGroupData, battleinfo.battleid);
               ObjectSelf.GetInstance().LimitFightMgr.m_TroopType = battleinfo.trooptype;
               ObjectSelf.GetInstance().LimitFightMgr.m_MonsterTroopType = battleinfo.monstertrooptype;
               ObjectSelf.GetInstance().LimitFightMgr.m_BeginRoundNum = battleinfo.groupnum;
               ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter = true;
               ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum    = battleinfo.groupnum;

               ObjectSelf.GetInstance().LimitFightMgr.RoundOverProcess(attrinfo);

               SceneManager.Inst.EnterBattleScene(battleinfo.battleid);
           }

        }
	}	
}
