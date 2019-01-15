using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;

namespace GNET
{
	public partial class SEndlessPass: Protocol
	{
        public const int END_OK = 1; // 成功
        public const int END_ERROR = 2; // 失败

	    public int result;
        public endlessBattleInfo battleinfo;
        public endlessAttr attrinfo;

        public const int PROTOCOL_TYPE = 788936;

        public SEndlessPass()
            : base(PROTOCOL_TYPE)
		 {
             battleinfo = new endlessBattleInfo();
             attrinfo = new endlessAttr();
		 } 

		public override object Clone()
		{
            SEndlessPass obj = new SEndlessPass();
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
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.U_GameTips, "#0000045");
            } 
           else
           {
               CampaignMonsterGroupData pMonsterGroupData = new CampaignMonsterGroupData();
               int nGroup = battleinfo.groupnum - 1;//默认初始第一轮为1.对应数组索引-1;
               int nIndex = 0;

               if (battleinfo.monstergroup.Count > 0)// [6/17/2015 Zmy]
               {
                   foreach (int value in battleinfo.monstergroup)
                   {
                       if (nIndex >= GlobalMembers.MAX_MONSTER_GROUP_COUNT || nGroup >= GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP)
                           continue;
                       if (value != 0)
                       {
                           pMonsterGroupData.IDs[nGroup, nIndex] = value;
                           nIndex++;

                           //极限试炼后面的怪物动态加载，后期看运行效果再选择其他方式优化 [6/17/2015 Zmy]
                           AssetLoader.Inst.DynamicLoadLimitMonsterRes(value);
                       }
                   }
               }
              
               pMonsterGroupData.Count = GlobalMembers.MAX_CAMPAIGN_MONSTER_GROUP;//极限试炼最大波数

               ObjectSelf.GetInstance().OnCacheMonsterGroupData(pMonsterGroupData, battleinfo.battleid);
               SceneObjectManager.GetInstance().UpdateMonsterGroupData();
               ObjectSelf.GetInstance().LimitFightMgr.m_TroopType = battleinfo.trooptype;
               ObjectSelf.GetInstance().LimitFightMgr.m_MonsterTroopType = battleinfo.monstertrooptype;
               ObjectSelf.GetInstance().LimitFightMgr.m_RoundNum = battleinfo.groupnum;

               ObjectSelf.GetInstance().LimitFightMgr.RoundOverProcess(attrinfo,false);

               GameEventDispatcher.Inst.dispatchEvent(GameEventID.F_LimitBoutEnd);
           }

        }
	}	
}
