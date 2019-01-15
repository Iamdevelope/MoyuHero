using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamFaction.GameNetWork.Data;
using DreamFaction.GameNetWork;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;

namespace GNET
{
	public partial class SBeginBattle: Protocol
	{

        public BattelInfo battleinfo;

        public const int PROTOCOL_TYPE = 787941;

        public SBeginBattle()
            : base(PROTOCOL_TYPE)
		 {
             battleinfo = new BattelInfo();
		 } 

		public override object Clone()
		{
            SBeginBattle obj = new SBeginBattle();
			return obj; 
		}

        public override OctetsStream marshal(OctetsStream _os_)
		{
            _os_.marshal(battleinfo);

            return _os_;
		}

        public override OctetsStream unmarshal(OctetsStream _os_)
		{
            battleinfo.unmarshal(_os_);
            return _os_;
		}

        public override int PriorPolicy() { return 1; }

        public override bool SizePolicy(int size) { return  size <= 1024; }

        public override void Process() 
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
            List<int> _indroplist = new List<int>();
            foreach (int value in battleinfo.indroplist)
            {
                _indroplist.Add(value);
            }
            pMonsterGroupData.Count = ++nGroup;//总波数 [4/1/2015 Zmy]

            ObjectSelf.GetInstance().OnCacheMonsterGroupData(pMonsterGroupData, battleinfo.battleid);

            ObjectSelf.GetInstance().OnCacheCurrentBattleReward(battleinfo.heroexp, battleinfo.teamexp, battleinfo.tili, 0, _indroplist);

            ObjectSelf.GetInstance().BattleStageData.CheckSpecialStageData(battleinfo.battleid);

            SceneManager.Inst.EnterBattleScene(battleinfo.battleid);

            //if(_indroplist.Count > 0)
            //{
            //    for (int i = 0; i < _indroplist.Count; i++)
            //    {
            //        InnerdropTemplate _inner = (InnerdropTemplate)DataTemplate.GetInstance().m_InnerdropTable.getTableData(_indroplist[i]);
            //        if (_inner.getDropparameter1() != -1)
            //        {
            //            Debug.Log("haole ");
            //        }
            //    }
            //}

        }
	}	
}
